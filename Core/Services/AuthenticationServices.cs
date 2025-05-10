using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dto_s.IdentityDto;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager, IConfiguration configuration,IMapper mapper) : IAuthenticationServices
    {
        public async Task<bool> CheckEmailsAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var User = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundExpception(email);
            if (User.Address is not null) 
                return mapper.Map<AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(User.UserName);

        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
             var User = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundExpception(email);
            if (User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Street = addressDto.Street;
                User.Address.Country = addressDto.Country;
               

            }
            else
            {
                var NewAddress = mapper.Map<AddressDto,Address>(addressDto);


            }
            await userManager.UpdateAsync(User);
            return mapper.Map<AddressDto>(User.Address);
        }


        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundExpception(email);
            return new UserDto
            {
                DisplayName = User.DisplayName,
                Email = email,
                Token = await CreateToken(User)
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new UserNotFoundExpception(loginDto.Email);
            var IsPasswordValid = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
                return new UserDto
                {
                    DisplayName = User.DisplayName,
                    Email = loginDto.Email,
                    Token = await CreateToken(User)


                };
            else
            {

                throw new UnAuthorizedException("Invalid Password");
            }
        }

        

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)

        {
            var User = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email,
            };
            var result = await userManager.CreateAsync(User, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = await CreateToken(User)
                };
            }
            else
            {
               var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }

        }

 
        private   async Task<string> CreateToken(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
              
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),


            };

            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: Cred
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

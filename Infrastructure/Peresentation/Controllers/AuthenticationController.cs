using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.IdentityDto;
namespace Peresentation.Controllers
{

    public class AuthenticationController(IServicesManger servicesManager) : ApiBaseController
    {
        // Login
        [HttpPost("Login")] // POST baseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await servicesManager.authenticationServices.LoginAsync(loginDto);
            return Ok(User);
        }

        // Register
        [HttpPost("Register")] // POST baseUrl/api/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await servicesManager.authenticationServices.RegisterAsync(registerDto);
            return Ok(User);
        }
        [HttpGet("CheckEmail")] // GET baseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await servicesManager.authenticationServices.CheckEmailsAsync(email);
            return Ok(result);
        }

        [HttpGet("CurrentUser")] // GET baseUrl/api/Authentication/CheckUserName
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var currentUser = await servicesManager.authenticationServices.GetCurrentUserAsync(email);
            return Ok(currentUser);
        }
        [HttpGet("Address")] // GET baseUrl/api/Authentication/CheckUserName
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address =  servicesManager.authenticationServices.GetCurrentUserAddressAsync(email);
            return Ok(address);
          
            
        }
        [HttpPost("Address")] // PUT baseUrl/api/Authentication/CheckUserName
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await servicesManager.authenticationServices.UpdateCurrentUserAddressAsync(email, addressDto);
            return Ok(UpdatedAddress);
        }
    }

}

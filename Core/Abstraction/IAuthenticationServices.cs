using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto_s.IdentityDto;

namespace Abstraction
{
    public interface IAuthenticationServices
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);

        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailsAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email );
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto addressDto);
        Task<UserDto> GetCurrentUserAsync(string email);

    }
}

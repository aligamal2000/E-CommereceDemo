using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Identiy;
using Shared.Dto_s.IdentityDto;

namespace Services.MappingProfiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile() 
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}

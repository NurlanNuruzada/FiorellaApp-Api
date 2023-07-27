using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Services
{
    public interface ITokenHandler
    {
        public Task<TokenResponseDto> CreateAccessToken(int addminutes,int refreshTokenMinutes,AppUser user);
    }
}

using DotnetPractice.Common.Dtos.Auth;
using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.BuisinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenResponseDto> GenerateNewTokenAsync(LoginDto loginInfo);
    }
}

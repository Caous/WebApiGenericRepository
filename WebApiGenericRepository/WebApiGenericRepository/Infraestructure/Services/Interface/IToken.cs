using System.Collections.Generic;
using System.Security.Claims;

namespace WebApiGenericRepository.Infraestructure.Services
{
    public interface IToken
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

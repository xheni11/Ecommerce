using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Common.Auth
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string userName, IEnumerable<Claim> claims);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string roles);
    }

}

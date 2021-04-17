using Ecommerce.Common.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Common.Helpers
{
    public class Tokens
    {
        public static string GenerateJwt(IEnumerable<Claim> claims, IJwtFactory jwtFactory, string userName,
            JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var claimList = claims?.ToList();
            int.TryParse(claimList?.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value, out int id);
            var response = new
            {
                id,
                auth_token = jwtFactory.GenerateEncodedToken(userName, claimList),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }

}

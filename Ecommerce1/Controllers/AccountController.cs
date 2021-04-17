using Ecommerce.Common.Auth;
using Ecommerce.Models.Requests.User;
using Ecommerce.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using Ecommerce.Common.Helpers;
using System.Net;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IOptions<JwtIssuerOptions> jwtOptions, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = jwtOptions.Value;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserLoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userModel)
        {
            var existingUser = await _userManager.FindByNameAsync(userModel.UserName);

            if (existingUser == null)
            {
                return Unauthorized();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(userModel.UserName, userModel.Password,
                userModel.RememberMe, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            var identity = await GetClaimsIdentity(existingUser, userModel.Password);

            if (identity == null)
            {
                return BadRequest();
            }

            string jwt = Tokens.GenerateJwt(identity.Claims, _jwtFactory, userModel.UserName, _jwtOptions,
                new JsonSerializerSettings { Formatting = Formatting.Indented });
            var authData = JsonConvert.DeserializeObject<AuthData>(jwt);

            var userBo = new UserLoginResponse
            {
                UserName = existingUser.UserName,
                Token = authData.Auth_Token
            };

            return Ok(userBo);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(IdentityUser userToVerify, string password)
        {
            // check the credentials
            if (!await _userManager.CheckPasswordAsync(userToVerify, password))
            // Credentials are invalid, or account doesn't exist
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var roles = await _userManager.GetRolesAsync(userToVerify);

            if (roles == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify.UserName,
                userToVerify.Id.ToString(), roles[0]));
        }

    }
}

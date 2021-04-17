using Ecommerce.Common;
using Ecommerce.Converters;
using Ecommerce.DTO;
using Ecommerce.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.DTO.Mappings;
using Ecommerce.Models.Responses;

namespace Ecommerce1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string AccessToken = "access_token";
        private readonly UserConverter _userConverter;
        private readonly RoleConverter _roleConverter;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _userConverter = new UserConverter();
            _roleConverter = new RoleConverter();

        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Get()
        {
           // string token = await HttpContext.GetTokenAsync(AccessToken);
            //int userId = TokenHelper.GetUserFromToken(token).Id;

            var users = _userManager.Users.AsEnumerable().Select(UserMapping.ToDTO)
                .ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(UserMapping.ToEntity(user));
                string roleName = roles.FirstOrDefault();
                if (roleName != null)
                {
                    var appRole = await _roleManager.FindByNameAsync(roleName);
                    var userRole = _roleConverter.Convert(appRole);
                    user.Role = userRole;
                }
                else
                {
                    user.Role = null;
                } // TODO remove this condition After deleting from db users with null role
            }

            return Ok(users);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("isExisting/{username}/{id}")]
        // [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> IsExisting(string username, string id)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                if (user.Id == id)
                {
                    return Ok(false);
                }

                return Ok(true);
            }

            return Ok(false);
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var returnUser = _userConverter.Convert(UserMapping.ToDTO(user)); 
            return Ok(returnUser);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest user)
        {
            UserDTO userDTO = _userConverter.Convert(user);           
            userDTO.Password = RandomPasswordGenerator.GenerateRandomPassword(8, true, true, true, true);
            var creatingUser = UserMapping.ToEntityCreate(userDTO);
            var result = await _userManager.CreateAsync(creatingUser, userDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var setRoleResult = await _userManager.AddToRoleAsync(creatingUser, user.Role.Name);
            if (setRoleResult == null)
            {
                throw new ArgumentNullException(nameof(setRoleResult));
            }

            creatingUser = await _userManager.FindByNameAsync(user.UserName);
            var role = user.Role;
            UserResponse response= _userConverter.Convert(UserMapping.ToDTO(creatingUser));
            response.Role = role;

            return Ok(response);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
         
            appUser.UserName = user.UserName;
            appUser.Email = user.Email;
            appUser.PhoneNumber = user.PhoneNumber;

            var result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(appUser);
                if (roles.Count > 0)
                {
                    if (roles.First() != user.Role.Name)
                    {
                        var removeRole = await _userManager.RemoveFromRoleAsync(appUser, roles.First());
                        var addRole = await _userManager.AddToRoleAsync(appUser, user.Role.Name);

                        if (!removeRole.Succeeded && !addRole.Succeeded)
                        {
                            return BadRequest(removeRole.Errors);
                        }
                    }
                }

                return Ok(user);
            }

            return BadRequest(result.Errors);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            existingUser.UserName = $"{existingUser.UserName}_Deleted";

            var result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var lockoutDate = DateTime.MaxValue;

            await _userManager.SetLockoutEnabledAsync(existingUser, true);
            var lockoutResult = await _userManager.SetLockoutEndDateAsync(existingUser, lockoutDate);
            if (lockoutResult == null)
            {
                throw new ArgumentNullException(nameof(lockoutResult));
            }

            return Ok();
        }

    }
}

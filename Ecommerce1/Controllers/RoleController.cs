using Ecommerce.Converters;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController:ControllerBase
    {
        private readonly RoleConverter _conv;

        private readonly RoleManager<IdentityRole> _roleManager;

        // GET: /<controller>/
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _conv = new RoleConverter();
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            IList<RoleDTO> roles = _roleManager.Roles.Select(_conv.Convert).ToList();
            return Ok(roles);
        }

      //  [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _roleManager.FindByIdAsync(id.ToString());

            return Ok(result);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(RoleDTO role)
        {
            var newRole = new IdentityRole
            {
                Name = role.Name
            };
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return Ok(newRole);
            }

            return BadRequest(result.Errors);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(RoleDTO role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id.ToString());

            existingRole.Name = role.Name;
            existingRole.NormalizedName = role.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
            {
                return Ok(existingRole);
            }

            return BadRequest(result.Errors);
        }

       // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id.ToString());
            var result = await _roleManager.DeleteAsync(existingRole);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        }
}

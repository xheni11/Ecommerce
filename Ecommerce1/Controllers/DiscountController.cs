using Ecommerce.DTO;
using Ecommerce.IBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly ILogger<DiscountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public DiscountController(IDiscountService discountService, ILogger<DiscountController> logger)
        {
            _discountService = discountService;
            _logger = logger;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(IEnumerable<DiscountDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] DiscountDTO discount)
        {
            return Ok(_discountService.Create(discount));
        }


        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete( int id )
        {
            _discountService.Delete(id);
            return Ok();
        }
    }
}

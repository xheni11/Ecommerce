using Ecommerce.DTO;
using Ecommerce.IBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [Route("buy")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "User")]
        public async Task<IActionResult> Buy([FromBody] OrderDTO order)
        {
            var result = _orderService.Create(order);
            return result==null?Ok("There is not enough quantity in warehouse! "):Ok(result);
        }
        [HttpGet]
        [Route("get by id")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_orderService.GetById(id));

        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), (int)HttpStatusCode.OK)]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int pageNumber, string? productName, DateTime? dateFrom, DateTime? dateTo)
        {
            return Ok(_orderService.GetAll(pageNumber, productName, null, dateFrom, dateTo));

        }
    }
}

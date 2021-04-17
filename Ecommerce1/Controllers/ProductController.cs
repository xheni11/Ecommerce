using Ecommerce.DTO;
using Ecommerce.IBLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo)
        {
            return Ok( _productService.GetAll(pageNumber, name, isAvaliable, dateFrom, dateTo, User.IsInRole("Admin")));

        }

        [HttpGet]
        [Route("get products with discount")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        //  [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetWithDiscount(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo)
        {
            return Ok(_productService.GetProductsWithDiscount(pageNumber, name, isAvaliable, dateFrom, dateTo, User.IsInRole("Admin")));

        }

        [HttpGet]
        [Route("get by id")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
       // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok( _productService.GetById(id));

        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDTO product)
        {
            return Ok(_productService.Create(product));
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            _productService.Update(product);
            return Ok();
        }

        [HttpPut]
        [Route("set product no public")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetProductToPublic([FromBody] ProductDTO product)
        {
            _productService.SetProductToPublic(product);
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete( int id)
        {
            _productService.Delete(id);
            return Ok();
        }
    }
}

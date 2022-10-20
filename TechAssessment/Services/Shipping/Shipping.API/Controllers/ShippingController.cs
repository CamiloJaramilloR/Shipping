using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Services;
using Shipping.Domain;

namespace Shipping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShippingController : Controller
    {
        private readonly IShippingPackageService _shippingPackageService;

        public ShippingController(IShippingPackageService shippingPackageService)
        {
            this._shippingPackageService = shippingPackageService;
        }

        [HttpGet]
        [Route("GetLowestRate")]
        public async Task<IActionResult> GetLowestRate([FromBody] Package package)
        {
            var response = await _shippingPackageService.GetLowestRate(package);
            return Ok(response);
        }
    }
}

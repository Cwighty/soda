using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [Route("checkout")]
    [ApiController]
    public class CheckoutController : Controller
    {
        [HttpPost]
        public ActionResult CheckoutItems() {
        
            return Ok();
        }
    }

}

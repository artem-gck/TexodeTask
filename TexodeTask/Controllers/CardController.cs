using Microsoft.AspNetCore.Mvc;

namespace TexodeTask.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

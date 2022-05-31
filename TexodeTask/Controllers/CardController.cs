using Microsoft.AspNetCore.Mvc;

namespace TexodeTask.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : Controller
    {
        public ActionResult<string> Index()
        {
            return "Hello";
        }
    }
}

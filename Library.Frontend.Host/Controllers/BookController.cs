using System.Web.Mvc;

namespace Library.Frontend.Host.Controllers
{
    public class BookController : Controller
    {
        public ActionResult List()
        {
            return View();
        }
    }
}
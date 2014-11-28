using System.Web.Mvc;

namespace BankingManagementClient.Host.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "Client");
        }
    }
}
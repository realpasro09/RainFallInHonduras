using System.Web.Mvc;

namespace Rainfall.Web.Controllers {
  public class HomeController : Controller {
    public ActionResult Index() {
      return View();
    }
  }
}
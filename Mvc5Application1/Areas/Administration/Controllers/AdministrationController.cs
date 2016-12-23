using System.Web.Mvc;

namespace Mvc5Application1.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        //
        // GET: /Administration/User/

        public ActionResult Index()
        {
            return View();
        }
    }
}
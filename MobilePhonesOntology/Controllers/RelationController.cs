using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class RelationController : Controller
    {
        public ActionResult Index(string relation = null)
        {
            return View();
        }
    }
}
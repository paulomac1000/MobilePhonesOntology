using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PhoneController : Controller
    {
        public ActionResult Index(string brand, string model = null)
        {
            return View();
        }
    }
}
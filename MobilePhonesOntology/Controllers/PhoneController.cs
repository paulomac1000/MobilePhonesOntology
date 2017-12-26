using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PhoneController : Controller
    {
        public ActionResult Index(string brand = null, string model = null)
        {
            if (string.IsNullOrEmpty(brand) && string.IsNullOrEmpty(model))
            {
                
            }

            if (!string.IsNullOrEmpty(brand) & string.IsNullOrEmpty(model))
            {

            }

            return View();
        }
    }
}
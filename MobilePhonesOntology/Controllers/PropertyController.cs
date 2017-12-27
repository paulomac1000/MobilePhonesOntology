using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models.Enums;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PropertyController : Controller
    {
        public ActionResult Index(string property = null)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new Exception("property is empty");
            }
            else
            {
                var countInBrandAndModels = CacheHelper.BrandsAndModels.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Predicate, NodeName.Property) == property).Count();

                var countInPhones = CacheHelper.Phones.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Predicate, NodeName.Property) == property).Count();

                var result = $"Property {property} has been found:" +
                    $"{countInBrandAndModels} from {CacheHelper.BrandsAndModels.Triples.Count} triples in graph BrandsAndModels \n" +
                    $"{countInPhones} from {CacheHelper.Phones.Triples.Count} triples in graph Phones \n";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
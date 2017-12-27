using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PhoneController : Controller
    {
        public ActionResult Index(string brand = null, string model = null)
        {
            if (string.IsNullOrEmpty(brand) && string.IsNullOrEmpty(model))
            {
                return RedirectToAction("Index", "Find");
            }
            else if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(model))
            {
                var triples = CacheHelper.Phones.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Subject, NodeName.Brand) == brand &&
                    GraphHelper.GetFromNode(t.Subject, NodeName.Model) == model);

                if (!triples.Any())
                    throw new Exception($"unable find {brand} {model}");

                var phone = triples.Select(t => new TripleViewModel
                {
                    Subject = $"{brand} {model}",
                    SubjectUri = t.Subject.ToString(),
                    Predicate = GraphHelper.GetFromNode(t.Predicate, NodeName.Relation),
                    PredicateUri = t.Predicate.ToString(),
                    Object = GraphHelper.GetFromNode(t.Object, NodeName.Property),
                    ObjectUri = t.Object.ToString(),
                });

                return Json(phone, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new Exception("brand or model is empty");
            }
        }
    }
}
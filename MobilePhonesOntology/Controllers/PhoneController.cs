using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PhoneController : Controller
    {
        public ActionResult Index(PhoneSimple model)
        {
            if (string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                return RedirectToAction("Index", "Find");
            }
            else if (!string.IsNullOrEmpty(model.Brand) && !string.IsNullOrEmpty(model.Model))
            {
                var triples = CacheHelper.Phones.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Subject, NodeName.Brand) == model.Brand &&
                    GraphHelper.GetFromNode(t.Subject, NodeName.Model) == model.Model);

                if (!triples.Any())
                    throw new Exception($"unable find {model.Brand} {model}");

                var phone = triples.Select(t => new TripleViewModel
                {
                    Subject = $"{model.Brand} {model.Model}",
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
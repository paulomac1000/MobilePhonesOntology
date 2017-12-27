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
        public ActionResult Index(PhoneSimple parameters)
        {
            if (string.IsNullOrEmpty(parameters.Brand) && string.IsNullOrEmpty(parameters.Model))
                return RedirectToAction("Index", "Find");

            if (string.IsNullOrEmpty(parameters.Brand) || string.IsNullOrEmpty(parameters.Model))
                throw new Exception("brand or model is empty");

            var triples = CacheHelper.Phones.Triples.Where(t =>
                GraphHelper.GetFromNode(t.Subject, NodeName.Brand) == parameters.Brand &&
                GraphHelper.GetFromNode(t.Subject, NodeName.Model) == parameters.Model).ToArray();

            if (!triples.Any())
                throw new Exception($"unable find {parameters.Brand} {parameters}");

            var phone = triples.Select(t => new TripleViewModel
            {
                Subject = $"{parameters.Brand} {parameters.Model}",
                SubjectUri = t.Subject.ToString(),
                Predicate = GraphHelper.GetFromNode(t.Predicate, NodeName.Relation),
                PredicateUri = t.Predicate.ToString(),
                Object = GraphHelper.GetFromNode(t.Object, NodeName.Property),
                ObjectUri = t.Object.ToString(),
            });

            return Json(phone, JsonRequestBehavior.AllowGet);
        }
    }
}
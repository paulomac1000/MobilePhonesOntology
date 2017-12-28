using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VDS.RDF;

namespace MobilePhonesOntology.Controllers
{
    public class FindController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Index(PhoneSimple model)
        {
            IEnumerable<Triple> triples = null;
            if (string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                return View();
            }
            //only brand given
            else if (!string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                     GraphHelper.GetFromNode(t.Object, NodeName.Brand) == model.Brand);
            }
            //only model given
            else if (!string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                     GraphHelper.GetFromNode(t.Subject, NodeName.Model) == model.Model);
            }
            //both given
            else
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Subject, NodeName.Model) == model.Model &&
                    GraphHelper.GetFromNode(t.Object, NodeName.Brand) == model.Brand);
            }

            if (!triples.Any())
                return Json($"unable find {model.Brand} {model}", JsonRequestBehavior.AllowGet);

            var phones = triples.Select(t => new FindViewModel
            {
                Brand = GraphHelper.GetFromNode(t.Object, NodeName.Brand),
                Model = GraphHelper.GetFromNode(t.Subject, NodeName.Model),
                Uri = t.Subject.ToString()
            });

            return Json(phones, JsonRequestBehavior.AllowGet);
        }
    }
}
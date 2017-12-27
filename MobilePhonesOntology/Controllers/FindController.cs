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
                throw new Exception($"unable find {model.Brand} {model}");

            var phones = triples.Select(t => new FindViewModel
            {
                // todo
            });

            return Json(phones, JsonRequestBehavior.AllowGet);
        }
    }
}
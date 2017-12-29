using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VDS.RDF;

namespace MobilePhonesOntology.Controllers
{
    public class FindController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(PhoneSimple parameters)
        {
            var model = new FindViewModel
            {
                Brand = parameters.Brand,
                Model = parameters.Model
            };

            IEnumerable<Triple> triples = null;
            if (string.IsNullOrEmpty(model.Brand) && string.IsNullOrEmpty(model.Model))
            {
                return View(model);
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
            {
                ModelState.AddModelError("", $"Unable find {model.Brand} {model}.");
                return View();
            }

            model.Phones = triples.Select(t => new PhoneSimpleWithUri
            {
                Brand = GraphHelper.GetFromNode(t.Object, NodeName.Brand),
                Model = GraphHelper.GetFromNode(t.Subject, NodeName.Model),
                Uri = t.Subject.ToString()
            });
            model.Succes = true;
            
            return View(model);
        }
    }
}
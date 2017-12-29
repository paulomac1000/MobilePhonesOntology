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
        public ActionResult Index(string brand, string model)
        {
            var viewModel = new FindViewModel
            {
                Brand = brand,
                Model = model
            };

            IEnumerable<Triple> triples = null;
            if (string.IsNullOrEmpty(viewModel.Brand) && string.IsNullOrEmpty(viewModel.Model))
            {
                return View();
            }
            //only brand given
            else if (!string.IsNullOrEmpty(viewModel.Brand) && string.IsNullOrEmpty(viewModel.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                     GraphHelper.GetFromNode(t.Object, NodeName.Brand) == viewModel.Brand);
            }
            //only model given
            else if (!string.IsNullOrEmpty(viewModel.Brand) && string.IsNullOrEmpty(viewModel.Model))
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                     GraphHelper.GetFromNode(t.Subject, NodeName.Model) == viewModel.Model);
            }
            //both given
            else
            {
                triples = CacheHelper.BrandsAndModels.Triples.Where(t =>
                    GraphHelper.GetFromNode(t.Subject, NodeName.Model) == viewModel.Model &&
                    GraphHelper.GetFromNode(t.Object, NodeName.Brand) == viewModel.Brand);
            }

            if (!triples.Any())
            {
                ModelState.AddModelError("", $"unable find {viewModel.Brand} {viewModel}");
                return View();
            }

            viewModel.Phones = triples.Select(t => new PhoneSimpleWithUri
            {
                Brand = GraphHelper.GetFromNode(t.Object, NodeName.Brand),
                Model = GraphHelper.GetFromNode(t.Subject, NodeName.Model),
                Uri = t.Subject.ToString()
            });

            return View(viewModel);
        }
    }
}
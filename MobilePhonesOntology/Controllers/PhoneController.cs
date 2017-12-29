using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PhoneController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Index(PhoneSimple parameters)
        {
            var model = new PhoneViewModel
            {
                Brand = parameters.Brand,
                Model = parameters.Model
            };

            if (string.IsNullOrEmpty(parameters.Brand) && string.IsNullOrEmpty(parameters.Model))
            {
                model.ErrorMessage = "No parameter given.";
                return View(model);
            }

            if (string.IsNullOrEmpty(parameters.Brand) || string.IsNullOrEmpty(parameters.Model))
            {
                model.ErrorMessage = "You have to give both parameters.";
                return View(model);
            }

            var triples = CacheHelper.Phones.Triples.Where(t =>
                GraphHelper.GetFromNode(t.Subject, NodeName.Brand) == parameters.Brand &&
                GraphHelper.GetFromNode(t.Subject, NodeName.Model) == parameters.Model).ToArray();

            if (!triples.Any())
            {
                model.ErrorMessage = $"Unable find {parameters.Brand} {parameters.Model}.  If You clicked right link, there is propably diffrence between data got from phonegg.com and fonoapi.";
                return View(model);
            }

            model.Triples = triples.Select(t => new TripleSimple
            {
                Subject = $"{parameters.Brand} {parameters.Model}",
                SubjectUri = t.Subject.ToString(),
                Predicate = GraphHelper.GetFromNode(t.Predicate, NodeName.Relation),
                PredicateUri = t.Predicate.ToString(),
                Object = GraphHelper.GetFromNode(t.Object, NodeName.Property),
                ObjectUri = t.Object.ToString(),
            });
            model.Succes = true;

            return View(model);
        }
    }
}
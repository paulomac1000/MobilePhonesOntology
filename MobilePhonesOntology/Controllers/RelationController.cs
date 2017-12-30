using MobilePhonesOntology.Extensions;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class RelationController : Controller
    {
        public ActionResult Index(string relation = null)
        {
            var model = new RelationViewModel
            {
                RelationName = relation
            };

            if (string.IsNullOrEmpty(relation))
            {
                model.ErrorMessage = $"Relation has been not given.";
                return View(model);
            }

            var triples = CacheHelper.Phones.Triples.Where(t =>
                t.Predicate.GetFromNode(NodeName.Relation) == model.RelationName);

            if (!triples.Any())
            {
                model.ErrorMessage = $"Unable find relation {model.RelationName}.";
                return View(model);
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Relation {model.RelationName} has been found {triples.Count()} times.<br>");
            stringBuilder.AppendLine($"There are {CacheHelper.BrandsAndModels.Triples.Count()} phones.<br>");
            stringBuilder.AppendLine($"<br>The following values were found:<br>");

            var properties = triples.Select(t => t.Object.GetFromNode(NodeName.Property));
            var grouped = properties.GroupBy(i => i).OrderByDescending(x => x.Count());

            foreach (var group in grouped)
            {
                stringBuilder.AppendLine($"Count: {group.Count()} Value: {group.Key}<br>");
            }

            model.ResponseMessage = stringBuilder.ToString();
            model.Succes = true;

            return View(model);
        }
    }
}
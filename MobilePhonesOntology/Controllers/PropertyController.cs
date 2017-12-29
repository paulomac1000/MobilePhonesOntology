using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class PropertyController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Index(string property = null)
        {
            var model = new PropertyViewModel
            {
                PropertyName = property
            };

            if (string.IsNullOrEmpty(property))
            {
                model.ErrorMessage = $"Property has been not given.";
                return View(model);
            }

            var triples = CacheHelper.Phones.Triples.Where(t =>
                GraphHelper.GetFromNode(t.Object, NodeName.Property) == model.PropertyName);

            if (!triples.Any())
            {
                model.ErrorMessage = $"Unable find relation {model.PropertyName}.";
                return View(model);
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Property {model.PropertyName} has been found {triples.Count()} times.<br>");
            stringBuilder.AppendLine($"There are {CacheHelper.BrandsAndModels.Triples.Count()} phones.<br>");
            stringBuilder.AppendLine($"<br>The following relation using this property:<br>");

            var relations = triples.Select(t => GraphHelper.GetFromNode(t.Predicate, NodeName.Relation));
            var grouped = relations.GroupBy(i => i).OrderByDescending(x => x.Count());

            foreach (var group in grouped)
            {
                stringBuilder.AppendLine($"Count: {group.Count()} Value: {group.Key}<br>");
            }

            stringBuilder.AppendLine($"<br>This property is used by:<br>");

            relations = triples.Select(t => GraphHelper.GetFromNode(t.Subject, NodeName.Brand));
            grouped = relations.GroupBy(i => i).OrderByDescending(x => x.Count());

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
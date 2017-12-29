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

            var triples = CacheHelper.Phones.Triples.Where(t =>
                GraphHelper.GetFromNode(t.Predicate, NodeName.Relation) == model.RelationName);

            if (!triples.Any())
            {
                model.ErrorMessage = $"Unable find relation {model.RelationName}.";
                return View(model);
            }

            var values = triples.Select(t => GraphHelper.GetFromNode(t.Object, NodeName.Property));

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Relation {model.RelationName} has been found {triples.Count()}.<br>");
            stringBuilder.AppendLine($"There are {CacheHelper.BrandsAndModels.Triples.Count()} phones.<br>");
            stringBuilder.AppendLine($"The following values were found:<br>");

            var grouped = values.GroupBy(i => i).OrderBy(x => x.Key);

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
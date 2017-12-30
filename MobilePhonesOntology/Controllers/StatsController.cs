using MobilePhonesOntology.Extensions;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using MobilePhonesOntology.ViewModels;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MobilePhonesOntology.Controllers
{
    public class StatsController : Controller
    {
        public ActionResult Index()
        {
            var model = new StatsViewModel
            {
                NumberOfBrandAndModels = CacheHelper.BrandsAndModels.Triples.Count,
                NumbersOfRelations = CacheHelper.Phones.Triples.Count,
                NamesOfRelations = typeof(Phone).GetTypeInfo().DeclaredProperties.Select(p => p.Name),
            };
            model.NumbersOfUniqueRelations = model.NamesOfRelations.Count();

            var groupedUniquePhones = CacheHelper.Phones.Triples.Select(t => t.Subject.ToString()).GroupBy(i => i);
            model.NumberOfPhones = groupedUniquePhones.Count();

            var groupedRelations = CacheHelper.Phones.Triples.Select(t => t.Predicate.GetFromNode(NodeName.Relation)).GroupBy(i => i);
            model.NumbersOfUniqueProperties = groupedRelations.Count();

            return View(model);
        }
    }
}
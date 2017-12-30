using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using Quartz;
using System.Linq;

namespace MobilePhonesOntology.Quartz
{
    public class OnStartupJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CacheHelper.BrandsAndModels = OntologyHelper.LoadGraph(Strings.BrandsAndModelsGraphName);
            if (!CacheHelper.BrandsAndModels.Triples.Any())
            {
                var phonesWithBrands = DataDownloadHelper.GetAllSimplePhones();
                var graphOfBrandsAndModels = OntologyHelper.CreateGraphOfBrandsAndModels(phonesWithBrands, Strings.Domain);
                CacheHelper.BrandsAndModels = graphOfBrandsAndModels;
            }

            CacheHelper.Phones = OntologyHelper.LoadGraph(Strings.PhonesGraphName);
            if (!CacheHelper.Phones.Triples.Any())
            {
                const int brandId = 51;
                const string brandName = "Acer";
                const string brandUrl = "https://www.phonegg.com/brand/51-Acer";

                var brand = new Brand
                {
                    Id = brandId,
                    Name = brandName,
                    Url = brandUrl
                };
                var phonesSimpleByBrand = DataDownloadHelper.GetPhonesByBrand(brand);
                var phonesByBrand = phonesSimpleByBrand.Select(phoneSimple => DataDownloadHelper.GetPhone(phoneSimple.Model, phoneSimple.Brand))
                    .Select(task => task.GetAwaiter().GetResult()).Where(phone => phone != null).ToList();
                CacheHelper.Phones = OntologyHelper.CreateGraphOfPhones(phonesByBrand, Strings.Domain);
            }
        }
    }
}
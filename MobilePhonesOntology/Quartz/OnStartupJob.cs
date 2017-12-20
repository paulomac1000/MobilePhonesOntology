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
            var phonesWithBrands = DataDownloadHelper.GetAllSimplePhones();
            var graphOfBrandsAndModels = OntologyHelper.CreateGraphOfBrandsAndModels(phonesWithBrands, Strings.Domain);
            CacheHelper.BrandsAndModels = graphOfBrandsAndModels;

            var brand = new Brand
            {
                Id = 51,
                Name = "Acer",
                Url = "https://www.phonegg.com/brand/51-Acer"
            };
            var phonesSimpleByBrand = DataDownloadHelper.GetPhonesByBrand(brand);
            var phonesByBrand = phonesSimpleByBrand.Select(phoneSimple => DataDownloadHelper.GetPhone(phoneSimple.Model, phoneSimple.Brand))
                .Select(task => task.GetAwaiter().GetResult()).Where(phone => phone != null).ToList();
            CacheHelper.BrandsAndModels = OntologyHelper.CreateGraphOfPhones(phonesByBrand, Strings.Domain);
        }
    }
}
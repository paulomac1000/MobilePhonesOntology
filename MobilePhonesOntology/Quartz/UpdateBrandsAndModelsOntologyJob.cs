using MobilePhonesOntology.Helpers;
using Quartz;

namespace MobilePhonesOntology.Quartz
{
    public class UpdateBrandsAndModelsOntologyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            const string domain = "http://localhost:16273";

            var phonesWithBrands = DataDownloadHelper.GetAllSimplePhones();
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(phonesWithBrands, domain);
            CacheHelper.BrandsAndModels = graph;
        }
    }
}
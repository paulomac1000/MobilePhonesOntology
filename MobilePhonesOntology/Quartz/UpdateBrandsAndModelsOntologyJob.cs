using MobilePhonesOntology.Helpers;
using Quartz;

namespace MobilePhonesOntology.Quartz
{
    public class UpdateBrandsAndModelsOntologyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var phonesWithBrands = DataDownloadHelper.GetAllSimplePhones();
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(phonesWithBrands, Strings.Domain);
            CacheHelper.BrandsAndModels = graph;
        }
    }
}
using MobilePhonesOntology.Helpers;
using Quartz;

namespace MobilePhonesOntology.Quartz
{
    public class UpdatePhonesOntologyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var task = DataDownloadHelper.GetAllPhones();
            var phones = task.GetAwaiter().GetResult();

            var graph = OntologyHelper.CreateGraphOfPhones(phones, Strings.Domain);
            CacheHelper.BrandsAndModels = graph;
        }
    }
}
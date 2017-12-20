using MobilePhonesOntology.Helpers;
using Quartz;

namespace MobilePhonesOntology.Quartz
{
    public class UpdatePhonesOntologyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            const string domain = "http://localhost:16273";

            var task = DataDownloadHelper.GetAllPhones();
            var phones = task.GetAwaiter().GetResult();

            var graph = OntologyHelper.CreateGraphOfPhones(phones, domain);
            CacheHelper.BrandsAndModels = graph;
        }
    }
}
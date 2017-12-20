using Quartz;
using Quartz.Impl;

namespace MobilePhonesOntology.Quartz
{
    public class Scheduler
    {
        public class JobScheduler
        {
            public static void Start()
            {
                var scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                //ontology - on startup
                var updateOnStartupJob = JobBuilder.Create<OnStartupJob>().Build();
                var triggerupdateOnStartupJob = TriggerBuilder.Create()
                    .StartNow()
                    .Build();
                scheduler.ScheduleJob(updateOnStartupJob, triggerupdateOnStartupJob);

                //ontology - brands with models
                var updateBrandsAndModelsOntologyJob = JobBuilder.Create<UpdateBrandsAndModelsOntologyJob>().Build();
                var triggerupdateBrandsAndModelsOntology = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(4, 0))
                    )
                    .Build();
                scheduler.ScheduleJob(updateBrandsAndModelsOntologyJob, triggerupdateBrandsAndModelsOntology);

                //ontology - phones
                var updatePhonesOntologyJob = JobBuilder.Create<UpdatePhonesOntologyJob>().Build();
                var triggerUpdatePhonesOntologyJob = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(4, 30))
                    )
                    .Build();
                scheduler.ScheduleJob(updatePhonesOntologyJob, triggerUpdatePhonesOntologyJob);
            }
        }
    }
}
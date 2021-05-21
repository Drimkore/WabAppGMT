using Quartz;
using Quartz.Impl;

namespace WebApplication3.Jobs
{
    public class GameAdderScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<GameAdder>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x       
                    .WithIntervalInMinutes(30)
                    .RepeatForever())                   
                .Build();                              

            await scheduler.ScheduleJob(job, trigger);


        }
    }
}
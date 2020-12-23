using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginApp
{
    public class JobScheduler
    {
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {

                // construct a scheduler factory
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = await schedulerFactory.GetScheduler();
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<DueAmountReminder>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                            s.OnEveryDay()
                            //.WithIntervalInMinutes(1)
                            .WithIntervalInHours(24)
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(06, 45))
                    )
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception e)
            {
               
            }
        }
    }
}
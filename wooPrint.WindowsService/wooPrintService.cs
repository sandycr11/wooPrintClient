using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using wooPrint.WindowsService.Managers;

namespace wooPrint.WindowsService
{
    /// <summary>
    ///
    /// </summary>
    public partial class wooPrintService : ServiceBase
    {
        private IScheduler _sched;

        public wooPrintService()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            bool result = await StartWoocommerceWatching();
            if (result)
                Trace.TraceInformation("wooPrint service started successfully");
        }

        protected override void OnStop()
        {
            StopWoocommerceWatching();
            Trace.TraceInformation("wooPrint service stoped successfully");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async Task<bool> StartWoocommerceWatching()
        {
            try
            {
                var props = new NameValueCollection
                {
                    {"quartz.serializer.type", "binary"}
                };
                var factory = new StdSchedulerFactory(props);
                _sched = await factory.GetScheduler();
                await _sched.Start();

                // create jobs
                var jobOrdersWatcher = JobBuilder
                    .Create<WooCommerceOrdersWatcher>()
                    .WithIdentity("wooCommerceOrdersWatcher", "wooPrint")
                    .Build();

                // create trigger for orders watcher
                var ordersWatcherTrigger = TriggerBuilder
                    .Create()
                    .WithIdentity("ordersWatcherTrigger", "wooPrint")
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(Core.Configuration.WooPrintConfiguration.Config().ApiService.CheckInterval).RepeatForever())
                    .StartNow()
                    .Build();

                // Schedule the job using the job and trigger
                await _sched.ScheduleJob(jobOrdersWatcher, ordersWatcherTrigger);

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        private async void StopWoocommerceWatching()
        {
            if (_sched != null && _sched.IsStarted)
                await _sched.Shutdown();
        }
    }
}
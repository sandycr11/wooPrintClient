using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using MetroFramework.Forms;
using MetroFramework;
using wooPrint.Core.Configuration;
using System.Diagnostics;
using wooPrint.Core.Utils;
using System.Threading;
using Quartz;
using wooPrint.DesktopApp.Managers;

using Quartz.Impl;
using System.Collections.Specialized;

namespace wooPrint.DesktopApp
{
    /// <summary>
    ///
    /// </summary>
    public partial class MainForm : MetroForm
    {
        private IScheduler _sched;

        public MainForm()
        {
            InitializeComponent();
            toolStripMenuItemExit.Click += (e, o) => { FromSystemTray(true); };
            FromSystemTray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            await StartWoocommerceWatching();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButtonAccept_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        /// <summary>
        ///
        /// </summary>
        private void LoadConfiguration()
        {
            try
            {
                var apiServerConfig = WooPrintConfiguration.Config().ApiService;

                metroTextBoxUrl.Text = apiServerConfig.Url;
                metroTextBoxApiKey.Text = apiServerConfig.APIKey;
                metroTextBoxApiSecret.Text = apiServerConfig.APISecret;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                MetroMessageBox.Show(this, "Ha ocurrido un error cargando la configuración.", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 140);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void SaveConfiguration()
        {
            var saveThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    WooPrintConfiguration.Config().ApiService.Url = metroTextBoxUrl.Text;
                    WooPrintConfiguration.Config().ApiService.APIKey = metroTextBoxApiKey.Text;
                    WooPrintConfiguration.Config().ApiService.APISecret = metroTextBoxApiSecret.Text;

                    WooPrintConfiguration.Config().Save();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    MetroMessageBox.Show(this, "Ha ocurrido un error guardando la configuración.", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 140);
                }
            }));

            saveThread.Start();
        }

        private void FromSystemTray(bool closing = false)
        {
            if (closing)
            {
                Application.Exit();
            }
            else
            {
                Show();
                BringToFront();

                ShowIcon = true;
                ShowInTaskbar = true;
                TopMost = true;
                WindowState = FormWindowState.Normal;

                Refresh();
            }
        }

        private void ToSystemTray()
        {
            WindowState = FormWindowState.Minimized;
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = false;

            Hide();
            Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            e.Cancel = true;
            ToSystemTray();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            FromSystemTray();
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

                jobOrdersWatcher.JobDataMap.Put("notifier", notifyIcon);

                // create trigger for orders watcher
                var ordersWatcherTrigger = TriggerBuilder
                    .Create()
                    .WithIdentity("ordersWatcherTrigger", "wooPrint")
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
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

        #region Windows Message Handler

        protected override void WndProc(ref Message m)
        {
            var messageResult = ProcessUtils.ReceiveMessage(ref m);
            if (!string.IsNullOrWhiteSpace(messageResult)
                && messageResult.Equals(ProcessUtils.ProcessPassphrase, StringComparison.InvariantCultureIgnoreCase))
            {
                FromSystemTray();
            }

            base.WndProc(ref m);
        }

        #endregion Windows Message Handler

        private delegate void Function();
    }
}
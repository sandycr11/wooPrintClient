using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Quartz;
using Quartz.Impl;
using wooPrint.DesktopApp.Configuration;
using wooPrint.DesktopApp.Managers;
using wooPrint.DesktopApp.Utils;

namespace wooPrint.DesktopApp
{
    /// <summary>
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

        /// <summary>
        /// </summary>
        private void LoadConfiguration()
        {
            try
            {
                metroTextBoxUrl.Text = ConfigurationManager.GetInstance().Config.ApiUrl;
                metroTextBoxApiKey.Text = ConfigurationManager.GetInstance().Config.ApiKey;
                metroTextBoxApiSecret.Text = ConfigurationManager.GetInstance().Config.ApiSecret;

                metroTextBoxOrderHeader.Text = ConfigurationManager.GetInstance().Config.OrderHeader;
                metroTextBoxOrderSubHeader.Text = ConfigurationManager.GetInstance().Config.OrderSubHeader;
                metroTextBoxOrderFooter.Text = ConfigurationManager.GetInstance().Config.OrderFooter;

                var logoPath = ConfigurationManager.GetInstance().Config.OrderLogoPath;
                if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
                    pictureBoxTicketLogo.BackgroundImage = Image.FromFile(logoPath);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            e.Cancel = true;
            ToSystemTray();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            await StartWoocommerceWatching();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButtonAccept_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            FromSystemTray();
        }

        /// <summary>
        /// </summary>
        private void SaveConfiguration()
        {
            var saveThread = new Thread(() =>
            {
                try
                {
                    ConfigurationManager.GetInstance().Config.ApiUrl = metroTextBoxUrl.Text;
                    ConfigurationManager.GetInstance().Config.ApiKey = metroTextBoxApiKey.Text;
                    ConfigurationManager.GetInstance().Config.ApiSecret = metroTextBoxApiSecret.Text;

                    ConfigurationManager.GetInstance().Config.OrderHeader = metroTextBoxOrderHeader.Text;
                    ConfigurationManager.GetInstance().Config.OrderSubHeader = metroTextBoxOrderSubHeader.Text;
                    ConfigurationManager.GetInstance().Config.OrderFooter = metroTextBoxOrderFooter.Text;

                    ConfigurationManager.GetInstance().Config.LastOrderChecked = "";

                    ConfigurationManager.GetInstance().Save();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    MetroMessageBox.Show(this, "Ha ocurrido un error guardando la configuración.", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 140);
                }
            });

            saveThread.Start();
        }

        /// <summary>
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

        private void ToSystemTray()
        {
            WindowState = FormWindowState.Minimized;
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = false;

            Hide();
            Refresh();
        }

        #region Windows Message Handler

        protected override void WndProc(ref Message m)
        {
            var messageResult = ProcessUtils.ReceiveMessage(ref m);
            if (!string.IsNullOrWhiteSpace(messageResult)
                && messageResult.Equals(ProcessUtils.ProcessPassphrase, StringComparison.InvariantCultureIgnoreCase))
                FromSystemTray();

            base.WndProc(ref m);
        }

        #endregion Windows Message Handler

        private void pictureBoxTicketLogo_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog.FileName;
                    if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                        return;

                    var image = Image.FromFile(filePath);
                    pictureBoxTicketLogo.BackgroundImage = image;

                    ConfigurationManager.GetInstance().Config.OrderLogoPath = filePath;
                }
                catch (Exception exception)
                {
                    Trace.TraceError(exception.ToString());
                }
            }
            else
            {
                pictureBoxTicketLogo.BackgroundImage = new Bitmap(1, 1);
                ConfigurationManager.GetInstance().Config.OrderLogoPath = "";
            }
        }

        private delegate void Function();
    }
}
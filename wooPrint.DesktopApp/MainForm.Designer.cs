namespace wooPrint.DesktopApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.metroStyleExtender = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.pictureBoxTicketLogo = new System.Windows.Forms.PictureBox();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroLabelURL = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxUrl = new MetroFramework.Controls.MetroTextBox();
            this.metroLabelApiKey = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxApiKey = new MetroFramework.Controls.MetroTextBox();
            this.metroLabelApiSecret = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxApiSecret = new MetroFramework.Controls.MetroTextBox();
            this.metroButtonAccept = new MetroFramework.Controls.MetroButton();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.metroContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxServerConfig = new System.Windows.Forms.GroupBox();
            this.groupBoxTicketaConfig = new System.Windows.Forms.GroupBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxOrderHeader = new MetroFramework.Controls.MetroTextBox();
            this.metroLabelOrderHeader = new MetroFramework.Controls.MetroLabel();
            this.metroLabelOrderSubHeader = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxOrderSubHeader = new MetroFramework.Controls.MetroTextBox();
            this.metroLabelFooter = new MetroFramework.Controls.MetroLabel();
            this.metroTextBoxOrderFooter = new MetroFramework.Controls.MetroTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTicketLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.metroContextMenu.SuspendLayout();
            this.groupBoxServerConfig.SuspendLayout();
            this.groupBoxTicketaConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxTicketLogo
            // 
            this.metroStyleExtender.SetApplyMetroTheme(this.pictureBoxTicketLogo, true);
            this.pictureBoxTicketLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxTicketLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTicketLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTicketLogo.Location = new System.Drawing.Point(145, 166);
            this.pictureBoxTicketLogo.Name = "pictureBoxTicketLogo";
            this.pictureBoxTicketLogo.Size = new System.Drawing.Size(342, 95);
            this.pictureBoxTicketLogo.TabIndex = 7;
            this.pictureBoxTicketLogo.TabStop = false;
            this.pictureBoxTicketLogo.Click += new System.EventHandler(this.pictureBoxTicketLogo_Click);
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // metroLabelURL
            // 
            this.metroLabelURL.AutoSize = true;
            this.metroLabelURL.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelURL.Location = new System.Drawing.Point(6, 35);
            this.metroLabelURL.Name = "metroLabelURL";
            this.metroLabelURL.Size = new System.Drawing.Size(57, 20);
            this.metroLabelURL.TabIndex = 0;
            this.metroLabelURL.Text = "API Url:";
            // 
            // metroTextBoxUrl
            // 
            // 
            // 
            // 
            this.metroTextBoxUrl.CustomButton.Image = null;
            this.metroTextBoxUrl.CustomButton.Location = new System.Drawing.Point(362, 2);
            this.metroTextBoxUrl.CustomButton.Name = "";
            this.metroTextBoxUrl.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxUrl.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxUrl.CustomButton.TabIndex = 1;
            this.metroTextBoxUrl.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxUrl.CustomButton.UseSelectable = true;
            this.metroTextBoxUrl.CustomButton.Visible = false;
            this.metroTextBoxUrl.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxUrl.Lines = new string[0];
            this.metroTextBoxUrl.Location = new System.Drawing.Point(97, 25);
            this.metroTextBoxUrl.MaxLength = 32767;
            this.metroTextBoxUrl.Name = "metroTextBoxUrl";
            this.metroTextBoxUrl.PasswordChar = '\0';
            this.metroTextBoxUrl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxUrl.SelectedText = "";
            this.metroTextBoxUrl.SelectionLength = 0;
            this.metroTextBoxUrl.SelectionStart = 0;
            this.metroTextBoxUrl.ShortcutsEnabled = true;
            this.metroTextBoxUrl.Size = new System.Drawing.Size(390, 30);
            this.metroTextBoxUrl.TabIndex = 1;
            this.metroTextBoxUrl.UseSelectable = true;
            this.metroTextBoxUrl.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxUrl.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelApiKey
            // 
            this.metroLabelApiKey.AutoSize = true;
            this.metroLabelApiKey.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelApiKey.Location = new System.Drawing.Point(6, 79);
            this.metroLabelApiKey.Name = "metroLabelApiKey";
            this.metroLabelApiKey.Size = new System.Drawing.Size(62, 20);
            this.metroLabelApiKey.TabIndex = 2;
            this.metroLabelApiKey.Text = "API Key:";
            // 
            // metroTextBoxApiKey
            // 
            // 
            // 
            // 
            this.metroTextBoxApiKey.CustomButton.Image = null;
            this.metroTextBoxApiKey.CustomButton.Location = new System.Drawing.Point(362, 2);
            this.metroTextBoxApiKey.CustomButton.Name = "";
            this.metroTextBoxApiKey.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxApiKey.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxApiKey.CustomButton.TabIndex = 1;
            this.metroTextBoxApiKey.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxApiKey.CustomButton.UseSelectable = true;
            this.metroTextBoxApiKey.CustomButton.Visible = false;
            this.metroTextBoxApiKey.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxApiKey.Lines = new string[0];
            this.metroTextBoxApiKey.Location = new System.Drawing.Point(97, 69);
            this.metroTextBoxApiKey.MaxLength = 32767;
            this.metroTextBoxApiKey.Name = "metroTextBoxApiKey";
            this.metroTextBoxApiKey.PasswordChar = '\0';
            this.metroTextBoxApiKey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxApiKey.SelectedText = "";
            this.metroTextBoxApiKey.SelectionLength = 0;
            this.metroTextBoxApiKey.SelectionStart = 0;
            this.metroTextBoxApiKey.ShortcutsEnabled = true;
            this.metroTextBoxApiKey.Size = new System.Drawing.Size(390, 30);
            this.metroTextBoxApiKey.TabIndex = 3;
            this.metroTextBoxApiKey.UseSelectable = true;
            this.metroTextBoxApiKey.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxApiKey.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelApiSecret
            // 
            this.metroLabelApiSecret.AutoSize = true;
            this.metroLabelApiSecret.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelApiSecret.Location = new System.Drawing.Point(6, 124);
            this.metroLabelApiSecret.Name = "metroLabelApiSecret";
            this.metroLabelApiSecret.Size = new System.Drawing.Size(79, 20);
            this.metroLabelApiSecret.TabIndex = 4;
            this.metroLabelApiSecret.Text = "API Secret:";
            // 
            // metroTextBoxApiSecret
            // 
            // 
            // 
            // 
            this.metroTextBoxApiSecret.CustomButton.Image = null;
            this.metroTextBoxApiSecret.CustomButton.Location = new System.Drawing.Point(362, 2);
            this.metroTextBoxApiSecret.CustomButton.Name = "";
            this.metroTextBoxApiSecret.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxApiSecret.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxApiSecret.CustomButton.TabIndex = 1;
            this.metroTextBoxApiSecret.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxApiSecret.CustomButton.UseSelectable = true;
            this.metroTextBoxApiSecret.CustomButton.Visible = false;
            this.metroTextBoxApiSecret.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxApiSecret.Lines = new string[0];
            this.metroTextBoxApiSecret.Location = new System.Drawing.Point(97, 114);
            this.metroTextBoxApiSecret.MaxLength = 32767;
            this.metroTextBoxApiSecret.Name = "metroTextBoxApiSecret";
            this.metroTextBoxApiSecret.PasswordChar = '\0';
            this.metroTextBoxApiSecret.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxApiSecret.SelectedText = "";
            this.metroTextBoxApiSecret.SelectionLength = 0;
            this.metroTextBoxApiSecret.SelectionStart = 0;
            this.metroTextBoxApiSecret.ShortcutsEnabled = true;
            this.metroTextBoxApiSecret.Size = new System.Drawing.Size(390, 30);
            this.metroTextBoxApiSecret.TabIndex = 5;
            this.metroTextBoxApiSecret.UseSelectable = true;
            this.metroTextBoxApiSecret.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxApiSecret.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroButtonAccept
            // 
            this.metroButtonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButtonAccept.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonAccept.Highlight = true;
            this.metroButtonAccept.Location = new System.Drawing.Point(430, 536);
            this.metroButtonAccept.Name = "metroButtonAccept";
            this.metroButtonAccept.Size = new System.Drawing.Size(87, 33);
            this.metroButtonAccept.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonAccept.TabIndex = 11;
            this.metroButtonAccept.Text = "Save";
            this.metroButtonAccept.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButtonAccept.UseSelectable = true;
            this.metroButtonAccept.UseStyleColors = true;
            this.metroButtonAccept.Click += new System.EventHandler(this.metroButtonAccept_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "WooPrint";
            this.notifyIcon.ContextMenuStrip = this.metroContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "WooPrint";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // metroContextMenu
            // 
            this.metroContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.metroContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExit});
            this.metroContextMenu.Name = "metroContextMenu";
            this.metroContextMenu.Size = new System.Drawing.Size(103, 28);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(102, 24);
            this.toolStripMenuItemExit.Text = "Exit";
            // 
            // groupBoxServerConfig
            // 
            this.groupBoxServerConfig.Controls.Add(this.metroTextBoxUrl);
            this.groupBoxServerConfig.Controls.Add(this.metroLabelURL);
            this.groupBoxServerConfig.Controls.Add(this.metroLabelApiKey);
            this.groupBoxServerConfig.Controls.Add(this.metroTextBoxApiKey);
            this.groupBoxServerConfig.Controls.Add(this.metroLabelApiSecret);
            this.groupBoxServerConfig.Controls.Add(this.metroTextBoxApiSecret);
            this.groupBoxServerConfig.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxServerConfig.Location = new System.Drawing.Point(23, 75);
            this.groupBoxServerConfig.Name = "groupBoxServerConfig";
            this.groupBoxServerConfig.Size = new System.Drawing.Size(493, 161);
            this.groupBoxServerConfig.TabIndex = 18;
            this.groupBoxServerConfig.TabStop = false;
            this.groupBoxServerConfig.Text = "Server Config";
            // 
            // groupBoxTicketaConfig
            // 
            this.groupBoxTicketaConfig.Controls.Add(this.pictureBoxTicketLogo);
            this.groupBoxTicketaConfig.Controls.Add(this.metroLabel1);
            this.groupBoxTicketaConfig.Controls.Add(this.metroTextBoxOrderHeader);
            this.groupBoxTicketaConfig.Controls.Add(this.metroLabelOrderHeader);
            this.groupBoxTicketaConfig.Controls.Add(this.metroLabelOrderSubHeader);
            this.groupBoxTicketaConfig.Controls.Add(this.metroTextBoxOrderSubHeader);
            this.groupBoxTicketaConfig.Controls.Add(this.metroLabelFooter);
            this.groupBoxTicketaConfig.Controls.Add(this.metroTextBoxOrderFooter);
            this.groupBoxTicketaConfig.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTicketaConfig.Location = new System.Drawing.Point(23, 263);
            this.groupBoxTicketaConfig.Name = "groupBoxTicketaConfig";
            this.groupBoxTicketaConfig.Size = new System.Drawing.Size(493, 267);
            this.groupBoxTicketaConfig.TabIndex = 19;
            this.groupBoxTicketaConfig.TabStop = false;
            this.groupBoxTicketaConfig.Text = "Tickets Config";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(6, 166);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(89, 20);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Ticket Logo:";
            // 
            // metroTextBoxOrderHeader
            // 
            // 
            // 
            // 
            this.metroTextBoxOrderHeader.CustomButton.Image = null;
            this.metroTextBoxOrderHeader.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.metroTextBoxOrderHeader.CustomButton.Name = "";
            this.metroTextBoxOrderHeader.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxOrderHeader.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxOrderHeader.CustomButton.TabIndex = 1;
            this.metroTextBoxOrderHeader.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxOrderHeader.CustomButton.UseSelectable = true;
            this.metroTextBoxOrderHeader.CustomButton.Visible = false;
            this.metroTextBoxOrderHeader.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxOrderHeader.Lines = new string[0];
            this.metroTextBoxOrderHeader.Location = new System.Drawing.Point(145, 25);
            this.metroTextBoxOrderHeader.MaxLength = 32767;
            this.metroTextBoxOrderHeader.Name = "metroTextBoxOrderHeader";
            this.metroTextBoxOrderHeader.PasswordChar = '\0';
            this.metroTextBoxOrderHeader.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxOrderHeader.SelectedText = "";
            this.metroTextBoxOrderHeader.SelectionLength = 0;
            this.metroTextBoxOrderHeader.SelectionStart = 0;
            this.metroTextBoxOrderHeader.ShortcutsEnabled = true;
            this.metroTextBoxOrderHeader.Size = new System.Drawing.Size(342, 30);
            this.metroTextBoxOrderHeader.TabIndex = 1;
            this.metroTextBoxOrderHeader.UseSelectable = true;
            this.metroTextBoxOrderHeader.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxOrderHeader.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelOrderHeader
            // 
            this.metroLabelOrderHeader.AutoSize = true;
            this.metroLabelOrderHeader.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelOrderHeader.Location = new System.Drawing.Point(6, 35);
            this.metroLabelOrderHeader.Name = "metroLabelOrderHeader";
            this.metroLabelOrderHeader.Size = new System.Drawing.Size(104, 20);
            this.metroLabelOrderHeader.TabIndex = 0;
            this.metroLabelOrderHeader.Text = "Ticket Header:";
            // 
            // metroLabelOrderSubHeader
            // 
            this.metroLabelOrderSubHeader.AutoSize = true;
            this.metroLabelOrderSubHeader.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelOrderSubHeader.Location = new System.Drawing.Point(6, 79);
            this.metroLabelOrderSubHeader.Name = "metroLabelOrderSubHeader";
            this.metroLabelOrderSubHeader.Size = new System.Drawing.Size(133, 20);
            this.metroLabelOrderSubHeader.TabIndex = 2;
            this.metroLabelOrderSubHeader.Text = "Ticket Sub Header:";
            // 
            // metroTextBoxOrderSubHeader
            // 
            // 
            // 
            // 
            this.metroTextBoxOrderSubHeader.CustomButton.Image = null;
            this.metroTextBoxOrderSubHeader.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.metroTextBoxOrderSubHeader.CustomButton.Name = "";
            this.metroTextBoxOrderSubHeader.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxOrderSubHeader.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxOrderSubHeader.CustomButton.TabIndex = 1;
            this.metroTextBoxOrderSubHeader.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxOrderSubHeader.CustomButton.UseSelectable = true;
            this.metroTextBoxOrderSubHeader.CustomButton.Visible = false;
            this.metroTextBoxOrderSubHeader.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxOrderSubHeader.Lines = new string[0];
            this.metroTextBoxOrderSubHeader.Location = new System.Drawing.Point(145, 69);
            this.metroTextBoxOrderSubHeader.MaxLength = 32767;
            this.metroTextBoxOrderSubHeader.Name = "metroTextBoxOrderSubHeader";
            this.metroTextBoxOrderSubHeader.PasswordChar = '\0';
            this.metroTextBoxOrderSubHeader.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxOrderSubHeader.SelectedText = "";
            this.metroTextBoxOrderSubHeader.SelectionLength = 0;
            this.metroTextBoxOrderSubHeader.SelectionStart = 0;
            this.metroTextBoxOrderSubHeader.ShortcutsEnabled = true;
            this.metroTextBoxOrderSubHeader.Size = new System.Drawing.Size(342, 30);
            this.metroTextBoxOrderSubHeader.TabIndex = 3;
            this.metroTextBoxOrderSubHeader.UseSelectable = true;
            this.metroTextBoxOrderSubHeader.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxOrderSubHeader.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelFooter
            // 
            this.metroLabelFooter.AutoSize = true;
            this.metroLabelFooter.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelFooter.Location = new System.Drawing.Point(6, 124);
            this.metroLabelFooter.Name = "metroLabelFooter";
            this.metroLabelFooter.Size = new System.Drawing.Size(98, 20);
            this.metroLabelFooter.TabIndex = 4;
            this.metroLabelFooter.Text = "Ticket Footer:";
            // 
            // metroTextBoxOrderFooter
            // 
            // 
            // 
            // 
            this.metroTextBoxOrderFooter.CustomButton.Image = null;
            this.metroTextBoxOrderFooter.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.metroTextBoxOrderFooter.CustomButton.Name = "";
            this.metroTextBoxOrderFooter.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxOrderFooter.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxOrderFooter.CustomButton.TabIndex = 1;
            this.metroTextBoxOrderFooter.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxOrderFooter.CustomButton.UseSelectable = true;
            this.metroTextBoxOrderFooter.CustomButton.Visible = false;
            this.metroTextBoxOrderFooter.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxOrderFooter.Lines = new string[0];
            this.metroTextBoxOrderFooter.Location = new System.Drawing.Point(145, 114);
            this.metroTextBoxOrderFooter.MaxLength = 32767;
            this.metroTextBoxOrderFooter.Name = "metroTextBoxOrderFooter";
            this.metroTextBoxOrderFooter.PasswordChar = '\0';
            this.metroTextBoxOrderFooter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxOrderFooter.SelectedText = "";
            this.metroTextBoxOrderFooter.SelectionLength = 0;
            this.metroTextBoxOrderFooter.SelectionStart = 0;
            this.metroTextBoxOrderFooter.ShortcutsEnabled = true;
            this.metroTextBoxOrderFooter.Size = new System.Drawing.Size(342, 30);
            this.metroTextBoxOrderFooter.TabIndex = 5;
            this.metroTextBoxOrderFooter.UseSelectable = true;
            this.metroTextBoxOrderFooter.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxOrderFooter.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "PNG Files|*.png|JPG Files|8.jpg|GIF Files|*.gif";
            this.openFileDialog.Title = "Select Logo";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(540, 592);
            this.Controls.Add(this.groupBoxTicketaConfig);
            this.Controls.Add(this.groupBoxServerConfig);
            this.Controls.Add(this.metroButtonAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.Text = "WooCommerce Printer v2.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTicketLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.metroContextMenu.ResumeLayout(false);
            this.groupBoxServerConfig.ResumeLayout(false);
            this.groupBoxServerConfig.PerformLayout();
            this.groupBoxTicketaConfig.ResumeLayout(false);
            this.groupBoxTicketaConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleExtender metroStyleExtender;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Controls.MetroButton metroButtonAccept;
        private MetroFramework.Controls.MetroTextBox metroTextBoxApiSecret;
        private MetroFramework.Controls.MetroLabel metroLabelApiSecret;
        private MetroFramework.Controls.MetroTextBox metroTextBoxApiKey;
        private MetroFramework.Controls.MetroLabel metroLabelApiKey;
        private MetroFramework.Controls.MetroTextBox metroTextBoxUrl;
        private MetroFramework.Controls.MetroLabel metroLabelURL;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.GroupBox groupBoxTicketaConfig;
        private System.Windows.Forms.PictureBox pictureBoxTicketLogo;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox metroTextBoxOrderHeader;
        private MetroFramework.Controls.MetroLabel metroLabelOrderHeader;
        private MetroFramework.Controls.MetroLabel metroLabelOrderSubHeader;
        private MetroFramework.Controls.MetroTextBox metroTextBoxOrderSubHeader;
        private MetroFramework.Controls.MetroLabel metroLabelFooter;
        private MetroFramework.Controls.MetroTextBox metroTextBoxOrderFooter;
        private System.Windows.Forms.GroupBox groupBoxServerConfig;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}


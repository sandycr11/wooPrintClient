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
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.metroContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // metroLabelURL
            // 
            this.metroLabelURL.AutoSize = true;
            this.metroLabelURL.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelURL.Location = new System.Drawing.Point(24, 85);
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
            this.metroTextBoxUrl.CustomButton.Location = new System.Drawing.Point(368, 2);
            this.metroTextBoxUrl.CustomButton.Name = "";
            this.metroTextBoxUrl.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxUrl.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxUrl.CustomButton.TabIndex = 1;
            this.metroTextBoxUrl.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxUrl.CustomButton.UseSelectable = true;
            this.metroTextBoxUrl.CustomButton.Visible = false;
            this.metroTextBoxUrl.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxUrl.Lines = new string[0];
            this.metroTextBoxUrl.Location = new System.Drawing.Point(121, 75);
            this.metroTextBoxUrl.MaxLength = 32767;
            this.metroTextBoxUrl.Name = "metroTextBoxUrl";
            this.metroTextBoxUrl.PasswordChar = '\0';
            this.metroTextBoxUrl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxUrl.SelectedText = "";
            this.metroTextBoxUrl.SelectionLength = 0;
            this.metroTextBoxUrl.SelectionStart = 0;
            this.metroTextBoxUrl.ShortcutsEnabled = true;
            this.metroTextBoxUrl.Size = new System.Drawing.Size(396, 30);
            this.metroTextBoxUrl.TabIndex = 1;
            this.metroTextBoxUrl.UseSelectable = true;
            this.metroTextBoxUrl.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxUrl.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelApiKey
            // 
            this.metroLabelApiKey.AutoSize = true;
            this.metroLabelApiKey.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelApiKey.Location = new System.Drawing.Point(24, 129);
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
            this.metroTextBoxApiKey.CustomButton.Location = new System.Drawing.Point(368, 2);
            this.metroTextBoxApiKey.CustomButton.Name = "";
            this.metroTextBoxApiKey.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxApiKey.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxApiKey.CustomButton.TabIndex = 1;
            this.metroTextBoxApiKey.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxApiKey.CustomButton.UseSelectable = true;
            this.metroTextBoxApiKey.CustomButton.Visible = false;
            this.metroTextBoxApiKey.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxApiKey.Lines = new string[0];
            this.metroTextBoxApiKey.Location = new System.Drawing.Point(121, 119);
            this.metroTextBoxApiKey.MaxLength = 32767;
            this.metroTextBoxApiKey.Name = "metroTextBoxApiKey";
            this.metroTextBoxApiKey.PasswordChar = '\0';
            this.metroTextBoxApiKey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxApiKey.SelectedText = "";
            this.metroTextBoxApiKey.SelectionLength = 0;
            this.metroTextBoxApiKey.SelectionStart = 0;
            this.metroTextBoxApiKey.ShortcutsEnabled = true;
            this.metroTextBoxApiKey.Size = new System.Drawing.Size(396, 30);
            this.metroTextBoxApiKey.TabIndex = 3;
            this.metroTextBoxApiKey.UseSelectable = true;
            this.metroTextBoxApiKey.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxApiKey.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelApiSecret
            // 
            this.metroLabelApiSecret.AutoSize = true;
            this.metroLabelApiSecret.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabelApiSecret.Location = new System.Drawing.Point(24, 174);
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
            this.metroTextBoxApiSecret.CustomButton.Location = new System.Drawing.Point(368, 2);
            this.metroTextBoxApiSecret.CustomButton.Name = "";
            this.metroTextBoxApiSecret.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.metroTextBoxApiSecret.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxApiSecret.CustomButton.TabIndex = 1;
            this.metroTextBoxApiSecret.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxApiSecret.CustomButton.UseSelectable = true;
            this.metroTextBoxApiSecret.CustomButton.Visible = false;
            this.metroTextBoxApiSecret.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBoxApiSecret.Lines = new string[0];
            this.metroTextBoxApiSecret.Location = new System.Drawing.Point(121, 164);
            this.metroTextBoxApiSecret.MaxLength = 32767;
            this.metroTextBoxApiSecret.Name = "metroTextBoxApiSecret";
            this.metroTextBoxApiSecret.PasswordChar = '\0';
            this.metroTextBoxApiSecret.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxApiSecret.SelectedText = "";
            this.metroTextBoxApiSecret.SelectionLength = 0;
            this.metroTextBoxApiSecret.SelectionStart = 0;
            this.metroTextBoxApiSecret.ShortcutsEnabled = true;
            this.metroTextBoxApiSecret.Size = new System.Drawing.Size(396, 30);
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
            this.metroButtonAccept.Location = new System.Drawing.Point(430, 214);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(540, 268);
            this.Controls.Add(this.metroButtonAccept);
            this.Controls.Add(this.metroTextBoxApiSecret);
            this.Controls.Add(this.metroLabelApiSecret);
            this.Controls.Add(this.metroTextBoxApiKey);
            this.Controls.Add(this.metroLabelApiKey);
            this.Controls.Add(this.metroTextBoxUrl);
            this.Controls.Add(this.metroLabelURL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.Text = "WooCommerce Printer v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.metroContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}


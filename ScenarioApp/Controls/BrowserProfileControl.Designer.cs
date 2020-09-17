namespace ScenarioApp.Controls
{
    partial class BrowserProfileControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlName = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFolder = new System.Windows.Forms.Panel();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlStartUrl = new System.Windows.Forms.Panel();
            this.tbStartUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbUserAgent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlLanguage = new System.Windows.Forms.Panel();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTimeZone = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTimezone = new System.Windows.Forms.ComboBox();
            this.cbTimezoneCountry = new System.Windows.Forms.ComboBox();
            this.pnlIp = new System.Windows.Forms.Panel();
            this.pnlIpInfo = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnSetByIpInfo = new System.Windows.Forms.Button();
            this.pnlOneProxy = new System.Windows.Forms.GroupBox();
            this.pnlName.SuspendLayout();
            this.pnlFolder.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlStartUrl.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlLanguage.SuspendLayout();
            this.pnlTimeZone.SuspendLayout();
            this.pnlIp.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlName
            // 
            this.pnlName.Controls.Add(this.tbName);
            this.pnlName.Controls.Add(this.label1);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(0, 75);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(698, 49);
            this.pnlName.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(230, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(424, 26);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование";
            // 
            // pnlFolder
            // 
            this.pnlFolder.Controls.Add(this.tbFolder);
            this.pnlFolder.Controls.Add(this.label2);
            this.pnlFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFolder.Location = new System.Drawing.Point(0, 124);
            this.pnlFolder.Name = "pnlFolder";
            this.pnlFolder.Size = new System.Drawing.Size(698, 49);
            this.pnlFolder.TabIndex = 1;
            // 
            // tbFolder
            // 
            this.tbFolder.Enabled = false;
            this.tbFolder.Location = new System.Drawing.Point(230, 12);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.ReadOnly = true;
            this.tbFolder.Size = new System.Drawing.Size(424, 26);
            this.tbFolder.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Папка";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(698, 75);
            this.panel3.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(530, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(382, 20);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(126, 42);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // pnlStartUrl
            // 
            this.pnlStartUrl.Controls.Add(this.tbStartUrl);
            this.pnlStartUrl.Controls.Add(this.label3);
            this.pnlStartUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStartUrl.Location = new System.Drawing.Point(0, 173);
            this.pnlStartUrl.Name = "pnlStartUrl";
            this.pnlStartUrl.Size = new System.Drawing.Size(698, 49);
            this.pnlStartUrl.TabIndex = 3;
            // 
            // tbStartUrl
            // 
            this.tbStartUrl.Location = new System.Drawing.Point(230, 12);
            this.tbStartUrl.Name = "tbStartUrl";
            this.tbStartUrl.Size = new System.Drawing.Size(424, 26);
            this.tbStartUrl.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Начальный адрес";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbUserAgent);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 222);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(698, 49);
            this.panel5.TabIndex = 4;
            // 
            // tbUserAgent
            // 
            this.tbUserAgent.Location = new System.Drawing.Point(230, 12);
            this.tbUserAgent.Name = "tbUserAgent";
            this.tbUserAgent.Size = new System.Drawing.Size(424, 26);
            this.tbUserAgent.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Юзер агент";
            // 
            // pnlLanguage
            // 
            this.pnlLanguage.Controls.Add(this.cbLanguage);
            this.pnlLanguage.Controls.Add(this.label5);
            this.pnlLanguage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLanguage.Location = new System.Drawing.Point(0, 521);
            this.pnlLanguage.Name = "pnlLanguage";
            this.pnlLanguage.Size = new System.Drawing.Size(698, 49);
            this.pnlLanguage.TabIndex = 5;
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            "ar",
            "am",
            "bg",
            "bn",
            "ca",
            "cs",
            "da",
            "de",
            "el",
            "en",
            "en_GB",
            "en_US",
            "es",
            "es_419",
            "et",
            "fa",
            "fi",
            "fil",
            "fr",
            "gu",
            "he",
            "hi",
            "hr",
            "hu",
            "id",
            "it",
            "ja",
            "kn",
            "ko",
            "lt",
            "lv",
            "ml",
            "mr",
            "ms",
            "nl",
            "no",
            "pl",
            "pt_BR",
            "pt_PT",
            "ro",
            "ru",
            "sk",
            "sl",
            "sr",
            "sv",
            "sw",
            "ta",
            "te",
            "th",
            "tr",
            "uk",
            "vi",
            "zh_CN",
            "zh_TW"});
            this.cbLanguage.Location = new System.Drawing.Point(230, 12);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(424, 28);
            this.cbLanguage.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Язык";
            // 
            // pnlTimeZone
            // 
            this.pnlTimeZone.Controls.Add(this.label7);
            this.pnlTimeZone.Controls.Add(this.label6);
            this.pnlTimeZone.Controls.Add(this.cbTimezone);
            this.pnlTimeZone.Controls.Add(this.cbTimezoneCountry);
            this.pnlTimeZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimeZone.Location = new System.Drawing.Point(0, 570);
            this.pnlTimeZone.Name = "pnlTimeZone";
            this.pnlTimeZone.Size = new System.Drawing.Size(698, 89);
            this.pnlTimeZone.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Временная зона";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Страна временной зоны";
            // 
            // cbTimezone
            // 
            this.cbTimezone.FormattingEnabled = true;
            this.cbTimezone.Location = new System.Drawing.Point(230, 48);
            this.cbTimezone.Name = "cbTimezone";
            this.cbTimezone.Size = new System.Drawing.Size(424, 28);
            this.cbTimezone.TabIndex = 4;
            // 
            // cbTimezoneCountry
            // 
            this.cbTimezoneCountry.FormattingEnabled = true;
            this.cbTimezoneCountry.Location = new System.Drawing.Point(230, 12);
            this.cbTimezoneCountry.Name = "cbTimezoneCountry";
            this.cbTimezoneCountry.Size = new System.Drawing.Size(424, 28);
            this.cbTimezoneCountry.TabIndex = 3;
            // 
            // pnlIp
            // 
            this.pnlIp.Controls.Add(this.pnlIpInfo);
            this.pnlIp.Controls.Add(this.panel8);
            this.pnlIp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIp.Location = new System.Drawing.Point(0, 416);
            this.pnlIp.Name = "pnlIp";
            this.pnlIp.Size = new System.Drawing.Size(698, 105);
            this.pnlIp.TabIndex = 7;
            // 
            // pnlIpInfo
            // 
            this.pnlIpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIpInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlIpInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlIpInfo.Name = "pnlIpInfo";
            this.pnlIpInfo.Size = new System.Drawing.Size(580, 105);
            this.pnlIpInfo.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnSetByIpInfo);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(580, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(118, 105);
            this.panel8.TabIndex = 0;
            // 
            // btnSetByIpInfo
            // 
            this.btnSetByIpInfo.Location = new System.Drawing.Point(4, 23);
            this.btnSetByIpInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetByIpInfo.Name = "btnSetByIpInfo";
            this.btnSetByIpInfo.Size = new System.Drawing.Size(110, 35);
            this.btnSetByIpInfo.TabIndex = 0;
            this.btnSetByIpInfo.Text = "SetByIpInfo";
            this.btnSetByIpInfo.UseVisualStyleBackColor = true;
            // 
            // pnlOneProxy
            // 
            this.pnlOneProxy.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOneProxy.Location = new System.Drawing.Point(0, 271);
            this.pnlOneProxy.Name = "pnlOneProxy";
            this.pnlOneProxy.Size = new System.Drawing.Size(698, 145);
            this.pnlOneProxy.TabIndex = 9;
            this.pnlOneProxy.TabStop = false;
            this.pnlOneProxy.Text = "Прокси";
            // 
            // BrowserProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTimeZone);
            this.Controls.Add(this.pnlLanguage);
            this.Controls.Add(this.pnlIp);
            this.Controls.Add(this.pnlOneProxy);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlStartUrl);
            this.Controls.Add(this.pnlFolder);
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.panel3);
            this.Name = "BrowserProfileControl";
            this.Size = new System.Drawing.Size(698, 677);
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.pnlFolder.ResumeLayout(false);
            this.pnlFolder.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlStartUrl.ResumeLayout(false);
            this.pnlStartUrl.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlLanguage.ResumeLayout(false);
            this.pnlLanguage.PerformLayout();
            this.pnlTimeZone.ResumeLayout(false);
            this.pnlTimeZone.PerformLayout();
            this.pnlIp.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel pnlStartUrl;
        private System.Windows.Forms.TextBox tbStartUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbUserAgent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlLanguage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Panel pnlTimeZone;
        private System.Windows.Forms.ComboBox cbTimezoneCountry;
        private System.Windows.Forms.ComboBox cbTimezone;
        private System.Windows.Forms.Panel pnlIp;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSetByIpInfo;
        private System.Windows.Forms.Panel pnlIpInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox pnlOneProxy;
    }
}

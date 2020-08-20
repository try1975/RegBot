namespace ScenarioApp.Controls
{
    partial class FingerprintControl
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
            this.lbDevices = new System.Windows.Forms.ListBox();
            this.tbCurrentDevice = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.btnWebShow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIpWeb = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lbTimeZones = new System.Windows.Forms.ListBox();
            this.lbLocale = new System.Windows.Forms.ListBox();
            this.pnlProfiles = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDevices
            // 
            this.lbDevices.FormattingEnabled = true;
            this.lbDevices.ItemHeight = 20;
            this.lbDevices.Location = new System.Drawing.Point(24, 67);
            this.lbDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(331, 344);
            this.lbDevices.TabIndex = 0;
            // 
            // tbCurrentDevice
            // 
            this.tbCurrentDevice.Location = new System.Drawing.Point(23, 32);
            this.tbCurrentDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbCurrentDevice.Name = "tbCurrentDevice";
            this.tbCurrentDevice.Size = new System.Drawing.Size(331, 26);
            this.tbCurrentDevice.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbProxy);
            this.groupBox1.Controls.Add(this.btnWebShow);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbIpWeb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1066, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проверка отпечатка браузера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Текущий прокси";
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(658, 63);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(332, 26);
            this.tbProxy.TabIndex = 5;
            // 
            // btnWebShow
            // 
            this.btnWebShow.Location = new System.Drawing.Point(464, 54);
            this.btnWebShow.Name = "btnWebShow";
            this.btnWebShow.Size = new System.Drawing.Size(176, 40);
            this.btnWebShow.TabIndex = 4;
            this.btnWebShow.Text = "Показать сайт";
            this.btnWebShow.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сайт для проверки";
            // 
            // cmbIpWeb
            // 
            this.cmbIpWeb.FormattingEnabled = true;
            this.cmbIpWeb.Items.AddRange(new object[] {
            "https://yandex.ru/internet/",
            "https://2ip.ru/",
            "https://browserleaks.com/ip",
            "https://whatismytimezone.com/",
            "https://whoer.net/",
            "https://panopticlick.eff.org/",
            "https://arh.antoinevastel.com/bots/areyouheadless"});
            this.cmbIpWeb.Location = new System.Drawing.Point(27, 60);
            this.cmbIpWeb.Name = "cmbIpWeb";
            this.cmbIpWeb.Size = new System.Drawing.Size(403, 28);
            this.cmbIpWeb.TabIndex = 0;
            this.cmbIpWeb.Text = "https://yandex.ru/internet/";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(413, 9);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(65, 20);
            this.lblCountry.TabIndex = 18;
            this.lblCountry.Text = "Страна";
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(415, 32);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(162, 28);
            this.cmbCountry.TabIndex = 19;
            // 
            // lbTimeZones
            // 
            this.lbTimeZones.FormattingEnabled = true;
            this.lbTimeZones.ItemHeight = 20;
            this.lbTimeZones.Location = new System.Drawing.Point(415, 67);
            this.lbTimeZones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbTimeZones.Name = "lbTimeZones";
            this.lbTimeZones.Size = new System.Drawing.Size(331, 344);
            this.lbTimeZones.TabIndex = 20;
            // 
            // lbLocale
            // 
            this.lbLocale.FormattingEnabled = true;
            this.lbLocale.ItemHeight = 20;
            this.lbLocale.Items.AddRange(new object[] {
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
            this.lbLocale.Location = new System.Drawing.Point(805, 67);
            this.lbLocale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbLocale.Name = "lbLocale";
            this.lbLocale.Size = new System.Drawing.Size(138, 344);
            this.lbLocale.TabIndex = 21;
            // 
            // pnlProfiles
            // 
            this.pnlProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProfiles.Location = new System.Drawing.Point(0, 546);
            this.pnlProfiles.Name = "pnlProfiles";
            this.pnlProfiles.Size = new System.Drawing.Size(1066, 251);
            this.pnlProfiles.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbCurrentDevice);
            this.panel1.Controls.Add(this.lbDevices);
            this.panel1.Controls.Add(this.lbLocale);
            this.panel1.Controls.Add(this.cmbCountry);
            this.panel1.Controls.Add(this.lbTimeZones);
            this.panel1.Controls.Add(this.lblCountry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 428);
            this.panel1.TabIndex = 23;
            // 
            // FingerprintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlProfiles);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FingerprintControl";
            this.Size = new System.Drawing.Size(1066, 797);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbDevices;
        private System.Windows.Forms.TextBox tbCurrentDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWebShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIpWeb;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.ListBox lbTimeZones;
        private System.Windows.Forms.ListBox lbLocale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.Panel pnlProfiles;
        private System.Windows.Forms.Panel panel1;
    }
}

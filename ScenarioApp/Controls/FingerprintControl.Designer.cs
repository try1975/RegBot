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
            this.btnWebShow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIpWeb = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lbTimeZones = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDevices
            // 
            this.lbDevices.FormattingEnabled = true;
            this.lbDevices.ItemHeight = 20;
            this.lbDevices.Location = new System.Drawing.Point(99, 354);
            this.lbDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(331, 344);
            this.lbDevices.TabIndex = 0;
            // 
            // tbCurrentDevice
            // 
            this.tbCurrentDevice.Location = new System.Drawing.Point(99, 294);
            this.tbCurrentDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbCurrentDevice.Name = "tbCurrentDevice";
            this.tbCurrentDevice.Size = new System.Drawing.Size(331, 26);
            this.tbCurrentDevice.TabIndex = 1;
            // 
            // groupBox1
            // 
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
            "https://whoer.net/"});
            this.cmbIpWeb.Location = new System.Drawing.Point(27, 60);
            this.cmbIpWeb.Name = "cmbIpWeb";
            this.cmbIpWeb.Size = new System.Drawing.Size(403, 28);
            this.cmbIpWeb.TabIndex = 0;
            this.cmbIpWeb.Text = "https://yandex.ru/internet/";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(596, 152);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(65, 20);
            this.lblCountry.TabIndex = 18;
            this.lblCountry.Text = "Страна";
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(600, 175);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(162, 28);
            this.cmbCountry.TabIndex = 19;
            // 
            // lbTimeZones
            // 
            this.lbTimeZones.FormattingEnabled = true;
            this.lbTimeZones.ItemHeight = 20;
            this.lbTimeZones.Location = new System.Drawing.Point(599, 285);
            this.lbTimeZones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbTimeZones.Name = "lbTimeZones";
            this.lbTimeZones.Size = new System.Drawing.Size(331, 344);
            this.lbTimeZones.TabIndex = 20;
            // 
            // FingerprintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTimeZones);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.cmbCountry);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbCurrentDevice);
            this.Controls.Add(this.lbDevices);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FingerprintControl";
            this.Size = new System.Drawing.Size(1066, 797);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

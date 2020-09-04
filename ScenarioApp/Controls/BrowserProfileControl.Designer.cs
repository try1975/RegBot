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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbStartUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbUserAgent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbTimezone = new System.Windows.Forms.ComboBox();
            this.cbTimezoneCountry = new System.Windows.Forms.ComboBox();
            this.pnlIp = new System.Windows.Forms.Panel();
            this.pnlIpInfo = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnSetByIpInfo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlOneProxy = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlIp.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 32);
            this.panel1.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(153, 8);
            this.tbName.Margin = new System.Windows.Forms.Padding(2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(284, 20);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbFolder);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 81);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(465, 32);
            this.panel2.TabIndex = 1;
            // 
            // tbFolder
            // 
            this.tbFolder.Enabled = false;
            this.tbFolder.Location = new System.Drawing.Point(153, 8);
            this.tbFolder.Margin = new System.Windows.Forms.Padding(2);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.ReadOnly = true;
            this.tbFolder.Size = new System.Drawing.Size(284, 20);
            this.tbFolder.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Папка";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(465, 49);
            this.panel3.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(353, 13);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(255, 13);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 27);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbStartUrl);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 113);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(465, 32);
            this.panel4.TabIndex = 3;
            // 
            // tbStartUrl
            // 
            this.tbStartUrl.Location = new System.Drawing.Point(153, 8);
            this.tbStartUrl.Margin = new System.Windows.Forms.Padding(2);
            this.tbStartUrl.Name = "tbStartUrl";
            this.tbStartUrl.Size = new System.Drawing.Size(284, 20);
            this.tbStartUrl.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Начальный адрес";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbUserAgent);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 145);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(465, 32);
            this.panel5.TabIndex = 4;
            // 
            // tbUserAgent
            // 
            this.tbUserAgent.Location = new System.Drawing.Point(153, 8);
            this.tbUserAgent.Margin = new System.Windows.Forms.Padding(2);
            this.tbUserAgent.Name = "tbUserAgent";
            this.tbUserAgent.Size = new System.Drawing.Size(284, 20);
            this.tbUserAgent.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Идентификатор браузера";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cbLanguage);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 303);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(465, 32);
            this.panel6.TabIndex = 5;
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
            this.cbLanguage.Location = new System.Drawing.Point(153, 8);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(284, 21);
            this.cbLanguage.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Язык";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.cbTimezone);
            this.panel7.Controls.Add(this.cbTimezoneCountry);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 335);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(465, 58);
            this.panel7.TabIndex = 6;
            // 
            // cbTimezone
            // 
            this.cbTimezone.FormattingEnabled = true;
            this.cbTimezone.Location = new System.Drawing.Point(153, 31);
            this.cbTimezone.Margin = new System.Windows.Forms.Padding(2);
            this.cbTimezone.Name = "cbTimezone";
            this.cbTimezone.Size = new System.Drawing.Size(284, 21);
            this.cbTimezone.TabIndex = 4;
            // 
            // cbTimezoneCountry
            // 
            this.cbTimezoneCountry.FormattingEnabled = true;
            this.cbTimezoneCountry.Location = new System.Drawing.Point(153, 8);
            this.cbTimezoneCountry.Margin = new System.Windows.Forms.Padding(2);
            this.cbTimezoneCountry.Name = "cbTimezoneCountry";
            this.cbTimezoneCountry.Size = new System.Drawing.Size(284, 21);
            this.cbTimezoneCountry.TabIndex = 3;
            // 
            // pnlIp
            // 
            this.pnlIp.Controls.Add(this.pnlIpInfo);
            this.pnlIp.Controls.Add(this.panel8);
            this.pnlIp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIp.Location = new System.Drawing.Point(0, 235);
            this.pnlIp.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIp.Name = "pnlIp";
            this.pnlIp.Size = new System.Drawing.Size(465, 68);
            this.pnlIp.TabIndex = 7;
            // 
            // pnlIpInfo
            // 
            this.pnlIpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIpInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlIpInfo.Name = "pnlIpInfo";
            this.pnlIpInfo.Size = new System.Drawing.Size(386, 68);
            this.pnlIpInfo.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnSetByIpInfo);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(386, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(79, 68);
            this.panel8.TabIndex = 0;
            // 
            // btnSetByIpInfo
            // 
            this.btnSetByIpInfo.Location = new System.Drawing.Point(3, 15);
            this.btnSetByIpInfo.Name = "btnSetByIpInfo";
            this.btnSetByIpInfo.Size = new System.Drawing.Size(73, 23);
            this.btnSetByIpInfo.TabIndex = 0;
            this.btnSetByIpInfo.Text = "SetByIpInfo";
            this.btnSetByIpInfo.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Страна временной зоны";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Временная зона";
            // 
            // pnlOneProxy
            // 
            this.pnlOneProxy.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOneProxy.Location = new System.Drawing.Point(0, 177);
            this.pnlOneProxy.Margin = new System.Windows.Forms.Padding(2);
            this.pnlOneProxy.Name = "pnlOneProxy";
            this.pnlOneProxy.Size = new System.Drawing.Size(465, 58);
            this.pnlOneProxy.TabIndex = 8;
            // 
            // BrowserProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pnlIp);
            this.Controls.Add(this.pnlOneProxy);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BrowserProfileControl";
            this.Size = new System.Drawing.Size(465, 440);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlIp.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbStartUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbUserAgent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox cbTimezoneCountry;
        private System.Windows.Forms.ComboBox cbTimezone;
        private System.Windows.Forms.Panel pnlIp;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSetByIpInfo;
        private System.Windows.Forms.Panel pnlIpInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlOneProxy;
    }
}

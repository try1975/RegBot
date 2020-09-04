namespace ScenarioApp.Controls
{
    partial class IpInfoControl
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
            this.tbIpInfoTimezoneCountry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetIpInfo = new System.Windows.Forms.Button();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbIpInfoLanguage = new System.Windows.Forms.TextBox();
            this.tbIpInfoTimezone = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbIpInfoTimezoneCountry
            // 
            this.tbIpInfoTimezoneCountry.Location = new System.Drawing.Point(78, 37);
            this.tbIpInfoTimezoneCountry.Name = "tbIpInfoTimezoneCountry";
            this.tbIpInfoTimezoneCountry.Size = new System.Drawing.Size(126, 20);
            this.tbIpInfoTimezoneCountry.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Страна ip";
            // 
            // btnGetIpInfo
            // 
            this.btnGetIpInfo.Location = new System.Drawing.Point(6, 15);
            this.btnGetIpInfo.Name = "btnGetIpInfo";
            this.btnGetIpInfo.Size = new System.Drawing.Size(75, 23);
            this.btnGetIpInfo.TabIndex = 2;
            this.btnGetIpInfo.Text = "Инфо об ip";
            this.btnGetIpInfo.UseVisualStyleBackColor = true;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(78, 11);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(126, 20);
            this.tbIp.TabIndex = 3;
            // 
            // tbIpInfoLanguage
            // 
            this.tbIpInfoLanguage.Location = new System.Drawing.Point(318, 11);
            this.tbIpInfoLanguage.Name = "tbIpInfoLanguage";
            this.tbIpInfoLanguage.Size = new System.Drawing.Size(126, 20);
            this.tbIpInfoLanguage.TabIndex = 4;
            // 
            // tbIpInfoTimezone
            // 
            this.tbIpInfoTimezone.Location = new System.Drawing.Point(318, 37);
            this.tbIpInfoTimezone.Name = "tbIpInfoTimezone";
            this.tbIpInfoTimezone.Size = new System.Drawing.Size(126, 20);
            this.tbIpInfoTimezone.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGetIpInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(489, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(92, 81);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbIpInfoTimezone);
            this.panel2.Controls.Add(this.tbIpInfoLanguage);
            this.panel2.Controls.Add(this.tbIp);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbIpInfoTimezoneCountry);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(489, 81);
            this.panel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Временная зона ip";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Язык ip";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "ip адрес";
            // 
            // IpInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "IpInfoControl";
            this.Size = new System.Drawing.Size(581, 81);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbIpInfoTimezoneCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetIpInfo;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.TextBox tbIpInfoLanguage;
        private System.Windows.Forms.TextBox tbIpInfoTimezone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

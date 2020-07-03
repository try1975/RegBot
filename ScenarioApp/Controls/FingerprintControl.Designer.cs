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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWebShow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIpWeb = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(66, 230);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(176, 121);
            this.listBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(66, 191);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWebShow);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbIpWeb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(711, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проверка отпечатка браузера";
            // 
            // btnWebShow
            // 
            this.btnWebShow.Location = new System.Drawing.Point(309, 35);
            this.btnWebShow.Margin = new System.Windows.Forms.Padding(2);
            this.btnWebShow.Name = "btnWebShow";
            this.btnWebShow.Size = new System.Drawing.Size(117, 26);
            this.btnWebShow.TabIndex = 4;
            this.btnWebShow.Text = "Показать сайт";
            this.btnWebShow.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сайт для проверки";
            // 
            // cmbIpWeb
            // 
            this.cmbIpWeb.FormattingEnabled = true;
            this.cmbIpWeb.Items.AddRange(new object[] {
            "https://yandex.ru/internet/",
            "https://2ip.ru/",
            "https://browserleaks.com/ip"});
            this.cmbIpWeb.Location = new System.Drawing.Point(18, 39);
            this.cmbIpWeb.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIpWeb.Name = "cmbIpWeb";
            this.cmbIpWeb.Size = new System.Drawing.Size(270, 21);
            this.cmbIpWeb.TabIndex = 0;
            this.cmbIpWeb.Text = "https://yandex.ru/internet/";
            // 
            // FingerprintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "FingerprintControl";
            this.Size = new System.Drawing.Size(711, 518);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWebShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIpWeb;
    }
}

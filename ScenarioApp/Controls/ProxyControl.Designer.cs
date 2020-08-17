namespace ScenarioApp.Controls
{
    partial class ProxyControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIpWebShow = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIpWeb = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbProxies = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveProxy = new System.Windows.Forms.Button();
            this.lblProxyPath = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnIpWebShow);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbProxy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbIpWeb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(463, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проверка ip адреса браузера";
            // 
            // btnIpWebShow
            // 
            this.btnIpWebShow.Location = new System.Drawing.Point(319, 59);
            this.btnIpWebShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnIpWebShow.Name = "btnIpWebShow";
            this.btnIpWebShow.Size = new System.Drawing.Size(117, 26);
            this.btnIpWebShow.TabIndex = 4;
            this.btnIpWebShow.Text = "Показать сайт";
            this.btnIpWebShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Текущий прокси";
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(18, 79);
            this.tbProxy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(270, 20);
            this.tbProxy.TabIndex = 2;
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
            "https://browserleaks.com/ip",
            "https://whatismyipaddress.com/",
            "http://httpbin.org/ip",
            "http://free-proxy.cz/ru/ipinfo",
            "https://ipstack.com/"});
            this.cmbIpWeb.Location = new System.Drawing.Point(18, 39);
            this.cmbIpWeb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbIpWeb.Name = "cmbIpWeb";
            this.cmbIpWeb.Size = new System.Drawing.Size(270, 21);
            this.cmbIpWeb.TabIndex = 0;
            this.cmbIpWeb.Text = "https://yandex.ru/internet/";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbProxies);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 109);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(463, 292);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Доступные прокси";
            // 
            // tbProxies
            // 
            this.tbProxies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProxies.Location = new System.Drawing.Point(2, 77);
            this.tbProxies.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbProxies.Multiline = true;
            this.tbProxies.Name = "tbProxies";
            this.tbProxies.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbProxies.Size = new System.Drawing.Size(459, 213);
            this.tbProxies.TabIndex = 2;
            this.tbProxies.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSaveProxy);
            this.panel1.Controls.Add(this.lblProxyPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 62);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(261, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Двойной клик мыши для выбора прокси текущим";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Формат [логин:пароль@]адрес:порт";
            // 
            // btnSaveProxy
            // 
            this.btnSaveProxy.Location = new System.Drawing.Point(9, 4);
            this.btnSaveProxy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveProxy.Name = "btnSaveProxy";
            this.btnSaveProxy.Size = new System.Drawing.Size(108, 21);
            this.btnSaveProxy.TabIndex = 1;
            this.btnSaveProxy.Text = "Сохранить прокси";
            this.btnSaveProxy.UseVisualStyleBackColor = true;
            // 
            // lblProxyPath
            // 
            this.lblProxyPath.AutoSize = true;
            this.lblProxyPath.Location = new System.Drawing.Point(121, 8);
            this.lblProxyPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProxyPath.Name = "lblProxyPath";
            this.lblProxyPath.Size = new System.Drawing.Size(55, 13);
            this.lblProxyPath.TabIndex = 0;
            this.lblProxyPath.Text = "ProxyPath";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "https://yandex.ru/internet/",
            "https://2ip.ru/",
            "https://browserleaks.com/ip"});
            this.comboBox1.Location = new System.Drawing.Point(27, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(403, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "https://yandex.ru/internet/";
            // 
            // ProxyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProxyControl";
            this.Size = new System.Drawing.Size(463, 401);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIpWebShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIpWeb;
        private System.Windows.Forms.TextBox tbProxies;
        private System.Windows.Forms.Label lblProxyPath;
        private System.Windows.Forms.Button btnSaveProxy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

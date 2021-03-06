﻿namespace ScenarioApp.Controls
{
    partial class SmsServiceControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvRequests = new ADGV.AdvancedDataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gbServices = new System.Windows.Forms.GroupBox();
            this.rbIg = new System.Windows.Forms.RadioButton();
            this.rbOk = new System.Windows.Forms.RadioButton();
            this.rbVk = new System.Windows.Forms.RadioButton();
            this.rbFacebook = new System.Windows.Forms.RadioButton();
            this.rbGmail = new System.Windows.Forms.RadioButton();
            this.rbYandex = new System.Windows.Forms.RadioButton();
            this.rbMailRu = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblSmsService = new System.Windows.Forms.Label();
            this.cmbSmsService = new System.Windows.Forms.ComboBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.rbTw = new System.Windows.Forms.RadioButton();
            this.rbTg = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.gbServices.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 833);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1402, 72);
            this.panel2.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1191, 18);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(190, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(28, 18);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(314, 35);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Получить номер и отслеживать смс";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(847, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 833);
            this.panel1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(555, 833);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Лог";
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(4, 24);
            this.tbLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(547, 804);
            this.tbLog.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(843, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 833);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.gbServices);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(843, 833);
            this.panel3.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgvRequests);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 230);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(843, 603);
            this.panel5.TabIndex = 24;
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToOrderColumns = true;
            this.dgvRequests.AutoGenerateContextFilters = true;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.DateWithTime = false;
            this.dgvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequests.Location = new System.Drawing.Point(0, 71);
            this.dgvRequests.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersWidth = 62;
            this.dgvRequests.Size = new System.Drawing.Size(843, 532);
            this.dgvRequests.TabIndex = 1;
            this.dgvRequests.TimeFilter = false;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(843, 71);
            this.panel6.TabIndex = 0;
            // 
            // gbServices
            // 
            this.gbServices.Controls.Add(this.rbTg);
            this.gbServices.Controls.Add(this.rbTw);
            this.gbServices.Controls.Add(this.rbIg);
            this.gbServices.Controls.Add(this.rbOk);
            this.gbServices.Controls.Add(this.rbVk);
            this.gbServices.Controls.Add(this.rbFacebook);
            this.gbServices.Controls.Add(this.rbGmail);
            this.gbServices.Controls.Add(this.rbYandex);
            this.gbServices.Controls.Add(this.rbMailRu);
            this.gbServices.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbServices.Location = new System.Drawing.Point(0, 92);
            this.gbServices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbServices.Name = "gbServices";
            this.gbServices.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbServices.Size = new System.Drawing.Size(843, 138);
            this.gbServices.TabIndex = 22;
            this.gbServices.TabStop = false;
            this.gbServices.Text = "Сервисы";
            // 
            // rbIg
            // 
            this.rbIg.AutoSize = true;
            this.rbIg.Location = new System.Drawing.Point(325, 77);
            this.rbIg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbIg.Name = "rbIg";
            this.rbIg.Size = new System.Drawing.Size(106, 24);
            this.rbIg.TabIndex = 6;
            this.rbIg.Text = "Instagram";
            this.rbIg.UseVisualStyleBackColor = true;
            // 
            // rbOk
            // 
            this.rbOk.AutoSize = true;
            this.rbOk.Location = new System.Drawing.Point(164, 78);
            this.rbOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbOk.Name = "rbOk";
            this.rbOk.Size = new System.Drawing.Size(153, 24);
            this.rbOk.TabIndex = 5;
            this.rbOk.Text = "Одноклассники";
            this.rbOk.UseVisualStyleBackColor = true;
            // 
            // rbVk
            // 
            this.rbVk.AutoSize = true;
            this.rbVk.Location = new System.Drawing.Point(36, 78);
            this.rbVk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbVk.Name = "rbVk";
            this.rbVk.Size = new System.Drawing.Size(117, 24);
            this.rbVk.TabIndex = 4;
            this.rbVk.Text = "ВКонтакте";
            this.rbVk.UseVisualStyleBackColor = true;
            // 
            // rbFacebook
            // 
            this.rbFacebook.AutoSize = true;
            this.rbFacebook.Location = new System.Drawing.Point(318, 43);
            this.rbFacebook.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbFacebook.Name = "rbFacebook";
            this.rbFacebook.Size = new System.Drawing.Size(105, 24);
            this.rbFacebook.TabIndex = 3;
            this.rbFacebook.Text = "Facebook";
            this.rbFacebook.UseVisualStyleBackColor = true;
            // 
            // rbGmail
            // 
            this.rbGmail.AutoSize = true;
            this.rbGmail.Location = new System.Drawing.Point(232, 43);
            this.rbGmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbGmail.Name = "rbGmail";
            this.rbGmail.Size = new System.Drawing.Size(75, 24);
            this.rbGmail.TabIndex = 2;
            this.rbGmail.Text = "Gmail";
            this.rbGmail.UseVisualStyleBackColor = true;
            // 
            // rbYandex
            // 
            this.rbYandex.AutoSize = true;
            this.rbYandex.Location = new System.Drawing.Point(132, 43);
            this.rbYandex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbYandex.Name = "rbYandex";
            this.rbYandex.Size = new System.Drawing.Size(88, 24);
            this.rbYandex.TabIndex = 1;
            this.rbYandex.Text = "Yandex";
            this.rbYandex.UseVisualStyleBackColor = true;
            // 
            // rbMailRu
            // 
            this.rbMailRu.AutoSize = true;
            this.rbMailRu.Checked = true;
            this.rbMailRu.Location = new System.Drawing.Point(36, 43);
            this.rbMailRu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbMailRu.Name = "rbMailRu";
            this.rbMailRu.Size = new System.Drawing.Size(83, 24);
            this.rbMailRu.TabIndex = 0;
            this.rbMailRu.TabStop = true;
            this.rbMailRu.Text = "MailRu";
            this.rbMailRu.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblSmsService);
            this.panel4.Controls.Add(this.cmbSmsService);
            this.panel4.Controls.Add(this.cmbCountry);
            this.panel4.Controls.Add(this.lblCountry);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(843, 92);
            this.panel4.TabIndex = 23;
            // 
            // lblSmsService
            // 
            this.lblSmsService.AutoSize = true;
            this.lblSmsService.Location = new System.Drawing.Point(24, 14);
            this.lblSmsService.Name = "lblSmsService";
            this.lblSmsService.Size = new System.Drawing.Size(95, 20);
            this.lblSmsService.TabIndex = 18;
            this.lblSmsService.Text = "Смс сервис";
            // 
            // cmbSmsService
            // 
            this.cmbSmsService.FormattingEnabled = true;
            this.cmbSmsService.Location = new System.Drawing.Point(28, 37);
            this.cmbSmsService.Name = "cmbSmsService";
            this.cmbSmsService.Size = new System.Drawing.Size(162, 28);
            this.cmbSmsService.TabIndex = 19;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(246, 37);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(162, 28);
            this.cmbCountry.TabIndex = 21;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(242, 14);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(125, 20);
            this.lblCountry.TabIndex = 20;
            this.lblCountry.Text = "Страна номера";
            // 
            // rbTw
            // 
            this.rbTw.AutoSize = true;
            this.rbTw.Location = new System.Drawing.Point(439, 78);
            this.rbTw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTw.Name = "rbTw";
            this.rbTw.Size = new System.Drawing.Size(81, 24);
            this.rbTw.TabIndex = 7;
            this.rbTw.Text = "Twitter";
            this.rbTw.UseVisualStyleBackColor = true;
            // 
            // rbTg
            // 
            this.rbTg.AutoSize = true;
            this.rbTg.Location = new System.Drawing.Point(528, 78);
            this.rbTg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTg.Name = "rbTg";
            this.rbTg.Size = new System.Drawing.Size(100, 24);
            this.rbTg.TabIndex = 8;
            this.rbTg.Text = "Telegram";
            this.rbTg.UseVisualStyleBackColor = true;
            // 
            // SmsServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SmsServiceControl";
            this.Size = new System.Drawing.Size(1402, 905);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.gbServices.ResumeLayout(false);
            this.gbServices.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbSmsService;
        private System.Windows.Forms.Label lblSmsService;
        private System.Windows.Forms.GroupBox gbServices;
        private System.Windows.Forms.RadioButton rbOk;
        private System.Windows.Forms.RadioButton rbVk;
        private System.Windows.Forms.RadioButton rbFacebook;
        private System.Windows.Forms.RadioButton rbGmail;
        private System.Windows.Forms.RadioButton rbYandex;
        private System.Windows.Forms.RadioButton rbMailRu;
        private System.Windows.Forms.Panel panel5;
        private ADGV.AdvancedDataGridView dgvRequests;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbIg;
        private System.Windows.Forms.RadioButton rbTw;
        private System.Windows.Forms.RadioButton rbTg;
    }
}

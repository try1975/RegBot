﻿namespace ScenarioApp.Controls
{
    partial class RegBotControl
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
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvItems = new ADGV.AdvancedDataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbSmsService = new System.Windows.Forms.ComboBox();
            this.lblSmsService = new System.Windows.Forms.Label();
            this.cbSmsAuto = new System.Windows.Forms.CheckBox();
            this.btnMailRuEmail = new System.Windows.Forms.Button();
            this.btnYandexEmail = new System.Windows.Forms.Button();
            this.btnGmail = new System.Windows.Forms.Button();
            this.btnFacebook = new System.Windows.Forms.Button();
            this.btnVk = new System.Windows.Forms.Button();
            this.btnMailRuPhone = new System.Windows.Forms.Button();
            this.btnYandexPhone = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbLastname = new System.Windows.Forms.TextBox();
            this.lblLastname = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.btnGenerateRu = new System.Windows.Forms.Button();
            this.btnGenerateEn = new System.Windows.Forms.Button();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1429, 804);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "История";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1423, 51);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvItems);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1423, 747);
            this.panel4.TabIndex = 1;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToOrderColumns = true;
            this.dgvItems.AutoGenerateContextFilters = true;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.DateWithTime = true;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.RowTemplate.Height = 28;
            this.dgvItems.Size = new System.Drawing.Size(1423, 747);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.TimeFilter = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1429, 804);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Регистрация";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1423, 274);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSmsAuto);
            this.groupBox1.Controls.Add(this.lblSmsService);
            this.groupBox1.Controls.Add(this.cmbSmsService);
            this.groupBox1.Controls.Add(this.lblCountry);
            this.groupBox1.Controls.Add(this.cmbCountry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(1139, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 274);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Телефон";
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(31, 133);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(162, 28);
            this.cmbCountry.TabIndex = 17;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(27, 109);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(125, 20);
            this.lblCountry.TabIndex = 16;
            this.lblCountry.Text = "Страна номера";
            // 
            // cmbSmsService
            // 
            this.cmbSmsService.FormattingEnabled = true;
            this.cmbSmsService.Location = new System.Drawing.Point(31, 66);
            this.cmbSmsService.Name = "cmbSmsService";
            this.cmbSmsService.Size = new System.Drawing.Size(162, 28);
            this.cmbSmsService.TabIndex = 14;
            // 
            // lblSmsService
            // 
            this.lblSmsService.AutoSize = true;
            this.lblSmsService.Location = new System.Drawing.Point(27, 43);
            this.lblSmsService.Name = "lblSmsService";
            this.lblSmsService.Size = new System.Drawing.Size(95, 20);
            this.lblSmsService.TabIndex = 13;
            this.lblSmsService.Text = "Смс сервис";
            // 
            // cbSmsAuto
            // 
            this.cbSmsAuto.AutoSize = true;
            this.cbSmsAuto.Checked = true;
            this.cbSmsAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSmsAuto.Location = new System.Drawing.Point(31, 182);
            this.cbSmsAuto.Name = "cbSmsAuto";
            this.cbSmsAuto.Size = new System.Drawing.Size(152, 24);
            this.cbSmsAuto.TabIndex = 18;
            this.cbSmsAuto.Text = "автоматически";
            this.cbSmsAuto.UseVisualStyleBackColor = true;
            // 
            // btnMailRuEmail
            // 
            this.btnMailRuEmail.Location = new System.Drawing.Point(19, 14);
            this.btnMailRuEmail.Name = "btnMailRuEmail";
            this.btnMailRuEmail.Size = new System.Drawing.Size(189, 48);
            this.btnMailRuEmail.TabIndex = 22;
            this.btnMailRuEmail.Text = "mail.ru - почта";
            this.btnMailRuEmail.UseVisualStyleBackColor = true;
            // 
            // btnYandexEmail
            // 
            this.btnYandexEmail.Location = new System.Drawing.Point(19, 80);
            this.btnYandexEmail.Name = "btnYandexEmail";
            this.btnYandexEmail.Size = new System.Drawing.Size(189, 48);
            this.btnYandexEmail.TabIndex = 23;
            this.btnYandexEmail.Text = "yandex.ru - почта";
            this.btnYandexEmail.UseVisualStyleBackColor = true;
            // 
            // btnGmail
            // 
            this.btnGmail.Location = new System.Drawing.Point(19, 151);
            this.btnGmail.Name = "btnGmail";
            this.btnGmail.Size = new System.Drawing.Size(189, 48);
            this.btnGmail.TabIndex = 24;
            this.btnGmail.Text = "gmail.com";
            this.btnGmail.UseVisualStyleBackColor = true;
            // 
            // btnFacebook
            // 
            this.btnFacebook.Location = new System.Drawing.Point(19, 207);
            this.btnFacebook.Name = "btnFacebook";
            this.btnFacebook.Size = new System.Drawing.Size(189, 48);
            this.btnFacebook.TabIndex = 25;
            this.btnFacebook.Text = "facebook.com";
            this.btnFacebook.UseVisualStyleBackColor = true;
            // 
            // btnVk
            // 
            this.btnVk.Location = new System.Drawing.Point(214, 207);
            this.btnVk.Name = "btnVk";
            this.btnVk.Size = new System.Drawing.Size(189, 48);
            this.btnVk.TabIndex = 26;
            this.btnVk.Text = "vk.com";
            this.btnVk.UseVisualStyleBackColor = true;
            // 
            // btnMailRuPhone
            // 
            this.btnMailRuPhone.Location = new System.Drawing.Point(214, 14);
            this.btnMailRuPhone.Name = "btnMailRuPhone";
            this.btnMailRuPhone.Size = new System.Drawing.Size(189, 48);
            this.btnMailRuPhone.TabIndex = 27;
            this.btnMailRuPhone.Text = "mail.ru - телефон";
            this.btnMailRuPhone.UseVisualStyleBackColor = true;
            // 
            // btnYandexPhone
            // 
            this.btnYandexPhone.Location = new System.Drawing.Point(214, 80);
            this.btnYandexPhone.Name = "btnYandexPhone";
            this.btnYandexPhone.Size = new System.Drawing.Size(189, 48);
            this.btnYandexPhone.TabIndex = 28;
            this.btnYandexPhone.Text = "yandex.ru - телефон";
            this.btnYandexPhone.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnYandexPhone);
            this.panel5.Controls.Add(this.btnMailRuEmail);
            this.panel5.Controls.Add(this.btnMailRuPhone);
            this.panel5.Controls.Add(this.btnYandexEmail);
            this.panel5.Controls.Add(this.btnVk);
            this.panel5.Controls.Add(this.btnGmail);
            this.panel5.Controls.Add(this.btnFacebook);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(428, 274);
            this.panel5.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 277);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1423, 524);
            this.panel3.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1423, 524);
            this.textBox1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1437, 837);
            this.tabControl1.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGenerateRu);
            this.panel6.Controls.Add(this.btnGenerateEn);
            this.panel6.Controls.Add(this.rbFemale);
            this.panel6.Controls.Add(this.rbMale);
            this.panel6.Controls.Add(this.lblSex);
            this.panel6.Controls.Add(this.lblBirthDate);
            this.panel6.Controls.Add(this.dtpBirthDate);
            this.panel6.Controls.Add(this.tbPassword);
            this.panel6.Controls.Add(this.lblPassword);
            this.panel6.Controls.Add(this.tbLastname);
            this.panel6.Controls.Add(this.lblLastname);
            this.panel6.Controls.Add(this.tbFirstName);
            this.panel6.Controls.Add(this.lblFirstName);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(428, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(711, 274);
            this.panel6.TabIndex = 26;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(18, 187);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(350, 26);
            this.tbPassword.TabIndex = 12;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(14, 162);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 20);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Пароль";
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(18, 116);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(350, 26);
            this.tbLastname.TabIndex = 10;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Location = new System.Drawing.Point(14, 91);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(81, 20);
            this.lblLastname.TabIndex = 9;
            this.lblLastname.Text = "Фамилия";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(18, 50);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(350, 26);
            this.tbFirstName.TabIndex = 8;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(14, 25);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(40, 20);
            this.lblFirstName.TabIndex = 7;
            this.lblFirstName.Text = "Имя";
            // 
            // btnGenerateRu
            // 
            this.btnGenerateRu.Location = new System.Drawing.Point(405, 190);
            this.btnGenerateRu.Name = "btnGenerateRu";
            this.btnGenerateRu.Size = new System.Drawing.Size(250, 32);
            this.btnGenerateRu.TabIndex = 29;
            this.btnGenerateRu.Text = "Сгенерировать данные руск.";
            this.btnGenerateRu.UseVisualStyleBackColor = true;
            // 
            // btnGenerateEn
            // 
            this.btnGenerateEn.Location = new System.Drawing.Point(405, 151);
            this.btnGenerateEn.Name = "btnGenerateEn";
            this.btnGenerateEn.Size = new System.Drawing.Size(250, 32);
            this.btnGenerateEn.TabIndex = 28;
            this.btnGenerateEn.Text = "Сгенерировать данные англ.";
            this.btnGenerateEn.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(527, 111);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(97, 24);
            this.rbFemale.TabIndex = 27;
            this.rbFemale.Text = "женский";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(423, 113);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(97, 24);
            this.rbMale.TabIndex = 26;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "мужской";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(420, 86);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(40, 20);
            this.lblSex.TabIndex = 25;
            this.lblSex.Text = "Пол";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(420, 22);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(128, 20);
            this.lblBirthDate.TabIndex = 24;
            this.lblBirthDate.Text = "Дата рождения";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Location = new System.Drawing.Point(423, 45);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 26);
            this.dtpBirthDate.TabIndex = 23;
            // 
            // RegBotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RegBotControl";
            this.Size = new System.Drawing.Size(1437, 837);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel4;
        private ADGV.AdvancedDataGridView dgvItems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnYandexPhone;
        private System.Windows.Forms.Button btnMailRuEmail;
        private System.Windows.Forms.Button btnMailRuPhone;
        private System.Windows.Forms.Button btnYandexEmail;
        private System.Windows.Forms.Button btnVk;
        private System.Windows.Forms.Button btnGmail;
        private System.Windows.Forms.Button btnFacebook;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbSmsAuto;
        private System.Windows.Forms.Label lblSmsService;
        private System.Windows.Forms.ComboBox cmbSmsService;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGenerateRu;
        private System.Windows.Forms.Button btnGenerateEn;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbLastname;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lblFirstName;
    }
}

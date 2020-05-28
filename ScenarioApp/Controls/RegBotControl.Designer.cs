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
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvItems = new ADGV.AdvancedDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGenerateRu = new System.Windows.Forms.Button();
            this.btnGenerateEn = new System.Windows.Forms.Button();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbLastname = new System.Windows.Forms.TextBox();
            this.lblLastname = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnYandexPhone = new System.Windows.Forms.Button();
            this.btnMailRuEmail = new System.Windows.Forms.Button();
            this.btnMailRuPhone = new System.Windows.Forms.Button();
            this.btnYandexEmail = new System.Windows.Forms.Button();
            this.btnVk = new System.Windows.Forms.Button();
            this.btnGmail = new System.Windows.Forms.Button();
            this.btnFacebook = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSmsAuto = new System.Windows.Forms.CheckBox();
            this.lblSmsService = new System.Windows.Forms.Label();
            this.cmbSmsService = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cbCountryAuto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(950, 518);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "История";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvItems);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(2, 35);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(946, 481);
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
            this.dgvItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.RowTemplate.Height = 28;
            this.dgvItems.Size = new System.Drawing.Size(946, 481);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.TimeFilter = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(946, 33);
            this.panel2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(950, 518);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Регистрация";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 180);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(946, 336);
            this.panel3.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(946, 336);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 178);
            this.panel1.TabIndex = 3;
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
            this.panel6.Location = new System.Drawing.Point(285, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(472, 178);
            this.panel6.TabIndex = 26;
            // 
            // btnGenerateRu
            // 
            this.btnGenerateRu.Location = new System.Drawing.Point(270, 123);
            this.btnGenerateRu.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateRu.Name = "btnGenerateRu";
            this.btnGenerateRu.Size = new System.Drawing.Size(167, 21);
            this.btnGenerateRu.TabIndex = 29;
            this.btnGenerateRu.Text = "Сгенерировать данные руск.";
            this.btnGenerateRu.UseVisualStyleBackColor = true;
            // 
            // btnGenerateEn
            // 
            this.btnGenerateEn.Location = new System.Drawing.Point(270, 98);
            this.btnGenerateEn.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateEn.Name = "btnGenerateEn";
            this.btnGenerateEn.Size = new System.Drawing.Size(167, 21);
            this.btnGenerateEn.TabIndex = 28;
            this.btnGenerateEn.Text = "Сгенерировать данные англ.";
            this.btnGenerateEn.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(351, 72);
            this.rbFemale.Margin = new System.Windows.Forms.Padding(2);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(69, 17);
            this.rbFemale.TabIndex = 27;
            this.rbFemale.Text = "женский";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(282, 73);
            this.rbMale.Margin = new System.Windows.Forms.Padding(2);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(70, 17);
            this.rbMale.TabIndex = 26;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "мужской";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(280, 56);
            this.lblSex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(27, 13);
            this.lblSex.TabIndex = 25;
            this.lblSex.Text = "Пол";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(280, 14);
            this.lblBirthDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(86, 13);
            this.lblBirthDate.TabIndex = 24;
            this.lblBirthDate.Text = "Дата рождения";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Location = new System.Drawing.Point(282, 29);
            this.dtpBirthDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(135, 20);
            this.dtpBirthDate.TabIndex = 23;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(12, 122);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(235, 20);
            this.tbPassword.TabIndex = 12;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(9, 105);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(45, 13);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Пароль";
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(12, 75);
            this.tbLastname.Margin = new System.Windows.Forms.Padding(2);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(235, 20);
            this.tbLastname.TabIndex = 10;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Location = new System.Drawing.Point(9, 59);
            this.lblLastname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(56, 13);
            this.lblLastname.TabIndex = 9;
            this.lblLastname.Text = "Фамилия";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(12, 32);
            this.tbFirstName.Margin = new System.Windows.Forms.Padding(2);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(235, 20);
            this.tbFirstName.TabIndex = 8;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(9, 16);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(29, 13);
            this.lblFirstName.TabIndex = 7;
            this.lblFirstName.Text = "Имя";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnOk);
            this.panel5.Controls.Add(this.btnYandexPhone);
            this.panel5.Controls.Add(this.btnMailRuEmail);
            this.panel5.Controls.Add(this.btnMailRuPhone);
            this.panel5.Controls.Add(this.btnYandexEmail);
            this.panel5.Controls.Add(this.btnVk);
            this.panel5.Controls.Add(this.btnGmail);
            this.panel5.Controls.Add(this.btnFacebook);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(285, 178);
            this.panel5.TabIndex = 25;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(143, 135);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(126, 31);
            this.btnOk.TabIndex = 29;
            this.btnOk.Text = "ok.ru";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnYandexPhone
            // 
            this.btnYandexPhone.Location = new System.Drawing.Point(143, 52);
            this.btnYandexPhone.Margin = new System.Windows.Forms.Padding(2);
            this.btnYandexPhone.Name = "btnYandexPhone";
            this.btnYandexPhone.Size = new System.Drawing.Size(126, 31);
            this.btnYandexPhone.TabIndex = 28;
            this.btnYandexPhone.Text = "yandex.ru - телефон";
            this.btnYandexPhone.UseVisualStyleBackColor = true;
            // 
            // btnMailRuEmail
            // 
            this.btnMailRuEmail.Location = new System.Drawing.Point(13, 9);
            this.btnMailRuEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnMailRuEmail.Name = "btnMailRuEmail";
            this.btnMailRuEmail.Size = new System.Drawing.Size(126, 31);
            this.btnMailRuEmail.TabIndex = 22;
            this.btnMailRuEmail.Text = "mail.ru - почта";
            this.btnMailRuEmail.UseVisualStyleBackColor = true;
            // 
            // btnMailRuPhone
            // 
            this.btnMailRuPhone.Location = new System.Drawing.Point(143, 9);
            this.btnMailRuPhone.Margin = new System.Windows.Forms.Padding(2);
            this.btnMailRuPhone.Name = "btnMailRuPhone";
            this.btnMailRuPhone.Size = new System.Drawing.Size(126, 31);
            this.btnMailRuPhone.TabIndex = 27;
            this.btnMailRuPhone.Text = "mail.ru - телефон";
            this.btnMailRuPhone.UseVisualStyleBackColor = true;
            // 
            // btnYandexEmail
            // 
            this.btnYandexEmail.Location = new System.Drawing.Point(13, 52);
            this.btnYandexEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnYandexEmail.Name = "btnYandexEmail";
            this.btnYandexEmail.Size = new System.Drawing.Size(126, 31);
            this.btnYandexEmail.TabIndex = 23;
            this.btnYandexEmail.Text = "yandex.ru - почта";
            this.btnYandexEmail.UseVisualStyleBackColor = true;
            // 
            // btnVk
            // 
            this.btnVk.Location = new System.Drawing.Point(143, 98);
            this.btnVk.Margin = new System.Windows.Forms.Padding(2);
            this.btnVk.Name = "btnVk";
            this.btnVk.Size = new System.Drawing.Size(126, 31);
            this.btnVk.TabIndex = 26;
            this.btnVk.Text = "vk.com";
            this.btnVk.UseVisualStyleBackColor = true;
            // 
            // btnGmail
            // 
            this.btnGmail.Location = new System.Drawing.Point(13, 98);
            this.btnGmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnGmail.Name = "btnGmail";
            this.btnGmail.Size = new System.Drawing.Size(126, 31);
            this.btnGmail.TabIndex = 24;
            this.btnGmail.Text = "gmail.com";
            this.btnGmail.UseVisualStyleBackColor = true;
            // 
            // btnFacebook
            // 
            this.btnFacebook.Location = new System.Drawing.Point(13, 135);
            this.btnFacebook.Margin = new System.Windows.Forms.Padding(2);
            this.btnFacebook.Name = "btnFacebook";
            this.btnFacebook.Size = new System.Drawing.Size(126, 31);
            this.btnFacebook.TabIndex = 25;
            this.btnFacebook.Text = "facebook.com";
            this.btnFacebook.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCountryAuto);
            this.groupBox1.Controls.Add(this.cbSmsAuto);
            this.groupBox1.Controls.Add(this.lblSmsService);
            this.groupBox1.Controls.Add(this.cmbSmsService);
            this.groupBox1.Controls.Add(this.lblCountry);
            this.groupBox1.Controls.Add(this.cmbCountry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(757, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(189, 178);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Телефон";
            // 
            // cbSmsAuto
            // 
            this.cbSmsAuto.AutoSize = true;
            this.cbSmsAuto.Checked = true;
            this.cbSmsAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSmsAuto.Location = new System.Drawing.Point(21, 68);
            this.cbSmsAuto.Margin = new System.Windows.Forms.Padding(2);
            this.cbSmsAuto.Name = "cbSmsAuto";
            this.cbSmsAuto.Size = new System.Drawing.Size(103, 17);
            this.cbSmsAuto.TabIndex = 18;
            this.cbSmsAuto.Text = "автоматически";
            this.cbSmsAuto.UseVisualStyleBackColor = true;
            // 
            // lblSmsService
            // 
            this.lblSmsService.AutoSize = true;
            this.lblSmsService.Location = new System.Drawing.Point(18, 28);
            this.lblSmsService.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSmsService.Name = "lblSmsService";
            this.lblSmsService.Size = new System.Drawing.Size(67, 13);
            this.lblSmsService.TabIndex = 13;
            this.lblSmsService.Text = "Смс сервис";
            // 
            // cmbSmsService
            // 
            this.cmbSmsService.FormattingEnabled = true;
            this.cmbSmsService.Location = new System.Drawing.Point(21, 43);
            this.cmbSmsService.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSmsService.Name = "cmbSmsService";
            this.cmbSmsService.Size = new System.Drawing.Size(109, 21);
            this.cmbSmsService.TabIndex = 14;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(18, 98);
            this.lblCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(84, 13);
            this.lblCountry.TabIndex = 16;
            this.lblCountry.Text = "Страна номера";
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(21, 113);
            this.cmbCountry.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(109, 21);
            this.cmbCountry.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(958, 544);
            this.tabControl1.TabIndex = 3;
            // 
            // cbCountryAuto
            // 
            this.cbCountryAuto.AutoSize = true;
            this.cbCountryAuto.Checked = true;
            this.cbCountryAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCountryAuto.Location = new System.Drawing.Point(21, 138);
            this.cbCountryAuto.Margin = new System.Windows.Forms.Padding(2);
            this.cbCountryAuto.Name = "cbCountryAuto";
            this.cbCountryAuto.Size = new System.Drawing.Size(103, 17);
            this.cbCountryAuto.TabIndex = 19;
            this.cbCountryAuto.Text = "автоматически";
            this.cbCountryAuto.UseVisualStyleBackColor = true;
            // 
            // RegBotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "RegBotControl";
            this.Size = new System.Drawing.Size(958, 544);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbCountryAuto;
    }
}

namespace ScenarioApp.Controls
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnYandexPhone = new System.Windows.Forms.Button();
            this.btnMailRuPhone = new System.Windows.Forms.Button();
            this.btnVk = new System.Windows.Forms.Button();
            this.btnFacebook = new System.Windows.Forms.Button();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.btnGmail = new System.Windows.Forms.Button();
            this.cmbSmsService = new System.Windows.Forms.ComboBox();
            this.lblSmsService = new System.Windows.Forms.Label();
            this.btnGenerateEn = new System.Windows.Forms.Button();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.btnYandexEmail = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbLastname = new System.Windows.Forms.TextBox();
            this.lblLastname = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.btnMailRuEmail = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvItems = new ADGV.AdvancedDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnGenerateRu = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
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
            this.panel1.Controls.Add(this.btnGenerateRu);
            this.panel1.Controls.Add(this.btnYandexPhone);
            this.panel1.Controls.Add(this.btnMailRuPhone);
            this.panel1.Controls.Add(this.btnVk);
            this.panel1.Controls.Add(this.btnFacebook);
            this.panel1.Controls.Add(this.cmbCountry);
            this.panel1.Controls.Add(this.lblCountry);
            this.panel1.Controls.Add(this.btnGmail);
            this.panel1.Controls.Add(this.cmbSmsService);
            this.panel1.Controls.Add(this.lblSmsService);
            this.panel1.Controls.Add(this.btnGenerateEn);
            this.panel1.Controls.Add(this.rbFemale);
            this.panel1.Controls.Add(this.rbMale);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.lblBirthDate);
            this.panel1.Controls.Add(this.dtpBirthDate);
            this.panel1.Controls.Add(this.btnYandexEmail);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.tbLastname);
            this.panel1.Controls.Add(this.lblLastname);
            this.panel1.Controls.Add(this.tbFirstName);
            this.panel1.Controls.Add(this.lblFirstName);
            this.panel1.Controls.Add(this.btnMailRuEmail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 178);
            this.panel1.TabIndex = 3;
            // 
            // btnYandexPhone
            // 
            this.btnYandexPhone.Location = new System.Drawing.Point(152, 51);
            this.btnYandexPhone.Margin = new System.Windows.Forms.Padding(2);
            this.btnYandexPhone.Name = "btnYandexPhone";
            this.btnYandexPhone.Size = new System.Drawing.Size(126, 31);
            this.btnYandexPhone.TabIndex = 21;
            this.btnYandexPhone.Text = "yandex.ru - телефон";
            this.btnYandexPhone.UseVisualStyleBackColor = true;
            // 
            // btnMailRuPhone
            // 
            this.btnMailRuPhone.Location = new System.Drawing.Point(152, 8);
            this.btnMailRuPhone.Margin = new System.Windows.Forms.Padding(2);
            this.btnMailRuPhone.Name = "btnMailRuPhone";
            this.btnMailRuPhone.Size = new System.Drawing.Size(126, 31);
            this.btnMailRuPhone.TabIndex = 20;
            this.btnMailRuPhone.Text = "mail.ru - телефон";
            this.btnMailRuPhone.UseVisualStyleBackColor = true;
            // 
            // btnVk
            // 
            this.btnVk.Location = new System.Drawing.Point(152, 133);
            this.btnVk.Margin = new System.Windows.Forms.Padding(2);
            this.btnVk.Name = "btnVk";
            this.btnVk.Size = new System.Drawing.Size(126, 31);
            this.btnVk.TabIndex = 19;
            this.btnVk.Text = "vk.com";
            this.btnVk.UseVisualStyleBackColor = true;
            // 
            // btnFacebook
            // 
            this.btnFacebook.Location = new System.Drawing.Point(22, 133);
            this.btnFacebook.Margin = new System.Windows.Forms.Padding(2);
            this.btnFacebook.Name = "btnFacebook";
            this.btnFacebook.Size = new System.Drawing.Size(126, 31);
            this.btnFacebook.TabIndex = 18;
            this.btnFacebook.Text = "facebook.com";
            this.btnFacebook.UseVisualStyleBackColor = true;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(788, 66);
            this.cmbCountry.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(109, 21);
            this.cmbCountry.TabIndex = 17;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(785, 51);
            this.lblCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(84, 13);
            this.lblCountry.TabIndex = 16;
            this.lblCountry.Text = "Страна номера";
            // 
            // btnGmail
            // 
            this.btnGmail.Location = new System.Drawing.Point(22, 97);
            this.btnGmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnGmail.Name = "btnGmail";
            this.btnGmail.Size = new System.Drawing.Size(126, 31);
            this.btnGmail.TabIndex = 15;
            this.btnGmail.Text = "gmail.com";
            this.btnGmail.UseVisualStyleBackColor = true;
            // 
            // cmbSmsService
            // 
            this.cmbSmsService.FormattingEnabled = true;
            this.cmbSmsService.Location = new System.Drawing.Point(788, 23);
            this.cmbSmsService.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSmsService.Name = "cmbSmsService";
            this.cmbSmsService.Size = new System.Drawing.Size(109, 21);
            this.cmbSmsService.TabIndex = 14;
            // 
            // lblSmsService
            // 
            this.lblSmsService.AutoSize = true;
            this.lblSmsService.Location = new System.Drawing.Point(785, 8);
            this.lblSmsService.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSmsService.Name = "lblSmsService";
            this.lblSmsService.Size = new System.Drawing.Size(67, 13);
            this.lblSmsService.TabIndex = 13;
            this.lblSmsService.Text = "Смс сервис";
            // 
            // btnGenerateEn
            // 
            this.btnGenerateEn.Location = new System.Drawing.Point(612, 93);
            this.btnGenerateEn.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateEn.Name = "btnGenerateEn";
            this.btnGenerateEn.Size = new System.Drawing.Size(167, 21);
            this.btnGenerateEn.TabIndex = 12;
            this.btnGenerateEn.Text = "Сгенерировать данные англ.";
            this.btnGenerateEn.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(693, 67);
            this.rbFemale.Margin = new System.Windows.Forms.Padding(2);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(69, 17);
            this.rbFemale.TabIndex = 11;
            this.rbFemale.Text = "женский";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(624, 68);
            this.rbMale.Margin = new System.Windows.Forms.Padding(2);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(70, 17);
            this.rbMale.TabIndex = 10;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "мужской";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(622, 51);
            this.lblSex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(27, 13);
            this.lblSex.TabIndex = 9;
            this.lblSex.Text = "Пол";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(622, 9);
            this.lblBirthDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(86, 13);
            this.lblBirthDate.TabIndex = 8;
            this.lblBirthDate.Text = "Дата рождения";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Location = new System.Drawing.Point(624, 24);
            this.dtpBirthDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(135, 20);
            this.dtpBirthDate.TabIndex = 7;
            // 
            // btnYandexEmail
            // 
            this.btnYandexEmail.Location = new System.Drawing.Point(22, 51);
            this.btnYandexEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnYandexEmail.Name = "btnYandexEmail";
            this.btnYandexEmail.Size = new System.Drawing.Size(126, 31);
            this.btnYandexEmail.TabIndex = 2;
            this.btnYandexEmail.Text = "yandex.ru - почта";
            this.btnYandexEmail.UseVisualStyleBackColor = true;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(348, 113);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(235, 20);
            this.tbPassword.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(345, 97);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(45, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Пароль";
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(348, 67);
            this.tbLastname.Margin = new System.Windows.Forms.Padding(2);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(235, 20);
            this.tbLastname.TabIndex = 4;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Location = new System.Drawing.Point(345, 51);
            this.lblLastname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(56, 13);
            this.lblLastname.TabIndex = 3;
            this.lblLastname.Text = "Фамилия";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(348, 24);
            this.tbFirstName.Margin = new System.Windows.Forms.Padding(2);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(235, 20);
            this.tbFirstName.TabIndex = 2;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(345, 8);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(29, 13);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "Имя";
            // 
            // btnMailRuEmail
            // 
            this.btnMailRuEmail.Location = new System.Drawing.Point(22, 8);
            this.btnMailRuEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnMailRuEmail.Name = "btnMailRuEmail";
            this.btnMailRuEmail.Size = new System.Drawing.Size(126, 31);
            this.btnMailRuEmail.TabIndex = 0;
            this.btnMailRuEmail.Text = "mail.ru - почта";
            this.btnMailRuEmail.UseVisualStyleBackColor = true;
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
            // btnGenerateRu
            // 
            this.btnGenerateRu.Location = new System.Drawing.Point(612, 118);
            this.btnGenerateRu.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateRu.Name = "btnGenerateRu";
            this.btnGenerateRu.Size = new System.Drawing.Size(167, 21);
            this.btnGenerateRu.TabIndex = 22;
            this.btnGenerateRu.Text = "Сгенерировать данные руск.";
            this.btnGenerateRu.UseVisualStyleBackColor = true;
            // 
            // RegBotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "RegBotControl";
            this.Size = new System.Drawing.Size(958, 544);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVk;
        private System.Windows.Forms.Button btnFacebook;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Button btnGmail;
        private System.Windows.Forms.ComboBox cmbSmsService;
        private System.Windows.Forms.Label lblSmsService;
        private System.Windows.Forms.Button btnGenerateEn;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Button btnYandexEmail;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbLastname;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Button btnMailRuEmail;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel4;
        private ADGV.AdvancedDataGridView dgvItems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnYandexPhone;
        private System.Windows.Forms.Button btnMailRuPhone;
        private System.Windows.Forms.Button btnGenerateRu;
    }
}

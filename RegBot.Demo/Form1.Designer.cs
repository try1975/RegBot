namespace RegBot.Demo
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnMailRu = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnYandex = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGmail = new System.Windows.Forms.Button();
            this.cmbSmsService = new System.Windows.Forms.ComboBox();
            this.lblSmsService = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.advancedDataGridView1 = new ADGV.AdvancedDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMailRu
            // 
            this.btnMailRu.Location = new System.Drawing.Point(33, 12);
            this.btnMailRu.Name = "btnMailRu";
            this.btnMailRu.Size = new System.Drawing.Size(167, 48);
            this.btnMailRu.TabIndex = 0;
            this.btnMailRu.Text = "mail.ru";
            this.btnMailRu.UseVisualStyleBackColor = true;
            this.btnMailRu.Click += new System.EventHandler(this.BtnMailRu_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1111, 400);
            this.textBox1.TabIndex = 1;
            // 
            // btnYandex
            // 
            this.btnYandex.Location = new System.Drawing.Point(33, 79);
            this.btnYandex.Name = "btnYandex";
            this.btnYandex.Size = new System.Drawing.Size(167, 48);
            this.btnYandex.TabIndex = 2;
            this.btnYandex.Text = "yandex.ru";
            this.btnYandex.UseVisualStyleBackColor = true;
            this.btnYandex.Click += new System.EventHandler(this.BtnYandex_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGmail);
            this.panel1.Controls.Add(this.cmbSmsService);
            this.panel1.Controls.Add(this.lblSmsService);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.rbFemale);
            this.panel1.Controls.Add(this.rbMale);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.lblBirthDate);
            this.panel1.Controls.Add(this.dtpBirthDate);
            this.panel1.Controls.Add(this.btnYandex);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.tbLastname);
            this.panel1.Controls.Add(this.lblLastname);
            this.panel1.Controls.Add(this.tbFirstName);
            this.panel1.Controls.Add(this.lblFirstName);
            this.panel1.Controls.Add(this.btnMailRu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 220);
            this.panel1.TabIndex = 3;
            // 
            // btnGmail
            // 
            this.btnGmail.Location = new System.Drawing.Point(33, 150);
            this.btnGmail.Name = "btnGmail";
            this.btnGmail.Size = new System.Drawing.Size(167, 48);
            this.btnGmail.TabIndex = 15;
            this.btnGmail.Text = "gmail.com";
            this.btnGmail.UseVisualStyleBackColor = true;
            this.btnGmail.Click += new System.EventHandler(this.BtnGmail_Click);
            // 
            // cmbSmsService
            // 
            this.cmbSmsService.FormattingEnabled = true;
            this.cmbSmsService.Location = new System.Drawing.Point(934, 35);
            this.cmbSmsService.Name = "cmbSmsService";
            this.cmbSmsService.Size = new System.Drawing.Size(161, 28);
            this.cmbSmsService.TabIndex = 14;
            // 
            // lblSmsService
            // 
            this.lblSmsService.AutoSize = true;
            this.lblSmsService.Location = new System.Drawing.Point(930, 12);
            this.lblSmsService.Name = "lblSmsService";
            this.lblSmsService.Size = new System.Drawing.Size(95, 20);
            this.lblSmsService.TabIndex = 13;
            this.lblSmsService.Text = "Смс сервис";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(689, 171);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(200, 32);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Случайные данные";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(792, 103);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(97, 24);
            this.rbFemale.TabIndex = 11;
            this.rbFemale.Text = "женский";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(689, 104);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(97, 24);
            this.rbMale.TabIndex = 10;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "мужской";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(685, 79);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(40, 20);
            this.lblSex.TabIndex = 9;
            this.lblSex.Text = "Пол";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(685, 14);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(128, 20);
            this.lblBirthDate.TabIndex = 8;
            this.lblBirthDate.Text = "Дата рождения";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Location = new System.Drawing.Point(689, 37);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 26);
            this.dtpBirthDate.TabIndex = 7;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(274, 174);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(351, 26);
            this.tbPassword.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(270, 150);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 20);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Пароль";
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(274, 103);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(351, 26);
            this.tbLastname.TabIndex = 4;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Location = new System.Drawing.Point(270, 79);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(81, 20);
            this.lblLastname.TabIndex = 3;
            this.lblLastname.Text = "Фамилия";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(274, 37);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(351, 26);
            this.tbFirstName.TabIndex = 2;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(270, 13);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(40, 20);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "Имя";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 223);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1111, 400);
            this.panel3.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1125, 659);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1117, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Регистрация";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1117, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "История";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.advancedDataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1111, 590);
            this.panel4.TabIndex = 1;
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.AllowUserToAddRows = false;
            this.advancedDataGridView1.AllowUserToDeleteRows = false;
            this.advancedDataGridView1.AllowUserToOrderColumns = true;
            this.advancedDataGridView1.AutoGenerateContextFilters = true;
            this.advancedDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.DateWithTime = true;
            this.advancedDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.ReadOnly = true;
            this.advancedDataGridView1.RowTemplate.Height = 28;
            this.advancedDataGridView1.Size = new System.Drawing.Size(1111, 590);
            this.advancedDataGridView1.TabIndex = 1;
            this.advancedDataGridView1.TimeFilter = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1111, 30);
            this.panel2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 659);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "RegBot.Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMailRu;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnYandex;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbLastname;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cmbSmsService;
        private System.Windows.Forms.Label lblSmsService;
        private System.Windows.Forms.Button btnGmail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private ADGV.AdvancedDataGridView advancedDataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}


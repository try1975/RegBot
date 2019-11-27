namespace ScenarioApp.Controls
{
    partial class ScenarioControl
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
            this.btnEmailCheckControl = new System.Windows.Forms.Button();
            this.btnRegBotControl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCollectVkWallControl = new System.Windows.Forms.Button();
            this.btnCheckVkCredential = new System.Windows.Forms.Button();
            this.btnPostVkControl = new System.Windows.Forms.Button();
            this.btnCheckVkAccount = new System.Windows.Forms.Button();
            this.btnWhoisControl = new System.Windows.Forms.Button();
            this.btnYandexSearchControl = new System.Windows.Forms.Button();
            this.btnGoogleSearchControl = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlWorkArea = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEmailCheckControl);
            this.panel1.Controls.Add(this.btnRegBotControl);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnWhoisControl);
            this.panel1.Controls.Add(this.btnYandexSearchControl);
            this.panel1.Controls.Add(this.btnGoogleSearchControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 523);
            this.panel1.TabIndex = 0;
            // 
            // btnEmailCheckControl
            // 
            this.btnEmailCheckControl.Location = new System.Drawing.Point(17, 316);
            this.btnEmailCheckControl.Name = "btnEmailCheckControl";
            this.btnEmailCheckControl.Size = new System.Drawing.Size(196, 24);
            this.btnEmailCheckControl.TabIndex = 9;
            this.btnEmailCheckControl.Text = "Валидность ящиков";
            this.btnEmailCheckControl.UseVisualStyleBackColor = true;
            // 
            // btnRegBotControl
            // 
            this.btnRegBotControl.Location = new System.Drawing.Point(17, 27);
            this.btnRegBotControl.Name = "btnRegBotControl";
            this.btnRegBotControl.Size = new System.Drawing.Size(196, 24);
            this.btnRegBotControl.TabIndex = 8;
            this.btnRegBotControl.Text = "Регистрация";
            this.btnRegBotControl.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCollectVkWallControl);
            this.groupBox1.Controls.Add(this.btnCheckVkCredential);
            this.groupBox1.Controls.Add(this.btnPostVkControl);
            this.groupBox1.Controls.Add(this.btnCheckVkAccount);
            this.groupBox1.Location = new System.Drawing.Point(17, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 147);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ВКонтакте";
            // 
            // btnCollectVkWallControl
            // 
            this.btnCollectVkWallControl.Location = new System.Drawing.Point(29, 19);
            this.btnCollectVkWallControl.Name = "btnCollectVkWallControl";
            this.btnCollectVkWallControl.Size = new System.Drawing.Size(138, 24);
            this.btnCollectVkWallControl.TabIndex = 0;
            this.btnCollectVkWallControl.Text = "стена";
            this.btnCollectVkWallControl.UseVisualStyleBackColor = true;
            // 
            // btnCheckVkCredential
            // 
            this.btnCheckVkCredential.Location = new System.Drawing.Point(29, 109);
            this.btnCheckVkCredential.Name = "btnCheckVkCredential";
            this.btnCheckVkCredential.Size = new System.Drawing.Size(138, 24);
            this.btnCheckVkCredential.TabIndex = 6;
            this.btnCheckVkCredential.Text = "проверка логин/пароль";
            this.btnCheckVkCredential.UseVisualStyleBackColor = true;
            // 
            // btnPostVkControl
            // 
            this.btnPostVkControl.Location = new System.Drawing.Point(29, 49);
            this.btnPostVkControl.Name = "btnPostVkControl";
            this.btnPostVkControl.Size = new System.Drawing.Size(138, 24);
            this.btnPostVkControl.TabIndex = 4;
            this.btnPostVkControl.Text = "сообщение группе";
            this.btnPostVkControl.UseVisualStyleBackColor = true;
            // 
            // btnCheckVkAccount
            // 
            this.btnCheckVkAccount.Location = new System.Drawing.Point(29, 79);
            this.btnCheckVkAccount.Name = "btnCheckVkAccount";
            this.btnCheckVkAccount.Size = new System.Drawing.Size(138, 24);
            this.btnCheckVkAccount.TabIndex = 5;
            this.btnCheckVkAccount.Text = "проверка аккаунта";
            this.btnCheckVkAccount.UseVisualStyleBackColor = true;
            // 
            // btnWhoisControl
            // 
            this.btnWhoisControl.Location = new System.Drawing.Point(17, 117);
            this.btnWhoisControl.Name = "btnWhoisControl";
            this.btnWhoisControl.Size = new System.Drawing.Size(196, 24);
            this.btnWhoisControl.TabIndex = 3;
            this.btnWhoisControl.Text = "Проверка домена";
            this.btnWhoisControl.UseVisualStyleBackColor = true;
            // 
            // btnYandexSearchControl
            // 
            this.btnYandexSearchControl.Location = new System.Drawing.Point(17, 87);
            this.btnYandexSearchControl.Name = "btnYandexSearchControl";
            this.btnYandexSearchControl.Size = new System.Drawing.Size(196, 24);
            this.btnYandexSearchControl.TabIndex = 2;
            this.btnYandexSearchControl.Text = "Поиск Яндекс";
            this.btnYandexSearchControl.UseVisualStyleBackColor = true;
            // 
            // btnGoogleSearchControl
            // 
            this.btnGoogleSearchControl.Location = new System.Drawing.Point(17, 57);
            this.btnGoogleSearchControl.Name = "btnGoogleSearchControl";
            this.btnGoogleSearchControl.Size = new System.Drawing.Size(196, 24);
            this.btnGoogleSearchControl.TabIndex = 1;
            this.btnGoogleSearchControl.Text = "Поиск Гугл";
            this.btnGoogleSearchControl.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(234, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 523);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // pnlWorkArea
            // 
            this.pnlWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkArea.Location = new System.Drawing.Point(237, 0);
            this.pnlWorkArea.Name = "pnlWorkArea";
            this.pnlWorkArea.Size = new System.Drawing.Size(790, 523);
            this.pnlWorkArea.TabIndex = 2;
            // 
            // ScenarioControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlWorkArea);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "ScenarioControl";
            this.Size = new System.Drawing.Size(1027, 523);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCollectVkWallControl;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlWorkArea;
        private System.Windows.Forms.Button btnGoogleSearchControl;
        private System.Windows.Forms.Button btnYandexSearchControl;
        private System.Windows.Forms.Button btnWhoisControl;
        private System.Windows.Forms.Button btnPostVkControl;
        private System.Windows.Forms.Button btnCheckVkAccount;
        private System.Windows.Forms.Button btnCheckVkCredential;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRegBotControl;
        private System.Windows.Forms.Button btnEmailCheckControl;
    }
}

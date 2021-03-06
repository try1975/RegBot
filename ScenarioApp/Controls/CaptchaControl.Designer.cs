﻿namespace ScenarioApp.Controls
{
    partial class CaptchaControl
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.tbProgress = new System.Windows.Forms.TextBox();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel12 = new System.Windows.Forms.Panel();
            this.gbImgCaptcha = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbAcImgAnswer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImgLoad = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbUrl = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAcRefreshBalance = new System.Windows.Forms.Button();
            this.lblAcBalanceValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnRecaptcha = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.panel12.SuspendLayout();
            this.gbImgCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnExecute);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 473);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(736, 47);
            this.pnlButtons.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(596, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(127, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(19, 12);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Выполнить";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Visible = false;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.tbProgress);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProgress.Location = new System.Drawing.Point(465, 0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(271, 473);
            this.pnlProgress.TabIndex = 6;
            // 
            // tbProgress
            // 
            this.tbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProgress.Location = new System.Drawing.Point(0, 0);
            this.tbProgress.Multiline = true;
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbProgress.Size = new System.Drawing.Size(271, 473);
            this.tbProgress.TabIndex = 0;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(462, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 473);
            this.splitter4.TabIndex = 9;
            this.splitter4.TabStop = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.gbImgCaptcha);
            this.panel12.Controls.Add(this.panel3);
            this.panel12.Controls.Add(this.panel2);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(462, 473);
            this.panel12.TabIndex = 10;
            // 
            // gbImgCaptcha
            // 
            this.gbImgCaptcha.Controls.Add(this.pictureBox1);
            this.gbImgCaptcha.Controls.Add(this.panel1);
            this.gbImgCaptcha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImgCaptcha.Location = new System.Drawing.Point(0, 39);
            this.gbImgCaptcha.Name = "gbImgCaptcha";
            this.gbImgCaptcha.Size = new System.Drawing.Size(462, 281);
            this.gbImgCaptcha.TabIndex = 2;
            this.gbImgCaptcha.TabStop = false;
            this.gbImgCaptcha.Text = "Капча картинкой";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(456, 227);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbAcImgAnswer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnImgLoad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 35);
            this.panel1.TabIndex = 0;
            // 
            // tbAcImgAnswer
            // 
            this.tbAcImgAnswer.Location = new System.Drawing.Point(66, 8);
            this.tbAcImgAnswer.Name = "tbAcImgAnswer";
            this.tbAcImgAnswer.Size = new System.Drawing.Size(213, 20);
            this.tbAcImgAnswer.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Решение";
            // 
            // btnImgLoad
            // 
            this.btnImgLoad.Location = new System.Drawing.Point(310, 6);
            this.btnImgLoad.Name = "btnImgLoad";
            this.btnImgLoad.Size = new System.Drawing.Size(138, 23);
            this.btnImgLoad.TabIndex = 0;
            this.btnImgLoad.Text = "Загрузить и решить";
            this.btnImgLoad.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRecaptcha);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cbUrl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 320);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 153);
            this.panel3.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Url";
            // 
            // cbUrl
            // 
            this.cbUrl.FormattingEnabled = true;
            this.cbUrl.Items.AddRange(new object[] {
            "https://antcpt.com/eng/information/demo-form/image.html",
            "https://captcha.com/captcha-examples.html",
            "https://www.google.com/recaptcha/api2/demo",
            "https://antcpt.com/eng/information/demo-form/_recaptcha-2.html"});
            this.cbUrl.Location = new System.Drawing.Point(14, 26);
            this.cbUrl.Name = "cbUrl";
            this.cbUrl.Size = new System.Drawing.Size(437, 21);
            this.cbUrl.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAcRefreshBalance);
            this.panel2.Controls.Add(this.lblAcBalanceValue);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 39);
            this.panel2.TabIndex = 6;
            // 
            // btnAcRefreshBalance
            // 
            this.btnAcRefreshBalance.Location = new System.Drawing.Point(313, 3);
            this.btnAcRefreshBalance.Name = "btnAcRefreshBalance";
            this.btnAcRefreshBalance.Size = new System.Drawing.Size(138, 23);
            this.btnAcRefreshBalance.TabIndex = 8;
            this.btnAcRefreshBalance.Text = "Обновить баланс";
            this.btnAcRefreshBalance.UseVisualStyleBackColor = true;
            // 
            // lblAcBalanceValue
            // 
            this.lblAcBalanceValue.AutoSize = true;
            this.lblAcBalanceValue.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblAcBalanceValue.Location = new System.Drawing.Point(160, 8);
            this.lblAcBalanceValue.Name = "lblAcBalanceValue";
            this.lblAcBalanceValue.Size = new System.Drawing.Size(13, 13);
            this.lblAcBalanceValue.TabIndex = 7;
            this.lblAcBalanceValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Баланс anti-captcha.com :";
            // 
            // btnRecaptcha
            // 
            this.btnRecaptcha.Location = new System.Drawing.Point(313, 53);
            this.btnRecaptcha.Name = "btnRecaptcha";
            this.btnRecaptcha.Size = new System.Drawing.Size(138, 23);
            this.btnRecaptcha.TabIndex = 4;
            this.btnRecaptcha.Text = "Загрузить и решить";
            this.btnRecaptcha.UseVisualStyleBackColor = true;
            // 
            // CaptchaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.pnlButtons);
            this.Name = "CaptchaControl";
            this.Size = new System.Drawing.Size(736, 520);
            this.pnlButtons.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.gbImgCaptcha.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.TextBox tbProgress;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.GroupBox gbImgCaptcha;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImgLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUrl;
        private System.Windows.Forms.TextBox tbAcImgAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAcRefreshBalance;
        private System.Windows.Forms.Label lblAcBalanceValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRecaptcha;
    }
}

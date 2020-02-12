namespace ScenarioApp.Controls
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
            this.cbUrl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButtons.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.panel12.SuspendLayout();
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
            this.panel12.Controls.Add(this.label1);
            this.panel12.Controls.Add(this.cbUrl);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(462, 473);
            this.panel12.TabIndex = 10;
            // 
            // cbUrl
            // 
            this.cbUrl.FormattingEnabled = true;
            this.cbUrl.Items.AddRange(new object[] {
            "https://antcpt.com/eng/information/demo-form/image.html",
            "https://captcha.com/captcha-examples.html"});
            this.cbUrl.Location = new System.Drawing.Point(19, 46);
            this.cbUrl.Name = "cbUrl";
            this.cbUrl.Size = new System.Drawing.Size(437, 21);
            this.cbUrl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Url";
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
            this.panel12.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUrl;
    }
}

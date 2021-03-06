﻿namespace ScenarioApp.Controls
{
    partial class WhoisControl
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
            this.label8 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
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
            this.pnlButtons.Location = new System.Drawing.Point(0, 465);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(784, 47);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(644, 12);
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
            this.pnlProgress.Location = new System.Drawing.Point(414, 0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(370, 465);
            this.pnlProgress.TabIndex = 5;
            // 
            // tbProgress
            // 
            this.tbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProgress.Location = new System.Drawing.Point(0, 0);
            this.tbProgress.Multiline = true;
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbProgress.Size = new System.Drawing.Size(370, 465);
            this.tbProgress.TabIndex = 0;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(411, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 465);
            this.splitter4.TabIndex = 8;
            this.splitter4.TabStop = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label8);
            this.panel12.Controls.Add(this.textBox8);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(411, 465);
            this.panel12.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Домен";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(48, 43);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(190, 244);
            this.textBox8.TabIndex = 0;
            this.textBox8.Text = "meduza.io\r\nhabr.com\r\naddd.fro";
            // 
            // WhoisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.pnlButtons);
            this.Name = "WhoisControl";
            this.Size = new System.Drawing.Size(784, 512);
            this.pnlButtons.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.TextBox tbProgress;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button btnSave;
    }
}

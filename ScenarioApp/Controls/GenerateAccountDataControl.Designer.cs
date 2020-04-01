namespace ScenarioApp.Controls
{
    partial class GenerateAccountDataControl
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerateEn = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel6 = new System.Windows.Forms.Panel();
            this.udAccountCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerateRu = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAccountCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 547);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(672, 47);
            this.panel4.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(528, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(127, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnGenerateEn
            // 
            this.btnGenerateEn.Location = new System.Drawing.Point(19, 90);
            this.btnGenerateEn.Name = "btnGenerateEn";
            this.btnGenerateEn.Size = new System.Drawing.Size(215, 23);
            this.btnGenerateEn.TabIndex = 1;
            this.btnGenerateEn.Text = "Сгенерировать аккаунты англ.";
            this.btnGenerateEn.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(302, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(370, 547);
            this.panel5.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(370, 547);
            this.textBox3.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(299, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 547);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGenerateRu);
            this.panel6.Controls.Add(this.udAccountCount);
            this.panel6.Controls.Add(this.btnGenerateEn);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(299, 547);
            this.panel6.TabIndex = 8;
            // 
            // udAccountCount
            // 
            this.udAccountCount.Location = new System.Drawing.Point(19, 41);
            this.udAccountCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udAccountCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udAccountCount.Name = "udAccountCount";
            this.udAccountCount.Size = new System.Drawing.Size(53, 20);
            this.udAccountCount.TabIndex = 3;
            this.udAccountCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество аккаунтов";
            // 
            // btnGenerateRu
            // 
            this.btnGenerateRu.Location = new System.Drawing.Point(19, 119);
            this.btnGenerateRu.Name = "btnGenerateRu";
            this.btnGenerateRu.Size = new System.Drawing.Size(215, 23);
            this.btnGenerateRu.TabIndex = 4;
            this.btnGenerateRu.Text = "Сгенерировать аккаунты руск.";
            this.btnGenerateRu.UseVisualStyleBackColor = true;
            // 
            // GenerateAccountDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Name = "GenerateAccountDataControl";
            this.Size = new System.Drawing.Size(672, 594);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAccountCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerateEn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown udAccountCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerateRu;
    }
}

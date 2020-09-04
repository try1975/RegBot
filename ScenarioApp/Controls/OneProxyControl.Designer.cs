namespace ScenarioApp.Controls
{
    partial class OneProxyControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.btnCheckProxy = new System.Windows.Forms.Button();
            this.lbCheckProxy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Прокси";
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(84, 16);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(219, 20);
            this.tbProxy.TabIndex = 1;
            // 
            // btnCheckProxy
            // 
            this.btnCheckProxy.Location = new System.Drawing.Point(361, 14);
            this.btnCheckProxy.Name = "btnCheckProxy";
            this.btnCheckProxy.Size = new System.Drawing.Size(109, 23);
            this.btnCheckProxy.TabIndex = 2;
            this.btnCheckProxy.Text = "Проверить прокси";
            this.btnCheckProxy.UseVisualStyleBackColor = true;
            // 
            // lbCheckProxy
            // 
            this.lbCheckProxy.AutoSize = true;
            this.lbCheckProxy.Location = new System.Drawing.Point(84, 45);
            this.lbCheckProxy.Name = "lbCheckProxy";
            this.lbCheckProxy.Size = new System.Drawing.Size(155, 13);
            this.lbCheckProxy.TabIndex = 3;
            this.lbCheckProxy.Text = "Результат проверки прокси: ";
            // 
            // OneProxyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbCheckProxy);
            this.Controls.Add(this.btnCheckProxy);
            this.Controls.Add(this.tbProxy);
            this.Controls.Add(this.label1);
            this.Name = "OneProxyControl";
            this.Size = new System.Drawing.Size(499, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.Button btnCheckProxy;
        private System.Windows.Forms.Label lbCheckProxy;
    }
}

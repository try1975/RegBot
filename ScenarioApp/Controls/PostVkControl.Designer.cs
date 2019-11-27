namespace ScenarioApp.Controls
{
    partial class PostVkControl
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
            this.panel13 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.button5);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel13.Location = new System.Drawing.Point(0, 396);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(701, 47);
            this.panel13.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(19, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Выполнить";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.textBox9);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(385, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(316, 396);
            this.panel14.TabIndex = 6;
            // 
            // textBox9
            // 
            this.textBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox9.Location = new System.Drawing.Point(0, 0);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox9.Size = new System.Drawing.Size(316, 396);
            this.textBox9.TabIndex = 0;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter5.Location = new System.Drawing.Point(382, 0);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(3, 396);
            this.splitter5.TabIndex = 9;
            this.splitter5.TabStop = false;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label7);
            this.panel15.Controls.Add(this.textBox10);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(382, 396);
            this.panel15.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(242, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Сообщение для https://vk.com/club188446341 ";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(48, 43);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(299, 95);
            this.textBox10.TabIndex = 0;
            this.textBox10.Text = "Кролики – это не только ценный мех, но и 3-4 килограмма диетического, легкоусвояе" +
    "мого мяса";
            // 
            // PostVkControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.splitter5);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel13);
            this.Name = "PostVkControl";
            this.Size = new System.Drawing.Size(701, 443);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox10;
    }
}

namespace ScenarioApp.Controls
{
    partial class SelectPersonControl
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
            this.cmbVkAccounts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFbAccounts = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMailruAccounts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYandexAccounts = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGmailAccounts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbVkAccounts
            // 
            this.cmbVkAccounts.FormattingEnabled = true;
            this.cmbVkAccounts.Location = new System.Drawing.Point(69, 88);
            this.cmbVkAccounts.Name = "cmbVkAccounts";
            this.cmbVkAccounts.Size = new System.Drawing.Size(270, 21);
            this.cmbVkAccounts.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ВКонтакте";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фейсбук";
            // 
            // cmbFbAccounts
            // 
            this.cmbFbAccounts.FormattingEnabled = true;
            this.cmbFbAccounts.Location = new System.Drawing.Point(69, 142);
            this.cmbFbAccounts.Name = "cmbFbAccounts";
            this.cmbFbAccounts.Size = new System.Drawing.Size(270, 21);
            this.cmbFbAccounts.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(69, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(137, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Мэйлру";
            // 
            // cmbMailruAccounts
            // 
            this.cmbMailruAccounts.FormattingEnabled = true;
            this.cmbMailruAccounts.Location = new System.Drawing.Point(69, 196);
            this.cmbMailruAccounts.Name = "cmbMailruAccounts";
            this.cmbMailruAccounts.Size = new System.Drawing.Size(270, 21);
            this.cmbMailruAccounts.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Яндекс";
            // 
            // cmbYandexAccounts
            // 
            this.cmbYandexAccounts.FormattingEnabled = true;
            this.cmbYandexAccounts.Location = new System.Drawing.Point(69, 236);
            this.cmbYandexAccounts.Name = "cmbYandexAccounts";
            this.cmbYandexAccounts.Size = new System.Drawing.Size(270, 21);
            this.cmbYandexAccounts.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Жмэйл";
            // 
            // cmbGmailAccounts
            // 
            this.cmbGmailAccounts.FormattingEnabled = true;
            this.cmbGmailAccounts.Location = new System.Drawing.Point(69, 276);
            this.cmbGmailAccounts.Name = "cmbGmailAccounts";
            this.cmbGmailAccounts.Size = new System.Drawing.Size(270, 21);
            this.cmbGmailAccounts.TabIndex = 9;
            // 
            // SelectPersonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbGmailAccounts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbYandexAccounts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMailruAccounts);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFbAccounts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbVkAccounts);
            this.Name = "SelectPersonControl";
            this.Size = new System.Drawing.Size(898, 584);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVkAccounts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFbAccounts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMailruAccounts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYandexAccounts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGmailAccounts;
    }
}

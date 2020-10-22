namespace ScenarioApp.Controls
{
    partial class BrowserProfilesControl
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteBrowserProfile = new System.Windows.Forms.Button();
            this.btnEditBrowserProfile = new System.Windows.Forms.Button();
            this.btnBrowserProfileStart = new System.Windows.Forms.Button();
            this.btnNewBrowserProfile = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1181, 335);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1181, 335);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnDeleteBrowserProfile);
            this.panel2.Controls.Add(this.btnEditBrowserProfile);
            this.panel2.Controls.Add(this.btnBrowserProfileStart);
            this.panel2.Controls.Add(this.btnNewBrowserProfile);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1181, 100);
            this.panel2.TabIndex = 3;
            // 
            // btnDeleteBrowserProfile
            // 
            this.btnDeleteBrowserProfile.Location = new System.Drawing.Point(623, 32);
            this.btnDeleteBrowserProfile.Name = "btnDeleteBrowserProfile";
            this.btnDeleteBrowserProfile.Size = new System.Drawing.Size(171, 44);
            this.btnDeleteBrowserProfile.TabIndex = 4;
            this.btnDeleteBrowserProfile.Text = "Удалить профиль";
            this.btnDeleteBrowserProfile.UseVisualStyleBackColor = true;
            // 
            // btnEditBrowserProfile
            // 
            this.btnEditBrowserProfile.Location = new System.Drawing.Point(446, 32);
            this.btnEditBrowserProfile.Name = "btnEditBrowserProfile";
            this.btnEditBrowserProfile.Size = new System.Drawing.Size(171, 44);
            this.btnEditBrowserProfile.TabIndex = 3;
            this.btnEditBrowserProfile.Text = "Изменить профиль";
            this.btnEditBrowserProfile.UseVisualStyleBackColor = true;
            // 
            // btnBrowserProfileStart
            // 
            this.btnBrowserProfileStart.Location = new System.Drawing.Point(800, 32);
            this.btnBrowserProfileStart.Name = "btnBrowserProfileStart";
            this.btnBrowserProfileStart.Size = new System.Drawing.Size(165, 44);
            this.btnBrowserProfileStart.TabIndex = 2;
            this.btnBrowserProfileStart.Text = "Старт профиля";
            this.btnBrowserProfileStart.UseVisualStyleBackColor = true;
            // 
            // btnNewBrowserProfile
            // 
            this.btnNewBrowserProfile.Location = new System.Drawing.Point(274, 32);
            this.btnNewBrowserProfile.Name = "btnNewBrowserProfile";
            this.btnNewBrowserProfile.Size = new System.Drawing.Size(166, 44);
            this.btnNewBrowserProfile.TabIndex = 1;
            this.btnNewBrowserProfile.Text = "Новый профиль";
            this.btnNewBrowserProfile.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(971, 32);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(165, 44);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // BrowserProfilesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "BrowserProfilesControl";
            this.Size = new System.Drawing.Size(1181, 435);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNewBrowserProfile;
        private System.Windows.Forms.Button btnBrowserProfileStart;
        private System.Windows.Forms.Button btnEditBrowserProfile;
        private System.Windows.Forms.Button btnDeleteBrowserProfile;
        private System.Windows.Forms.Button btnRefresh;
    }
}

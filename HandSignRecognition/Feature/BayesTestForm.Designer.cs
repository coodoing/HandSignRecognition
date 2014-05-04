namespace HandSignRecognition.Feature
{
    partial class BayesTestForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataShowLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bayesDataGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cleanButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bayesDataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataShowLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 47);
            this.panel1.TabIndex = 0;
            // 
            // dataShowLabel
            // 
            this.dataShowLabel.AutoSize = true;
            this.dataShowLabel.Location = new System.Drawing.Point(201, 18);
            this.dataShowLabel.Name = "dataShowLabel";
            this.dataShowLabel.Size = new System.Drawing.Size(0, 12);
            this.dataShowLabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bayes测试结果显示：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bayesDataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 383);
            this.panel2.TabIndex = 1;
            // 
            // bayesDataGridView
            // 
            this.bayesDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bayesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bayesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bayesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.bayesDataGridView.Name = "bayesDataGridView";
            this.bayesDataGridView.RowTemplate.Height = 23;
            this.bayesDataGridView.Size = new System.Drawing.Size(667, 383);
            this.bayesDataGridView.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cleanButton);
            this.panel3.Controls.Add(this.testButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 366);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(667, 64);
            this.panel3.TabIndex = 2;
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(370, 11);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(92, 40);
            this.cleanButton.TabIndex = 1;
            this.cleanButton.Text = "清空数据";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(171, 11);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(107, 40);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "开始测试";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // BayesTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 430);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "BayesTestForm";
            this.Text = "Bayes测试";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bayesDataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dataShowLabel;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.DataGridView bayesDataGridView;
    }
}
namespace HandSignRecognition.Feature
{
    partial class OutlineFeatureForm
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
            this.featureDataGridView = new System.Windows.Forms.DataGridView();
            this.featureExtractButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.featureDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // featureDataGridView
            // 
            this.featureDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.featureDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.featureDataGridView.Location = new System.Drawing.Point(12, 12);
            this.featureDataGridView.Name = "featureDataGridView";
            this.featureDataGridView.RowTemplate.Height = 23;
            this.featureDataGridView.Size = new System.Drawing.Size(771, 404);
            this.featureDataGridView.TabIndex = 0;
            // 
            // featureExtractButton
            // 
            this.featureExtractButton.Location = new System.Drawing.Point(164, 436);
            this.featureExtractButton.Name = "featureExtractButton";
            this.featureExtractButton.Size = new System.Drawing.Size(105, 39);
            this.featureExtractButton.TabIndex = 1;
            this.featureExtractButton.Text = "提取特征";
            this.featureExtractButton.UseVisualStyleBackColor = true;
            this.featureExtractButton.Click += new System.EventHandler(this.featureExtractButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(467, 436);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(101, 39);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "退出";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // OutlineFeatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 517);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.featureExtractButton);
            this.Controls.Add(this.featureDataGridView);
            this.Name = "OutlineFeatureForm";
            this.Text = "轮廓特征ET1|DT12提取";
            this.Load += new System.EventHandler(this.OutlineFeatureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.featureDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView featureDataGridView;
        private System.Windows.Forms.Button featureExtractButton;
        private System.Windows.Forms.Button exitButton;
    }
}
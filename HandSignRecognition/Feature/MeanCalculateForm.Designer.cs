namespace HandSignRecognition.Feature
{
    partial class MeanCalculateForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdButton = new System.Windows.Forms.Button();
            this.mvButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.classChooseComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdButton);
            this.groupBox1.Controls.Add(this.mvButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "u和∑计算";
            // 
            // rdButton
            // 
            this.rdButton.Location = new System.Drawing.Point(416, 80);
            this.rdButton.Name = "rdButton";
            this.rdButton.Size = new System.Drawing.Size(75, 23);
            this.rdButton.TabIndex = 3;
            this.rdButton.Text = "LDA降维处理";
            this.rdButton.UseVisualStyleBackColor = true;
            this.rdButton.Click += new System.EventHandler(this.rdButton_Click);
            // 
            // mvButton
            // 
            this.mvButton.Location = new System.Drawing.Point(416, 34);
            this.mvButton.Name = "mvButton";
            this.mvButton.Size = new System.Drawing.Size(75, 23);
            this.mvButton.TabIndex = 2;
            this.mvButton.Text = "μ、∑计算";
            this.mvButton.UseVisualStyleBackColor = true;
            this.mvButton.Click += new System.EventHandler(this.mvButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "2、降维后训练样本各类的均值μ和协方差矩阵∑：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "1、计算训练样本各类的原始均值μ和协方差矩阵∑：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resultTextBox);
            this.groupBox2.Controls.Add(this.classChooseComboBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(555, 317);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "计算结果";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(0, 46);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(555, 271);
            this.resultTextBox.TabIndex = 2;
            // 
            // classChooseComboBox
            // 
            this.classChooseComboBox.FormattingEnabled = true;
            this.classChooseComboBox.Location = new System.Drawing.Point(337, 20);
            this.classChooseComboBox.Name = "classChooseComboBox";
            this.classChooseComboBox.Size = new System.Drawing.Size(154, 20);
            this.classChooseComboBox.TabIndex = 1;
            this.classChooseComboBox.SelectedIndexChanged += new System.EventHandler(this.classChooseComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择样本类进行查询";
            // 
            // MeanCalculateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 442);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MeanCalculateForm";
            this.Text = "均值u和协方差∑计算";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button rdButton;
        private System.Windows.Forms.Button mvButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox classChooseComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox resultTextBox;
    }
}
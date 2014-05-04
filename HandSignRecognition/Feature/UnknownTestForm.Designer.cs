namespace HandSignRecognition.Feature
{
    partial class UnknownTestForm
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
            this.rbnearest = new System.Windows.Forms.RadioButton();
            this.rbKn = new System.Windows.Forms.RadioButton();
            this.rbBayes = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filepathText = new System.Windows.Forms.TextBox();
            this.selectSampleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblunknownclassify = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.classifyButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.lblerrorinfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnearest);
            this.groupBox1.Controls.Add(this.rbKn);
            this.groupBox1.Controls.Add(this.rbBayes);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "为未知样本选择分类方法";
            // 
            // rbnearest
            // 
            this.rbnearest.AutoSize = true;
            this.rbnearest.Location = new System.Drawing.Point(402, 38);
            this.rbnearest.Name = "rbnearest";
            this.rbnearest.Size = new System.Drawing.Size(71, 16);
            this.rbnearest.TabIndex = 2;
            this.rbnearest.TabStop = true;
            this.rbnearest.Text = "最近邻法";
            this.rbnearest.UseVisualStyleBackColor = true;
            // 
            // rbKn
            // 
            this.rbKn.AutoSize = true;
            this.rbKn.Location = new System.Drawing.Point(256, 38);
            this.rbKn.Name = "rbKn";
            this.rbKn.Size = new System.Drawing.Size(71, 16);
            this.rbKn.TabIndex = 1;
            this.rbKn.TabStop = true;
            this.rbKn.Text = "Kn近邻法";
            this.rbKn.UseVisualStyleBackColor = true;
            // 
            // rbBayes
            // 
            this.rbBayes.AutoSize = true;
            this.rbBayes.Checked = true;
            this.rbBayes.Location = new System.Drawing.Point(94, 38);
            this.rbBayes.Name = "rbBayes";
            this.rbBayes.Size = new System.Drawing.Size(77, 16);
            this.rbBayes.TabIndex = 0;
            this.rbBayes.TabStop = true;
            this.rbBayes.Text = "Bayes分类";
            this.rbBayes.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.filepathText);
            this.groupBox2.Controls.Add(this.selectSampleButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 332);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择未知样本";
            // 
            // filepathText
            // 
            this.filepathText.Location = new System.Drawing.Point(60, 59);
            this.filepathText.Name = "filepathText";
            this.filepathText.Size = new System.Drawing.Size(370, 21);
            this.filepathText.TabIndex = 2;
            // 
            // selectSampleButton
            // 
            this.selectSampleButton.Location = new System.Drawing.Point(473, 57);
            this.selectSampleButton.Name = "selectSampleButton";
            this.selectSampleButton.Size = new System.Drawing.Size(88, 23);
            this.selectSampleButton.TabIndex = 1;
            this.selectSampleButton.Text = "选择未知样本";
            this.selectSampleButton.UseVisualStyleBackColor = true;
            this.selectSampleButton.Click += new System.EventHandler(this.selectSampleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "指定一个PCX图像文件作为未知样本，并进行分类：(可从左边列表中选中)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblerrorinfo);
            this.groupBox3.Controls.Add(this.lblunknownclassify);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.classifyButton);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(604, 242);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "未知样本分类测试结果";
            // 
            // lblunknownclassify
            // 
            this.lblunknownclassify.AutoSize = true;
            this.lblunknownclassify.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblunknownclassify.ForeColor = System.Drawing.Color.Red;
            this.lblunknownclassify.Location = new System.Drawing.Point(468, 106);
            this.lblunknownclassify.Name = "lblunknownclassify";
            this.lblunknownclassify.Size = new System.Drawing.Size(51, 27);
            this.lblunknownclassify.TabIndex = 3;
            this.lblunknownclassify.Text = "XXX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(241, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "该未知样本所属分类为：";
            // 
            // classifyButton
            // 
            this.classifyButton.Location = new System.Drawing.Point(473, 57);
            this.classifyButton.Name = "classifyButton";
            this.classifyButton.Size = new System.Drawing.Size(75, 23);
            this.classifyButton.TabIndex = 1;
            this.classifyButton.Text = "分类";
            this.classifyButton.UseVisualStyleBackColor = true;
            this.classifyButton.Click += new System.EventHandler(this.classifyButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.previewPictureBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 40);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 193);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "预览PCX图片";
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPictureBox.Location = new System.Drawing.Point(3, 17);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(183, 173);
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;
            // 
            // lblerrorinfo
            // 
            this.lblerrorinfo.AutoSize = true;
            this.lblerrorinfo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblerrorinfo.Location = new System.Drawing.Point(387, 147);
            this.lblerrorinfo.Name = "lblerrorinfo";
            this.lblerrorinfo.Size = new System.Drawing.Size(15, 21);
            this.lblerrorinfo.TabIndex = 4;
            this.lblerrorinfo.Text = "";
            // 
            // UnknownTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 408);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UnknownTestForm";
            this.Text = "未知样本分类";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbnearest;
        private System.Windows.Forms.RadioButton rbKn;
        private System.Windows.Forms.RadioButton rbBayes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filepathText;
        private System.Windows.Forms.Button selectSampleButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Button classifyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblunknownclassify;
        private System.Windows.Forms.Label lblerrorinfo;
    }
}
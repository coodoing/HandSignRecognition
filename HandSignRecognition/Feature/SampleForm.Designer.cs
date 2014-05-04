namespace HandSignRecognition.Feature
{
    partial class SampleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textDirPath = new System.Windows.Forms.TextBox();
            this.browerDirButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectedSampleListBox = new System.Windows.Forms.ListBox();
            this.unSelSampleListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openRadioButton = new System.Windows.Forms.RadioButton();
            this.closeRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "训练样本路径：";
            // 
            // textDirPath
            // 
            this.textDirPath.Location = new System.Drawing.Point(31, 57);
            this.textDirPath.Name = "textDirPath";
            this.textDirPath.Size = new System.Drawing.Size(280, 21);
            this.textDirPath.TabIndex = 1;
            // 
            // browerDirButton
            // 
            this.browerDirButton.Location = new System.Drawing.Point(370, 51);
            this.browerDirButton.Name = "browerDirButton";
            this.browerDirButton.Size = new System.Drawing.Size(91, 31);
            this.browerDirButton.TabIndex = 2;
            this.browerDirButton.Text = "浏览";
            this.browerDirButton.UseVisualStyleBackColor = true;
            this.browerDirButton.Click += new System.EventHandler(this.browerDirButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(129, 375);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(99, 34);
            this.confirmButton.TabIndex = 4;
            this.confirmButton.Text = "确定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(399, 375);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 34);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // selectedSampleListBox
            // 
            this.selectedSampleListBox.FormattingEnabled = true;
            this.selectedSampleListBox.HorizontalScrollbar = true;
            this.selectedSampleListBox.ItemHeight = 12;
            this.selectedSampleListBox.Location = new System.Drawing.Point(31, 164);
            this.selectedSampleListBox.Name = "selectedSampleListBox";
            this.selectedSampleListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedSampleListBox.Size = new System.Drawing.Size(280, 184);
            this.selectedSampleListBox.TabIndex = 6;
            // 
            // unSelSampleListBox
            // 
            this.unSelSampleListBox.FormattingEnabled = true;
            this.unSelSampleListBox.HorizontalScrollbar = true;
            this.unSelSampleListBox.ItemHeight = 12;
            this.unSelSampleListBox.Location = new System.Drawing.Point(408, 164);
            this.unSelSampleListBox.Name = "unSelSampleListBox";
            this.unSelSampleListBox.Size = new System.Drawing.Size(233, 184);
            this.unSelSampleListBox.TabIndex = 7;
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(327, 190);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = ">>";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(327, 258);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 9;
            this.removeButton.Text = "<<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "训练样本：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "测试样本：";
            // 
            // openRadioButton
            // 
            this.openRadioButton.AutoSize = true;
            this.openRadioButton.Checked = true;
            this.openRadioButton.Location = new System.Drawing.Point(44, 98);
            this.openRadioButton.Name = "openRadioButton";
            this.openRadioButton.Size = new System.Drawing.Size(125, 16);
            this.openRadioButton.TabIndex = 12;
            this.openRadioButton.TabStop = true;
            this.openRadioButton.Text = "开测试(open test)";
            this.openRadioButton.UseVisualStyleBackColor = true;
            // 
            // closeRadioButton
            // 
            this.closeRadioButton.AutoSize = true;
            this.closeRadioButton.Location = new System.Drawing.Point(215, 97);
            this.closeRadioButton.Name = "closeRadioButton";
            this.closeRadioButton.Size = new System.Drawing.Size(131, 16);
            this.closeRadioButton.TabIndex = 13;
            this.closeRadioButton.Text = "闭测试(close test)";
            this.closeRadioButton.UseVisualStyleBackColor = true;
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 451);
            this.Controls.Add(this.closeRadioButton);
            this.Controls.Add(this.openRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.unSelSampleListBox);
            this.Controls.Add(this.selectedSampleListBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.browerDirButton);
            this.Controls.Add(this.textDirPath);
            this.Controls.Add(this.label1);
            this.Name = "SampleForm";
            this.Text = "训练样本提取";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textDirPath;
        private System.Windows.Forms.Button browerDirButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListBox selectedSampleListBox;
        private System.Windows.Forms.ListBox unSelSampleListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton openRadioButton;
        private System.Windows.Forms.RadioButton closeRadioButton;
    }
}
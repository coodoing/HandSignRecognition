using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Fireball.Drawing;
using HandSignRecognition.Core;
using HandSignRecognition.Core.Base;
using HandSignRecognition.Feature;
using System.Collections;

namespace HandSignRecognition
{
    /// <summary>
    /// 整个MainForm类可以分解成几个子控件，将相应的事件进行分离操作
    /// </summary>
    public partial class MainForm : Form
    {

        #region Data 数据

        private PCXImage pcxImage;
        private List<PCXImage> pcxlist;
        private ArrayList sampleArray;

        #endregion

        #region Mainform初始化

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.viewToolStripMenuItem.Visible = false;
            this.editToolStripMenuItem.Visible = false;
            this.toolStripStatusLabel3.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.timerStrip.Interval = 1000;
            this.timerStrip.Start();
        }

        #endregion

        #region  ToolStripMenuItem控件所处理的事件
        /*
         * ToolStripMenuItem控件所处理的事件
         */

        #region 文件菜单

        // 关于"打开"菜单的单击事件
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.InitialDirectory = @"C:\";
            //ofdlg.Filter = "Image Files (*.pcx,*.bmp, *.jpg,*png)|*.pcx;*.jpg;*.bmp;*.png";
            ofdlg.Filter = "Image Files (*.pcx)|*.pcx|All Files (*.*)|*.*";
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string filename = ofdlg.FileName;
                //this.pictureBox.Image = Image.FromFile(filename); //C#中不能直接操作PCX图片
                Image imageToSet = null;
                imageToSet = PCXHelper.LoadPCX(filename);
                this.pcxImage = PCXHelper.GetPCXImage(filename);
                pictureBox.Image = imageToSet;
            }
        }

        // 打印
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pcxImage == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                pictureBox.Image = PCXHelper.ScalePCX(pcxImage.OldFilename);
            }
        }

        // 新建
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        // pictureBox paint事件
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        #region PCX查看菜单

        // 关于"查看签名"菜单的单击事件
        private void signToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image imageToSet = null;
            if (pcxImage == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                imageToSet = PCXHelper.ReviewPCX(pcxImage.OldFilename);
                pictureBox.Image = imageToSet;
            }
        }

        // 关于"二值化数据"菜单的单击事件
        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pcxImage == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                //pictureBox.Image = null;
                string binary = PCXHelper.BinaryPCX(pcxImage.OldFilename);
                Graphics g = this.pictureBox.CreateGraphics();
                g.Clear(Color.White);
                SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷
                g.DrawString(binary, new Font("Arial", 3), b1, new PointF(0, 0));
            }
        }

        // 将图片按比例放大
        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pcxImage == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                pictureBox.Image = PCXHelper.ScalePCX(pcxImage.OldFilename);
            }
        }

        #endregion

        #region 特征提取和估值

        // 训练样本的选择
        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleForm sform = new SampleForm();
            sform.ShowDialog();
        }

        // 特征提取
        private void fetureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 首先 如果16个训练样本没进行选择的话
            pcxlist = SelectedPCXHelper.GetSelPCXFromLB();
            if (pcxlist == null || pcxlist.Count == 0)
            {
                MessageBox.Show(this, "还没选择训练样本，请按步骤来", "提示信息", MessageBoxButtons.OK);
            }
            else
            {
                OutlineFeatureForm ofform = new OutlineFeatureForm();
                ofform.ShowDialog();
            }
        }

        //均值u和协方差∑计算
        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sampleArray = FeatureHelper.GetFeaturesList();
            if (sampleArray == null || sampleArray.Count == 1)
            {
                MessageBox.Show(this, "还没有提取特征值，请按步骤来", "提示信息", MessageBoxButtons.OK);
            }
            else
            {
                MeanCalculateForm mcform = new MeanCalculateForm();
                mcform.ShowDialog();
            }

            #region none：测试

            //MeanCalculateForm mcform = new MeanCalculateForm();
            //mcform.ShowDialog();

            #endregion
        }

        #endregion

        #region 分类测试菜单

        // Bayes测试
        private void bayesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果训练样本集列表为空或测试样本集列表为空，不能继续
            if (SelectedPCXHelper.GetSelPCXFromLB().Count == 0 || SelectedPCXHelper.GetUnselPCXList().Count == 0)
            {
                MessageBox.Show(this, "您还未提取样本特征，或者还未设置测试样本集！", "提示信息", MessageBoxButtons.OK);
            }
            else
            {
                BayesTestForm btform = new BayesTestForm();
                btform.ShowDialog();
            }

        }


        // 最近邻测试
        private void nearestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 前提条件判断

            if (SelectedPCXHelper.GetSelPCXFromLB().Count == 0 || SelectedPCXHelper.GetUnselPCXList().Count == 0)
            {
                MessageBox.Show(this, "您还未提取样本特征，或者还未设置测试样本集！", "提示信息", MessageBoxButtons.OK);
            }

            else if (Constant.openchecked == false) //不是开测试的话
            {
                MessageBox.Show(this, "最近邻分类只能使用开测试，请重新选择测试方法！", "提示信息", MessageBoxButtons.OK);
            }

            else
            {
                NearestTestForm ntform = new NearestTestForm();
                // show与showDialog的区别
                ntform.ShowDialog();
            }
            #endregion
        }

        // Kn近邻测试
        private void knToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 前提条件判断

            if (SelectedPCXHelper.GetSelPCXFromLB().Count == 0 || SelectedPCXHelper.GetUnselPCXList().Count == 0)
            {
                MessageBox.Show(this, "您还未提取样本特征，或者还未设置测试样本集！", "提示信息", MessageBoxButtons.OK);
            }

            else if (Constant.openchecked == false) //不是开测试的话
            {
                MessageBox.Show(this, "Kn近邻分类只能使用开测试，请重新选择测试方法！", "提示信息", MessageBoxButtons.OK);
            }

            else
            {
                KNTestForm kntform = new KNTestForm();
                // show与showDialog的区别
                kntform.ShowDialog();
            }
            #endregion


        }

        // 未知样本测试
        private void unknownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPCXHelper.GetSelPCXFromLB().Count == 0 || SelectedPCXHelper.GetUnselPCXList().Count == 0)
            {
                MessageBox.Show(this, "您还未提取样本特征，或者还未设置测试样本集！", "提示信息", MessageBoxButtons.OK);
            }
            else
            {
                UnknownTestForm utform = new UnknownTestForm();
                utform.ShowDialog();
            }
        }


        #endregion

        #region 数据保存菜单
        #endregion

        #region 视图编辑菜单
        #endregion

        #region 帮助菜单

        // 关于"关于"菜单的单击事件        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Copyright(C) by @AirFly \n     20101402097 向智", "关于‘脱机手写签名识别’", MessageBoxButtons.OK);
        }

        // 使用说明菜单
        private void stateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder stateInfo = new StringBuilder();
            stateInfo.Append("                 简单的使用说明：        ");
            stateInfo.Append("\n");
            stateInfo.Append("1：文件菜单：你可以选择你指定目录下的.pcx文件，进行查看。");
            stateInfo.Append("\n");
            stateInfo.Append("2：PCX查看菜单：你可以对图片进行放大处理以及二值化处理。");
            stateInfo.Append("\n");
            stateInfo.Append("3：特征提取|估值菜单中：按顺序1,2,3先后选取训练样本（这里需要选择测试类型：是开测试还是闭测试）。");
            stateInfo.Append("\n");
            stateInfo.Append("特征提取以及均值与方差的计算");
            stateInfo.Append("\n");
            stateInfo.Append("4：分类测试菜单栏：在对样本进行训练学习后：如果是开测试，可以对测试样本分别同Bayes，最近邻，K近邻法进行测试；");
            stateInfo.Append("\n");
            stateInfo.Append("若为闭测试，则只能进行Bayes测试。你还可以任意选取一个未知样本进行Bayes，最近邻，K近邻法测试。在K近邻测试的时候，你还可以查看k值与识别率的函数关系图。");
            stateInfo.Append("\n");
            stateInfo.Append("5：帮助菜单：可以查看使用说明和程序相关点。");
            stateInfo.Append("\n");
            stateInfo.Append("\n");
            stateInfo.Append("\n");
            stateInfo.Append("\n");
            stateInfo.Append("\n");
            stateInfo.Append("附注：开发环境是VS2010，C#。运行前先将DocLib下面的FreeImage.dll放到C:\\Windows\\System32下面");
            stateInfo.Append("\n");
            stateInfo.Append("为Release调试");
            stateInfo.Append("");


            Graphics g = this.pictureBox.CreateGraphics();
            g.Clear(Color.White);
            SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷
            g.DrawString(stateInfo.ToString(), new Font("Arial", 12), b1, new PointF(0, 0));
        }

        #endregion


        #endregion

        #region  其他的一些辅助事件
        /*
         * 其他的一些辅助事件         
         */

        private void timerStrip_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel3.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        #endregion

    }
}

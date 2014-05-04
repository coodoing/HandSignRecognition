using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HandSignRecognition.Core;
using HandSignRecognition.Classifier;
using HandSignRecognition.Core.Base;
using System.Collections;

namespace HandSignRecognition.Feature
{
    public partial class UnknownTestForm : Form
    {
        #region  窗口初始化

        public UnknownTestForm()
        {
            InitializeComponent();
            ControlInit();
        }

        private void ControlInit()
        {
            this.classifyButton.Enabled = false;
            if (FeatureHelper.GetFeaturesList().Count == FeatureHelper.GetTestFeaturesList().Count)
            {
                this.rbKn.Enabled = false;
                this.rbnearest.Enabled = false;
            }
        }

        #endregion

        #region 事件方法

        // 选择未知样本        
        private void selectSampleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.InitialDirectory = @"C:\";
            ofdlg.Filter = "Image Files (*.pcx)|*.pcx|All Files (*.*)|*.*";
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofdlg.FileName;
                this.filepathText.Text = filepath;
                Image imageToSet = null;
                imageToSet = PCXHelper.LoadPCX(filepath);
                previewPictureBox.Image = imageToSet;
                this.classifyButton.Enabled = true;
            }
        }

        // 分类运算
        private void classifyButton_Click(object sender, EventArgs e)
        {
            //前提检验
            if (SelectedPCXHelper.GetSelPCXFromLB().Count == 0 || SelectedPCXHelper.GetUnselPCXList().Count == 0)
            {
                MessageBox.Show(this, "您还未提取样本特征，或者还未设置测试样本集！", "提示信息", MessageBoxButtons.OK);
            }

            else
            {
                string correctRate = null; //正确率
                string myfilepath = filepathText.Text.ToString();

                Features feature;

                #region 这里做一个文件名为unknown.pcx的判断

                string filename = FeatureHelper.GetUnknownName(myfilepath);
                if (filename.ToLower().Equals("unknown"))
                {
                    feature = new Features(myfilepath);
                }
                else
                {
                    int classID = Convert.ToInt32(FeatureHelper.GetUpperFoldername(myfilepath));
                    feature = new Features(myfilepath, classID);
                }

                #endregion

                #region Bayes分类法

                if (rbBayes.Checked)
                {
                    #region 数据初始化

                    CheckInit();
                    double correctCount = 0.0;

                    #endregion

                    IList sampleList = FeatureHelper.GetFeaturesList();            //获取原始训练样本            
                    //从降维器获取降维后的新样本
                    IList newSampleList = MDAHelper.GetMDSampleList();
                    MVHelper.SetSampleList((ArrayList)newSampleList);




                    Bayes bayes = Bayes.GetInstance();
                    bayes.TrainSampleList = newSampleList;                 //向贝叶斯分类器注入降维后的训练样本

                    //int classID = Convert.ToInt32(FeatureHelper.GetUpperFoldername(myfilepath));
                    //Features feature = new Features(myfilepath, classID);
                    feature = MDAHelper.MDSample(feature); //测试样本降维
                    int testClassID = bayes.DecisionFunction(feature);//用贝叶斯决策进行测试样本分类
                    //结果显示
                    lblunknownclassify.Text = testClassID.ToString("000");
                    if (feature.classID == testClassID)
                    {
                        lblerrorinfo.Text = "Bayes分类法分类正确";
                        lblerrorinfo.ForeColor = Color.Green;
                    }

                    //unknown.pcx处理
                    else if (feature.classID == -1)
                    {

                    }
                    else
                    {
                        lblerrorinfo.Text = "Bayes分类法分类失败";
                        lblerrorinfo.ForeColor = Color.Green;
                    }


                }

                #endregion

                #region Kn近邻法

                if (rbKn.Checked)
                {
                    #region 相关数据初始化

                    CheckInit();
                    int testResult = -1;
                    double correctCount = 0.0;
                    int kvalue = Constant.kvalue;

                    #endregion

                    #region 有效的情况下进行计算

                    if (KCheck(kvalue))
                    {
                        KnNear my_knearest = new KnNear();

                        //int classID = Convert.ToInt32(FeatureHelper.GetUpperFoldername(myfilepath));
                        //Features currfeature = new Features(myfilepath, classID);
                        testResult = my_knearest.DoK_nearest(feature, FeatureHelper.GetFeaturesList(), kvalue);

                        //testResult为K近邻的分类结果
                        // 其实testResult的结果直接就是result求的值
                        string result = testResult.ToString("000");
                        lblunknownclassify.Text = result;
                        result = ResultConvert(result);

                        int testID = Convert.ToInt32(result);
                        if (testID > 0 && testID == feature.classID)
                        {
                            //correctRate = "分类正确率： " + Constant.kn_Rate;
                            lblerrorinfo.Text = "Kn近邻法分类正确";
                            lblerrorinfo.ForeColor = Color.Green;
                        }

                        //unknown.pcx处理
                        else if (feature.classID == -1)
                        {

                        }

                        else
                        {
                            lblerrorinfo.Text = "Kn近邻法分类失败！";
                            lblerrorinfo.ForeColor = Color.Green;
                        }
                    }

                    #endregion

                }

                #endregion

                #region 最近邻法

                if (rbnearest.Checked)
                {
                    #region 初始化

                    CheckInit();
                    int testResult = -1;
                    double correctCount = 0.0;

                    #endregion

                    #region 最近邻分类

                    if (NearestCheck())
                    {
                        Nearest nearest = new Nearest();
                        //int classID = Convert.ToInt32(FeatureHelper.GetUpperFoldername(myfilepath));
                        //Features currfeature = new Features(myfilepath, classID);
                        testResult = nearest.Do_Nearest(feature, FeatureHelper.GetFeaturesList());

                        string result = testResult.ToString("000");
                        lblunknownclassify.Text = result;
                        if (testResult > 0 && testResult == feature.classID)
                        {
                            lblerrorinfo.Text = "最近邻法分类正确";
                            lblerrorinfo.ForeColor = Color.Green;
                        }

                            //unknown.pcx处理
                        else if (feature.classID == -1)
                        {

                        }

                        else
                        {
                            lblerrorinfo.Text = "最近邻法分类失败！";
                            lblerrorinfo.ForeColor = Color.Green;
                        }
                    }

                    #endregion
                }

                #endregion
            }

        }


        #endregion

        #region 公共方法

        // 判断K值的合法性
        public bool KCheck(int k)
        {
            if (FeatureHelper.GetFeaturesList().Count == FeatureHelper.GetTestFeaturesList().Count)
            {
                MessageBox.Show(this, "Kn近邻法首先进行开测试进行样本提取", "提示信息", MessageBoxButtons.OK);
                return false;
            }

            if (k % 2 == 0)
            {
                MessageBox.Show(this, "K 值必须是奇数！", "提示信息", MessageBoxButtons.OK);
                return false;
            }
            if (k < 1 || k > 49)
            {
                MessageBox.Show(this, "K 值必须在 1 到 49 之间！", "提示信息", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        // 最近邻是否为开测试测试
        public bool NearestCheck()
        {
            // 判断测试样本与训练样本是否一样多，或者判断关于开闭测试的radioButton选择状态
            if (FeatureHelper.GetFeaturesList().Count == FeatureHelper.GetTestFeaturesList().Count)
            {
                MessageBox.Show(this, "最近近邻法首先进行开测试进行样本提取", "提示信息", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        // 将K-近邻法结果进行转换
        private string ResultConvert(string value)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (value[0].Equals('0'))
                {
                    result.Append(value[1]);
                    result.Append(value[2]);
                    break;
                }
            }
            //result.ToString();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[0].Equals('0'))
                {
                    result.Remove(0, 1);
                    break;
                }
            }

            return result.ToString(); ;
        }

        // 每次radioButton变化时候的初始化函数
        private void CheckInit()
        {
            this.lblunknownclassify.Text = string.Empty;
            this.lblerrorinfo.Text = string.Empty;
        }

        // 图片预览
        private void Preview()
        {

        }

        #endregion

    }
}

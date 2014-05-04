using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using HandSignRecognition.Core;
using HandSignRecognition.Classifier;
using HandSignRecognition.Core.Base;

namespace HandSignRecognition.Feature
{
    public partial class BayesTestForm : Form
    {

        #region  窗口初始化

        public BayesTestForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件方法

        private void testButton_Click(object sender, EventArgs e)
        {
            #region 数据初始化

            double correctCount = 0.0;
            double correctRate = 0.0;
                 IList sampleList = FeatureHelper.GetFeaturesList();            //获取原始训练样本            
            




            
            //从降维器获取降维后的新样本
            IList newSampleList = MDAHelper.GetMDSampleList();
            MVHelper.SetSampleList((ArrayList)newSampleList);
            //


            Bayes bayes = Bayes.GetInstance();
            bayes.TrainSampleList = newSampleList;                 //向贝叶斯分类器注入降维后的训练样本，主要是Bayes中变量的初始化

            IList testSampleList = FeatureHelper.GetTestFeaturesList();        //获取测试样本

            #endregion

            #region DataGridView操作

            bayesDataGridView.DataSource = null;
            bayesDataGridView.Rows.Clear();
            bayesDataGridView.Refresh();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();  
            // 或者直接将arr作为参数传入
            //FeatureHelper.GetSamplesFeatures(); //初始化训练和测试样本            

            dt.Columns.Add("文件夹", typeof(string));
            dt.Columns.Add("所属类别", typeof(string));
            dt.Columns.Add("测试类别", typeof(string));
            dt.Columns.Add("正误判断", typeof(string));
            for (int i = 0; i < testSampleList.Count; i++)
            {
                DataRow row = dt.NewRow();
                string rightOrWrong = "×";
                Features feature = (Features)testSampleList[i];
                row[0] = feature.Filepath;
                row[1] = feature.classID;

                feature = MDAHelper.MDSample(feature); //测试样本降维
                int testClassID = bayes.DecisionFunction(feature);//用贝叶斯决策进行测试样本分类
                // 用StringBuilder加快字符串处理速度。【值类型和堆类型】
                StringBuilder sb = new StringBuilder();
                sb.Append("类");
                sb.Append(testClassID.ToString());
                sb.ToString();
                row[2] = sb;
                if (feature.classID == testClassID)
                {
                    correctCount++;
                    row[3] = " ";
                }
                else
                {
                    row[3] = rightOrWrong;
                    //this.bayesDataGridView.DefaultCellStyle.ForeColor = Color.Red;
                }

                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            this.bayesDataGridView.DataSource = ds.Tables[0];

            #endregion

            #region Bayes分类性能显示

            correctRate = (correctCount / Convert.ToDouble(testSampleList.Count)) * 100.0;
            Constant.bayes_Rate = correctRate.ToString("0.000") + "%";
            dataShowLabel.Text = "测试样本总数 " + testSampleList.Count + " ，Bayes判断正确 " + correctCount + " 个，正确率为：" + Constant.bayes_Rate;

            #endregion

        }

        // 清空操作
        private void cleanButton_Click(object sender, EventArgs e)
        {
            bayesDataGridView.DataSource = null;
            bayesDataGridView.Rows.Clear();
            bayesDataGridView.Refresh();
        }

        #endregion

        #region  公共方法
        #endregion

    }
}

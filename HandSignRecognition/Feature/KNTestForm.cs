using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HandSignRecognition.Classifier;
using System.Collections;
using HandSignRecognition.Core;
using HandSignRecognition.Core.Base;

namespace HandSignRecognition.Feature
{
    public partial class KNTestForm : Form
    {

        #region 窗体初始化

        public KNTestForm()
        {            
            InitializeComponent();
            this.clearButton.Visible = false;//加载顺序不能反
        }

        #endregion

        #region 事件方法(delegate委托，多线程Thread)

        // 测试启动
        private void knTestButton_Click(object sender, EventArgs e)
        {
            int kvalue = Convert.ToInt32(kValueText.Text);
            Constant.kvalue = kvalue;

            #region K有效的情况下进行计算

            if (KCheck(kvalue))
            {
                #region 数据初始化

                double correctCount = 0;
                double correctRate = 0.0;
                int testResult = 0;

                KnNear my_knearest = new KnNear();

                IList testSampleList = FeatureHelper.GetTestFeaturesList();        //获取原始测试
                #endregion

                #region DataGridView操作

                knDataGridView.DataSource = null;
                knDataGridView.Rows.Clear();
                knDataGridView.Refresh();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Columns.Add("样本路径", typeof(string));
                dt.Columns.Add("样本测试结果类", typeof(string));
                dt.Columns.Add("所取K值", typeof(string));
                dt.Columns.Add("正误判断", typeof(string));
                foreach (Features feature in testSampleList)
                {
                    testResult = my_knearest.DoK_nearest(feature, FeatureHelper.GetFeaturesList(), kvalue);
                    if (testResult > 0)
                    {
                        DataRow row = dt.NewRow();
                        string rightOrWrong = "×";
                        row[0] = feature.Filepath;
                        row[1] = string.Format("类{0}", testResult);
                        row[2] = kvalue;
                        //检查结果是否正确
                        if (testResult == feature.classID)
                        {
                            correctCount++;
                            row[3] = " ";
                        }
                        else
                        {
                            row[3] = rightOrWrong;
                            //this.knDataGridView.DefaultCellStyle.ForeColor = Color.Red;
                        }
                        dt.Rows.Add(row);
                    }
                }
                ds.Tables.Add(dt);
                this.knDataGridView.DataSource = ds.Tables[0];

                #endregion

                #region Kn近邻法性能显示
                correctRate = (correctCount / Convert.ToDouble(testSampleList.Count)) * 100.0;
                Constant.kn_Rate = correctRate.ToString("0.000") + "%";

                resultLabel.Text = "测试样本总数 " + testSampleList.Count + " ，Kn近邻法判断正确 "
                    + correctCount + " 个，正确率为：" + Constant.kn_Rate;

                #endregion

            }

            #endregion
            
        }

        // 清空数据
        private void clearButton_Click(object sender, EventArgs e)
        {

        }

        // K函数绘图
        private void btn_kplot_Click(object sender, EventArgs e)
        {

            KFunctionForm kffom = new KFunctionForm();
            kffom.ShowDialog();

        }

        #endregion

        #region 公共方法

        // k值有效性检验
        public bool KCheck(int k)
        {
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

        #endregion

    }
}

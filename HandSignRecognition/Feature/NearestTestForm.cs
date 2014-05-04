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
using System.Reflection;

namespace HandSignRecognition.Feature
{
    public partial class NearestTestForm : Form
    {
        #region 数据Data

        private delegate void AsyncEventHandler();//声明委托
        private delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        #endregion

        #region 窗体初始化

        public NearestTestForm()
        {
            InitializeComponent();
        }

        private void NearestTestForm_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("样本路径", typeof(string));
            dt.Columns.Add("样本类", typeof(string));
            dt.Columns.Add("样本测试结果类", typeof(string));
            dt.Columns.Add("正误判断", typeof(string));

            ds.Tables.Add(dt);
            this.dgv_result.DataSource = ds.Tables[0];
        }

        #endregion

        #region 事件方法

        // 清空datagridview事件
        private void btn_clear_Click(object sender, EventArgs e)
        {

        }

        // 最近邻分类事件
        private void btn_nearestclassify_Click(object sender, EventArgs e)
        {

            #region 同步方法

            double correctCount = 0;
            double correctRate = 0.0;
            int count = 0;
            Cursor.Current = Cursors.WaitCursor;
            DataSet ds = GetViewDS(out correctCount, out correctRate, out count);
            this.dgv_result.DataSource = ds.Tables[0];
            Cursor.Current = Cursors.Default;

            #region Kn近邻法性能显示
            correctRate = (correctCount / Convert.ToDouble(count)) * 100.0;
            Constant.kn_Rate = correctRate.ToString("0.000") + "%";

            lbl_result.Text = "测试样本总数 " + count + " ，Kn近邻法判断正确 "
                + correctCount + " 个，正确率为：" + Constant.kn_Rate;

            #endregion


            #endregion

            #region delegate异步加载实现

            //AsyncEventHandler asy = new AsyncEventHandler(AsynLoadView);
            ////异步调用开始，没有回调函数和AsyncState,都为null
            //IAsyncResult ia = asy.BeginInvoke(null, null);
            //asy.EndInvoke(ia);

            #endregion

            #region 基于事件Event异步加载

            #endregion

            #region Background控件异步实现

            //this.backgroundWorker1.RunWorkerAsync();

            #endregion

        }              

        #region none:Background事件

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending)   //查看用户是否取消该线程
            {
                return;
            }

            System.Threading.Thread.Sleep(50);          //干点实际的事

            double correctCount = 0;
            double correctRate = 0.0;
            int count =0;
            DataSet ds = GetViewDS(out correctCount, out correctRate, out count);
            this.dgv_result.DataSource = ds.Tables[0];
            //SetControlPropertyValue(dgv_result, "DataSource", ds.Tables[0]);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        

        #endregion                

        #endregion

        // 获取Dataset数据
        public DataSet GetViewDS(out double ccount, out double crate, out int count)
        {
            #region 数据初始化

            double correctCount = 0;
            double correctRate = 0.0;
            int testResult = 0;

            Nearest nearest = new Nearest();

            IList testSampleList = FeatureHelper.GetTestFeaturesList();        //获取原始测试
            #endregion

            #region DataGridView操作


            dgv_result.DataSource = null;
            dgv_result.Rows.Clear();
            dgv_result.Refresh();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("样本路径", typeof(string));
            dt.Columns.Add("样本类", typeof(string));
            dt.Columns.Add("样本测试结果类", typeof(string));
            dt.Columns.Add("正误判断", typeof(string));
            foreach (Features feature in testSampleList)
            {
                testResult = nearest.Do_Nearest(feature, FeatureHelper.GetFeaturesList());
                if (testResult > 0)
                {
                    DataRow row = dt.NewRow();
                    string rightOrWrong = "×";
                    row[0] = feature.Filepath;
                    row[1] = feature.classID;
                    row[2] = string.Format("类{0}", testResult);
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
            //this.dgv_result.DataSource = ds.Tables[0];
            //SetControlPropertyValue(dgv_result, "DataSource", ds.Tables[0]);

            #endregion

            #region Kn近邻法性能显示
            //correctRate = (correctCount / Convert.ToDouble(testSampleList.Count)) * 100.0;
            //Constant.kn_Rate = correctRate.ToString("0.000") + "%";

            //lbl_result.Text = "测试样本总数 " + testSampleList.Count + " ，Kn近邻法判断正确 "
            //    + correctCount + " 个，正确率为：" + Constant.kn_Rate;

            #endregion

            ccount = correctCount;
            crate = correctRate;
            count = testSampleList.Count;

            return ds;
        }


        #region none:公共方法

        #region none：Event事件       

        #endregion

        #region none：delegate异步加载处理

        private void AsynLoadView()
        {

        }

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        #endregion

        #endregion

    }
}

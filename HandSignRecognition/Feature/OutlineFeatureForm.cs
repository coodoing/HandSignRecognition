using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HandSignRecognition.Core.Base;
using HandSignRecognition.Core;
using System.Collections;

namespace HandSignRecognition.Feature
{
    public partial class OutlineFeatureForm : Form
    {
        #region Data数据

        private List<PCXImage> pcxlist;

        #endregion

        #region 窗口初始化

        public OutlineFeatureForm()
        {
            InitializeComponent();
        }

        private void OutlineFeatureForm_Load(object sender, EventArgs e)
        {
            FeatureHelper.ds = new DataSet();
            FeatureHelper.dt = new DataTable();
            GridviewInit();
            ColumnInit();
        }

        // gridview的初始化
        private void GridviewInit()
        {
            //只读属性设置 
            featureDataGridView.ReadOnly = true;
            //行幅自动变化 
            featureDataGridView.AllowUserToResizeRows = true;
            //高度设定 
            featureDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //标头设定 
            featureDataGridView.RowHeadersVisible = true;
            //标题行高 featureDataGridView.ColumnHeadersHeight = 25; 
            featureDataGridView.RowTemplate.Height = 23;
            //行选择设定 
            featureDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //多行选择 
            featureDataGridView.MultiSelect = false;
            //选择状态解除 
            featureDataGridView.ClearSelection();
            //head文字居中 
            featureDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //选择状态的行的颜色 
            featureDataGridView.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            featureDataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            //设定交替行颜色 
            featureDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            featureDataGridView.RowsDefaultCellStyle.BackColor = Color.LightGray;
            //可否手动调整行大小 
            featureDataGridView.AllowUserToResizeRows = false;

        }

        private void ColumnInit()
        {
            featureDataGridView.DataSource = null;
            featureDataGridView.Rows.Clear();
            featureDataGridView.Refresh();
            DataSet ds = FeatureHelper.InitDataset();
            this.featureDataGridView.DataSource = ds.Tables[0];
        }


        #endregion

        #region 事件方法

        //在进行特征提取的过程中，又重新初始化了gridview
        private void featureExtractButton_Click(object sender, EventArgs e)
        {
            //FeatureHelper.FeatureExtract();
            #region 检查前提条件
            //无论开测试还是闭测试，都要计算训练样本列表，因此要求训练样本文件列表不为空
            #endregion
            FeatureHelper.ds = new DataSet();
            FeatureHelper.dt = new DataTable();
            GridviewInit();
            featureDataGridView.DataSource = null;
            featureDataGridView.Rows.Clear();
            featureDataGridView.Refresh();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();

            // 或者直接将arr作为参数传入
            FeatureHelper.GetSamplesFeatures(); //初始化训练样本
            arr = FeatureHelper.GetFeaturesList();

            #region 列初始化

            dt.Columns.Add("文件夹", typeof(string));
            dt.Columns.Add("类别", typeof(string));
            string colname = string.Empty;
            for (int i = 2, j = 1; i < 26; i++, j++)
            {
                colname = string.Format("ET1({0})", j.ToString());
                dt.Columns.Add(colname, typeof(string));
            }
            for (int i = 26, j = 1; i < 50; i++, j++)
            {
                colname = string.Format("DT12({0})", j.ToString());
                dt.Columns.Add(colname, typeof(string));
            }

            #endregion

            #region Datatable行设值

            for (int i = 0; i < arr.Count; i++)
            {
                DataRow row = dt.NewRow();
                Features f = (Features)arr[i];//装箱
                row[0] = f.Filepath;
                row[1] = f.classID;
                for (int j = 2; j < 50; j++)
                {
                    row[j] = f.feature_vector[j - 2];
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            this.featureDataGridView.DataSource = ds.Tables[0];

            #endregion
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            FeatureHelper.Close();
            this.Close();
        }

        #endregion














        #region None：公共方法
        #endregion

    }
}

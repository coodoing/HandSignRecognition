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
using HandSignRecognition.Core.Base;

namespace HandSignRecognition.Feature
{
    public partial class MeanCalculateForm : Form
    {
        #region Data数据

        private ArrayList classFeatureList;
        private ArrayList rdFeatureList;

        #endregion

        #region 初始化Form
        public MeanCalculateForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件处理

        // μ、∑计算
        private void mvButton_Click(object sender, EventArgs e)
        {
            ArrayList samples = FeatureHelper.GetFeaturesList();
            MVHelper.SetSampleList(samples);
            ResetResultView();
        }

        // 降维处理
        private void rdButton_Click(object sender, EventArgs e)
        {
            //新得到的样本特征
            ArrayList mdSamples = MDAHelper.GetMDSampleList();
            MVHelper.SetSampleList(mdSamples);

            //再用这个新样本，算出每一类的均值和协方差矩阵
            ArrayList changeSamples = MVHelper.GetClassFeatureList(mdSamples);
            ResetRDResultView(mdSamples);
        }

        // 类别选取发生变化时候的处理
        private void classChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.AppStarting;
            int classID = Int32.Parse(classChooseComboBox.Items[classChooseComboBox.SelectedIndex].ToString());
            if (rdFeatureList == null || rdFeatureList.Count == 0)
            {
                ShowResultView(classID);
            }
            else
            {
                ShowResultView(classID, rdFeatureList);
            }
        }

        #endregion

        #region 公共方法

        // 重置相关结果视图
        private void ResetResultView()
        {
            classChooseComboBox.Items.Clear();
            classFeatureList = MVHelper.GetClassFeatureList(FeatureHelper.GetFeaturesList());
            //按照类列表中的内容，生成类选择下拉列表的项目
            for (int i = 0; i < classFeatureList.Count; i++)
            {
                Sample sample = (Sample)classFeatureList[i];
                string classIDStr = "000" + sample.ClassID;
                classChooseComboBox.Items.Add(classIDStr.Substring(classIDStr.Length - 3));
            }
            //初始选择
            classChooseComboBox.SelectedIndex = 0;
            //计算初始选择的结果 
            ShowResultView(Int32.Parse((string)this.classChooseComboBox.Items[0]));
        }

        // 降维后重置结果视图
        private void ResetRDResultView(ArrayList arr)
        {
            classChooseComboBox.Items.Clear();
            // 降维后的ArrayList发生变化
            rdFeatureList = MVHelper.GetClassFeatureList(arr);
            //按照类列表中的内容，生成类选择下拉列表的项目
            for (int i = 0; i < rdFeatureList.Count; i++)
            {
                Sample sample = (Sample)rdFeatureList[i];
                string classIDStr = "000" + sample.ClassID;
                classChooseComboBox.Items.Add(classIDStr.Substring(classIDStr.Length - 3));
            }
            //初始选择
            classChooseComboBox.SelectedIndex = 0;
            //计算初始选择的结果 
            ShowResultView(Int32.Parse((string)this.classChooseComboBox.Items[0]), rdFeatureList);
        }

        private void ShowResultView(int classID, ArrayList rdFeatureList)
        {
            resultTextBox.Text = "";
            Sample sample;
            // 这里有点错误
            if (rdFeatureList.Count == 1)
            {
                sample = (Sample)rdFeatureList[0];
            }
            //其实这里classID应该明确的是该类在选择样本中的index序号
            else
            {
                sample = (Sample)rdFeatureList[classID - 1];
            }

            int dimension = sample.MeanVector.Rows;
            string classIDStr = "000" + sample.ClassID;
            string classViewString = "";
            classViewString += "类别ID：" + classIDStr.Substring(classIDStr.Length - 3) + "\r\n\r\n";
            classViewString += "均值向量： [ " + dimension + " 维向量 ]\r\n" + sample.MeanVector.Transpose().ToString("\t", true, true) + "\r\n\r\n";
            classViewString += "协方差矩阵： [ " + dimension + " * " + dimension + " 维矩阵 ]\r\n" + sample.ConMatrix.ToString("\t", true, true);
            resultTextBox.Text = classViewString;
        }

        //显示结果视图,即显示类的均值向量和协方差矩阵
        private void ShowResultView(int classID)
        {
            resultTextBox.Text = "";
            Sample sample;
            if (classFeatureList.Count == 1)
            {
                sample = (Sample)classFeatureList[0];
            }
            else
            {
                sample = (Sample)classFeatureList[classID - 1];
            }

            int dimension = sample.MeanVector.Rows;
            string classIDStr = "000" + sample.ClassID;
            string classViewString = "";
            classViewString += "类别ID：" + classIDStr.Substring(classIDStr.Length - 3) + "\r\n\r\n";
            classViewString += "均值向量： [ " + dimension + " 维向量 ]\r\n" + sample.MeanVector.Transpose().ToString("\t", true, true) + "\r\n\r\n";
            classViewString += "协方差矩阵： [ " + dimension + " * " + dimension + " 维矩阵 ]\r\n" + sample.ConMatrix.ToString("\t", true, true);
            resultTextBox.Text = classViewString;
        }

        #endregion

    }
}

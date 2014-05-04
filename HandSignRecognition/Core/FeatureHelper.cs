using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HandSignRecognition.Core.Base;
using System.Drawing;
using System.Drawing.Imaging;
using Fireball.Drawing;
using System.Collections;

namespace HandSignRecognition.Core
{
    /// <summary>
    ///  特征操作类：该文件中，不把类的属性和操作方法分离
    ///  
    /// 主要是将PCXImage列表转换成相应的Feature列表
    /// </summary>
    public class FeatureHelper
    {
        #region 属性值

        private static Features feature;
        //pcxlist不能在这里进行初始化,即不能设为null,只能new一个实例
        private static List<PCXImage> pcxlist;//训练样本的pcx图片列表
        private static ArrayList sampleArray; //存放训练样本特征
        private static List<PCXImage> testpcxlist; //测试样本的pcx图片列表
        private static ArrayList testSampleArray;//存放测试样本特征
        

        public static DataSet ds = new DataSet();
        public static DataTable dt = new DataTable();

        #endregion

        #region 构造函数

        public FeatureHelper()
        {
        }

        #endregion

        #region Feature的set，get方法

        // 设置训练样本特征
        public static void SetFeatures()
        {
            // 初始化feature的时候pcxlist不为空
            int index, classID;
            string filepath;
            //首先清空以前的样本对象列表
            if (sampleArray == null || sampleArray.Count == 0)
            {
                sampleArray = new ArrayList();
            }
            else
            {
                sampleArray.Clear();
            }

            foreach (PCXImage pcx in pcxlist) // 获取训练样本的Sample
            {
                filepath = pcx.OldFilename;
                classID = Convert.ToInt32(GetUpperFoldername(filepath));

                // 这里同样可以采用其他形式的构造函数
                feature = new Features(filepath, classID);//构造函数-1  
                sampleArray.Add(feature);//这里用ArrayList代替List实现
            }
        }

        // 设置测试样本特征
        public static void SetTestFeatures()
        {
            // 初始化feature的时候pcxlist不为空
            int index, classID;
            string filepath;
            //首先清空以前的样本对象列表
            if (testSampleArray == null || testSampleArray.Count == 0)
            {
                testSampleArray = new ArrayList();
            }
            else
            {
                testSampleArray.Clear();
            }

            foreach (PCXImage pcx in testpcxlist) // 获取测试样本的Sample
            {
                filepath = pcx.OldFilename;
                classID = Convert.ToInt32(GetUpperFoldername(filepath));
                feature = new Features(filepath, classID);//构造函数-1
                testSampleArray.Add(feature);//这里用ArrayList代替List实现
            }
        }

        // 没有用到
        public static Features GetFeature()
        {
            return feature;
        }

        // 获取训练样本特征List
        public static ArrayList GetFeaturesList()
        {
            return sampleArray;
        }

        // 获取测试样本特征List
        public static ArrayList GetTestFeaturesList()
        {
            return testSampleArray;
        }

        // 设置训练样本和测试样本的值
        public static void GetSamplesFeatures()
        {
            //因为在此之前，已经通过InitDataSet方法得到训练样本和测试样本的数据
            SetFeatures();
            SetTestFeatures();
        }

        #endregion

        #region Dataset与Datatable操作

        public static DataSet InitDataset()
        {
            dt.Columns.Add("文件夹", typeof(string));
            dt.Columns.Add("类别", typeof(string));

            string colname = string.Empty;
            for (int i = 2, j = 1; i < 26; i++, j++)
            {
                colname = string.Format("ET1({0})", j.ToString());
                dt.Columns.Add(colname, typeof(string));
                // featureDataGridView.Columns[colname].Width = 50;
            }

            for (int i = 26, j = 1; i < 50; i++, j++)
            {
                colname = string.Format("DT12({0})", j.ToString());
                dt.Columns.Add(colname, typeof(string));
            }

            #region DataGridView初始化：傻瓜式方法
            //DataGridViewTextBoxColumn column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            //DataGridViewTextBoxColumn column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            //this.SuspendLayout();

            //column1.HeaderText = "文件夹";
            //column1.Name = "Column1";
            //column1.Width = 100;
            //column2.HeaderText = "类别";
            //column2.Name = "Column2";
            //column2.Width = 50;
            //this.featureDataGridView.Columns.AddRange(new DataGridViewColumn[] {
            //column1,
            //column2});

            //for (int i = 2, j = 1; i < 26; i++, j++)
            //{
            //    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            //    colname = string.Format("ET1({0})", j.ToString());
            //    column.HeaderText = colname;
            //    column.Width = 50;
            //    this.featureDataGridView.Columns.AddRange(column);
            //}

            //for (int i = 26, j = 1; i < 50; i++, j++)
            //{
            //    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            //    colname = string.Format("DT12({0})", j.ToString());
            //    column.HeaderText = colname;
            //    column.Width = 60;
            //    this.featureDataGridView.Columns.AddRange(column);
            //}
            #endregion

            pcxlist = SelectedPCXHelper.GetSelPCXFromLB();
            testpcxlist = SelectedPCXHelper.GetUnselPCXList();
            foreach (PCXImage pcx in pcxlist)
            {
                DataRow row = dt.NewRow();//ds.Tables[0].NewRow();

                PCXImage pImage = new PCXImage();
                pImage.IsBinary = pcx.IsBinary;
                pImage.IsDraw = pcx.IsDraw;
                pImage.OldFilename = pcx.OldFilename;
                pImage.PcxData = pcx.PcxData;
                pImage.PcxSize = pcx.PcxSize;
                string filenameF = pImage.OldFilename;
                int charindex = filenameF.LastIndexOf("\\");
                //获取文件名 而不是路径名
                string filename = filenameF.Substring(charindex + 1);
                int _index = filename.IndexOf("-");
                string typename = filename.Substring(_index - 2, 2);
                row[0] = filename;
                row[1] = typename;
                dt.Rows.Add(row);
                //ds.Tables[0].Rows.Add(row);
            }
            ds.Tables.Add(dt);
            //DataView view = ds.Tables[0].DefaultView;
            return ds;
        }

        public static void Close()
        {
            ds = null;
            dt = null;
        }

        #endregion

        #region string字符串一些操作
        /*out与ref关键字*/        

        // 获取文件名006-006
        public static string GetUpperFoldername(string filepath)
        {
            if (filepath.Equals(""))
                return "0";
            int idx2 = filepath.LastIndexOf("\\");
            int idx1 = filepath.LastIndexOf("\\", idx2 - 1);
            return filepath.Substring(idx1 + 1, idx2 - idx1 - 1);
        }

        // 对unknown.pcx文件的处理
        public static string GetUnknownName(string filepath)
        {
            int index1 = filepath.LastIndexOf(".");
            return filepath.Substring(index1 - 7, 7);
        }

        #endregion













        #region none：特征提取

        //public static void FeatureExtract()
        //{
        //    pcxlist = SelectedPCXHelper.GetSelPCXFromLB();
        //    SetFeatures();
        //    //foreach (PCXImage pcx in pcxlist)
        //    for (int i = 0; i < pcxlist.Count; i++)
        //    {
        //        PCXImage pcx = pcxlist[i];
        //        ET1(pcx, i);
        //        DT12(pcx, i);
        //    }
        //}

        //// PCX图片轮廓特征提取计数
        //public static int FeatureCount(string filename)
        //{
        //    int number = 0;
        //    Image imageToSet = null;
        //    Fireball.Drawing.FreeImage image = new FreeImage(filename);
        //    imageToSet = image.GetBitmap();
        //    Bitmap bmp = new Bitmap(imageToSet);
        //    int width = bmp.Width;
        //    int height = bmp.Height;
        //    BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    #region unsafe代码循环遍历BMP

        //    unsafe
        //    {
        //        byte* p = (byte*)bmData.Scan0;
        //        int offset = bmData.Stride - width * 3; //only correct when PixelFormat is Format24bppRgb
        //        for (int i = 0; i < height; i++)
        //        {
        //            for (int j = 0; j < width; j++)
        //            {
        //                // 黑色的话就显0
        //                if (p[0] == 255 && p[1] == 255 && p[2] == 255)
        //                {
        //                    number++;
        //                }
        //                else
        //                    break;
        //                p += 3;
        //            }
        //            p += offset;
        //        }
        //    }

        //    #endregion

        //    bmp.UnlockBits(bmData);
        //    bmp.Dispose();
        //    return 0;
        //}

        //// 轮廓特征提取中的ET1
        //public void ET1(PCXImage pcx, int iRow)
        //{
        //    // this索引器
        //    DataRow row = dt.Rows[iRow];
        //    int i, j, k, step, num = 0;
        //    string fnum = string.Empty;
        //    #region 上边缘提取

        //    i = 0; step = 20;//第1至第5个
        //    for (k = 0; k < 5; k++)
        //    {
        //        for (num = 0; i <= step; i++)
        //            for (j = 0; j < 94; j++)
        //            {
        //                if (imageMartix[j, i] == 255)
        //                    num++;
        //                else
        //                    break;
        //            }
        //        ET1_feature[k] = num;
        //        step = step + 21;
        //    }
        //    //第6个
        //    for (i = 106, num = 0; i <= 128; i++)
        //        for (j = 0; j < 94; j++)
        //        {
        //            if (imageMartix[j, i] == 255)
        //                num++;
        //            else
        //                break;
        //        }
        //    ET1_feature[5] = num;

        //    #endregion

        //    #region 下边缘提取
        //    #endregion

        //    #region 左边缘提取
        //    #endregion

        //    #region 右边缘提取
        //    #endregion

        //}

        //// // 轮廓特征提取中的DT12
        //public void DT12(PCXImage pcx, int iRow)
        //{

        //}

        #endregion


    }
}

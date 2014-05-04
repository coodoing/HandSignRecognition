using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Fireball.Drawing;
using System.Drawing.Imaging;

namespace HandSignRecognition.Core.Base
{
    /// <summary> 
    /// Author:AirFly
    /// Date:12/3/2010 10:38:00 PM 
    /// Company:DCBI
    /// Copyright:2010-2013 
    /// CLR Version:4.0.30319.1 
    /// Blog Address:http://www.cnblogs.com/ttltry-air/
    /// Class1 Illustration: All rights reserved please do not encroach!   
    /// GUID:963d892f-4cc4-4c96-93e1-ef1a1133160b 
    /// Description: Feature为一个完整的特征类
    public class Features
    {
        #region 属性值

        private PCXImage pcximage;
        private string filepath;
        private int[,] imageMartix = new int[94, 129];
        // 类名
        public int classID = -1;
        // 总体是48维的特征向量
        //ET1特征向量
        public int[] ET1_feature = new int[24];
        //DT12特征向量
        public int[] DT12_feature = new int[24];
        //合并的48维特征向量
        public double[] feature_vector = new double[48];

        public string Filepath
        {
            set { this.filepath = value; }
            get { return this.filepath; }
        }
        public PCXImage Pcximage { get { if (pcximage != null) return pcximage; else return null; } }

        #endregion

        #region 构造函数

        //构造函数-1：由文件路径创建，需要先建立ImagePCX对象，类别由参数给定
        public Features(string p_FileFullName, int myclassID)
        {
            if (!File.Exists(p_FileFullName))
                return;
            this.filepath = p_FileFullName;
            Image image = PCXHelper.LoadPCX(p_FileFullName);
            this.pcximage = PCXHelper.GetPCXImage(p_FileFullName);
            PcxtoMartix();
            ET1();
            DT12();
            MergeFeature();
            classID = myclassID;
        }

        //构造函数-2：由文件路径创建，忽略类别
        public Features(string p_FileFullName)
        {
            if (!File.Exists(p_FileFullName))
                return;
            this.filepath = p_FileFullName;
            Image image = PCXHelper.LoadPCX(p_FileFullName);
            this.pcximage = PCXHelper.GetPCXImage(p_FileFullName);
            PcxtoMartix();
            ET1();
            DT12();
            MergeFeature();
            classID = -1;
        }

        //构造函数-3：由构造好的ImagePCX对象创建，类别由参数给定
        public Features(PCXImage pcx, int myclassID)
        {
            if (pcx == null)
                return;
            pcximage = pcx;
            PcxtoMartix();
            ET1();
            DT12();
            MergeFeature();
            classID = myclassID;
        }

        //构造函数-4：由构造好的ImagePCX对象创建，忽略类别
        public Features(PCXImage pcx)
        {
            if (pcx == null)
                return;
            this.pcximage = pcx; //初始化数据 这里也可以直接写函数Set、Get进行初始化
            PcxtoMartix();
            ET1();
            DT12();
            MergeFeature();
            classID = -1;
        }

        // 构造函数-5：无参构造
        public Features()
        {
        }

        #endregion

        #region 特征提取

        // 提取特征
        public void FeatureExtract()
        {
        }

        //将当前样本24个ET1和24个DT12合并在一个48个元素的数组里，形成48维特征向量
        private void MergeFeature()
        {
            int i;
            for (i = 0; i < 24; i++)
                feature_vector[i] = (double)ET1_feature[i];
            for (i = 24; i < 48; i++)
                feature_vector[i] = (double)DT12_feature[i - 24];
        }

        //将pcx图片数据转换成像素色彩矩阵
        private void PcxtoMartix()
        {
            #region Format32bppRgb处理
            // 直接由memeorystream内存处理是Format32bppRgb，而不是Format24bppRgb
            //byte[] imageByte = PCXHelper.ImageToByteArray(image); 
            //for (i = 0; i < pcximage.PcxSize.Height; i++)
            //{
            //    for (j = 0; j < pcximage.PcxSize.Width + 3; j++)
            //    {
            //        num = Convert.ToInt32(imageByte[pix]);
            //        if (num < 129)
            //            num = 0;
            //        else
            //            num = 255;
            //        if (j < 129)
            //        {
            //            imageMartix[i, j] = num;
            //        }
            //        pix++;
            //    }
            //}

            #endregion

            #region Bitmap对象与byte之间的处理

            long i = 0, j = 0;
            int num = 0, pix = 0;
            int number = 0;
            Image imageToSet = null;
            Fireball.Drawing.FreeImage image = new FreeImage(pcximage.OldFilename);
            imageToSet = image.GetBitmap();
            Bitmap bmp = new Bitmap(imageToSet);
            int width = bmp.Width;
            int height = bmp.Height;
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            #region unsafe代码循环遍历BMP

            unsafe
            {
                byte* p = (byte*)bmData.Scan0;
                int offset = bmData.Stride - width * 3; //only correct when PixelFormat is Format24bppRgb
                for (i = 0; i < height; i++)
                {
                    for (j = 0; j < width; j++)
                    {
                        // 黑色的话就显0
                        if (p[0] == 255 && p[1] == 255 && p[2] == 255)
                        {
                            this.imageMartix[i, j] = 255;
                        }
                        p += 3;
                    }
                    p += offset;
                }
            }

            #endregion

            bmp.UnlockBits(bmData);
            bmp.Dispose();

            #endregion

        }

        // 轮廓特征提取中的ET1
        public void ET1()
        {
            int i, j, k, step, num = 0;
            string fnum = string.Empty;

            /**
             * 每个点从4个角度（上、下、左、右）进行提取
             * 特征提取的时候，提取6 个ET1和6 个DT12轮廓特征。
             * 
             * **/

            #region 上边缘提取

            i = 0; step = 20;//第1至第5个
            for (k = 0; k < 5; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 0; j < 94; j++)
                    {
                        if (imageMartix[j, i] == 255)
                            num++;
                        else
                            break;
                    }
                ET1_feature[k] = num;
                step = step + 21;
            }
            //第6个
            for (i = 106, num = 0; i <= 128; i++)
                for (j = 0; j < 94; j++)
                {
                    if (imageMartix[j, i] == 255)
                        num++;
                    else
                        break;
                }
            ET1_feature[5] = num;

            #endregion

            #region 下边缘提取

            i = 0; step = 20;//第7至第11个
            for (k = 6; k < 11; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 93; j >= 0; j--)
                    {
                        if (imageMartix[j, i] == 255)
                            num++;
                        else
                            break;
                    }
                ET1_feature[k] = num;
                step = step + 21;
            }
            //第12个
            for (i = 106, num = 0; i <= 128; i++)
                for (j = 93; j >= 0; j--)
                {
                    if (imageMartix[j, i] == 255)
                        num++;
                    else
                        break;
                }
            ET1_feature[11] = num;

            #endregion

            #region 从左面边缘提取
            i = 0; step = 14;//第13至第17个
            for (k = 12; k < 17; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 0; j < 129; j++)
                    {
                        if (imageMartix[i, j] == 255)
                            num++;
                        else
                            break;
                    }
                ET1_feature[k] = num;
                step = step + 15;
            }
            //第18个
            for (i = 75, num = 0; i <= 93; i++)
                for (j = 0; j < 129; j++)
                {
                    if (imageMartix[i, j] == 255)
                        num++;
                    else
                        break;
                }
            ET1_feature[17] = num;
            #endregion

            #region 从右面边缘提取
            i = 0; step = 14;//第19至第23个
            for (k = 18; k < 23; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 128; j >= 0; j--)
                    {
                        if (imageMartix[i, j] == 255)
                            num++;
                        else
                            break;
                    }
                ET1_feature[k] = num;
                step = step + 15;
            }
            //第24个
            for (i = 75, num = 0; i <= 93; i++)
                for (j = 128; j >= 0; j--)
                {
                    if (imageMartix[i, j] == 255)
                        num++;
                    else
                        break;
                }
            ET1_feature[23] = num;
            #endregion

        }

        // 轮廓特征提取中的DT12
        public void DT12()
        {
            int i, j, k, step, num = 0;
            bool isblack;
            #region 从上面边缘提取
            i = 0; step = 20;
            isblack = false;
            for (k = 0; k < 5; k++)//第1至第5个
            {
                for (num = 0; i <= step; i++)
                    for (j = 0; j < 94; j++)
                    {
                        if ((imageMartix[j, i] == 0) && (j < 93) && (imageMartix[j + 1, i] == 255))
                            isblack = true; ;
                        if ((isblack) && (imageMartix[j, i] == 255))
                            num++;
                        if ((isblack) && (j < 93) && (imageMartix[j + 1, i] == 0))
                            break;
                    }
                DT12_feature[k] = num;
                step = step + 21;
            }
            //第6个
            isblack = false;
            for (i = 106, num = 0; i <= 128; i++)
                for (j = 0; j < 94; j++)
                {
                    if ((imageMartix[j, i] == 0) && (j < 93) && (imageMartix[j + 1, i] == 255))
                        isblack = true; ;
                    if ((isblack) && (imageMartix[j, i] == 255))
                        num++;
                    if ((isblack) && (j < 93) && (imageMartix[j + 1, i] == 0))
                        break;
                }
            DT12_feature[5] = num;
            #endregion

            #region 从下面边缘提取
            i = 0; step = 20;//第7至第11个
            isblack = false;
            for (k = 6; k < 11; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 93; j >= 0; j--)
                    {
                        if ((imageMartix[j, i] == 0) && (j > 0) && (imageMartix[j - 1, i] == 255))
                            isblack = true; ;
                        if ((isblack) && (imageMartix[j, i] == 255))
                            num++;
                        if ((isblack) && (j > 0) && (imageMartix[j - 1, i] == 0))
                            break;
                    }
                DT12_feature[k] = num;
                step = step + 21;
            }
            //第12个
            isblack = false;
            for (i = 106, num = 0; i <= 128; i++)
                for (j = 93; j >= 0; j--)
                {
                    if ((imageMartix[j, i] == 0) && (j > 0) && (imageMartix[j - 1, i] == 255))
                        isblack = true; ;
                    if ((isblack) && (imageMartix[j, i] == 255))
                        num++;
                    if ((isblack) && (j > 0) && (imageMartix[j - 1, i] == 0))
                        break;
                }
            DT12_feature[11] = num;
            #endregion

            #region 从左面边缘提取
            i = 0; step = 14;//第13至第17个
            isblack = false;
            for (k = 12; k < 17; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 0; j < 129; j++)
                    {
                        if ((imageMartix[i, j] == 0) && (j < 128) && (imageMartix[i, j + 1] == 255))
                            isblack = true; ;
                        if ((isblack) && (imageMartix[i, j] == 255))
                            num++;
                        if ((isblack) && (j < 128) && (imageMartix[i, j + 1] == 0))
                            break;
                    }
                DT12_feature[k] = num;
                step = step + 15;
            }
            //第18个
            isblack = false;
            for (i = 75, num = 0; i <= 93; i++)
                for (j = 0; j < 129; j++)
                {
                    if ((imageMartix[i, j] == 0) && (j < 128) && (imageMartix[i, j + 1] == 255))
                        isblack = true; ;
                    if ((isblack) && (imageMartix[i, j] == 255))
                        num++;
                    if ((isblack) && (j < 128) && (imageMartix[i, j + 1] == 0))
                        break;
                }
            DT12_feature[17] = num;
            #endregion

            #region 从右面边缘提取
            i = 0; step = 14;//第19至第23个
            isblack = false;
            for (k = 18; k < 23; k++)
            {
                for (num = 0; i <= step; i++)
                    for (j = 128; j >= 0; j--)
                    {
                        if ((imageMartix[i, j] == 0) && (j > 0) && (imageMartix[i, j - 1] == 255))
                            isblack = true; ;
                        if ((isblack) && (imageMartix[i, j] == 255))
                            num++;
                        if ((isblack) && (j > 0) && (imageMartix[i, j - 1] == 0))
                            break;
                    }
                DT12_feature[k] = num;
                step = step + 15;
            }
            //第24个
            isblack = false;
            for (i = 75, num = 0; i <= 93; i++)
                for (j = 128; j >= 0; j--)
                {
                    if ((imageMartix[i, j] == 0) && (j > 0) && (imageMartix[i, j - 1] == 255))
                        isblack = true; ;
                    if ((isblack) && (imageMartix[i, j] == 255))
                        num++;
                    if ((isblack) && (j > 0) && (imageMartix[i, j - 1] == 0))
                        break;
                }
            DT12_feature[23] = num;
            #endregion
        }

        #endregion

        #region 初始Feature文件

        //private int[,] featureData;
        //private int[,] testData; //[48,48]
        //private string[,] dgview;// 1024,50
        //private double[] testFeature;//[48];//测试样本的属性
        //private string[] featureValue; // [50]

        //public int[,] FeatureData
        //{
        //    set { this.featureData = value; }
        //    get { return this.featureData; }
        //}

        //public int[,] TestData
        //{
        //    set { this.testData = value; }
        //    get { return this.testData; }
        //}

        //public string[,] Dgview
        //{
        //    set { this.dgview = value; }
        //    get { return this.dgview; }
        //}

        //public double[] TestFeature
        //{
        //    set { this.testFeature = value; }
        //    get { return this.testFeature; }
        //}

        //public string[] FeatureValue
        //{
        //    set { this.featureValue = value; }
        //    get { return this.featureValue; }
        //}

        #endregion

    }
}

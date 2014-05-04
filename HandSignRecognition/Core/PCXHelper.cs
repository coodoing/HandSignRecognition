using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Fireball.Drawing;
using HandSignRecognition.Core.Base;
using System.Drawing.Imaging;
using System.IO;

namespace HandSignRecognition.Core
{
    /// <summary> 
    /// Author:AirFly
    /// Date:12/5/2010 10:38:00 PM 
    /// Company:DCBI
    /// Copyright:2010-2013 
    /// CLR Version:4.0.30319.1 
    /// Blog Address:http://www.cnblogs.com/ttltry-air/
    /// Class1 Illustration: All rights reserved please do not encroach!   
    /// GUID:963d892f-4cc4-4c96-93e1-ef1a1133160b 
    /// Description：处理PCX图片类
    /// </summary>
    public class PCXHelper
    {
        #region 属性
        private static PCXImage pcxImage = new PCXImage();

        private byte[] imagebyte;

        public byte[] imageByte
        {
            set { imagebyte = value; }
            get { return imagebyte; }
        }

        #endregion

        #region 静态方法

        // 加载路径下的PCX图片
        public static void Load(string filename)
        {
            Image image = LoadPCX(filename);
            byte[] bmpdata = ImageToByteArray(image);
            if (!bmpdata.Equals(null))
            {
                FileStream testfile = new FileStream("temp-file.bin", FileMode.Create, FileAccess.Write);
                testfile.Write(bmpdata, 0, bmpdata.Length);
                testfile.Close();
            }
        }

        // 加载和查看PCX图片
        public static Image LoadPCX(string filename)
        {
            if (filename == null || filename.Equals(string.Empty))
            {
                return null;
            }
            else
            {
                // 还有一种读取PCX文件的方法就是：将图片放到内存中进行读取
                Image imageToSet = null;
                Fireball.Drawing.FreeImage image = new FreeImage(filename);
                imageToSet = image.GetBitmap();
                SetPCXImage(imageToSet, filename);
                return imageToSet;
                #region  另一种加载图片信息
                #region 若允许调试信息，则将该图片解码后的二进制数据写入临时文件，否则什么也不做，提高速度
                //if (common.ENABLE_DEBUG == true)
                //{
                //    bmpdata = mypcxPic.imageByte;
                //    if (!bmpdata.Equals(null))
                //    {
                //        FileStream testfile = new FileStream("temp-file.bin", FileMode.Create, FileAccess.Write);
                //        testfile.Write(bmpdata, 0, bmpdata.Length);
                //        testfile.Close();
                //    }
                //}
                #endregion

                #endregion
            }
        }

        // 设置初始化pcxImage, 用于二值化数据
        public static void SetPCXImage(Image image, string filename)
        {
            pcxImage.IsBinary = false;
            pcxImage.IsDraw = true;
            pcxImage.OldFilename = filename;
            pcxImage.PcxSize = image.Size;
            pcxImage.PcxData = new int[94, 129];
        }

        // 获取PCXImage bean类里面的数据
        public static PCXImage GetPCXImage(string filename)
        {
            //可以不把下面的代码注释掉，这样就很好的实现获取功能。
            //Image image = LoadPCX(filename);
            //PCXImage pcxImage = new PCXImage();
            //pcxImage.IsBinary = false;
            //pcxImage.IsDraw = true;
            //pcxImage.OldFilename = filename;
            //pcxImage.PcxSize = image.Size;
            //pcxImage.PcxData = new int[94,129];
            return pcxImage;
        }

        public static PCXImage GetPCXImage(PCXImage pcximage)
        {
            PCXImage pImage = new PCXImage();
            pImage.IsBinary = pcximage.IsBinary;
            pImage.IsDraw = pcximage.IsDraw;
            pImage.OldFilename = pcximage.OldFilename;
            pImage.PcxData = pcximage.PcxData;
            pImage.PcxSize = pcximage.PcxSize;
            return pImage;
        }

        // 查看签名
        public static Image ReviewPCX(string filename)
        {
            return LoadPCX(filename);
        }

        // 二值化数据
        public static string BinaryPCX(string filename)
        {
            Image imageToSet = null;
            Fireball.Drawing.FreeImage image = new FreeImage(filename);
            imageToSet = image.GetBitmap();
            //FileStream fs = new FileStream(imageToSet, FileMode.Open, FileAccess.Read);
            //Image img = Image.FromStream(fs, false, false);
            //img.Dispose();
            //fs.Close();
            Bitmap bmp = new Bitmap(imageToSet);
            int width = bmp.Width;
            int height = bmp.Height;
            // 把图像内容锁定到系统内存 这里我们使用的是Format24bppRgb,也就是24位色。
            // 在这种格式下3个字节表示一种颜色，也就是我们通常所知道的R,G,B， 所以每个字节表示颜色的一个分量
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //StringBuilder binaryStr = new StringBuilder(10000);
            string binaryStr = TraverseBmp(bmp, bmData);
            // unsafe的位置不能放颠倒，不然由C#进行图像处理的速度非常慢
            bmp.UnlockBits(bmData);
            bmp.Dispose();

            return binaryStr.ToString();
        }

        // 遍历Bitmap每个像素，并根据RGB值将黑色，白色用0，1表示
        public static string TraverseBmp(Bitmap bmp, BitmapData bmData)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            StringBuilder binaryStr = new StringBuilder(10000);

            #region unsafe代码

            unsafe
            {
                byte* p = (byte*)bmData.Scan0;
                int offset = bmData.Stride - width * 3; //only correct when PixelFormat is Format24bppRgb
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        // bmp已经锁定，所以下面代码会报错
                        //Color c = bmp.GetPixel(i, j);
                        //int r, g, b;
                        //r = c.R;
                        //g = c.G;
                        //b = c.B;
                        // 黑色的话就显0
                        if (p[0] == 255 && p[1] == 255 && p[2] == 255)
                        {
                            binaryStr.Append(1);
                        }
                        else
                            binaryStr.Append(0);
                        binaryStr.Append("  ");// 在这里频繁的使用连接字符串操作，使用StringBuilder类不用+进行string的连接
                        //binaryStr.Append("*");
                        p += 3;
                    }
                    p += offset;
                    binaryStr.Append("\n");
                }
            }

            #endregion

            #region 最初的遍历方式，错误原因在于：binaryStr每次移动的距离不是简单的5
            //for (int i = 0; i < imageToSet.Height; i += 5)
            //{
            //    for (int j = 0; j < imageToSet.Width; j += 5)
            //    {
            //        // binaryStr每次移动的距离不是简单的5，而是与像素RGB所占byte字节数有关
            //        string str = string.Empty;                    
            //        if (c == Color.Black)
            //        {
            //            str += 1;
            //        }
            //        else
            //            str += 0;
            //        binaryStr += str;
            //        binaryStr += "";
            //    }
            //    binaryStr += "*";
            //    binaryStr += "\n";
            //}
            //binaryStr.ToString();
            //pcxImage.IsDraw = false;
            //pcxImage.IsBinary = true;
            #endregion

            return binaryStr.ToString();
        }

        // 将PCX图片放大处理
        public static Image ScalePCX(string filename)
        {
            Image imageToSet = null;
            Fireball.Drawing.FreeImage image = new FreeImage(filename);
            imageToSet = image.Rescale(129 * 5, 94 * 5).GetBitmap();
            return imageToSet;
        }

        #region Image与byte[]转换
        /*
         Info:http://www.cnblogs.com/billow/archive/2006/12/09/587446.html
         */

        // 用System.Drawing.Image.Save方法把图片存为 memorystream.，然后内存流用MemryStrea类的ToArray()方法返回一个byte 数组
        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        // 使用Image类的Image.FromStream方法通过由byte数组参数创建的MemoryStream对象生成一个Image，并返回该image对象
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        #endregion

        #endregion

    }
}

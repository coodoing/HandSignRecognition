using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HandSignRecognition.Core.Base;
using System.Drawing;
using System.Collections.Generic;


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
    /// Description：选择PCX样本的类
    /// </summary>
    public class SelectedPCXHelper
    {

        #region 属性

        //不能再这里进行调用赋值，只能初始化
        private static SelectedPCXImage selPcxImage = new SelectedPCXImage();
        public static List<PCXImage> pcxlist = new List<PCXImage>();
        private static List<PCXImage> unsellist = new List<PCXImage>();

        #endregion

        #region 静态方法

        public static void SetSelectedPCXImage(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            DirectoryInfo[] ChildDirectory;//子目录集
            FileInfo[] files = dir.GetFiles();
            ChildDirectory = dir.GetDirectories("*.*"); //得到子目录集

            int number = 0;
            //if (pcxlist.Count > 20) // pcxlist.Count > 20 逻辑思维有错误，因为是递归调用
            //{
            //    pcxlist = new List<PCXImage>();
            //}
            foreach (FileInfo file in files)
            {
                // 首先是初始化，PCX图片是自己处理的对象
                if (file.Extension.ToLower().Equals(".pcx"))
                {
                    number += 1;
                    Image image = PCXHelper.LoadPCX(file.FullName);
                    PCXImage pcximage = PCXHelper.GetPCXImage(file.FullName);
                    PCXImage pImage = PCXHelper.GetPCXImage(pcximage);
                    pcxlist.Add(pImage); // 这里的一个错误就是就是直接添加pcxlist.Add(pcximage)

                    #region Linq to object
                    //var bindFileList = from fileList in userfilelist
                    //                   select new BindFile
                    //                   {
                    //                       IconPath = ImageIconBind(fileList.FileExtend),
                    //                       LastWriteTime = fileList.LastWriteTime,
                    //                       Length = fileList.Length + "b",
                    //                       Name = fileList.FullName,
                    //                       FullName = fileList.FullName,
                    //                       UploaderInfo = fileList.UploaderInfo
                    //                   };
                    #endregion

                    pcxlist.ToList();
                }
            }
            pcxlist = pcxlist.ToList();
            selPcxImage.DirPath = dirPath;
            selPcxImage.Number = number;
            selPcxImage.PcxList = pcxlist;

            //递归子目录
            foreach (DirectoryInfo dirInfo in ChildDirectory)
                SetSelectedPCXImage(dirInfo.FullName);

        }

        // 获取SelectedPCXImage对象
        public static SelectedPCXImage GetSelectedPCXImage(string dirPath)
        {
            return selPcxImage;
        }

        //进一步封装，获取初始PCXImage(不是最终16*20个训练样本)列表List
        public static List<PCXImage> GetPCXImageList(string dirPath)
        {
            SelectedPCXImage selpcx = GetSelectedPCXImage(dirPath);
            List<PCXImage> pcxList = selpcx.PcxList;
            return pcxList;
        }

        // 根据ListBox中最后的list列表，选择80%个训练样本        
        public static void SetSelPCXFromLB(List<PCXImage> pcxList, string dirPath)
        {
            int number = 0;
            pcxlist = new List<PCXImage>(); //保证每次设置前选择的样本list为空
            foreach (PCXImage pcx in pcxList)
            {
                pcxlist.Add(pcx);
            }
            pcxlist = pcxlist.ToList();
            selPcxImage.DirPath = dirPath;
            selPcxImage.Number = number;
            selPcxImage.PcxList = pcxlist;
        }

        // 获取最终选择的总数(80%)训练样本
        public static List<PCXImage> GetSelPCXFromLB()
        {
            return pcxlist;
        }


        public static void SetUnselPCXList(List<PCXImage> pcxList, string dirPath)
        {
            unsellist = new List<PCXImage>(); //保证每次设置前选择的样本list为空
            foreach (PCXImage pcx in pcxList)
            {
                unsellist.Add(pcx);
            }
            unsellist = unsellist.ToList();
        }

        // 获取最终选择的总数(80%)训练样本
        public static List<PCXImage> GetUnselPCXList()
        {
            return unsellist;
        }

        // IList与List之间的转换
        public static System.Collections.Generic.List<string> ListConvert(System.Collections.IList listObjects)
        {
            List<string> convertedList = new List<string>();
            foreach (object listObject in listObjects)
            {
                convertedList.Add((string)listObject);
            }
            return convertedList;
        }

        #endregion

    }
}

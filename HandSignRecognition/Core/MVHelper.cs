using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HandSignRecognition.Core.Base;
using System.Windows.Forms;

namespace HandSignRecognition.Core
{
    /// <summary> 
    /// Author:AirFly
    /// Date:12/6/2010 10:38:00 PM 
    /// Company:DCBI
    /// Copyright:2010-2013 
    /// CLR Version:4.0.30319.1 
    /// Blog Address:http://www.cnblogs.com/ttltry-air/
    /// Class1 Illustration: All rights reserved please do not encroach!   
    /// GUID:963d892f-4cc4-4c96-93e1-ef1a1133160b 
    /// Description：μ、∑ 相关类
    /// </summary>
    public class MVHelper
    {
        #region 数据Data
        private int classID = -1;
        private static ArrayList samplelist;//获取所有训练样本的特征list
        private static ArrayList classFeaturelist; //获取所属类别classID的训练样本数
        private static int featureDimension = -1;

        public static int FeatureDimension
        {
            get
            {
                #region 48维及降维后的5维
                //if (this.featureDimension == -1)
                //{
                //    this.featureDimension = Constant.dimension;
                //}
                #endregion
                if (samplelist != null && samplelist.Count != 0)
                {
                    featureDimension = ((Features)samplelist[0]).feature_vector.Length;
                }
                return featureDimension;
            }
        }

        #endregion

        #region 训练样本的set和get

        public static void SetSampleList(ArrayList arrlist)
        {
            samplelist = arrlist;
        }

        public static ArrayList GetSampleList()
        {
            return samplelist;
        }

        #endregion

        #region 静态常用方法

        // 从所有样本中classID=1的样本 Sample为Feature的集合
        public static Sample SampleCollectByClassID(int classId)
        {
            Sample sample = new Sample(); // 新建样本类

            #region 样本类初始化

            // 获取所有Feature样本
            samplelist = GetSampleList();//FeatureHelper.GetFeaturesList();
            sample.ClassID = classId;

            //采集样本类拥有的所有样本
            for (int i = 0; i < samplelist.Count; i++)
            {
                Features feature = (Features)samplelist[i];
                if (feature.classID == sample.ClassID)
                {
                    sample.ClassSampleList.Add(feature);
                }
            }

            #endregion

            #region  计算均值向量

            MatrixHelper meanVectorBuffer = new MatrixHelper(FeatureDimension, 1);

            for (int i = 0; i < sample.ClassSampleList.Count; i++)
            {
                Features feature = (Features)sample.ClassSampleList[i]; //获取该类一个样本

                MatrixHelper tempVector = new MatrixHelper(FeatureDimension, 1, feature.feature_vector);

                meanVectorBuffer += tempVector;
            }
            sample.MeanVector = meanVectorBuffer.Multiply(1.00 / sample.ClassSampleList.Count);

            #endregion

            #region 计算协方差矩阵

            MatrixHelper conMatrixBuffer = new MatrixHelper(FeatureDimension, FeatureDimension);

            for (int i = 0; i < sample.ClassSampleList.Count; i++)
            {
                Features feature = (Features)sample.ClassSampleList[i]; //获取该类一个样本

                MatrixHelper tempVector = new MatrixHelper(FeatureDimension, 1, feature.feature_vector);

                tempVector -= sample.MeanVector;

                MatrixHelper tempVector_T = tempVector.Transpose(); //转置

                conMatrixBuffer += (tempVector * tempVector_T);
            }
            sample.ConMatrix = conMatrixBuffer.Multiply(1.00 / sample.ClassSampleList.Count);

            #endregion

            return sample;
        }

        // 从训练样本中样本中找到所有的样本类:1,2,3,4......20
        public static ArrayList GetClassFeatureList(ArrayList arrlist)
        {
            //    if (classFeaturelist == null || classFeaturelist.Count == 0)
            //    {
            //samplelist = GetSampleList(); //这里造成了循环
            samplelist = arrlist;
            IList classIDList = new ArrayList();//1，2，3，4.....
            if (samplelist == null || samplelist.Count == 0)
            {
                MessageBox.Show("");
            }
            else
            {
                for (int i = 0; i < samplelist.Count; i++)  //从训练样本中找到所有的类标注
                {
                    Features feature = (Features)samplelist[i];
                    int classID = feature.classID;
                    if (!classIDList.Contains(classID))
                    {
                        classIDList.Add(classID);
                    }
                }
                classFeaturelist = new ArrayList();//
                for (int i = 1; i <= classIDList.Count; i++)   //将所有训练样本归类
                {
                    // 也可以将classFeatureList设置为Map对象，classID与对应的sample对应起来
                    classFeaturelist.Add(SampleCollectByClassID((int)classIDList[i - 1]));
                }
            }
            // }
            return classFeaturelist;// 结果是已经归类好的全部特征样本
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandSignRecognition.Core.Base;
using System.Collections;
using HandSignRecognition.Core;

namespace HandSignRecognition.Classifier
{
    /// <summary> 
    /// Author:AirFly
    /// Date:12/4/2010 10:38:00 PM 
    /// Company:DCBI
    /// Copyright:2010-2013 
    /// CLR Version:4.0.30319.1 
    /// Blog Address:http://www.cnblogs.com/ttltry-air/
    /// Class1 Illustration: All rights reserved please do not encroach!   
    /// GUID:963d892f-4cc4-4c96-93e1-ef1a1133160b 
    /// Description: 最近邻法
    /// </summary>
    public class Nearest
    {
        //结构体，记录一个测试样本和任意一个训练样本之间的欧式距离，同时记录训练样本的类别ID
        struct sampleDistance
        {
            public int classID;
            public double distance;
        };

        //简单构造函数
        public Nearest()
        {
        }

        //进行最近邻分量
        //输入参数testsamples为要进行测试的样本集,trainingSamples为训练样本集
        //k_value是设置的K参数
        public int Do_Nearest(Features myTestSample, ArrayList trainingSamples)
        {
            #region 数据Data

            int i, j = 0;
            int index = 0; //index用于最近邻
            int result = 0; // class分类结果
            Features myTrainingSample;

            #endregion
            

            #region trainingCount 初始化

            ArrayList trainlist = MVHelper.GetClassFeatureList(trainingSamples);
            int trainingCount = 0;
            if (trainlist.Count == 1) //只选择一类的话
            {
                trainingCount = 1;//((Sample)trainlist[0]).ClassSampleList.Count;
            }
            else
            {
                trainingCount = trainlist.Count;
            }

            #endregion

            if (trainingCount == 0 || myTestSample == null)
                return -1;

            #region 最近邻分类算法

            //建立用来记录当前测试样本到每个训练样本的距离以及对应的训练样本类别
            //sampleDistance[] myDistance = new sampleDistance[trainingCount];
            sampleDistance[] myDimetion = new sampleDistance[Constant.classnumber];
            //依次计算当前tmpsample样本与训练样本集中Wi类Ni个样本的欧式距离
            for (i = 0; i < trainingCount; i++)
            {
                Sample sample = (Sample)trainlist[i];
                int n_SampleNum = sample.ClassSampleList.Count;
                // 这里myDistance的初始化应该与n_SampleNum具体值进行动态改变
                sampleDistance[] myDistance = new sampleDistance[n_SampleNum];
                for (int k = 0; k < n_SampleNum; k++)
                {
                    myTrainingSample = (Features)sample.ClassSampleList[k];
                    myDistance[k].classID = myTrainingSample.classID;
                    myDistance[k].distance = MeasureDistance(myTestSample.feature_vector, myTrainingSample.feature_vector);
                }
                //当前tmpsample样本与训练样本集中Wi类Ni个样本的欧式距离
                Array.Sort(myDistance, CompareByDistance);
                //myDimetion[i] = myDistance[0]; //不能这么直接赋值
                myDimetion[i].classID = myDistance[0].classID;
                myDimetion[i].distance = myDistance[0].distance;
                //index += n_SampleNum;
            }

            #endregion


            #region myDimetion排序时机问题选择
            //上面已经得到了当前tmpsample样本与训练样本集中c个类之间的距离,下面对其排序（升序）
            // 考虑到如果myDimetion长度只为1的话，就会报错
            if (trainlist.Count == 1)
            {
                result = myDimetion[0].classID;
                return result;
            }
            else
            {
                Array.Sort(myDimetion, CompareByDistance);
                result = myDimetion[0].classID;
                return result;
            }

            #endregion

        }

        //特定的比较函数
        private static int CompareByDistance(sampleDistance x, sampleDistance y)
        {
            if (x.distance < y.distance)
                return -1;
            else if (x.distance == y.distance)
                return 0;
            else
                return 1;
        }

        //求两个样本之间的欧式距离
        //输入参数：（2个长度为n的一维数组，分别为这两个样本的n个特征分量）
        private Double MeasureDistance(double[] X, double[] Y)
        {
            if (X.Length != Y.Length)
                return -1;

            int i, len = X.Length;
            double m, sum = 0;
            for (i = 0; i < len; i++)
            {
                m = (X[i] - Y[i]) * (X[i] - Y[i]);
                sum += m;
            }
            return Math.Sqrt(sum);
        }

        // 求两样本之间的马氏距离
        private Double MeasureMahalanobisDistance(double[] x, double[] y)
        {
            double sum = 0;
            return Math.Sqrt(sum);
        }


    }
}

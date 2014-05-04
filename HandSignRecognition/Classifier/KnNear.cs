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
    /// Date:12/5/2010 10:38:00 PM 
    /// Company:DCBI
    /// Copyright:2010-2013 
    /// CLR Version:4.0.30319.1 
    /// Blog Address:http://www.cnblogs.com/ttltry-air/
    /// Class1 Illustration: All rights reserved please do not encroach!   
    /// GUID:963d892f-4cc4-4c96-93e1-ef1a1133160b 
    /// Description: Kn近邻分类器
    /// </summary>
    public class KnNear
    {
        //结构体，记录一个测试样本和任意一个训练样本之间的欧式距离，同时记录训练样本的类别ID
        struct sampleDistance
        {
            public int classID;
            public double distance;
        };
        //结构体，记录一个测试样本的类别ID，同时包括这个ID出现的次数（即该类的样本个数）
        struct classAndNumber
        {
            public int classID;
            public int Number;
        };

        //简单构造函数
        public KnNear()
        {

        }

        //进行K近邻分量
        //输入参数testsamples为要进行测试的样本集,trainingSamples为训练样本集
        //k_value是设置的K参数
        public int DoK_nearest(Features myTestSample, ArrayList trainingSamples, int k_value)
        {
            int i, j, m = 0, trainingCount = trainingSamples.Count;
            int old_classid = -1, new_classid = -1;
            int max = 0, result = 0;
            Features myTrainingSample;

            if (trainingCount == 0 || myTestSample == null)
                return -1;

            //建立用来记录当前测试样本到每个训练样本的距离以及对应的训练样本类别
            sampleDistance[] myDistance = new sampleDistance[trainingCount];

            //依次计算当前tmpsample样本与训练样本集中所有样本的欧式距离
            for (i = 0; i < trainingCount; i++)
            {
                myTrainingSample = (Features)trainingSamples[i];
                myDistance[i].classID = myTrainingSample.classID;
                myDistance[i].distance = MeasureDistance(myTestSample.feature_vector, myTrainingSample.feature_vector);
            }
            //上面已经得到了当前这个测试样本与训练样本集中每个样本的距离，下面对其排序（升序）
            Array.Sort(myDistance, CompareDistance);
            //扫描一遍(k次即可)，找出排好序的数组中前K个最小的距离样本中有多少不同的类  
            classAndNumber[] myclassAndNum = new classAndNumber[Constant.classnumber];
            ArrayList existedClassid = new ArrayList();
            for (i = 0; i < k_value; i++)
            {
                // 这是为了排除当只选择一个001样本类时候，k-value的值必须有效
                if (k_value > myDistance.Length)
                {
                    break; 
                }
                new_classid = myDistance[i].classID;
                if (old_classid != new_classid)
                {
                    if (!existedClassid.Contains(new_classid))
                    {
                        old_classid = new_classid;
                        myclassAndNum[m].classID = new_classid;
                        existedClassid.Add(new_classid);
                        m++;
                    }
                }
            }
            //上述循环完成后，这K个样本共包含m个类。找出其每个类的classid，同时统计该类包含的样本个数，找到样本个数最大的一个作为结果
            for (i = 0; i < m; i++)
            {
                myclassAndNum[i].Number = 0;
                for (j = 0; j < k_value; j++)
                {
                    if (myDistance[j].classID == myclassAndNum[i].classID)
                        myclassAndNum[i].Number++;
                }
                if (myclassAndNum[i].Number > max)
                {
                    max = myclassAndNum[i].Number;
                    result = myclassAndNum[i].classID;
                }
            }
            //此时，得到的result是对测试样本进行K近邻估计分类的结果（一个classid），max是测试样本的K个最近邻中占多数的类所含样本数
            return result;
        }

        //特定的比较函数
        private static int CompareDistance(sampleDistance x, sampleDistance y)
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



    }
}

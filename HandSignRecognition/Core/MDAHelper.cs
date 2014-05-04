using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HandSignRecognition.Core.Base;

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
    /// Description：降维处理函数
    /// </summary>
    public class MDAHelper
    {

        #region 数据Data

        private static MatrixHelper matrix;
        private static ArrayList sourceSampleList; //原始训练样本实例
        private static ArrayList mdSampleList;//降维后的训练样本

        #endregion

        #region 静态方法

        // 获取降维后的新矩阵
        public static MatrixHelper GetMatrix()
        {
            if (matrix == null)
            {

                #region 数据

                int sourceFeatureDimension = MVHelper.FeatureDimension; //从样本类工厂获取原始特征维数
                ArrayList sampleClassList = MVHelper.GetClassFeatureList(FeatureHelper.GetFeaturesList());//从样本类工厂获取所有样本类
                int num = 0;  //  记录所有样本个数
                MatrixHelper S_W = new MatrixHelper(sourceFeatureDimension, sourceFeatureDimension); //类内散布矩阵
                MatrixHelper S_B = new MatrixHelper(sourceFeatureDimension, sourceFeatureDimension); //类间散布矩阵
                MatrixHelper M_V = new MatrixHelper(sourceFeatureDimension, 1);                      //总体均值向量

                #endregion

                #region 计算 总类内散布矩阵 和 总均值向量

                for (int i = 0; i < sampleClassList.Count; i++)
                {
                    Sample sample = (Sample)sampleClassList[i];
                    int n_SampleNum = sample.ClassSampleList.Count; // 各类样本数
                    S_W += sample.ConMatrix.Multiply(n_SampleNum);    //类内散布矩阵 = 类协方差矩阵 * 类样本数;  
                    M_V += sample.MeanVector.Multiply(n_SampleNum);   //类向量和 = 类均值向量 * 类样本数
                    num += n_SampleNum;
                }
                M_V = M_V.Multiply(1.00 / num); //总体均值向量 = 所有向量和 / 向量个数

                #endregion

                #region 计算 类间散布矩阵

                for (int i = 0; i < sampleClassList.Count; i++)
                {
                    Sample sample = (Sample)sampleClassList[i];
                    int n_SampleNum = sample.ClassSampleList.Count;
                    MatrixHelper tempVector = sample.MeanVector - M_V;  // Mi - M
                    MatrixHelper tempMatrix = tempVector * tempVector.Transpose();  // ( Mi - M) * ( Mi - M)t
                    S_B += tempMatrix.Multiply(n_SampleNum); //类内散布矩阵 = 类协方差矩阵 * 类样本数
                }

                #endregion

                #region 计算最佳投影 W

                S_W.InvertGaussJordan(); // S_W 求逆
                MatrixHelper featureMatrix = S_W * S_B;
                double[] feaValue = new double[sourceFeatureDimension];
                MatrixHelper feaMatrix = new MatrixHelper(sourceFeatureDimension, sourceFeatureDimension);
                bool tag = featureMatrix.ComputeEvJacobi(feaValue, feaMatrix, 0.00000001); //计算 特征值 和 特征向量

                #endregion

                #region 对特征值排序

                ArrayList feaValueList = new ArrayList();
                for (int i = 0; i < feaValue.Length; i++)
                {
                    feaValueList.Add(feaValue[i]);
                }
                ArrayList feaValueSortList = new ArrayList(feaValueList);
                feaValueSortList.Sort(); //特征值排序

                #endregion

                #region 找出较大的k个特征值对应的特征向量组成 W

                int targetDimension = (int)Math.Ceiling(1.0 * num / sampleClassList.Count / 5 + 1); // 自动计算降维后的维数
                matrix = new MatrixHelper(sourceFeatureDimension, targetDimension);
                for (int i = sourceFeatureDimension - 1; i >= (sourceFeatureDimension - targetDimension); i--)
                {
                    double[] colVector = new double[sourceFeatureDimension];
                    feaMatrix.GetColVector(feaValueList.IndexOf(feaValueSortList[i]), colVector);
                    int j = sourceFeatureDimension - 1 - i;
                    for (int k = 0; k < sourceFeatureDimension; k++)
                    {
                        matrix[k, j] = colVector[k];
                    }
                }

                #endregion

            }
            return matrix;
        }

        // 将原始样本降维
        public static Features MDSample(Features sourceSample)
        {
            Features feature = new Features();
            feature.classID = sourceSample.classID;
            MatrixHelper W_T = GetMatrix().Transpose();
            feature.feature_vector = new double[W_T.Rows];
            MatrixHelper tempFeaVector = new MatrixHelper(sourceSample.feature_vector.Length, 1, sourceSample.feature_vector);
            (W_T * tempFeaVector).GetColVector(0, feature.feature_vector);
            return feature;
        }

        // 获取降维后新样本
        public static ArrayList GetMDSampleList()
        {
            int sourceFeatureDimension = Constant.dimension; //获取原始特征向量的维数
            sourceSampleList = FeatureHelper.GetFeaturesList(); ;
            int n_samepleNum = sourceSampleList.Count;
            MatrixHelper sourceSamples = new MatrixHelper(sourceFeatureDimension, n_samepleNum); //计算原始样本特征矩阵

            for (int i = 0; i < n_samepleNum; i++)
            {
                Features feature = (Features)sourceSampleList[i];
                for (int k = 0; k < sourceFeatureDimension; k++)
                {
                    sourceSamples[k, i] = feature.feature_vector[k];
                }
            }

            MatrixHelper W_T = GetMatrix().Transpose();
            MatrixHelper targetSamples = W_T * sourceSamples;  // S' = W * S

            mdSampleList = new ArrayList();
            for (int i = 0; i < n_samepleNum; i++)
            {
                Features feature = (Features)sourceSampleList[i];
                Features newSample = new Features();
                newSample.classID = feature.classID;
                newSample.feature_vector = new double[W_T.Rows];
                for (int k = 0; k < W_T.Rows; k++)
                {
                    newSample.feature_vector[k] = targetSamples[k, i];
                }
                mdSampleList.Add(newSample);
            }
            return mdSampleList;
        }

        #endregion

    }
}

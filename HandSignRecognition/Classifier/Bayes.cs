using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HandSignRecognition.Core;
using HandSignRecognition.Core.Base;

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
    /// Description: Bayes类
    /// </summary>
    public class Bayes
    {
        private IList classID_List = null;           //各类类标注
        private IList mean_Vector_List = null;        //各类均值向量
        private IList con_Matrix_Invert_List = null; //各类协方差矩阵的逆矩阵
        private IList con_Matrix_Det_List = null;    //各类协方差矩阵的行列式  
        private IList trainSampleList = null;       //贝叶斯分类器训练样本实例
        private static Bayes instance = null;

        public static Bayes GetInstance() //设计模式中的单例模式
        {
            if (instance == null)
            {
                instance = new Bayes();
            }
            return instance;
        }

        /*
         *  注入贝叶斯分类器训练学习的样本 
         *  
         * 在这里用IList与ArrayList的向上转型
         */
        public IList TrainSampleList
        {
            set
            {
                trainSampleList = value;
                this.mean_Vector_List = new ArrayList();
                this.classID_List = new ArrayList();
                this.con_Matrix_Det_List = new ArrayList();
                this.con_Matrix_Invert_List = new ArrayList();

                IList sampleClassList = MVHelper.GetClassFeatureList((ArrayList)trainSampleList); //从降维样本类工厂获取样本类
                for (int i = 0; i < sampleClassList.Count; i++) //学习各类 均值向量 和 协方差矩阵
                {
                    Sample sample = (Sample)sampleClassList[i];
                    MatrixHelper con_Matrix_Buffer = null;
                    classID_List.Add(sample.ClassID); //类标注
                    mean_Vector_List.Add(sample.MeanVector);//均值向量
                    con_Matrix_Buffer = new MatrixHelper(sample.ConMatrix);
                    con_Matrix_Det_List.Add(con_Matrix_Buffer.ComputeDetGauss());//类协方差矩阵行列式
                    con_Matrix_Buffer = new MatrixHelper(sample.ConMatrix); 
                    con_Matrix_Buffer.InvertGaussJordan();//逆
                    con_Matrix_Invert_List.Add(con_Matrix_Buffer); //类协方差矩阵
                }
            }
        }


        /*
         *  贝叶斯分类判决函数
         *  
         *  输入：待分类样本
         *  输出：样本所属类别
         */
        public int DecisionFunction(Features feature)
        {
            int classID = -2;
            double g_Value = Double.MaxValue;
            MatrixHelper xVector = new MatrixHelper(feature.feature_vector.Length, 1, feature.feature_vector);
            for (int i = 0; i < this.classID_List.Count; i++) // 每个类计算判决值
            {
                MatrixHelper x_m_vector = xVector - (MatrixHelper)this.mean_Vector_List[i]; // x-m
                MatrixHelper x_m_vector_T = x_m_vector.Transpose();
                MatrixHelper con_Matrix_Invert = (MatrixHelper)this.con_Matrix_Invert_List[i];
                double con_Matrix_Det = (double)this.con_Matrix_Det_List[i];
                double tempValue = ((x_m_vector_T * con_Matrix_Invert * x_m_vector)[0, 0]) + Math.Log(con_Matrix_Det);
                if (tempValue < g_Value)
                {
                    g_Value = tempValue;
                    classID = (int)this.classID_List[i];
                }
            }
            return classID;
        }
    }
}

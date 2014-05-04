using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HandSignRecognition.Core.Base
{
    /// <summary>
    /// 特征提取后的样本类
    /// 
    /// Sample中主要包含：每类训练样本的Feature列表；均值；方差
    /// </summary>
    public class Sample
    {
        #region 属性值

        //均值向量和协方差都是矩阵
        private int classID = -1;                      //类标示        
        private MatrixHelper meanVector = null;              //均值向量
        private MatrixHelper conMatrix = null;               //协方差矩阵
        private ArrayList classSampleList = new ArrayList();   // 具体每个类的样本集合，例如第一类001中样本的集合:001-001,001-002,...

        #endregion

        #region Get、Set方法

        public int ClassID
        {
            get { return classID; }
            set { classID = value; }
        }
        public MatrixHelper MeanVector
        {
            get { return meanVector; }
            set { meanVector = value; }
        }

        public MatrixHelper ConMatrix
        {
            get { return conMatrix; }
            set { conMatrix = value; }
        }

        public ArrayList ClassSampleList
        {
            get { return classSampleList; }
            set { classSampleList = value; }
        }

        #endregion

    }
}

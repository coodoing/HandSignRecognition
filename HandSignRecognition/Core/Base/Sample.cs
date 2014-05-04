using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HandSignRecognition.Core.Base
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
    /// Description: 特征提取后的样本类
    /// </summary>
    
    public class Sample
    {
        #region 属性值

        private int classID = -1;                      //类标示        
        private MatrixHelper meanVector = null;              //均值向量
        private MatrixHelper conMatrix = null;               //协方差矩阵
        private ArrayList classSampleList = new ArrayList();   // 具体每类样本集合

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

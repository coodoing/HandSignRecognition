using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandSignRecognition.Core.Base
{
    public class Matrixs
    {

        #region Matrix矩阵类属性
        private double[,] mean; //[20][48];//均值矩阵
        private double[,] cov;//[48][48];//协方差矩阵
        private double[,] covone;//[48][48];//归一化协方差矩阵
        private double[,] reversecov;//[48][48];////协方差矩阵的逆矩阵
        #endregion

    }
}

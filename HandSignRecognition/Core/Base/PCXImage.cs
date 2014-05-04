using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
    /// Description: PCX图片类
    /// </summary>
    public class PCXImage
    {
        #region 属性值

        private bool isBinary = false; //是否显示二进制数据
        private bool isDraw = false; //是否画图
        private string oldFilename;

        private Size pcxSize;
        private int[,] pcxData;

        #endregion

        #region  Set、Get方法

        public bool IsBinary
        {
            set { this.isBinary = value; }
            get
            {
                return this.isBinary;
            }
        }

        public bool IsDraw
        {
            set { this.isDraw = value; }
            get
            {
                return this.isDraw;
            }
        }

        public string OldFilename
        {
            set { this.oldFilename = value; }
            get
            {
                return this.oldFilename;
            }
        }

        public int[,] PcxData
        {
            set { this.pcxData = value; }
            get
            {
                return this.pcxData;
            }
        }

        public Size PcxSize
        {
            set { this.pcxSize = value; }
            get
            {
                return this.pcxSize;
            }
        }

        #endregion  

        #region  Info
        /*
         * //初始化等长二维数据
            int [,] ab1 = new int [2,3];//默认值为0;
            int [,] ab2 = new int [2,3]{{1,2,3},{4,5,6}};
            //初始化不等长二维数据
            int [][] abc = new int [2][];
            abc[0] = new int[]{1,2};
            abc[1] = new int[]{3,4,5,6};
         * 
         * **/
        #endregion
    }
}

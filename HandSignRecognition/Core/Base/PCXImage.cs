using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HandSignRecognition.Core.Base
{
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

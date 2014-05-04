using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// Description: 该类主要是操作训练样本PCX
    /// </summary>
    public class SelectedPCXImage
    {

        #region 属性值

        private List<PCXImage> pcxList;
        private string dirPath;
        private int number;

        #endregion

        #region Set、Get方法

        public List<PCXImage> PcxList
        {
            get { return this.pcxList; }
            set { this.pcxList = value; }
        }

        public string DirPath
        {
            get { return this.dirPath; }
            set { this.dirPath = value; }
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandSignRecognition.Core.Base
{
    /// <summary>
    /// 已选择的训练PCX列表
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

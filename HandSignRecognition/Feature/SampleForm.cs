using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HandSignRecognition.Core;
using HandSignRecognition.Core.Base;
using System.Collections;

namespace HandSignRecognition.Feature
{
    public partial class SampleForm : Form
    {

        #region Data数据

        private SelectedPCXImage selPcximage;
        private List<PCXImage> pcxlist;

        #endregion

        #region 窗口初始化

        public SampleForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件方法

        private void browerDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();
            fbdlg.Description = "选择要进行计算的目录";
            fbdlg.RootFolder = Environment.SpecialFolder.MyComputer;
            fbdlg.ShowNewFolderButton = true;
            DialogResult result = fbdlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.textDirPath.Text = fbdlg.SelectedPath;
                SelectedPCXHelper.pcxlist = new List<PCXImage>(); //每次单击浏览的时候都初始化
                SelectedPCXHelper.SetSelectedPCXImage(this.textDirPath.Text);
                //this.selPcximage = SelectedPCXHelper.GetSelectedPCXImage(this.textDirPath.Text);
                LoadLB(this.textDirPath.Text); //加载ListBox的数据
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (selectedSampleListBox.Items.Count == 0)
            {
                MessageBox.Show(this, "你没有选择训练样本，请先选择", "提示信息", MessageBoxButtons.OK);
            }
            else //设置开测试中80%的训练样本和20%测试样本
            {
                List<PCXImage> pcxList = new List<PCXImage>();
                int count = selectedSampleListBox.Items.Count;
                IList list = selectedSampleListBox.Items;
                List<string> strlist = (List<string>)SelectedPCXHelper.ListConvert(list);
                foreach (string str in strlist)
                {
                    string filename = string.Format(@"{0}", str);
                    Image image = PCXHelper.LoadPCX(filename);
                    PCXImage pcximage = PCXHelper.GetPCXImage(filename);
                    PCXImage pImage = PCXHelper.GetPCXImage(pcximage);
                    pcxList.Add(pImage);
                }
                pcxList = pcxList.ToList();
                SelectedPCXHelper.SetSelPCXFromLB(pcxList, textDirPath.Text);

                List<PCXImage> unselpcxList = new List<PCXImage>();
                IList unsellist = unSelSampleListBox.Items;
                List<string> unselstrlist = (List<string>)SelectedPCXHelper.ListConvert(unsellist);
                foreach (string str in unselstrlist)
                {
                    string filename = string.Format(@"{0}", str);
                    Image image = PCXHelper.LoadPCX(filename);
                    PCXImage pcximage = PCXHelper.GetPCXImage(filename);
                    PCXImage pImage = PCXHelper.GetPCXImage(pcximage);
                    unselpcxList.Add(pImage);
                }
                unselpcxList = unselpcxList.ToList();
                SelectedPCXHelper.SetUnselPCXList(unselpcxList, textDirPath.Text);
                this.Close();
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (selectedSampleListBox.SelectedIndex != -1)
            {
                this.unSelSampleListBox.Items.Add(this.selectedSampleListBox.SelectedItem.ToString());
                this.selectedSampleListBox.Items.Remove(this.selectedSampleListBox.SelectedItem);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (unSelSampleListBox.SelectedIndex != -1)
            {
                this.selectedSampleListBox.Items.Add(this.unSelSampleListBox.SelectedItem.ToString());
                this.unSelSampleListBox.Items.Remove(this.unSelSampleListBox.SelectedItem);
            }
        }

        #endregion

        #region 公共方法

        // 对测试样本和训练样本在ListBox中的初始化
        private void LoadLB(string path)
        {
            this.pcxlist = SelectedPCXHelper.GetPCXImageList(path);// 这是最初开始加载的pcxlist，不是移动变化后的
            selectedSampleListBox.Items.Clear();
            unSelSampleListBox.Items.Clear();
            // 在这里做一个radiobutton进行开闭测试的选择
            if (this.closeRadioButton.Checked)
            {
                Constant.openchecked = false;
                foreach (PCXImage pcx in pcxlist)
                {
                    selectedSampleListBox.Items.Add(pcx.OldFilename);
                    unSelSampleListBox.Items.Add(pcx.OldFilename);
                }
            }
            else
            {
                Constant.openchecked = true;
                foreach (PCXImage pcx in pcxlist)
                {
                    selectedSampleListBox.Items.Add(pcx.OldFilename);
                }
                Random ran = new Random();
                int count = pcxlist.Count;
                int testnum = (int)(count * Constant.testRate);

                #region 随机在每个类里面选择80%做训练样本，20%做测试样本

                #region 循环里面最好不要进行数值的初始化

                int p = count;
                int k = 0; //start
                int m = k + 20; //end

                #endregion

                for (int i = 0; i < testnum; i++)
                {
                    // 注意题意是：对每个人的手写签名，用其中80%的图像作为训练样本进行训练，用余下的20%的图像进行测试,所以这里的处理有个技巧性。
                    //int _index = ran.Next(0, count); //产生0到pcx总数的随机数，且count的数必须每次循环后减一，即count--。

                    int _index = ran.Next(k, m);

                    #region 数据移除
                    // 如果从训练样本中已经移除，则会出现_index大于剩余训练样本总和的情况，会报错。注意添加删除的顺序
                    //if (_index >= selectedSampleListBox.Items.Count)
                    //{
                    //    int number = selectedSampleListBox.Items.Count - 2;
                    //    selectedSampleListBox.Items.RemoveAt(number); //At 15
                    //    unSelSampleListBox.Items.Add(selectedSampleListBox.Items[number - 1]); //添加的是 14
                    //}
                    //else
                    //{
                    //    selectedSampleListBox.Items.RemoveAt(_index);
                    //    unSelSampleListBox.Items.Add(selectedSampleListBox.Items[_index - 1]);
                    //}                    
                    #endregion

                    int number = selectedSampleListBox.Items.Count;
                    // 顺序不能乱 先添加后删除
                    unSelSampleListBox.Items.Add(selectedSampleListBox.Items[_index]);
                    selectedSampleListBox.Items.RemoveAt(_index);
                    //count--;

                    // 20%做测试样本，80%做训练样本
                    if (selectedSampleListBox.Items.Count == p - 4) //说明移除四个
                        // 另外处理方式 unSelSampleListBox.Items.Count%4==0
                    {
                        p -= 4;// p变化
                        k += 16; //20-4                        
                        m = k + 20;
                    }
                    else
                    {
                        m--;
                    }

                }

                #endregion

            }


        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using HandSignRecognition.Core;
using HandSignRecognition.Classifier;
using HandSignRecognition.Core.Base;

namespace HandSignRecognition.Feature
{
    public partial class KFunctionForm : Form
    {

        #region Form初始化

        public KFunctionForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件处理

        private void KFunctionForm_Load(object sender, EventArgs e)
        {

            #region 数据初始化

            IList testSampleList = FeatureHelper.GetTestFeaturesList();        //获取原始测试            
            KnNear kn = new KnNear();










            //IDictionary泛型与IDictionary接口区别

            //每个元素都存储在 KeyValuePair<TKey, TValue> 对象中
            IDictionary<int, double> map = new Dictionary<int, double>();
            //每个元素都存储在DictionaryEntry 
            IDictionary dictionary = new Hashtable();
            //DictionaryEntry进行循环遍历




            ArrayList klist = new ArrayList();
            ArrayList ratelist = new ArrayList();

            #endregion

            #region 获取K和识别率

            for (int k = 1; k < 50; k += 2)
            {
                int testResult = 0;
                double correctCount = 0;
                double correctRate = 0.0;
                foreach (Features feature in testSampleList)
                {
                    testResult = kn.DoK_nearest(feature, FeatureHelper.GetFeaturesList(), k);
                    if (testResult > 0)
                    {
                        //检查结果是否正确
                        if (testResult == feature.classID)
                        {
                            correctCount++;
                        }
                    }
                }
                correctRate = (correctCount / Convert.ToDouble(testSampleList.Count)) * 100.0;
                map.Add(k, correctRate / 10);
                klist.Add(k);
                ratelist.Add(correctRate*2);
            }

            #endregion

            #region 显示点图

            PreviewFunctionImage(klist, ratelist);

            #endregion
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // 这里也可以创造画板
            Graphics g = e.Graphics;
        }

        #endregion

        #region 公共方法

        // 显示图片
        private void PreviewFunctionImage(IDictionary dictionary)
        {
            Bitmap bMap = new Bitmap(500, 500);
            Graphics gph = Graphics.FromImage(bMap);
            gph.Clear(Color.White);

            gph.TranslateTransform(100, 100);//移动坐标系到100，100，画蓝色矩形标记

            Point p = new Point(); //DrawLine(p,p1,p2) Pen p=Pens.red;(定义的红色的画笔)

            //foreach (KeyValuePair<string, string> kvp in openWith)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}",
            //        kvp.Key, kvp.Value);
            //}
        }

        // 画函数图
        private void PreviewFunctionImage(ArrayList klist, ArrayList ratelist)
        {
            Pen p = new Pen(Color.Red);
            Bitmap bMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gph = Graphics.FromImage(bMap);
            gph.Clear(Color.White);
            Point[] plist = new Point[klist.Count];
            if (klist.Count != ratelist.Count)
            {
                return;
            }
            else
            {
                int count = klist.Count;

                #region 直接画图
                //for (int i = 0; i < count-1; i++)
                //{
                //    int j = i + 1;

                //    Point point1 = new Point(Convert.ToInt32(klist[i]), Convert.ToInt32(ratelist[i]));
                //    Point point2 = new Point(Convert.ToInt32(klist[j]), Convert.ToInt32(ratelist[j])); //这里会出现超界的问题，解决方法是将循环的count-1
                //    gph.DrawLine(p, point1, point2);
                //}
                #region 直接操作Point[]
                //plist[i] = point1;
                //gph.DrawLines(p,plist);
                #endregion

                #endregion

                #region 在坐标系中画图

                #region 坐标系

                PointF cPt = new PointF(50, pictureBox1.Height - 40);//坐标原点    
                PointF[] xPt = new PointF[3]{
             new   PointF(cPt.Y+10,cPt.Y),
             new   PointF(cPt.Y,cPt.Y-4),
             new   PointF(cPt.Y,cPt.Y+4)};//X轴三角形    
                PointF[] yPt = new PointF[3]{
             new   PointF(cPt.X,cPt.X-10),
             new   PointF(cPt.X+4,cPt.X),
             new   PointF(cPt.X-4,cPt.X)};//Y轴三角形    
                gph.DrawString("K与K-近邻法识别率函数图", new Font("微软雅黑", 14),
                 Brushes.Black, new PointF(cPt.X + 12, cPt.X));//图表标题    
                //画X轴    
                gph.DrawLine(Pens.Black, cPt.X, cPt.Y, cPt.Y, cPt.Y);
                gph.DrawPolygon(Pens.Black, xPt);
                gph.FillPolygon(new SolidBrush(Color.Black), xPt);
                gph.DrawString("K值", new Font("宋体", 12),
                 Brushes.Black, new PointF(cPt.Y + 12, cPt.Y - 10));
                //画Y轴    
                gph.DrawLine(Pens.Black, cPt.X, cPt.Y, cPt.X, cPt.X);
                gph.DrawPolygon(Pens.Black, yPt);
                gph.FillPolygon(new SolidBrush(Color.Black), yPt);
                gph.DrawString("K-近邻法识别率", new Font("宋体", 12), Brushes.Black, new PointF(6, 7));

                #endregion

                #region 画函数图
                for (int i = 1; i <= count; i++)
                {
                    if (i < count - 1)
                    {
                        //X,Y轴坐标精度都为20
                        //画Y轴刻度
                        gph.DrawString((i * 10).ToString(), new Font("宋体", 11), Brushes.Black,
                         new PointF(cPt.X - 40, cPt.Y - i * 20 - 6));
                        gph.DrawLine(Pens.Black, cPt.X - 3, cPt.Y - i * 20, cPt.X, cPt.Y - i * 20); //画Y轴刻度线
                    }

                    //画X轴    
                    gph.DrawLine(Pens.Black, cPt.X + i * 20, cPt.Y, cPt.X + i * 20, cPt.Y + 4); //画刻度线
                    gph.DrawString(klist[i - 1].ToString(), new Font("宋体", 11), Brushes.Black,
                     new PointF(cPt.X + i * 20 - 5, cPt.Y + 10));//避免与X轴重合



                    float valuei1 = float.Parse(ratelist[i - 1].ToString());
                    //画点，用Ellipse代替点，极限
                    gph.DrawEllipse(Pens.Black, cPt.X + i * 20 - 1.5F, cPt.Y - valuei1 * 2 - 1.5F, 2, 2);
                    gph.FillEllipse(new SolidBrush(Color.Black), cPt.X + i * 20 - 1.5F, cPt.Y - valuei1 * 2 + 1.5F, 2, 2);

                    //画点上的数值
                    //gph.DrawString(string.Format("{0:F2}", valuei1), new Font("Arial", 11), Brushes.Black,
                    // new PointF(cPt.X + i * 20, cPt.Y - valuei1 * 2));
                    // 注意坐标系的变换
                    //画折线    
                    if (i > 1)
                        gph.DrawLine(Pens.Green, cPt.X + (i - 1) * 20, cPt.Y - (float.Parse(ratelist[i - 2].ToString())) * 2, cPt.X + i * 20, cPt.Y - valuei1 * 2);

                }
                #endregion

                #endregion

            }
            pictureBox1.Image = bMap;
            gph.Dispose();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandSignRecognition.Core
{
    /// <summary>
    /// 常量类
    /// </summary>
    public static class Constant
    {
        // 统一的提示信息
        public const string tipinfo = "";

        // 样本的特征维数
        public static readonly int dimension = 48;

        // 样本降维数
        public static readonly int rdDimension = 5;

        // 训练样本的比例
        public static readonly double trainRate = 0.8;

        // 测试样本的比率
        public static readonly double testRate = 0.2;

        // 设置调试开关（比如，允许调试时，读取文件时允许写入图片解码后的二进制临时文件等）
        public static bool ENABLE_DEBUG = false;

        // 测试得到的最近邻近邻正确率
        public static string n_Rate = string.Empty;

        // 测试得到的Kn近邻正确率
        public static string kn_Rate = string.Empty;

        // 测试得到的Bayes正确率
        public static string bayes_Rate = string.Empty;

        // 判断是否为开测试
        public static bool openchecked = false;

        // 类别总数
        public static int classnumber = 20;

        // 初始化kvalue
        public static int kvalue = 9;
    }
}

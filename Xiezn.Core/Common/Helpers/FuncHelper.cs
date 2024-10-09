using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xiezn.Core.Common.Helpers
{
    public static class FuncHelper
    {
        /// <summary>
        /// 文件目录如果不存在，就创建一个新的目录
        /// </summary>
        /// <param name="path"></param>
        public static void DicCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 图片转Base64编码
        /// </summary>
        /// <param name="imageFullName">图片完整路径</param>
        /// <returns></returns>
        public static string ImageToBase64(string imageFullName)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(imageFullName);
            return Convert.ToBase64String(imageArray);
        }

        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }

        /// <summary>
        /// 执行shell命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="strInput"></param>
        public static void ExecuteShell(string cmd, string strInput)
        {
            Process p = new Process();
            p.StartInfo.FileName = cmd;
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
            p.StandardInput.WriteLine(strInput);
            p.StandardInput.AutoFlush = true;
            string strOuput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }
    }
}

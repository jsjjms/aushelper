using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Robot.Debug
{
    public class ToFile : System.Web.UI.Page
    {
        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// 
        /// <returns></returns>
        public bool WriteTxt(string str)
        {
            return WriteTxt(str, "bugLog.txt");
        }
        /// <summary>
        /// 允许自定义保存的文件名
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool WriteTxt(string str, string fileName)
        {
            try
            {
                FileStream fs = new FileStream(Server.MapPath(fileName), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
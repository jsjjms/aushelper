using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Robot.Engine;

namespace Robot.i
{
    /// <summary>
    /// 调用小黄鸡功能
    /// </summary>
    public class robot_yc
    {
        private HTMLStrip strip;
        private const string RobotUrl = "";
        
        public robot_yc()
        {
            strip = new HTMLStrip();
        }

        public string getResponse(string msg)
        {
            string sb = "";
            sb = strip.GetURLContent(RobotUrl + System.Web.HttpUtility.UrlEncode(msg), Encoding.UTF8);
            return sb;
        }
    }
}
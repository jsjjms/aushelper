using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robot.Asterism
{
    public class Asterism
    {
        private string dt = "";
        private string cmd = "";
        private sinaAsterism sina;

        

        public enum DT_TYPE
        {
            DT_TODAY,
            DT_TOMORROW,
            DT_THISWEEK,
            DT_THISMONTH,
            DT_THISYEAR
        }
        private DT_TYPE type = DT_TYPE.DT_TODAY;

        public Asterism(List<string> P)
        {
            if (P.Count > 1)
            {
                dt = P[1];
                type = GetDTType();
            }

            cmd = P[0];

            sina = new sinaAsterism(type, cmd);
        }

        private DT_TYPE GetDTType()
        {
            DT_TYPE t = DT_TYPE.DT_TODAY;
            switch (dt)
            {
                case "今日":
                case "今天":
                    t = DT_TYPE.DT_TODAY;
                    break;
                case "明天":
                case "明日":
                    t = DT_TYPE.DT_TOMORROW;
                    break;
                /*
                case "本周":
                    t = DT_TYPE.DT_THISWEEK;
                    break;
                case "本月":
                    t = DT_TYPE.DT_THISMONTH;
                    break;
                case "今年":
                    t = DT_TYPE.DT_THISYEAR;
                    break;
                 */
            }
            return t;
        }
        /// <summary>
        /// 获得状态
        /// </summary>
        /// <returns></returns>
        public string GetStatus()
        {
            string status = "";
            if (cmd == "星座")
            {
                status = "请输入星座中文名称进行查询。\r\n比如： 天蝎，或者天蝎座";
            }
            else
            {
                status = sina.GetStatus();
            }
            return status;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Engine;
using System.Text;
using Robot.Database;

namespace Robot.Asterism
{

    /// <summary>
    /// 采集于新浪
    /// </summary>
    public class sinaAsterism
    {
        private Robot.Asterism.Asterism.DT_TYPE type;
        private string asterismStr = "";
        private string dt = "";
        private string title = "";

        //搜索数据库的关键字段
        private int id = 0;
        private int offset = 0;

        DataService ds;
        public sinaAsterism(Asterism.DT_TYPE t, string cmd)
        {
            type = t;
            offset = (t == Asterism.DT_TYPE.DT_TODAY ? 0 : (int)ASM_TYPE.ASM_NEXT_DAY_STATUS);
            title = cmd;
            asterismStr = GetAsterismStr(cmd);
            ds = new DataService();
        }


        public enum ASM_TYPE
        {
            ASM_ASTRO = 0,
            ASM_TAURUS,
            ASM_GEMINI,
            ASM_CANCER,
            ASM_LEO,
            ASM_VIRGO,
            ASM_LIBRA,
            ASM_SCORPIO,
            ASM_SAGITTARIUS,
            ASM_CAPRICORN,
            ASM_AQUARIUS,
            ASM_PISCES,
            /// <summary>
            /// 星座明日运程需要加上这个offset
            /// </summary>
            ASM_NEXT_DAY_STATUS
        };
        public string GetAsterismStr(string str)
        {
            string sb = "";
            switch (str)
            {
                case "白羊座":
                case "白羊":
                case "牧羊座":
                case "牧羊":
                    sb = "astro";
                    id = (int)ASM_TYPE.ASM_ASTRO;
                    break;
                case "金牛座":
                case "金牛":
                    sb = "taurus";
                    id = (int)ASM_TYPE.ASM_TAURUS;
                    break;
                case "双子座":
                case "双子":
                    sb = "gemini";
                    id = (int)ASM_TYPE.ASM_GEMINI;
                    break;
                case "巨蟹座":
                case "巨蟹":
                    sb = "cancer";
                    id = (int)ASM_TYPE.ASM_CANCER;
                    break;
                case "狮子座":
                case "狮子":
                    sb = "leo";
                    id = (int)ASM_TYPE.ASM_LEO;
                    break;
                case "处女座":
                case "处女":
                    sb = "virgo";
                    id = (int)ASM_TYPE.ASM_VIRGO;
                    break;
                case "天枰座":
                case "天枰":
                case "天平座":
                case "天平":
                    sb = "libra";
                    id = (int)ASM_TYPE.ASM_LIBRA;
                    break;
                case "天蝎座":
                case "天蝎":
                    sb = "scorpio";
                    id = (int)ASM_TYPE.ASM_SCORPIO;
                    break;
                case "射手座":
                case "射手":
                    sb = "sagittarius";
                    id = (int)ASM_TYPE.ASM_SAGITTARIUS;
                    break;
                case "魔蝎座":
                case "魔蝎":
                case "魔羯":
                case "魔羯座":
                case "摩羯":
                case "摩羯座":
                    sb = "capricorn";
                    id = (int)ASM_TYPE.ASM_CAPRICORN;
                    break;
                case "水瓶座":
                case "水瓶":
                    sb = "aquarius";
                    id = (int)ASM_TYPE.ASM_AQUARIUS;
                    break;
                case "双鱼座":
                case "双鱼":
                    sb = "pisces";
                    id = (int)ASM_TYPE.ASM_PISCES;
                    break;
            }
            id += offset;
            return sb;
        }
        /// <summary>
        /// 直接从数据库里取数据
        /// </summary>
        /// <returns></returns>
        public string GetStatus()
        {
            string sb = ds.GetAsterism(id);
            return sb;
        }
        /// <summary>
        /// 直接从网站上挖数据
        /// </summary>
        /// <returns></returns>
        public string GetStatus1()
        {
            string dtKey = "";
            string url = "http://vip.astro.sina.com.cn/astro/view/";
            switch (type)
            {
                case Asterism.DT_TYPE.DT_TOMORROW:
                    dtKey = "day";
                    title += "明日运势：\r\n";
                    string dtString = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    url += asterismStr + "/" + dtKey + "/" + dtString;
                    break;
                case Asterism.DT_TYPE.DT_THISWEEK:
                    dtKey = "weekly";
                    title += "本周运势：\r\n";
                    break;
                case Asterism.DT_TYPE.DT_THISMONTH:
                    dtKey = "monthly";
                    title += "本月运势：\r\n";
                    break;
                case Asterism.DT_TYPE.DT_THISYEAR:
                    dtKey = "year";
                    title += "今年运势：\r\n";
                    break;
                default:
                    dtKey = "day";
                    title += "今日运势：\r\n";
                    url += asterismStr + "/" + dtKey + "/";
                    break;
            }
            

            return Analytics(url);

        }
        /// <summary>
        /// 分析新浪返回的数据
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        private string Analytics(string url)
        {
            string s = "";
            HTMLStrip strip = new HTMLStrip();
            s = strip.stripHtml(strip.GetURLContent(url, Encoding.UTF8));
            int iStart = s.IndexOf("健康指数");
            s = s.Substring(iStart);
            string[] items = s.Split(' ');
            s = title;
            const int ITEM_LEN = 5;
            
            for (int i = 0; i < ITEM_LEN; i++)
            {
                switch (items[i].Substring(0, 4))
                {
                    case "健康指数":
                        s += "健康指数: " + items[i].Substring(4) + "\r\n";
                        break;
                    case "商谈指数":
                        s += "商谈指数: " + items[i].Substring(4) + "\r\n";
                        break;
                    case "幸运颜色":
                        s += "幸运颜色: " + items[i].Substring(4) + "\r\n";
                        break;
                    case "幸运数字":
                        s += "幸运数字: " + items[i].Substring(4) + "\r\n";
                        break;
                    case "速配星座":
                        s += "速配星座: " + items[i].Substring(4) + "\r\n";
                        break;    
                }
            }
            string data = "";
            for (int j = ITEM_LEN; j < items.Length; j++)
            {
                data += items[j];
            }
            data = data.Replace("&nbsp;", " ");
            s += data;
            return s;
        }
    }
}
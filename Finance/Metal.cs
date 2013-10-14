using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Engine;
using System.Text;

namespace Robot.Finance
{
    /// <summary>
    /// 抓取贵金属实时数据
    /// </summary>
    public class Metal
    {
        //http://live.wallstreetcn.com/livemarket see this
        struct m
        {
            public string name;
            public double price;
            public double maxPrice;
            public double minPrice;
        }

        private const string MetalUrl = "";

        private const int LEN = 30;
        public Metal()
        {
        }

        public string GetPrice()
        {
            HTMLStrip strip = new HTMLStrip();
            string sb = "";
            sb = strip.stripHtml(strip.GetURLContent(MetalUrl, Encoding.UTF8));
            string sGold = GetGold(sb);
            string sSilver = GetSilver(sb);
            sb = sGold + sSilver;

            return sb;
        }

        private string GetGold(string s)
        {
            const string name = "黄金";
            const int filedLen = 8;
            //format: Dec 131,317.201,318.901,314.90
            int start = s.IndexOf(name);
            s = s.Substring(start + name.Length, LEN);
            m metal = BuildStruct(s, name, filedLen);
            s = name + "\r\n当前价格:" + metal.price.ToString() + "\r\n最高价格:" + metal.maxPrice.ToString() + "\r\n最低价格:" + metal.minPrice.ToString()+"\r\n"; 
            return s;
        }

        private string GetSilver(string s)
        {
            const string name = "白银";
            const int filedLen = 6;
            //format: Dec 131,317.201,318.901,314.90
            int start = s.IndexOf(name);
            s = s.Substring(start + name.Length, LEN);
            m metal = BuildStruct(s, name, filedLen);
            s = name + "\r\n当前价格:" + metal.price.ToString() + "\r\n最高价格:" + metal.maxPrice.ToString() + "\r\n最低价格:" + metal.minPrice.ToString() + "\r\n";
            return s;
        }

        private m BuildStruct(string s,string name, int filedLen)
        {
            m metal = new m();
            metal.name = name;
            int iStart = 6;
            metal.price = double.Parse(s.Substring(iStart, filedLen));
            iStart += filedLen;
            metal.maxPrice = double.Parse(s.Substring(iStart, filedLen));
            iStart += filedLen;
            metal.minPrice = double.Parse(s.Substring(iStart, filedLen));

            return metal;
        }
    }
}
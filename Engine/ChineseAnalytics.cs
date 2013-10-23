using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net;
using Lucene.China;
using Lucene.Net.Analysis;
using System.IO;


namespace Robot.Engine
{
    public class ChineseAnalytics : System.Web.UI.Page
    {
        private string Chinese;
        List<string> items;
        public ChineseAnalytics(string str)
        {
            Chinese = str;
            items = new List<string>();
        }
        /// <summary>
        /// 将句子解析出来
        /// </summary>
        /// <returns></returns>
        public List<string> Parser()
        {
            items = new List<string>();
            Analyzer analyzer = new Lucene.China.ChineseAnalyzer(Server.MapPath("dll"));
            StringReader sr = new StringReader(Chinese);
            TokenStream stream = analyzer.TokenStream(null, sr);
            
            Token t = stream.Next();
            string t1 = "";
            while (t != null)
            {
                t1 = t.ToString();   //显示格式： (关键词,0,2) ，需要处理
                t1 = t1.Replace("(", "");
                char[] separator = { ',' };
                t1 = t1.Split(separator)[0];
                items.Add(t1);
                t = stream.Next();
            }
            return items;
        }
    }


}
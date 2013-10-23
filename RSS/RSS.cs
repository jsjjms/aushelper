using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Database;
using Robot.Engine;
using System.Xml;
using System.Text;
namespace Robot.RSS
{
    public class RSS
    {
        private const string RSS_Server = "";
        private const string RSS_Add = "";
        private const string RSS_Help = "";

        private string keyword = "";
        private const int MAX_NUM = 10;
        public RSS(List<string> Parameters)
        {
            if (Parameters.Count > 1)
            {
                keyword = Parameters[1];
            }
        }
        
        /// <summary>
        /// 返回summary
        /// </summary>
        /// <returns></returns>
        public string GetSummary()
        {
            string sb = "";
            if (keyword.Length == 0)
            {
                sb = "目前支持的RSS有： \r\n博客类：不许联想\r\n门户类：新足迹\r\n打折类：OZBargain\r\n其它：读书，电影，军事，和讯，时尚，笑话等；\r\n请输入 【rss 新足迹】，【rss 笑话】查看\r\n请<a href='" + RSS_Help + "'>点击查看</a>最新收录的RSS";
                return sb;
            }

            if (keyword.ToLower() == "add")
            {
                sb = "请点击<a href='" + RSS_Add + "'>这里</a>添加新的RSS!";
                return sb;
            }

            DataService ds = new DataService();
            Feed feed = ds.GetFeed(keyword);
            if (feed.url.Length == 0)
            {
                sb = "你搜索的RSS Feed不存在，请选择其他关键字，或者点击<a href='" + RSS_Add + "'>这里</a>查看如何添加您自己的RSS。";
            }
            else
            {
                sb = "已找到 " + feed.keyword + " 的RSS，请<a href='" + RSS_Server + "?keyword-mix=" + feed.keyword + "&feedUrl-mix=" + feed.url + "'>点击查看</a>";
            }
            //List<Feed> feeds = getAllFeeds(feed.url);
            //string header = feed.title + "的RSS：";
            //sb = GetSummary(MAX_NUM, feeds, header);
            return sb;
        }
        
    }
}
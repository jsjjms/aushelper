using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using Robot.Help;
using Robot.Debug;

namespace CommonService.Translation
{
    /// <summary>
    /// Youdao Translation, if requests more than 1000 times everyday, a request must be sent to youdao
    /// </summary>
    class YouDao
    {
        private const string key = "";

        private string transTxt = "";
        private ToFile toFile;

        public YouDao(List<string> p)
        {
            toFile = new ToFile();
            for (int i = 1; i < p.Count; i++)
            {
                transTxt += p[i] + " ";
            }
        }

        public string GetResult()
        {
            StringBuilder sb = new StringBuilder();
            if (transTxt == "")
            {
                Help help = new Help(Engine.CTYPE.C_TRANS);
                sb.Append("请输入你想翻译的内容\r\n" + help.getHelp());
                return sb.ToString();
            }

            XmlDocument document = new XmlDocument();
            
            document.Load("http://fanyi.youdao.com/openapi.do?keyfrom=faceoz&key=" + key + "&type=data&doctype=xml&version=1.1&q=" + System.Web.HttpUtility.UrlEncode(transTxt));
            XmlElement root = null;
            root = document.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/youdao-fanyi/errorCode");
            int errCode = 0;
            foreach (XmlNode n in nodes)
            {
                errCode = int.Parse(n.InnerText);
            }

            switch (errCode)
            {
                case 0:
                    nodes = root.SelectNodes("/youdao-fanyi/translation/paragraph");

                    foreach (XmlNode node in nodes)
                    {
                        sb.Append("翻译结果: \r\n");
                        sb.Append(node.InnerText + " \r\n");
                        break;
                    }
                    break;
                case 20:
                    sb.Append("你要翻译的文本过长！");
                    break;
                case 30:
                    sb.Append("要翻译的文本过长");
                    break;
                case 40:
                    sb.Append("不支持的语言类型");
                    break;
                case 50:
                    sb.Append("软件错误!");
                    break;
            }

            return sb.ToString();
        }
    }
}

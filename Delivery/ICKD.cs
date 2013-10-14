using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;

namespace Robot.Delivery
{
    /// <summary>
    /// 爱查快递接口
    /// </summary>
    public class ICKD
    {
        private const string ExpressAPIKey = "";
        private const string ExpressSecret = "";
        private string id = "";
        private string companyCode  = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Express No</param>
        /// <param name="code">Company Code</param>
        public ICKD(string key, string code)
        {
            id = key;
            companyCode = code;
        }

        public ICKD(string key, CommonService.Engine.CTYPE type)
        {
            id = key;
            switch (type)
            {
                case CommonService.Engine.CTYPE.C_DELIVERY_AAE:
                    companyCode = "aae";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_ST:
                    companyCode = "shentong";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_EMS:
                    companyCode = "ems";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_SF:
                    companyCode = "shunfeng";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_YT:
                    companyCode = "yuantong";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_ZT:
                    companyCode = "zhongtong";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_GHX:
                    companyCode = "pingyou";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_TT:
                    companyCode = "tiantian";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_YD:
                    companyCode = "yunda";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_HT:
                    companyCode = "huitong";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_QF:
                    companyCode = "quanfeng";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_CG:
                    companyCode = "chengguang";
                    break;
                case CommonService.Engine.CTYPE.C_DELIVERY_ZJS:
                    companyCode = "zhaijisong";
                    break;
            }
        }

        public string GetStatus()
        {
            string url = "http://api.ickd.cn/?id=" + ExpressAPIKey + "&secret=" + ExpressSecret + "&com=" + companyCode + "&nu=" + id + "&type=xml&encode=utf8&ord=asc";
            string sb = "";
            XmlDocument document = new XmlDocument();

            document.Load(url);
            XmlElement root = null;
            root = document.DocumentElement;

            XmlNodeList nodes = root.SelectNodes("/response/status");
            int errCode = 0;
            foreach (XmlNode n in nodes)
            {
                errCode = int.Parse(n.InnerText);
            }

            if (errCode == 0)
            {
                sb = "国内快递无法找到该单号";
            }
            else
            {
                List<string> dt = new List<string>();
                nodes = root.SelectNodes("/response/data/item/time");
                foreach (XmlNode n in nodes)
                {
                    dt.Add(n.InnerText);
                }
                List<string> context = new List<string>();
                nodes = root.SelectNodes("/response/data/item/context");
                foreach (XmlNode n in nodes)
                {
                    context.Add(n.InnerText);
                }

                for (int i = 0; i < dt.Count; i++)
                {
                    sb += "[" + dt[i] + "]" + context[i] + "[中国]\r\n";
                }
            }
            return sb;
        }
    }
}
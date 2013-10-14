using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonService.Engine;

namespace Robot.Help
{
    /// <summary>
    /// 全局的帮助
    /// </summary>
    public class Help
    {
        CTYPE t = CTYPE.C_HELP;

        public Help(CTYPE type)
        {
            t = type;
        }

        /// <summary>
        /// return all help strings
        /// </summary>
        /// <returns></returns>
        public string getHelp()
        {
            string str = "";
            switch (t)
            {
                case CTYPE.C_MAP:
                    str = MapHelp();
                    break;
                case CTYPE.C_TRANS:
                    str = TransHelp();
                    break;
                case CTYPE.C_WEATHER:
                    str = WeatherHelp();
                    break;
                case CTYPE.C_YP:
                    str = ypHelp();
                    break;
                case CTYPE.C_CURRENCY:
                    str = currencyHelp();
                    break;
                case CTYPE.C_DELIVERY:
                    str = kxDeliveryHelp();
                    break;
                default:
                    str = MainHelp();
                    break;
            }
            return str;
        }
        /// <summary>
        /// 全局帮助
        /// </summary>
        private string MainHelp()
        {
            string helpStr = "按下列格式发送指令给【澳洲生活帮手】\r\n查询油价: 【fuel mel】或者【fuel syd】\r\n翻译：【tr 需要被翻译的单词或者句子】\r\n天气: 【w】 \r\n 汇率： 【hl 中文国家名字，如澳洲】\r\n星座: 直接输入星座的中文名，比如【天蝎】，或者【天蝎座】。查询明日请输入 【天蝎 明日】（注意中间必须输入空格）\r\n地图: 【map 地址或者坐标】";

            return helpStr;
        }

        private string MapHelp()
        {
            string helpStr = "发送【map St peter girls, adelaide】可以搜索 St peter girls这个学校的地址，以及GPS坐标\r\n map 25.34566,-138.2322可以搜索这个坐标上对应的地址";
            return helpStr;
        }

        private string TransHelp()
        {
            string helpStr = "tr|translate [中文或者英文] 会直接返回翻译结果，比如 发送【tr 草泥马】会返回草泥马的英文";
            return helpStr;
        }

        private string WeatherHelp()
        {
            string helpStr = "w [adelaide|melbourne|sydney] 会返回城市的天气预报。\r\n比如 【w adelaide】返回阿德莱德的天气情况";
            return helpStr;
        }

        private string ypHelp()
        {
            string helpStr = "yp [商家信息] 返回相关商家以及联系方式。\r\n比如 \"yp 印度咖喱\" 返回和印度咖喱有关的餐厅或者商家";
            return helpStr;
        }

        private string currencyHelp()
        {
            string helpStr = "hl [国家] 返回该国家的汇率。 \r\n hl 比如 【hl 澳洲】 返回澳币当前的现汇买入价，现钞买入价，现汇卖出价，现钞卖出价，中间价";
            return helpStr;
        }

        private string kxDeliveryHelp()
        {
            string helpStr = "开心快线查询方法 发送：kx +单号。 比如 【kx R67802001E】";
            return helpStr;
        }
    }
}
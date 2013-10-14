using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonService.Engine;
using CommonService.Weather;
using CommonService.Google;
using CommonService.Translation;
using Robot.Debug;
using Robot.Help;
using Robot.YellowPage;
using Robot.CURRENCY;
using Robot.Delivery;
using Robot.i;
using Robot.Finance;
using Robot.Fuel;
using System.Text.RegularExpressions;
namespace CommonService
{
    /// <summary>
    /// According to message and user id, response the relevant message
    /// </summary>
    public class MessageService
    {
        private ToFile toFile;

        string userId = "";
        string msg = "";
        private List<string> Parameters;
        /// <summary>
        /// Valid check
        /// </summary>
        private ValidCheck vc;
        /// <summary>
        /// Category
        /// </summary>
        private category cg;
        /// <summary>
        /// Youdao Translation
        /// </summary>
        private YouDao yd;
        private yp_oversea yp;
        /// <summary>
        /// Instance of weather component
        /// </summary>
        private CommonService.Weather.Weather w;

        private const string WELCOME = "欢迎使用 澳洲生活帮手，我们为您提供最实用的信息和工具：\r\n1. 翻译： 发送 【tr 需要翻译的信息】， 生活帮手会为您实时翻译 \r\n2: 查询天气： 发送 【w】, 生活帮手会被您查询澳洲所有主要城市的天气温度情况 \r\n3查询油价： 发送【fuel mel】或者【fuel syd】 \r\n更多帮助，请发送 help ";
        /// <summary>
        /// Text message: UserId, UserMessage, MessageType
        /// Event       : EventKey, WXEvent, MessageType
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="uMsg"></param>
        /// <param name="msgType"></param>
        public MessageService(string uId, string uMsg, string msgType)
        {
            toFile = new ToFile();

            switch (msgType)
            {
                case "text":
                    userId = uId;
                    msg = uMsg.ToLower();
                    if (msg.Length > 0)
                    {
                        Parameters = new List<string>();
                        InitParameters();

                        vc = new ValidCheck(uId, Parameters);
                        cg = new category(Parameters);
                    }
                    break;
                case "event":
                    toFile.WriteTxt("Write data to event");
                    msg = uMsg.ToLower();
                    break;
            }
        }
        /// <summary>
        /// Split the parameters
        /// </summary>
        private void InitParameters()
        {
            //add 14/10/2013 系统需要过滤所有标点符号
            string pattern = @"[,.;:'!【】{}\[\]]";
            msg = Regex.Replace(msg, pattern, " ");
            string[] p = msg.Split(' ');
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].Trim().Replace("\r", "").Replace("\n", "").Length != 0 )
                {
                    Parameters.Add(p[i]);
                }
            }
        }

        /// <summary>
        /// Center to handler all incoming text messages
        /// </summary>
        /// <returns></returns>
        public string MsgCenterHandler()
        {
            if (msg.Length <= 0)
            {
                Help help = new Help(CTYPE.C_HELP);
                return help.getHelp();
            }

            StringBuilder sb = new StringBuilder();

            if (vc.p.bInBlacklist)
            {
                sb.Append("You are in the blacklist");
                return sb.ToString();
            }
            CTYPE t = cg.GetCategoryType(Parameters[0]);

            //Trap for delivery
            if (t >= CTYPE.C_DELIVERY_START && t <= CTYPE.C_DELIVERY_END)
            {
                Delivery delivery = new Delivery(t, Parameters);
                return delivery.GetStatus();
            }

            //trap for 星座
            if (t >= CTYPE.C_XZ_START && t <= CTYPE.C_XZ_END)
            {
                Robot.Asterism.Asterism asterism = new Robot.Asterism.Asterism(Parameters);
                return asterism.GetStatus();
            }

            switch (t)
            {
                case CTYPE.C_WEATHER:
                    if (Parameters.Count > 1)
                    {
                        w = new CommonService.Weather.Weather(Parameters[1]);
                    }
                    else
                    {
                        w = new CommonService.Weather.Weather();
                    }
                    sb.Append(w.GetWeather());
                    break;
                case CTYPE.C_MAP:
                    map m = new map(Parameters);
                    sb.Append(m.GetData());
                    break;
                case CTYPE.C_SEARCH:
                    //sb.Append("Not sure what you are looking for!");
                    break;
                case CTYPE.C_YP:
                    yp = new yp_oversea(Parameters);
                    break;
                case CTYPE.C_TRANS:
                    yd = new YouDao(Parameters);
                    sb.Append(yd.GetResult());
                    break;
                case CTYPE.C_HELP:
                    Help help = new Help(CTYPE.C_HELP);
                    sb.Append(help.getHelp());
                    break;
                case CTYPE.C_SAVE:
                    break;
                case CTYPE.C_MEM:
                    break;
                //Grab currency data from china bank
                case CTYPE.C_CURRENCY:
                    ChinaBank cb = new ChinaBank(Parameters);
                    sb.Append(cb.GrabData());
                    break;
                //贵金属
                case CTYPE.C_METAL:
                    Metal metal = new Metal();
                    sb.Append(metal.GetPrice());
                    break;
                case CTYPE.C_SUGGESTION:
                    Suggestion sug = new Suggestion(userId, Parameters, DateTime.Now.ToString());
                    sb.Append(sug.SaveSug());
                    break;
                case CTYPE.C_REPEAT:
                    break;
                case CTYPE.C_FUEL_PRICE:
                    FuelPrice price = new FuelPrice(Parameters);
                    sb.Append(price.GetPrice());
                    break;
                default:
                    sb.Append("亲爱的用户，你所输入的内容我们尚未能识别，如果您对该部分内容感兴趣，可以发送 【建议  您的建议内容，您的联系方式】给我们，我们的开发人员会尽力为您开发该功能。请发送 【help】查询我们已经开发好的功能。（发送时请不要带大括号）");
                    break;
            }
            return sb.ToString();
        }
        /// <summary>
        /// Basically handler all sub, and unsub events
        /// </summary>
        public string EventMsgHander()
        {
            StringBuilder sb = new StringBuilder();
            string eventMessage = msg;
            switch (eventMessage)
            {
                case "subscribe":
                    sb.Append(WELCOME);
                    break;
                case "unsubscribe":
                    break;
                case "CLICK":
                    break;
            }
            return sb.ToString();
        }
    }

    
}

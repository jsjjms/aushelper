using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Engine;
using System.Text;

namespace Robot.Delivery
{
    /// <summary>
    /// 快递集中点
    /// </summary>
    public class Delivery
    {
        private CommonService.Engine.CTYPE type;
        private string id = "";

        private string cmd = "";

        public Delivery(CommonService.Engine.CTYPE t, List<string> P)
        {
            type = t;
            cmd = P[0];
            if (P.Count > 1)
            {
                id = P[1];
            }
        }

        public string GetStatus()
        {
            if (id == "")
            {
                return "请输入快递单号! \r\n比如: " + cmd + " R67802001E";
            }

            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                 //for happylink
                case CommonService.Engine.CTYPE.C_DELIVERY:
                    HappyLink hl = new HappyLink(id);
                    sb.Append(hl.GrabStatus());
                    break;
                default:
                    ICKD ickd = new ICKD(id, type);
                    sb.Append(ickd.GetStatus());
                    break;
            }

            return sb.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Robot.Help;

namespace CommonService.Google
{
    class map
    {
        private GetAddressFromCoordinates getAddress;
        private GetCoordinatesFromAddress getCoordinates;

        public enum G_MAP
        {
            G_Unknown,
            G_GetCoordinates,
            G_GetAddress
        }

        private G_MAP gMapType = G_MAP.G_Unknown;
        private string uMsg = "";


        public map(List<string> msg)
        {
            if (msg.Count <= 1)
            {
                gMapType = G_MAP.G_Unknown;
            }
            else
            {
                for (int i = 1; i < msg.Count; i++ )
                    uMsg += msg[i]+" ";

                gMapType = CheckInputType();
            }
        }
        /// <summary>
        /// Smart check the input type
        /// </summary>
        /// <returns></returns>
        private G_MAP CheckInputType()
        {
            G_MAP type = G_MAP.G_Unknown;
            /*
             * Rules:
             * Coordinates must be separated by ',' and 
             * */
            string tmpMsg = uMsg.Replace('+', '0').Replace('-', '0').Replace(' ', '0').Replace(',', '0').Replace('.', '0') ;
            if (Regex.IsMatch(tmpMsg, @"^[0-9]+$"))
            {
                type = G_MAP.G_GetAddress;
            }
            else
            {
                type = G_MAP.G_GetCoordinates;
            }
            return type;
        }
        /// <summary>
        /// Get data accordingly.
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            StringBuilder sb = new StringBuilder();
            switch (gMapType)
            {
                case G_MAP.G_Unknown:
                    Help help = new Help(Engine.CTYPE.C_MAP);
                    sb.Append(help.getHelp());
                    break;
                case G_MAP.G_GetAddress:
                    getAddress = new GetAddressFromCoordinates();
                    sb.Append(getAddress.GetAddress(uMsg.Replace(" ", "")));
                    break;
                case G_MAP.G_GetCoordinates:
                    getCoordinates = new GetCoordinatesFromAddress();
                    sb.Append(getCoordinates.GetCoordinates(System.Web.HttpUtility.UrlEncode(uMsg.Trim())));
                    break;
            }
            return sb.ToString();
        }
    }
}

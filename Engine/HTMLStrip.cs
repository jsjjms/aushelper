using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Robot.Engine
{
    public class HTMLStrip
    {
        /// <summary>
        /// Get content from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="EncodingType"></param>
        /// <returns></returns>
        public string GetURLContent(string url, Encoding EncodingType)
        {
            string str = "";
            WebResponse oWebRps = null;
            WebRequest oWebRqst = WebRequest.Create(url);
            oWebRqst.Timeout = 5000;
            oWebRps = oWebRqst.GetResponse();
            if (oWebRps != null)
            {
                StreamReader oStreamRd = new StreamReader(oWebRps.GetResponseStream(), Encoding.UTF8);
                str = oStreamRd.ReadToEnd();
                oStreamRd.Close();
                oWebRps.Close();
            }
            return str;
        }

        public string stripHtml(string strHtml)
        {
            Regex objRegExp = new Regex("<(.|\n)+?>");
            string strOutput = objRegExp.Replace(strHtml, "");
            strOutput = strOutput.Replace("<", "&lt;");
            strOutput = strOutput.Replace(">", "&gt;");

            Regex r = new Regex(@"\s+");
            strOutput = r.Replace(strOutput, " ");
            strOutput.Trim();
            return strOutput;
        }
    }
}
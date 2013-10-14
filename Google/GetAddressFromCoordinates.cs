using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CommonService.Google
{
    class GetAddressFromCoordinates
    {
        public string GetAddress(string msg)
        {
            StringBuilder sb = new StringBuilder();

            XmlDocument document = new XmlDocument();
            string url = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + msg + "&sensor=true";
            document.Load(url);
            XmlElement root = null;
            root = document.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/GeocodeResponse/status");
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText != "OK")
                {
                    sb.Append("Unknown Coordinates!");
                }
                break;
            }
            sb.Append("Address is: \r\n");
            nodes = root.SelectNodes("/GeocodeResponse/result/formatted_address");

            foreach (XmlNode node in nodes)
            {
                sb.Append("Address is: \r\n");
                sb.Append(node.InnerText+ " \r\n");
                break;
            }

            sb.Append("Coordinates: \r\n");
            nodes = root.SelectNodes("/GeocodeResponse/result/geometry/location/*");
            for (int i = 0; i < 2; i++ )
            {
                sb.Append(nodes[i].Name + ": " + nodes[i].InnerText + " \r\n");
            }
            return sb.ToString();
        }
    }
}

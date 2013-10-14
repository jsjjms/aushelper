using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CommonService.Google
{
    class GetCoordinatesFromAddress
    {
        public string GetCoordinates(string msg)
        {
            StringBuilder sb = new StringBuilder();

            XmlDocument document = new XmlDocument();
            document.Load("http://maps.googleapis.com/maps/api/geocode/xml?address=" + msg + "&sensor=true");
            XmlElement root = null;
            root = document.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/GeocodeResponse/status");
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText != "OK")
                {
                    sb.Append("Unknown Address!");
                }
                break;
            }
            sb.Append("Address is: \r\n");
            nodes = root.SelectNodes("/GeocodeResponse/result/formatted_address");
            foreach (XmlNode node in nodes)
            {
                sb.Append(node.InnerText + " \r\n");
                break;
            }
            
            sb.Append("Coordinates: \r\n");
            nodes = root.SelectNodes("/GeocodeResponse/result/geometry/location/*");
            for (int i = 0; i < 2; i++)
            {
                sb.Append(nodes[i].Name + ": " + nodes[i].InnerText + " \r\n");
            }

            return sb.ToString();
        }
    }
}

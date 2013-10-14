using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Robot.YellowPage
{
    /// <summary>
    /// use oversea yellow page web service
    /// </summary>
    public class yp_oversea
    {
        private List<string> SearchKey;

        public yp_oversea(List<string> p)
        {
            SearchKey = p;
        }

        /// <summary>
        /// Return data from web service
        /// </summary>
        /// <returns></returns>
        public string Search()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

    }
}
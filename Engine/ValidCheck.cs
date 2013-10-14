using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonService.People;

namespace CommonService.Engine
{
    /// <summary>
    /// Check this person's personality, behaviour, hobby and so on
    /// </summary>
    class ValidCheck
    {
        public people p;

        public ValidCheck(string id, List<string> msg)
        {
            p = new people(id);
        }
        /// <summary>
        /// Fully check this user
        /// </summary>
        /// <returns></returns>
        private people check()
        {
            return p;
        }
    }
}

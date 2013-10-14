using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonService.People
{
    /// <summary>
    /// Define the characters of a person
    /// </summary>
    class people
    {
        /// <summary>
        /// Person's ID in our system
        /// </summary>
        public int key;
        /// <summary>
        /// Is this one in blacklist?
        /// </summary>
        public bool bInBlacklist;
        /// <summary>
        /// If this person is in the blacklist, when will be released?
        /// </summary>
        public DateTime release;
        private TimeSpan tl;
        public TimeSpan TimeLeft
        {
            get
            {
                return (DateTime.Now - release);
            }
        }
        /// <summary>
        /// When sort of hobbies this guy has
        /// </summary>
        public List<Addiction> hobby;
        /// <summary>
        /// What sort of temp hobbies this guy has
        /// </summary>
        public List<Addiction> tmpHobby;
        /// <summary>
        /// What sort of hobbies this guy hates
        /// </summary>
        public List<Addiction> hobbyBlackList;
        /// <summary>
        /// Load this people by ID
        /// </summary>
        /// <param name="id"></param>
        public people(string id)
        {
        }
    }
}

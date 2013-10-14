using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robot.Engine
{
    /// <summary>
    /// Define all data types
    /// </summary>

    //Univerval message type
    public class csMsg
    {
        public int key;
        public string content;     //msg content
        public DateTime dt;        //date and time when msg saved
        public int points;         //points of this message, basically the times this message was viewed
        public string msgType;
        public string sender;
        public byte attr;          //public : 1, private: 2
    }
}
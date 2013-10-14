using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Debug;
using System.Configuration;
using Robot.Database;

namespace Robot.Help
{
    /// <summary>
    /// 用户可以发送建议给 机器人
    /// </summary>
    public class Suggestion
    {
        private ToFile tofile;
        private SaveSuggestions sug;
        private string senderId = "";
        private string msg = "";
        private DateTime sendDT;
        public Suggestion(string id, List<string> uMsg, string dt)
        {
            sug = new SaveSuggestions();
            senderId = id;
            if (uMsg.Count > 1)
            {
                for (int i = 1; i < uMsg.Count; i++)
                {
                    msg += uMsg[i] + " ";
                }
            }
            else
            {
                msg = "";
            }

            try
            {
                sendDT = DateTime.Parse(dt);
            }
            catch
            {
                sendDT = DateTime.Now;
            }
        }

        public string SaveSug()
        {
            string sb = "建议内容不能为空，请重新输入";
            if (msg.Trim().Length == 0)
            {
                return sb;
            }
            csSuggestion s = new csSuggestion();
            s.dt = sendDT;
            s.message = msg;
            s.senderId = senderId;
            if (sug.saveSug(s, true))
            {
                sb = "意见已经反馈给管理员，我们会尽快处理您的提议！谢谢！";
            }
            else
            {
                sb = "处理超时，请稍候再试!";
            }

            return sb;
        }
    }
}
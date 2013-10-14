using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Robot.Debug;
using System.Configuration;
using System.Text;

namespace Robot.Database
{
    public class csSuggestion
    {
        public DateTime dt;
        public string message;
        public string senderId;
    }
    /// <summary>
    /// 将建议发送到数据里
    /// </summary>
    public class SaveSuggestions
    {
        private const string SuggestionLog = "SuggestionLog.txt";
        private ToFile tofile;
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
        public SaveSuggestions()
        {
            tofile = new ToFile();
        }
        /// <summary>
        /// Save the suggestion in a plain txt
        /// </summary>
        /// <param name="sug"></param>
        /// <param name="bTxt"></param>
        /// <returns></returns>
        public bool saveSug(csSuggestion sug, bool bTxt)
        {
            StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(sug.dt.ToString() + ", 发送人：" +sug.senderId + ", 建议：" + sug.message + "\r\n") ;
            return tofile.WriteTxt(sb.ToString(), SuggestionLog);
        }

        /// <summary>
        /// save it to database
        /// </summary>
        /// <param name="sug"></param>
        /// <returns></returns>
        public bool saveSug(csSuggestion sug)
        {
            bool result = false;
            SqlConnection db = new SqlConnection(connectionString);
            db.Open();
            SqlCommand cmd = new SqlCommand("InsertSuggestion", db);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@dt", SqlDbType.DateTime);
                cmd.Parameters["@dt"].Value = sug.dt;
                cmd.Parameters.Add("@id", SqlDbType.VarChar);
                cmd.Parameters["@id"].Value = sug.senderId;
                cmd.Parameters.Add("@msg", SqlDbType.VarChar);
                cmd.Parameters["@msg"].Value = sug.message;

                result = (cmd.ExecuteNonQuery() == 1);
                cmd.Dispose();
                db.Close();
                db.Dispose();
            }
            catch (Exception e)
            {
                tofile.WriteTxt(e.Message);
                cmd.Dispose();
                db.Close();
                db.Dispose();
            }
            return result;
        }
    }
}
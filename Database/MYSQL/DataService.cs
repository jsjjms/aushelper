using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using Robot.Engine;
using System.Data;
using Robot.Debug;


namespace Robot.Database
{
    public class DataService
    {
        private ToFile tofile;
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
        public DataService()
        {
            tofile = new ToFile();
        }
        /// <summary>
        /// 获取汇率数据
        /// </summary>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public string GetCurrency(int CountryCode)
        {
            string status = "";
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(sql.SQL.S_GET_CURRENCY), CountryCode.ToString()));
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status = reader.IsDBNull(0) ? "无法获取汇率数据，请稍候再试！" : reader.GetString(0);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return status;
        }

        /// <summary>
        /// 返回星座运程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAsterism(int id)
        {
            string status = "";
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(sql.SQL.S_GET_ASTERISM), id));
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status = reader.IsDBNull(0) ? "今日内容尚未生成，请稍后再试" : reader.GetString(0);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return status;
        }
        /// <summary>
        /// 取得用户之前发送的命令
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPreviousCmd(string id)
        {
            string order = "";
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(sql.SQL.S_GET_ORDER), id));
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order = reader.IsDBNull(0) ? "help" : reader.GetString(0);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return order;
        }
        /// <summary>
        /// 取得用户所在城市
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetCityId(string id)
        {
            int cityId = 0;
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(sql.SQL.S_GET_CITY_ID), id));
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cityId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return cityId;
        }
        /// <summary>
        /// 获取保存的油价值
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public string GetFuelPrice(int cityId)
        {
            string price = "";
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(sql.SQL.S_GET_FUEL_PRICE), cityId));
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    price = reader.IsDBNull(0) ? "油价数据空缺，请稍候再试" : reader.GetString(0);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return price;
        }

        /// <summary>
        /// Save the message
        /// </summary>
        public bool saveMsg(csMsg msg)
        {
            sql.SQL type = sql.SQL.S_SAVE_MSG;
            MySqlCommand cmd = new MySqlCommand(String.Format(sql.GetSQL(type), msg.content, msg.dt.ToString("yyyy-MM-dd HH:mm:ss"), msg.sender, msg.msgType));
            return (GeneralNonSelectQuery(cmd) == 1);   
        }
        /// <summary>
        /// 处理位置信息
        /// </summary>
        public class csHandleLocation
        {
        }
        /// <summary>
        /// 处理图片信息
        /// </summary>
        public class csHandleImg
        {
        }
        /// <summary>
        /// 处理声音信息
        /// </summary>
        public class csHandleAudio
        {
        }


        /// <summary>
        /// 一个公用interface执行所有非 select语句
        /// </summary>
        /// <param name="non_query_type"></param>
        /// <returns>返回影响数据库几行</returns>
        public int GeneralNonSelectQuery(MySqlCommand cmd)
        {
            int iReturn = 0;
            string connStr = sql.GetSQL(sql.SQL.S_CONNECTION_STR);
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                iReturn = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return iReturn;
        }
    }
}
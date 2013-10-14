﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionService
{
    public class sql
    {
        /// <summary>
        /// 每个类型对应一条SQL语句. 该部分操作都直接操作前台数据库
        /// </summary>
        public enum SQL
        {
            S_CONNECTION_STR,
            
        }

        /// <summary>
        /// 每个SQL语句对应一个类型
        /// </summary>
        public static string[] SQL_ARRAY = 
        {
        		//你可以使用你的测试服务器
            "server=localhost;user=XXXX;database=XXXX;port=3306;password=;charset=utf8",
            //记录用户发送的指令
            //YOUR SQL
        };

        /// <summary>
        /// 获得前台对应的SQL语句
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetSQL(SQL e)
        {
            return SQL_ARRAY[(int)e];
        }
    }
}

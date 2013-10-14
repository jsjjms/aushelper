using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robot.Debug;
using Robot.Help;
using Robot.Engine;
using System.Text;
using Robot.Database;
namespace Robot.CURRENCY
{
    enum CUR_TYPE
        {
            CT_START = 0,
            CT_PND = 0,     //英镑
            CT_HK,          //港币
            CT_US,          //美金
            CT_SWI_FRANCS,  //瑞士法郎
            CT_SIN,         //新加坡元
            CT_SWE_KRO,     //瑞典克朗
            CT_DAN_KRO,     //丹麦克朗
            CT_NOR_KRO,     //挪威克朗
            CT_JAP,         //日元
            CT_CAN,         //加拿大元
            CT_AUD,         //澳币
            CT_RIN,         //林吉特
            CT_ENU,         //欧元
            CT_IND_RUP,     //印尼卢比
            CT_MAC,         //澳门元
            CT_PHI_PEC,     //菲律宾比索
            CT_TAI_BAH,     //泰铢
            CT_NEWZ,        //新西兰元
            CT_KOR,         //韩国元
            CT_RUB,         //卢布
            CT_END = CT_RUB
        }

    /// <summary>
    /// 从中国银行获取汇率信息
    /// </summary>
    public class ChinaBank
    {
        struct OrderInChinaBank
        {
            public CUR_TYPE type;
            public string name;

            public OrderInChinaBank(CUR_TYPE t, string n)
            {
                type = t;
                name = n;
            }
        }

        private OrderInChinaBank[] orders =
        {
            new OrderInChinaBank(CUR_TYPE.CT_PND, "英镑"),
            new OrderInChinaBank(CUR_TYPE.CT_HK, "港币"),
            new OrderInChinaBank(CUR_TYPE.CT_US, "美元"),
            new OrderInChinaBank(CUR_TYPE.CT_SWI_FRANCS, "瑞士法郎"),
            new OrderInChinaBank(CUR_TYPE.CT_SIN, "新加坡元"),
            new OrderInChinaBank(CUR_TYPE.CT_SWE_KRO, "瑞典克朗"),
            new OrderInChinaBank(CUR_TYPE.CT_DAN_KRO, "丹麦克朗"),
            new OrderInChinaBank(CUR_TYPE.CT_NOR_KRO, "挪威克朗"),
            new OrderInChinaBank(CUR_TYPE.CT_JAP, "日元"),
            new OrderInChinaBank(CUR_TYPE.CT_CAN, "加拿大元"),
            new OrderInChinaBank(CUR_TYPE.CT_AUD, "澳大利亚元"),
            new OrderInChinaBank(CUR_TYPE.CT_RIN, "林吉特"),
            new OrderInChinaBank(CUR_TYPE.CT_ENU, "欧元"),
            new OrderInChinaBank(CUR_TYPE.CT_IND_RUP, "印尼卢比"),
            new OrderInChinaBank(CUR_TYPE.CT_MAC, "澳门元"),
            new OrderInChinaBank(CUR_TYPE.CT_PHI_PEC, "菲律宾比索"),
            new OrderInChinaBank(CUR_TYPE.CT_TAI_BAH, "泰国铢"),
            new OrderInChinaBank(CUR_TYPE.CT_NEWZ, "新西兰元"),
            new OrderInChinaBank(CUR_TYPE.CT_KOR, "韩国元"),
            new OrderInChinaBank(CUR_TYPE.CT_RUB, "卢布")
        };

        struct Country
        {
            public string sym;
            public CUR_TYPE type;

            public Country(string s, CUR_TYPE t)
            {
                sym = s;
                type = t;
            }
        }

        private Country[] countries =
        {
            new Country("aus", CUR_TYPE.CT_AUD),
            new Country("au", CUR_TYPE.CT_AUD),
            new Country("aud", CUR_TYPE.CT_AUD),
            new Country("australia", CUR_TYPE.CT_AUD),
            new Country("澳大利亚", CUR_TYPE.CT_AUD),
            new Country("澳币", CUR_TYPE.CT_AUD),
            new Country("澳元", CUR_TYPE.CT_AUD),
            new Country("澳洲", CUR_TYPE.CT_AUD),
            new Country("澳大利亚元", CUR_TYPE.CT_AUD),
            new Country("袋鼠国", CUR_TYPE.CT_AUD),

            new Country("us", CUR_TYPE.CT_US),
            new Country("ame", CUR_TYPE.CT_US),
            new Country("am", CUR_TYPE.CT_US),
            new Country("america", CUR_TYPE.CT_US),
            new Country("美国", CUR_TYPE.CT_US),
            new Country("美元", CUR_TYPE.CT_US),

            new Country("nz", CUR_TYPE.CT_NEWZ),
            new Country("nzd", CUR_TYPE.CT_NEWZ),
            new Country("新西兰", CUR_TYPE.CT_NEWZ),
            new Country("新西兰币", CUR_TYPE.CT_NEWZ),
            new Country("新西兰元", CUR_TYPE.CT_NEWZ),

            new Country("thai", CUR_TYPE.CT_TAI_BAH),
            new Country("泰国", CUR_TYPE.CT_TAI_BAH),
            new Country("泰铢", CUR_TYPE.CT_TAI_BAH),
            new Country("泰元", CUR_TYPE.CT_TAI_BAH),

            new Country("jap", CUR_TYPE.CT_JAP),
            new Country("ja", CUR_TYPE.CT_JAP),
            new Country("japan", CUR_TYPE.CT_JAP),
            new Country("日本", CUR_TYPE.CT_JAP),
            new Country("日元", CUR_TYPE.CT_JAP),
            new Country("日币", CUR_TYPE.CT_JAP),

            new Country("hk", CUR_TYPE.CT_HK),
            new Country("hongkong", CUR_TYPE.CT_HK),
            new Country("港元", CUR_TYPE.CT_HK),
            new Country("港币", CUR_TYPE.CT_HK),
            new Country("香港", CUR_TYPE.CT_HK),

            new Country("sin", CUR_TYPE.CT_SIN),
            new Country("sgd", CUR_TYPE.CT_SIN),
            new Country("新元", CUR_TYPE.CT_SIN),
            new Country("新加坡元", CUR_TYPE.CT_SIN),
            new Country("新币", CUR_TYPE.CT_SIN),
            new Country("新加坡", CUR_TYPE.CT_SIN),

            new Country("cad", CUR_TYPE.CT_CAN),
            new Country("canada", CUR_TYPE.CT_CAN),
            new Country("加元", CUR_TYPE.CT_CAN),
            new Country("加拿大元", CUR_TYPE.CT_CAN),
            new Country("加拿大", CUR_TYPE.CT_CAN),
            
            new Country("eur", CUR_TYPE.CT_ENU),
            new Country("欧元", CUR_TYPE.CT_ENU),
            new Country("欧洲", CUR_TYPE.CT_ENU)
        };
        private ToFile tofile;
        private string country;
        private int CountryCode = -1;

        public ChinaBank(List<string> Params)
        {
            tofile = new ToFile();

            //default this is AUD
            country = "aus";
            if (Params.Count > 1)
            {
                country = Params[1].ToLower();
                CountryCode = GetCountryCode(country);
            }
        }
        /// <summary>
        /// 获取国家编码
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        private int GetCountryCode(string country)
        {
            for (int i = 0; i < countries.Length; i++)
            {
                if (country == countries[i].sym)
                    return (int)countries[i].type;
            }

            return -1;
        }
      
       
        /// <summary>
        /// 获取汇率数据并转化好
        /// </summary>
        /// <returns></returns>
        public string GrabData()
        {
            string str = "";

            if (country.Trim() == "")
            {   
                Robot.Help.Help help = new Help.Help(CommonService.Engine.CTYPE.C_CURRENCY);
                str = help.getHelp();
                return str;
            }

            if (CountryCode == -1)
            {
                //invalid inquiry
                str = "您查询的国家不支持，请发送其他国家. \r\n目前支持澳洲、美国、新西兰、泰国、日本、香港、新加坡、加拿大、欧洲";
                return str;
            }

            DataService ds = new DataService();
            str = ds.GetCurrency(CountryCode);
            str = (str == "" ? "当前汇率数据尚未获取到，请稍候再试": str);
            return str;
        }
        
    }
}
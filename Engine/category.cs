using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonService.Engine
{
    public enum CTYPE
    {
        C_SEARCH = 0,               //search
        C_WEATHER = 1,              //weather
        C_MAP = 2,                  //map
        C_DELIVERY_START = 4,
        C_DELIVERY = 4,             //delivery  -- default is give to happylink
        C_DELIVERY_AAE = 5,
        C_DELIVERY_ST = 6,
        C_DELIVERY_EMS = 7,
        C_DELIVERY_SF = 8,
        C_DELIVERY_YT = 9,
        C_DELIVERY_ZT = 10,
        C_DELIVERY_GHX = 11,
        C_DELIVERY_TT = 12,
        C_DELIVERY_YD = 13,
        C_DELIVERY_HT = 14,
        C_DELIVERY_QF = 15,
        C_DELIVERY_CG = 16,
        C_DELIVERY_ZJS = 17,
        C_DELIVERY_END = C_DELIVERY_ZJS,
        C_YP = 18,                   //yellow page
        C_TRANS = 19,                //Translation
        C_SAVE = 20,                 //Save the information to system
        C_MEM = 21,                  //search information from db

        C_CURRENCY = 22,             //汇率
        C_METAL = 23,                //贵金属

        C_HELP = 24,
        C_I = 25,                     //robot
        C_SUGGESTION = 26,

        C_XZ_START = 27,
        C_XZ_BY = 27,
        C_XZ_JN = 28,
        C_XZ_SZ = 29,
        C_XZ_JX = 30,
        C_XZ_SZ_LION = 31,
        C_XZ_CN = 32,
        C_XZ_TP = 33,
        C_XZ_TX = 34,
        C_XZ_SS = 35,
        C_XZ_MX = 36,
        C_XZ_SP = 37,
        C_XZ_SY = 38,
        C_XZ_XZ = 39,
        C_XZ_END = C_XZ_XZ,

        C_FUEL_PRICE = 40,

        C_REPEAT = 41,
        C_RSS = 42,
        C_TEST = 43
    }
        
    struct category_data
    {
        /// <summary>
        /// Key of category
        /// </summary>
        public int key;
        /// <summary>
        /// Name of category
        /// </summary>
        public string c;
        /// <summary>
        /// Id of category
        /// </summary>
        public CTYPE categoryId;

        public category_data(int Key, string C, CTYPE Id)
        {
            key = Key;
            c = C;
            categoryId = Id;
        }
    }


    /// <summary>
    /// return a category and releavent message
    /// </summary>
    class category
    {
        private category_data[] cData =
        {
            //Category Weather
            new category_data(1, "weather", CTYPE.C_WEATHER),
            new category_data(2, "w", CTYPE.C_WEATHER),
            new category_data(50, "天气", CTYPE.C_WEATHER),

            //Category yellow page
            new category_data(3, "yp", CTYPE.C_YP),
            new category_data(4, "yellowpage", CTYPE.C_YP),
            new category_data(5, "hy", CTYPE.C_YP),

            //map:
            new category_data(6, "map", CTYPE.C_MAP),
            new category_data(51, "地图", CTYPE.C_WEATHER),
            //translation
            new category_data(7, "translate", CTYPE.C_TRANS),
            new category_data(8, "tr", CTYPE.C_TRANS),
            new category_data(9, "fy", CTYPE.C_TRANS),
            new category_data(10, "翻译", CTYPE.C_TRANS),

            //mem
            new category_data(100, "save", CTYPE.C_SAVE),
            new category_data(101, "sa", CTYPE.C_SAVE),

            new category_data(102, "mem", CTYPE.C_MEM),
            new category_data(103, "search", CTYPE.C_MEM),

            //currency
            new category_data(200, "currency", CTYPE.C_CURRENCY),
            new category_data(201, "cu", CTYPE.C_CURRENCY),
            new category_data(202, "hl", CTYPE.C_CURRENCY),
            new category_data(203, "汇率", CTYPE.C_CURRENCY),
            //happylink express
            new category_data(300, "kx", CTYPE.C_DELIVERY),
            new category_data(301, "kxkd", CTYPE.C_DELIVERY),
            new category_data(302, "happylink", CTYPE.C_DELIVERY),
            //new category_data(303, "hl", CTYPE.C_DELIVERY),
            //贵金属
            new category_data(310, "贵金属", CTYPE.C_METAL),
            new category_data(311, "gjs", CTYPE.C_METAL),
            new category_data(312, "gold", CTYPE.C_METAL),
            new category_data(313, "黄金", CTYPE.C_METAL),
            new category_data(314, "hj", CTYPE.C_METAL),
            new category_data(315, "白银", CTYPE.C_METAL),
            new category_data(316, "by", CTYPE.C_METAL),
            new category_data(317, "silver", CTYPE.C_METAL),

            //AAE
            new category_data(320, "aae", CTYPE.C_DELIVERY_AAE),
            new category_data(321, "stkd", CTYPE.C_DELIVERY_ST),
            new category_data(322, "ems", CTYPE.C_DELIVERY_EMS),
            new category_data(323, "sfkd", CTYPE.C_DELIVERY_SF),
            new category_data(324, "ytkd", CTYPE.C_DELIVERY_YT),
            new category_data(325, "ztkd", CTYPE.C_DELIVERY_ZT),
            new category_data(326, "ghx", CTYPE.C_DELIVERY_GHX),
            new category_data(327, "ttkd", CTYPE.C_DELIVERY_TT),
            new category_data(328, "ydkd", CTYPE.C_DELIVERY_YD),
            new category_data(329, "htkd", CTYPE.C_DELIVERY_HT),
            new category_data(330, "qfkd", CTYPE.C_DELIVERY_QF),
            new category_data(331, "cgkd", CTYPE.C_DELIVERY_CG),
            new category_data(332, "zjs", CTYPE.C_DELIVERY_ZJS),
            new category_data(333, "申通", CTYPE.C_DELIVERY_ST),
            new category_data(334, "顺丰", CTYPE.C_DELIVERY_SF),
            new category_data(335, "圆通", CTYPE.C_DELIVERY_YT),
            new category_data(336, "中通", CTYPE.C_DELIVERY_ZT),
            new category_data(337, "挂号信", CTYPE.C_DELIVERY_GHX),
            new category_data(338, "天天", CTYPE.C_DELIVERY_TT),
            new category_data(339, "韵达", CTYPE.C_DELIVERY_YD),
            new category_data(340, "汇通", CTYPE.C_DELIVERY_HT),
            new category_data(341, "全峰", CTYPE.C_DELIVERY_QF),
            new category_data(342, "程光", CTYPE.C_DELIVERY_CG),
            new category_data(343, "宅急送", CTYPE.C_DELIVERY_ZJS),

            //星座
            new category_data(500, "白羊", CTYPE.C_XZ_BY),
            new category_data(501, "白羊座", CTYPE.C_XZ_BY),
            new category_data(502, "金牛座", CTYPE.C_XZ_JN),
            new category_data(503, "金牛", CTYPE.C_XZ_JN),
            new category_data(504, "双子", CTYPE.C_XZ_SZ),
            new category_data(505, "双子座", CTYPE.C_XZ_SZ),
            new category_data(506, "巨蟹座", CTYPE.C_XZ_JX),
            new category_data(507, "巨蟹", CTYPE.C_XZ_JX),
            new category_data(508, "狮子座", CTYPE.C_XZ_SZ_LION),
            new category_data(509, "狮子", CTYPE.C_XZ_SZ_LION),
            new category_data(508, "处女座", CTYPE.C_XZ_CN),
            new category_data(509, "处女", CTYPE.C_XZ_CN),
            new category_data(510, "天枰座", CTYPE.C_XZ_TP),
            new category_data(511, "天枰", CTYPE.C_XZ_TP),
            new category_data(512, "天蝎座", CTYPE.C_XZ_TX),
            new category_data(513, "天蝎", CTYPE.C_XZ_TX),
            new category_data(514, "射手座", CTYPE.C_XZ_SS),
            new category_data(515, "射手", CTYPE.C_XZ_SS),
            new category_data(516, "魔蝎座", CTYPE.C_XZ_MX),
            new category_data(517, "魔蝎", CTYPE.C_XZ_MX),
            new category_data(518, "水瓶座", CTYPE.C_XZ_SP),
            new category_data(519, "水瓶", CTYPE.C_XZ_SP),
            new category_data(520, "双鱼座", CTYPE.C_XZ_SY),
            new category_data(521, "双鱼", CTYPE.C_XZ_SY),
            new category_data(522, "牧羊", CTYPE.C_XZ_BY),
            new category_data(523, "牧羊座", CTYPE.C_XZ_BY),
            new category_data(524, "星座", CTYPE.C_XZ_XZ),
            new category_data(525, "天平", CTYPE.C_XZ_TP),
            new category_data(526, "天平座", CTYPE.C_XZ_TP),
            new category_data(527, "魔羯", CTYPE.C_XZ_MX),
            new category_data(528, "魔羯座", CTYPE.C_XZ_MX),
            new category_data(529, "摩羯", CTYPE.C_XZ_MX),
            new category_data(530, "摩羯座", CTYPE.C_XZ_MX),

            new category_data(600, "油价", CTYPE.C_FUEL_PRICE),
            new category_data(601, "fuel", CTYPE.C_FUEL_PRICE),

            //suggestion
            new category_data(9995, "suggestion", CTYPE.C_SUGGESTION),
            new category_data(9996, "sug", CTYPE.C_SUGGESTION),
            new category_data(9997, "建议", CTYPE.C_SUGGESTION),
            new category_data(9998, "意见", CTYPE.C_SUGGESTION),
            //help
            new category_data(9999, "help", CTYPE.C_HELP),
            new category_data(10000, "?", CTYPE.C_REPEAT),
            new category_data(10001, "？", CTYPE.C_REPEAT),

            new category_data(10002, "rss", CTYPE.C_RSS),
            new category_data(10003, "test1", CTYPE.C_TEST)

        };

        public const int MAX_CATE_LEN = 8;
        
        public category(List<string> msg)
        {
            if (msg[0].Length == 0)
                return;

        }
        /// <summary>
        /// Get the type of a category
        /// return the index number
        /// </summary>
        /// <returns></returns>
        public CTYPE GetCategoryType(string msg)
        {
            CTYPE index = CTYPE.C_I;

            if (msg.Length > MAX_CATE_LEN)
            {
                //Go to search
                index = CTYPE.C_I;
            }
            else
            {
                //return category index
                for (int i = 0; i < cData.Length; i++)
                {
                    if (cData[i].c == msg)
                    {
                        return cData[i].categoryId;
                    }
                }
            }
            return index;
        }
    }
}

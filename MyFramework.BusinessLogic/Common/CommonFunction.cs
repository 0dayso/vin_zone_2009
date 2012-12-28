using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using MyFramework.DAL;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace MyFramework.BusinessLogic.Common
{
    public class CommonFunction
    {
        /// <summary>
        /// 安装下拉框
        /// </summary>
        /// <param name="myddl">控件对象</param>
        /// <param name="sql">查询对应值的SQL,第一列为content,第二列为value</param>
        public static void InitDDL(DropDownList myddl, string sql)
        {
            DataSet ds = new DataSet();
            string strError = "";
            DBA.SelectSQL("PublicFunction.cs182", sql, ref ds, ref strError);
            System.Data.DataTable dt = ds.Tables[0];
            myddl.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.ListItem _li = new System.Web.UI.WebControls.ListItem();
                    _li.Text = dt.Rows[i][0].ToString();
                    _li.Value = dt.Rows[i][1].ToString();
                    myddl.Items.Add(_li);
                }

            }
        }

        /// <summary>
        /// 安装下拉框

        /// </summary>
        /// <param name="myddl">控件对象</param>
        /// <param name="sql">查询对应值的SQL,第一列为content,第二列为value</param>
        /// <param name="strFilstName">第一列的文本</param>
        /// <param name="strFilstValue">第一列的值</param>
        public static void InitDDL(DropDownList myddl, string sql, string strFilstName, string strFilstValue)
        {
            DataSet ds = new DataSet();
            string ErrorInfo = "";
            DBA.SelectSQL("PublicFunction.cs182", sql, ref ds, ref ErrorInfo);
            System.Data.DataTable dt = ds.Tables[0];
            myddl.Items.Clear();
            System.Web.UI.WebControls.ListItem fistli = new System.Web.UI.WebControls.ListItem(strFilstName, strFilstValue);
            myddl.Items.Add(fistli);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.ListItem _li = new System.Web.UI.WebControls.ListItem();
                    _li.Text = dt.Rows[i][0].ToString();
                    _li.Value = dt.Rows[i][1].ToString();
                    myddl.Items.Add(_li);
                }

            }
        }
        /// <summary>
        /// 如果没数据时能显示一个表头
        /// zhb
        /// </summary>
        public static void GridViewBind(GridView toGrid, DataTable toDataSource)
        {
            if (toDataSource == null)
                return;
            if (toDataSource.Rows.Count == 0)
            {
                DataRow dr = toDataSource.NewRow();
                toDataSource.Rows.Add(dr);
                toGrid.DataSource = toDataSource;
                toGrid.DataBind();
                toGrid.Rows[0].Cells.Clear();
            }
            else
            {
                toGrid.DataSource = toDataSource;
                toGrid.DataBind();
            }
        }

        /// <summary>
        /// 取得url
        /// wsx
        /// </summary>
        public static string GetlocalUrl()
        {
            string lsUrl = "Http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/BSCRM/";
            return lsUrl;
        }
        /// <summary>
        /// 取得Ebooking系统的Url
        /// yanght
        /// </summary>
        public static string GetEbookinglocalUrl()
        {
            string lsUrl = "Http://" + System.Web.HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["EAppDir"] + "/";
            return lsUrl;
        }

        public static string GetJsjWebLocalUrl()
        {
            string lsUrl = "Http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/" + ConfigurationManager.AppSettings["AppDir"] + "/";
            return lsUrl;
        }

        #region 判断是否中文字符
        /// <summary>
        /// 判断是否中文字符
        /// </summary>
        /// <param name="tsStr"></param>
        /// <returns></returns>
        public static Boolean IsChineseStr(string tsStr)
        {
            Char[] cStrs = tsStr.ToCharArray();
            //判断是汉字还是拼音
            if (Convert.ToInt32(cStrs[0]) > 255)
                return true;
            else
                return false;
        }
        #endregion

        #region 根据城市的名称取城市的id
        /// <summary>
        /// 根据城市的名称取城市的id
        /// </summary>
        /// <param name="tsCityName"></param>
        /// <returns></returns>
        public static string GetCityId(string tsCityName, string[] CityInfo)
        {
            string lsCityId = "";
            string lsSql = "";

            if (tsCityName != "")
            {
                switch (CityInfo.Length)
                {
                    case 4:
                        lsCityId = "4";
                        lsSql = "Select Town_ID as ID from t_SD_Town where Town_Name='" + CityInfo[3] + "'";
                        break;
                    case 3:
                        lsCityId = "3";
                        lsSql = "Select City_ID as ID from t_SD_City where City_Name='" + CityInfo[2] + "'";
                        break;
                    case 2:
                        lsCityId = "2";
                        lsSql = "Select County_ID as ID from t_SD_County where County_Name='" + CityInfo[1] + "'";
                        break;
                    default:
                        lsCityId = "1";
                        lsSql = "Select Province_ID as ID from t_SD_Province where Province_Name='" + CityInfo[0] + "'";
                        break;
                }
                DataTable dsCity = new DataTable();

                DAL.DBA.FillDataTable(dsCity, lsSql);

                if (dsCity.Rows.Count > 0)

                    lsCityId += "_" + dsCity.Rows[0]["ID"].ToString();

                else

                    lsCityId = "";

            }
            return lsCityId;
        }
        #endregion

        #region "MD5加密"
        public static string GetCode(string pass)//MD5加密
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(pass);
            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < result.Length; i++)
            {
                ret += result[i].ToString("X");
            }
            return (ret);
        }
        #endregion

        #region 是否是正确手机号
        public static bool IsMobile(string strMobileNO)
        {
            if (strMobileNO == "")
                return false;
            Regex objAlphaPattern = new Regex(@"^13[0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$|^18[0-9]{1}[0-9]{8}$");

            return objAlphaPattern.IsMatch(strMobileNO);
        }
        #endregion

        #region 由名称得到简拼
        /// <summary>
        /// 由名称得到简拼
        /// </summary>
        /// <param name="tsHName"></param>
        /// <returns></returns>

        public static string getPinYin(string tsHName)
        {
            byte[] array = new byte[2];
            string returnstr = "";
            int chrasc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] nowchar = tsHName.ToCharArray();
            for (int j = 0; j < nowchar.Length; j++)
            {
                array = Encoding.Default.GetBytes(nowchar[j].ToString());
                array = Encoding.Default.GetBytes(nowchar[j].ToString());
                i1 = (short)(array[0]);
                i2 = (short)(array[1]);
                chrasc = i1 * 256 + i2 - 65536;
                if (chrasc > 0 && chrasc < 160)
                {

                    returnstr += nowchar[j];
                }
                else
                {
                    for (int i = (pyvalue.Length - 1); i >= 0; i--)
                    {
                        if (pyvalue[i] <= chrasc)
                        {
                            returnstr += pystr[i].Substring(0, 1);


                            break;
                        }
                    }
                }
            }
            return returnstr;
        }
        private static int[] pyvalue = new int[]//拼音对应的内码表
    {-20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,-20032,-20026, 
    -20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,-19756,-19751,-19746,-19741,-19739,-19728, 
    -19725,-19715,-19540,-19531,-19525,-19515,-19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263, 
    -19261,-19249,-19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,-19003,-18996, 
    -18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,-18731,-18722,-18710,-18697,-18696,-18526, 
    -18518,-18501,-18490,-18478,-18463,-18448,-18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, 
    -18181,-18012,-17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,-17733,-17730, 
    -17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,-17468,-17454,-17433,-17427,-17417,-17202, 
    -17185,-16983,-16970,-16942,-16915,-16733,-16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459, 
    -16452,-16448,-16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,-16212,-16205, 
    -16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,-15933,-15920,-15915,-15903,-15889,-15878, 
    -15707,-15701,-15681,-15667,-15661,-15659,-15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416, 
    -15408,-15394,-15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,-15149,-15144, 
    -15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,-14941,-14937,-14933,-14930,-14929,-14928, 
    -14926,-14922,-14921,-14914,-14908,-14902,-14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668, 
    -14663,-14654,-14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,-14170,-14159, 
    -14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,-14109,-14099,-14097,-14094,-14092,-14090, 
    -14087,-14083,-13917,-13914,-13910,-13907,-13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658, 
    -13611,-13601,-13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,-13340,-13329, 
    -13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,-13068,-13063,-13060,-12888,-12875,-12871, 
    -12860,-12858,-12852,-12849,-12838,-12831,-12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346, 
    -12320,-12300,-12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,-11781,-11604, 
    -11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,-11055,-11052,-11045,-11041,-11038,-11024, 
    -11020,-11019,-11018,-11014,-10838,-10832,-10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331, 
    -10329,-10328,-10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254};
        private static string[] pystr = new string[]{"a","ai","an","ang","ao","ba","bai","ban","bang","bao","bei","ben","beng","bi","bian","biao", 
   "bie","bin","bing","bo","bu","ca","cai","can","cang","cao","ce","ceng","cha","chai","chan","chang","chao","che","chen", 
   "cheng","chi","chong","chou","chu","chuai","chuan","chuang","chui","chun","chuo","ci","cong","cou","cu","cuan","cui", 
   "cun","cuo","da","dai","dan","dang","dao","de","deng","di","dian","diao","die","ding","diu","dong","dou","du","duan", 
   "dui","dun","duo","e","en","er","fa","fan","fang","fei","fen","feng","fo","fou","fu","ga","gai","gan","gang","gao", 
   "ge","gei","gen","geng","gong","gou","gu","gua","guai","guan","guang","gui","gun","guo","ha","hai","han","hang", 
   "hao","he","hei","hen","heng","hong","hou","hu","hua","huai","huan","huang","hui","hun","huo","ji","jia","jian", 
   "jiang","jiao","jie","jin","jing","jiong","jiu","ju","juan","jue","jun","ka","kai","kan","kang","kao","ke","ken", 
   "keng","kong","kou","ku","kua","kuai","kuan","kuang","kui","kun","kuo","la","lai","lan","lang","lao","le","lei", 
   "leng","li","lia","lian","liang","liao","lie","lin","ling","liu","long","lou","lu","lv","luan","lue","lun","luo", 
   "ma","mai","man","mang","mao","me","mei","men","meng","mi","mian","miao","mie","min","ming","miu","mo","mou","mu", 
   "na","nai","nan","nang","nao","ne","nei","nen","neng","ni","nian","niang","niao","nie","nin","ning","niu","nong", 
   "nu","nv","nuan","nue","nuo","o","ou","pa","pai","pan","pang","pao","pei","pen","peng","pi","pian","piao","pie", 
   "pin","ping","po","pu","qi","qia","qian","qiang","qiao","qie","qin","qing","qiong","qiu","qu","quan","que","qun", 
   "ran","rang","rao","re","ren","reng","ri","rong","rou","ru","ruan","rui","run","ruo","sa","sai","san","sang", 
   "sao","se","sen","seng","sha","shai","shan","shang","shao","she","shen","sheng","shi","shou","shu","shua", 
   "shuai","shuan","shuang","shui","shun","shuo","si","song","sou","su","suan","sui","sun","suo","ta","tai", 
   "tan","tang","tao","te","teng","ti","tian","tiao","tie","ting","tong","tou","tu","tuan","tui","tun","tuo", 
   "wa","wai","wan","wang","wei","wen","weng","wo","wu","xi","xia","xian","xiang","xiao","xie","xin","xing", 
   "xiong","xiu","xu","xuan","xue","xun","ya","yan","yang","yao","ye","yi","yin","ying","yo","yong","you", 
   "yu","yuan","yue","yun","za","zai","zan","zang","zao","ze","zei","zen","zeng","zha","zhai","zhan","zhang", 
   "zhao","zhe","zhen","zheng","zhi","zhong","zhou","zhu","zhua","zhuai","zhuan","zhuang","zhui","zhun","zhuo", 
   "zi","zong","zou","zu","zuan","zui","zun","zuo"};



        #endregion

    }
}

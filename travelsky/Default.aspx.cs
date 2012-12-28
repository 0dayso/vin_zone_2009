using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using cn.linkosky.sae2;
using System.Security.Cryptography;
using cn.linkosky;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    /*
            加密过程中的中间信息（用于调试核对本地加密代码）
            string AGID = "00000205";
            string Password = "admin";
            string APSecret = "0B20E6CADA8FB2450E49D7252D19B82BE1AD57E1A0CEE88C";
            string TimeStamp = "2010-12-31 15:43:22";

            string Authenticator = LinkoskyEncode.GenerateDigest(AGID + APSecret + TimeStamp);

            string AGInfo = "";

                string Digest = LinkoskyEncode.GenerateDigest(AGID + "$" + Password + "$" + TimeStamp);
                LinkoskyCryptography localLC = new LinkoskyCryptography();
                string strIV = "0102030405060708";
                String strEncrypt = localLC.Encrypt(AGID + "$" + Password + "$" + TimeStamp + "$" + Digest, APSecret, strIV);

                AGInfo = AGID + "$" + strEncrypt;

                string strTemp = "Authenticator:" + Authenticator + "</br>" +
                    "Digest:" + Digest + "</br>" +
                    "strEncrypt:" + strEncrypt + "</br>" +
                    "strEncrypt:" + AGInfo + "</br>";

    得到strTemp值为：
    Authenticator:ykVdCy1DfneykATbXDBwuofteAc=</br>Digest:eXE+itHJ+AyAvmDB16caDvUmXb8=</br>strEncrypt:lEpFgirbeqLFUmz4x9aCNx1I9OrZWTj+1pi9vbEeAz+3F89g78em61Lfi5kbj/QgXkR8MIIWVTcYgQWxgQkHDg==</br>strEncrypt:00000205$lEpFgirbeqLFUmz4x9aCNx1I9OrZWTj+1pi9vbEeAz+3F89g78em61Lfi5kbj/QgXkR8MIIWVTcYgQWxgQkHDg==</br>
    返回的AGToken为：
    lEpFgirbeqJ2DooekiqEWnuifRy63+K1/SwKXG6Z+mObSbcYV4apFeH4um/Lw0viMt7XFoZZjsJK4V/AgljDxmnwuPEds/CqX6L5e2s8nrg=

    */

    void test()
    {

        //>AGID： 00000289 
        //>密码： admin
        //>密钥： 14F675282729AC1DF214F5EE4959596B6FA00A842B3489A6 
        //>3DES加密向量：0102030405060708
        string AGID = "00000289";
        string Password = "admin";
        string TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Digest = string.Empty;
        string Authenticator = string.Empty;
        string APSecret = "14F675282729AC1DF214F5EE4959596B6FA00A842B3489A6";
        string strIV = "0102030405060708";

        Authenticator = GenerateDigest(AGID + APSecret + TimeStamp);
        Digest = GenerateDigest(AGID + "$" + Password + "$" + TimeStamp);
        String strEncrypt = Encrypt(AGID + "$" + Password + "$" + TimeStamp + "$" + Digest, APSecret, strIV);
        string AGInfo = AGID + "$" + strEncrypt;
        // 接口操作类
        CenterInterfaceForAG center = new CenterInterfaceForAG();

        ApplyAGTokenResult aa = new ApplyAGTokenResult();
        aa = center.ApplyAGToken(AGID, TimeStamp, Authenticator, AGInfo);
        Response.Write("aa");
    }

 

    // 1、生成摘要算法
    public static string GenerateDigest(string TobeDigest)
    {
        return ToBase64String(ComputeHash(ConvertStringToByteArray(TobeDigest)));
    }

    private static byte[] ComputeHash(byte[] buf)
    {
        return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(buf);
    }
    public static byte[] ConvertStringToByteArray(String Source)
    {
        return System.Text.Encoding.GetEncoding("utf-8").GetBytes(Source);//gb2312
    }
    private static string ToBase64String(byte[] buf)
    {
        return System.Convert.ToBase64String(buf);
    }
    private static byte[] FromBase64String(string Source)
    {
        return System.Convert.FromBase64String(Source);
    }

    //2、用户信息的3DES算法
    public string Encrypt(string strTobeEnCrypted, string strKEY, string strIV)
    {
        if (strTobeEnCrypted == "") return "";
        try
        {
            byte[] pKEY = HexStringToByteArray(strKEY);
            byte[] pIV = HexStringToByteArray(strIV);
            byte[] Encrypted;
            if (Encrypt(pKEY, pIV, ConvertStringToByteArray(strTobeEnCrypted), out Encrypted))
            {
                return ToBase64String(Encrypted);
            }
            else
            {
                return "";
            }
        }
        catch
        { }
        return "";
    }



    private static byte chr2hex(String chr)
    {
        if (chr.Equals("0"))
        {
            return 0x00;
        }
        else if (chr.Equals("1"))
        {
            return 0x01;
        }
        else if (chr.Equals("2"))
        {
            return 0x02;
        }
        else if (chr.Equals("3"))
        {
            return 0x03;
        }
        else if (chr.Equals("4"))
        {
            return 0x04;
        }
        else if (chr.Equals("5"))
        {
            return 0x05;
        }
        else if (chr.Equals("6"))
        {
            return 0x06;
        }
        else if (chr.Equals("7"))
        {
            return 0x07;
        }
        else if (chr.Equals("8"))
        {
            return 0x08;
        }
        else if (chr.Equals("9"))
        {
            return 0x09;
        }
        else if (chr.Equals("A"))
        {
            return 0x0a;
        }
        else if (chr.Equals("B"))
        {
            return 0x0b;
        }
        else if (chr.Equals("C"))
        {
            return 0x0c;
        }
        else if (chr.Equals("D"))
        {
            return 0x0d;
        }
        else if (chr.Equals("E"))
        {
            return 0x0e;
        }
        else if (chr.Equals("F"))
        {
            return 0x0f;
        }
        return 0x00;
    }

    public byte[] HexStringToByteArray(string s)
    {
        Byte[] buf = new byte[s.Length / 2];
        for (int i = 0; i < buf.Length; i++)
        {
            buf[i] = (byte)(chr2hex(s.Substring(i * 2, 1)) * 0x10 + chr2hex(s.Substring(i * 2 + 1, 1)));
        }
        return buf;
    }

    // private System.Security.Cryptography.TripleDESCryptoServiceProvider des;
    private bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out  byte[] Encrypted)
    {
        Encrypted = null;
        try
        {
            byte[] tmpiv ={ 0, 1, 2, 3, 4, 5, 6, 7 };
            for (int ii = 0; ii < 8; ii++)
            {
                tmpiv[ii] = IV[ii];
            }
            byte[] tmpkey ={ 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
            for (int ii = 0; ii < 24; ii++)
            {
                tmpkey[ii] = KEY[ii];
            }
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            ICryptoTransform tridesencrypt = des.CreateEncryptor(tmpkey, tmpiv);
            Encrypted = tridesencrypt.TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
            des.Clear();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string aa = "aa";
        test();
    }
}

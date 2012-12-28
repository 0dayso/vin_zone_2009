using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Mydemo_异步调用_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Run();
        this.Label1.Text = "aaa";

        string mes = DateTime.Now.Ticks + "提示：干别的事去喽！";
        Response.Write("<script>alert('" + mes + "');</script>");
    }

    public bool WriteData()
    {
        bool isExist = File.Exists(@"C:\dsData.txt");

        if (!isExist)
        {
            using (File.Create(@"C:\dsData.txt"))
            {

            }
        }

        using (StreamWriter sw = File.AppendText(@"C:\dsData.txt"))
        {
            for (int i = 0; i < 500; i++)
            {
                sw.WriteLine(DateTime.Now.Ticks + " : " + Convert.ToString(i));
            }
            sw.Flush();
            sw.Close();
        }
        return true;
    }

    //首先准备好，要进行异步的方法（能异步的，最好不多线程）
    private string MethodName(int Num, out int Num2)
    {
        Num2 = Num;
        WriteData();
        return "HelloWorld";
    }

    //程序终点
    //异步完成时，执行的方法（回调方法），此方法只能有IAsyncResult一个参数，但是该参数几乎万能，可以传递object
    private void CallBackMethod(IAsyncResult ar)
    {
        //从异步状态ar.AsyncState中，获取委托对象
        DelegateName dn = (DelegateName)ar.AsyncState;
        //输出参数
        int i;

        //一定要EndInvoke，否则你的下场很惨
        string r = dn.EndInvoke(out i, ar);
        using (StreamWriter sw = File.AppendText(@"C:\dsData.txt"))
        {
            sw.WriteLine(DateTime.Now.Ticks + Convert.ToString("异步完成喽！i的值是" + i.ToString() + ",r的值是" + r));
            sw.Flush();
            sw.Close();
        }

    }

    //定义与方法同签名的委托
    private delegate string DelegateName(int Num, out int Num2);

    //程序入口
    private void Run()
    {
        //实例化委托并初赋值
        DelegateName dn = new DelegateName(MethodName);
        //输出参数
        int i;
        //实例化回调方法
        //把AsyncCallback看成Delegate你就懂了，实际上AsyncCallback是一种特殊的Delegate，就像Event似的
        AsyncCallback acb = new AsyncCallback(CallBackMethod);
        //异步开始
        //如果参数acb换成null则表示没有回调方法
        //最后一个参数dn的地方，可以换成任意对象，该对象可以被回调方法从参数中获取出来，写成null也可以。
        //参数dn相当于该线程的ID，如果有多个异步线程，可以都是null，但是绝对不能一样，不能是同一个object，否则异常

        
        IAsyncResult iar = dn.BeginInvoke(1, out i, acb, dn);


        //去做别的事
        //…………
        using (StreamWriter sw = File.AppendText(@"C:\dsData.txt"))
        {
            sw.WriteLine(DateTime.Now.Ticks + "干别的事去喽！");
            sw.Flush();
            sw.Close();
        }
    }
}
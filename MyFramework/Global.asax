﻿<%@ Application Language="C#" %>

<script RunAt="server">


    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码

    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_OnEnd(object sender, EventArgs e) 
    {
        
    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

        Dictionary<string, string> userlist = (Dictionary<string, string>)Application["OnlineUserList"];
        //userlist.Where<Dictionary<string,string>>(x=>x.ke
        if (userlist == null) return;
        var list = from user in userlist
                   where user.Value.Contains(Session.SessionID)
                   select user;
        foreach (KeyValuePair<string, string> kvp in list)
        {
            userlist.Remove(kvp.Key);            
        }
        Application["OnlineUserList"] = userlist;
        //if (hOnline[Session.SessionID] != null)
        //{
        //    hOnline.Remove(Session.SessionID);
        //    Application.Lock();
        //    Application["Online"] = hOnline;
        //    Application.UnLock();
        //}
    }
       
</script>

<%@ WebHandler Language="C#" Class="Data" %>

using System;
using System.Web;

public class Data : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string method = context.Request.QueryString["MethodName"].ToString();
        context.Response.ContentType = "text/json";
        switch (method)
        {
            case "GetData" :
                context.Response.Write(GetData());
                break;
        }
        
    }

    protected string GetData()
    {
        return (@"{""FirstName"":""Ravi"", ""LastName"":""Baghel"", ""Blog"":""ravibaghel.wordpress.com""}");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
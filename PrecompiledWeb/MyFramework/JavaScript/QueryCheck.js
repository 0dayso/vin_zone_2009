// JScript 文件
//验证查询条件，必须输入一个条件
    function QueryCheck()
    {
        var txtQuerys = document.getElementsByTagName("input");
        if(txtQuerys.length>0)
        {
            for(var i=0;i<txtQuerys.length;i++)
            {
                if(txtQuerys[i].type=="text"&&txtQuerys[i].value!="")
                {
                    return true;
                }
            }
        }
        alert("必须输入一个条件！");
        return false;
    }


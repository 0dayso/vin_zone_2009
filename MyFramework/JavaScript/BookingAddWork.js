// JScript 文件
//****************************************************
     
      function Test( lbFieldsSelect,value)//判断是否以被添加到选中框
      {
        for (j = 0;j<lbFieldsSelect.length; j++)
        {                    
            if (lbFieldsSelect.options[j].value == value) return true;            
        }
        return false;
      }
        
     function SelectField()//添加按钮
     {
        var lbFields       = document.getElementById("lbFields");           
        var lbFieldsSelect = document.getElementById("lbFieldsSelect");
        
        for (i = 0;i<lbFields.length; i++)
        {   
            if ( lbFields.options[i].selected)
            {                     
                if  (Test(lbFieldsSelect,lbFields.options[i].value))
                {    
                    //已经选择该字段
                    continue;
                }
                lbFieldsSelect.options.add(document.createElement("OPTION"));
                lbFieldsSelect.options[lbFieldsSelect.length-1].text=lbFields.options[i].text;
                lbFieldsSelect.options[lbFieldsSelect.length-1].value=lbFields.options[i].value;
            }
        }
      }
            
     function UnSelectField()//删除按钮
     {
        var lbFields       = document.getElementById("lbFields");                
        var lbFieldsSelect = document.getElementById("lbFieldsSelect");
        for (i = 0;i<lbFieldsSelect.length; i++)
        {    
            if (lbFieldsSelect.options[i].selected)
            {    
                lbFieldsSelect.remove(i);
            }
        }       
     }
 
    function  TestHouseType()//得到选种后人员编号的数值，返回形式为:a,b,c....
    {
      var   Select      = document.getElementById( "lbFieldsSelect");
      var   HiddenValue = document.getElementById( "HiddenValue");
      var   SelectValue = "";
      for(i=0;i<Select.length;i++)
      {  
           if(i==0)
           {
            SelectValue = Select.options[i].value;
           }
           else
           {
            SelectValue +="|"+ Select.options[i].value ;
           }
      }
      HiddenValue.value = SelectValue;
      alert( SelectValue );
      if( SelectValue=="" )
      {
        alert("请选择房型!");
        return false;
      }
    }
    
    
    //onclick改变行的颜色
    if(!objbeforeItem)   
    {   
      var objbeforeItem                = null;   
      var objbeforeItembackgroundColor = null;   
    }   
    function   ItemOver( obj )   
    {   
      if( objbeforeItem )   
      {   
        objbeforeItem.style.backgroundColor = objbeforeItembackgroundColor;   
      }   
      objbeforeItembackgroundColor  = obj.style.backgroundColor;   
      objbeforeItem                 = obj;   
      obj.style.backgroundColor     = "#B9D1F3";             
    }   

       

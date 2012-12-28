


        // 重新设置页面循环的初始值

        if( top.jsRequestBegin != undefined ) top.jsRequestBegin = 1;


/***************************************************************************************************************/

        // 文本框输入公共判断
        function InputValueTest( ObjID, ObjName, HaveTrim )
        {
            if( document.all( ObjID ) == null )
            {
                alert( "JS错误：没有找到 "+ ObjName +" 这个对象" );
                return false;
            }
            
            if( HaveTrim == undefined )
            {            
                document.all( ObjID ).value = document.all( ObjID ).value.replace(/(^\s*)|(\s*$)/g,"");
            }   
            
            if( document.all( ObjID ).value == "" )
            {
                alert( "\""+ ObjName +"\" 的输入不能为空，也不能全是空格字符！" );
                document.all( ObjID ).focus();
                return false;
            }
            
            return true;
        }
        


		// 禁止所有按钮
		function DisabledButton()
		{
			var ButtonObj	= document.all.tags("button");
			for( var i=0; i<ButtonObj.length; i++ ) ButtonObj[i].disabled = true;
		}
		
		// 所有输入眶禁止操作
		function AllReadonly()
		{
			var InputObj	= document.all.tags("input");
			for( var i=0; i<InputObj.length; i++ ) 
			{
				if( InputObj[i].type.toLowerCase() == "checkbox" || InputObj[i].type.toLowerCase() == "radio" )
				{
					InputObj[i].disabled = true;
				}
				else
				{
					InputObj[i].readOnly = true;
				}
				if( InputObj[i].className == "日期" )
				{
					InputObj[i].disabled = true;
				}
				
			}	
			
			var TextareaObj	= document.all.tags("textarea");
			for( var i=0; i<TextareaObj.length; i++ ) TextareaObj[i].readOnly = true;
			
			var SelectObj	= document.all.tags("select");
			for( var i=0; i<SelectObj.length; i++ ) SelectObj[i].disabled = true;
		}
		

		// 关闭菜单栏的公共方法 
		
		function document.ondblclick()
		{
			var ThisTagName = document.activeElement.tagName.toLowerCase();
			if( ThisTagName == "select" || ThisTagName == "input" || ThisTagName == "textarea" || ThisTagName == "button" || ThisTagName == "a" ||
				ThisTagName == "table" || ThisTagName == "tr" || ThisTagName == "td" )
			return false;
		
			try
			{
				top.CloseOpenMenu();
				return false;
			}catch(e){}
		}

		//

		// 禁止页面选取事件

//		function document.onselectstart()
//		{
//			var ThisTag		= document.activeElement;
//			var ThisTagName = ThisTag.tagName.toLowerCase();
//			if( ThisTagName == "select" || ThisTagName == "input" || ThisTagName == "textarea" )
//			return true;
//			
//			
//			if( document.all.NoSelectEvent != undefined && document.all.NoSelectEvent.contains( ThisTag ) )
//			return true;

//			return false;
//		}
		



		// 打开模态窗口的公共方法
		
		function window.myopen( ThisAddress, ThisHeight, ThisWidth, Target )
		{
			var ThisTagName = document.activeElement.tagName.toLowerCase();
			var IsPost      = false;
			
			if( ThisTagName == "a" ) 
			{
			    TrSelect( document.activeElement.parentElement );
			    IsPost = true;
			}
							
			var ReturnValue = myopen_Menu( ThisAddress, ThisHeight, ThisWidth, Target, IsPost );	
			
			if( ThisTagName == "a" ) TrNoSelect( document.activeElement.parentElement );
			
			return ReturnValue;
		}
		
		//不做TR操作
		function window.myopen_Menu( ThisAddress, ThisHeight, ThisWidth, Target, IsPost )
		{
							
			if( ThisWidth == undefined  || ThisWidth == 0 )     ThisWidth	= screen.width - 100;
			if( ThisHeight == undefined || ThisHeight == 0 )	ThisHeight	= 500;
			if( IsPost == undefined )   IsPost = false;
			
		
			var ReturnValue = window.showModalDialog( ThisAddress, window, 'scroll:auto;help:no;status:no;dialogWidth:'+ ThisWidth +'px;dialogHeight:'+ ThisHeight +'px');
			if( ReturnValue == "true" ) 
			{
				if( Target == undefined || Target == '' )
				{
				    if( IsPost == true ) myopenEvent();
					DisabledButton();
					document.all.tags("form")[0].submit();
				}	
				else
					eval( Target +'( '+ Target +' );');
			}
			
			return ReturnValue;
		}
		
		// 对于需要Post的刷新，先确定是否有翻页控件，有设置
		function myopenEvent()
		{
		    if( document.all.NewGridViewPageThisNumber == undefined || document.all.NewGridViewPageNumber == undefined ) return;
		    document.all.NewGridViewPageNumber.value = document.all.NewGridViewPageThisNumber.innerHTML;
		}


	
/******************************************************************************************/		
		

		// 用于对列表框修改

		function TrSelect( e )
		{
			var TrObj = e;
			var WhileBieginID = 0;
			while( TrObj.tagName.toUpperCase() != "TR" ) 
			{
				TrObj = TrObj.parentNode;
				WhileBieginID ++;
				
				if( WhileBieginID > 5 ) return;
			}	
			
			if( TrObj.BColor == undefined ) TrObj.BColor = TrObj.style.backgroundColor;

			TrObj.style.backgroundColor = 'D8DFFC';				
			for( var i=0; i<TrObj.childNodes.length; i++ )
			{	
				TrObj.childNodes[i].style.borderBottom	= '#0066CC 1px solid';
			}									
		}

		function TrNoSelect( e )
		{
			var TrObj = e;
			var WhileBieginID = 0;
			while( TrObj.tagName.toUpperCase() != "TR" ) 
			{
				TrObj = TrObj.parentNode;
				WhileBieginID ++;
				
				if( WhileBieginID > 5 ) return;
			}	
			
			TrObj.style.backgroundColor = TrObj.BColor;	
			for( var i=0; i<TrObj.childNodes.length; i++ )
			{
				TrObj.childNodes[i].style.borderBottom	= '#AAAAAA 1px solid';	
			}
		}



	// 设置只能输入小数
	// onchange
	
		function ReturnformatXS(e)
		{
			if( parseFloat( e.value ) == 0 ) e.value = "";
		}
	
		function formatXS(e) 
		{
			var MoneyNumber = e.value.toString();
			if( isNaN( MoneyNumber ) ) MoneyNumber = "0.00";					

			e.value = MoneyNumber;
		}
		






	// 设置只能输入货币
	// onchange
	
		function ReturnformatCurrency(e)
		{
			if( parseFloat( e.value ) == 0 ) e.value = "";
		}
	
		function formatCurrency(e) 
		{
			var MoneyNumber = e.value.toString();
			if( isNaN( MoneyNumber ) ) MoneyNumber = "0.00";					

			e.value = Math.floor( MoneyNumber * 100 ) / 100;
		}
		

		
		

	// 判断文本框的长度输入限制
	// onpropertychange
	
		function maxlength( e, number ) 
		{
			if( e.value.length > number )
			{
				e.value	= e.value.substring( 0, number );
			}
		} 


	// 判断文本框只能输入数字
	// onchange
	
		function ReturnOnlyNumber(e)
		{
			if( parseInt( e.value ) == 0 ) e.value = "";
		}
		
		function OnlyNumber( e ) 
		{
			if( e.value == "" )
			{
				e.value = 0;
				return;
			}
			e.value = parseInt( e.value.replace( /[^0-9]/g,"" ) );
			if( e.value.toLowerCase() == "nan" ) e.value = 0;
		} 
        
          
     

	// 判断输入货币的范围
	
		function MoneyBound( e, MinSize, MaxSize )
		{
			if( parseFloat( e.value )+"" == "NaN" )
			{
				alert( '错误的输入值');
				return;
			}
			
			if( parseFloat( e.value ) < MinSize )
			{
				alert( '数值超出范围' );
				e.value = MinSize;
				return;
			}
			
			if( parseFloat( e.value ) > MaxSize )
			{
				alert( '数值超出范围' );
				e.value = MaxSize;
				return;
			}				
		}
		


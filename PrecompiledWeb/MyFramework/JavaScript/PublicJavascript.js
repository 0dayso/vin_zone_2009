


        // ��������ҳ��ѭ���ĳ�ʼֵ

        if( top.jsRequestBegin != undefined ) top.jsRequestBegin = 1;


/***************************************************************************************************************/

        // �ı������빫���ж�
        function InputValueTest( ObjID, ObjName, HaveTrim )
        {
            if( document.all( ObjID ) == null )
            {
                alert( "JS����û���ҵ� "+ ObjName +" �������" );
                return false;
            }
            
            if( HaveTrim == undefined )
            {            
                document.all( ObjID ).value = document.all( ObjID ).value.replace(/(^\s*)|(\s*$)/g,"");
            }   
            
            if( document.all( ObjID ).value == "" )
            {
                alert( "\""+ ObjName +"\" �����벻��Ϊ�գ�Ҳ����ȫ�ǿո��ַ���" );
                document.all( ObjID ).focus();
                return false;
            }
            
            return true;
        }
        


		// ��ֹ���а�ť
		function DisabledButton()
		{
			var ButtonObj	= document.all.tags("button");
			for( var i=0; i<ButtonObj.length; i++ ) ButtonObj[i].disabled = true;
		}
		
		// �����������ֹ����
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
				if( InputObj[i].className == "����" )
				{
					InputObj[i].disabled = true;
				}
				
			}	
			
			var TextareaObj	= document.all.tags("textarea");
			for( var i=0; i<TextareaObj.length; i++ ) TextareaObj[i].readOnly = true;
			
			var SelectObj	= document.all.tags("select");
			for( var i=0; i<SelectObj.length; i++ ) SelectObj[i].disabled = true;
		}
		

		// �رղ˵����Ĺ������� 
		
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

		// ��ֹҳ��ѡȡ�¼�

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
		



		// ��ģ̬���ڵĹ�������
		
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
		
		//����TR����
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
		
		// ������ҪPost��ˢ�£���ȷ���Ƿ��з�ҳ�ؼ���������
		function myopenEvent()
		{
		    if( document.all.NewGridViewPageThisNumber == undefined || document.all.NewGridViewPageNumber == undefined ) return;
		    document.all.NewGridViewPageNumber.value = document.all.NewGridViewPageThisNumber.innerHTML;
		}


	
/******************************************************************************************/		
		

		// ���ڶ��б���޸�

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



	// ����ֻ������С��
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
		






	// ����ֻ���������
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
		

		
		

	// �ж��ı���ĳ�����������
	// onpropertychange
	
		function maxlength( e, number ) 
		{
			if( e.value.length > number )
			{
				e.value	= e.value.substring( 0, number );
			}
		} 


	// �ж��ı���ֻ����������
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
        
          
     

	// �ж�������ҵķ�Χ
	
		function MoneyBound( e, MinSize, MaxSize )
		{
			if( parseFloat( e.value )+"" == "NaN" )
			{
				alert( '���������ֵ');
				return;
			}
			
			if( parseFloat( e.value ) < MinSize )
			{
				alert( '��ֵ������Χ' );
				e.value = MinSize;
				return;
			}
			
			if( parseFloat( e.value ) > MaxSize )
			{
				alert( '��ֵ������Χ' );
				e.value = MaxSize;
				return;
			}				
		}
		


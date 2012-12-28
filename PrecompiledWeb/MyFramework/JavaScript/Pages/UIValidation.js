//�ж��Ƿ�Ϊ��
function IsEmpty(tsValue)
{
	var exp = /^\s*$/;
	return (exp.test(tsValue));
}

//����ֵ��У�� �磺111.12
function IsInteger(tsValue){
	var exp, op;
	op = tsValue;
    exp = /^\s*[-\+]?\d+\s*$/;
    if (op.match(exp) == null){ 
        return false;
    }else{
		return true;
    }
}
//������У�� �磺111.12
function IsDouble(tsValue){
	var exp, op;
	op = tsValue;
	exp = new RegExp("^\\s*([-\\+])?(\\d+)?(\\" + "." + "(\\d+))?\\s*$");
    if(op.match(exp)==null) return false;
    else return true;
}
//������У�� �磺˵����ʽ����YYYY-MM-DD��YYYY-M-DD��YYYY-M-D��YYYY-MM-D
function IsDate(sDate){
	
	var sDate=sDate.replace(/(^\s+|\s+$)/g,"");
	if(sDate=="")	return true;
	var s = sDate.replace(/[\d]{4,4}[\.\-\/]{1}[\d]{1,2}[\.\-\/]{1}[\d]{1,2}/g,"");
	if (s=="") //
	{
		sDate=sDate.replace(/\-/g,"/");
		sDate=sDate.replace(/\./g,"/");

		var t=new Date(sDate);

		var iYear=t.getYear();
		if (iYear<100) (iYear+=1900);
		var iMonth=t.getMonth()+1;
		var iDay=t.getDate();

		var ar = sDate.split(/[/]/);

		if(ar[0] != iYear || ar[1] != iMonth || ar[2] != iDay)	{
			return false;
		}
	}else{
		return false;
	}
	return true;
}

//��������У�� ��2008
function ValidateYear(toControl)
{
    if(IsEmpty(toControl.value))return true;
    var len=parseInt(toControl.value);
    if(!IsInteger(toControl.value) || len >=10000||len<=999)
    {
        alert("��������λ��Ч�����!");
        toControl.focus();
    	return false;
    }
    return true;
}
//��������У�� ��200808
function IsYearMonth(tsYearMonth)
{
	var len=tsYearMonth.length;
	if(len!=6)	return false;
    var month = tsYearMonth.substr(4,2);
    if(parseFloat(month)>12 ||parseFloat(month)==0)
        return false;
	return true;
}
//ʱ����У�� �磺12:30
function IsDateTime(tsValue){
	var exp, op;
	op = tsValue;
	exp = new RegExp("((2[0-3])|([0-1][0-9]))[:]([0-5]\\d{1}$)");
    if(op.match(exp)==null) return false;
    else return true;
}
function ValidateDateTime(tsFileName,toObject,tbNotAllowEmpty)
{
    if(tbNotAllowEmpty)
       { if(!ValidateEmpty(tsFileName,toObject))return false;}
    else 
        if(IsEmpty(toObject.value)) return true;         
    if(!IsDateTime(toObject.value))
    {
        alert("��������Ч��"+ tsFileName +"��");
        toObject.focus();
        toObject.select();
        return false;
    }
    return true;

}
//������У�� �磺111,111.00
function IsCurrency(tsValue){
	var exp;
	exp = new RegExp("^\\s*([-\\+])?((((\\d+),)*(\\d+))|(\\d+)?)(\\.(\\d+))?\\s*$");
    if(tsValue.match(exp)==null) return false;
    else return true;
}
//�ж��Ƿ��Ǻ���
function IsCharacter(tsValue)
{
   var exp;
   exp=new RegExp("[\u4e00-\u9fa5]");
   if(tsValue.match(exp)==null) return false;
   else return true;
}
//�绰����/������У��
function IsTelephone(tsTelValue)
{
	var i,j,strTemp,len,newLen,newStrTemp;
	len=tsTelValue.length;
	if(len<8 || len>32)	return false;
	newStrTemp = tsTelValue.replace('-','');
	newStrTemp = tsTelValue.replace(' ','');
	newLen = newStrTemp.length;
	if(newLen>18) return false;
	strTemp="0123456789- ";
	for (i=0;i<len;i++){
		j=strTemp.indexOf(tsTelValue.charAt(i));
		if (j==-1) return false;
	}
	return true;
}
function ValidateTelephone(tsFileName,toObject,tbNotAllowEmpty){
    if(toObject != null)
    {        
        if(tbNotAllowEmpty)
           { if(!ValidateEmpty(tsFileName,toObject))return false;}
        else 
            if(IsEmpty(toObject.value)) return true;         
        if(!IsTelephone(toObject.value))
        {
            alert("��������Ч��"+ tsFileName +"��");
            toObject.focus();
            toObject.select();
            return false;
        }
    }
    return true;
}
//�ֻ���У��
function IsMobile(tsMobilValue)
{
	var MobileRe=/^(013|13|8613|015|15|8615|018|18|8618|014|14|8614)\d{9}$/;
	return MobileRe.test(tsMobilValue);
}
function ValidateMobile(tsFileName,toObject,tbNotAllowEmpty){    
    if(toObject != null)
    {
        if(tbNotAllowEmpty)
           { if(!ValidateEmpty(tsFileName,toObject))return false;}
        else 
            if(IsEmpty(toObject.value)) return true;           
        if(!IsMobile(toObject.value))
        {
            alert("��������Ч��"+ tsFileName +"��");
            toObject.focus();
            toObject.select();
            return false;
        }
    }
    return true;
}
//Email��֤
function IsEMail(tsEMail)
{
	if (tsEMail.length > 150)
		return false;
	if(tsEMail == "") return true;
	var regu = "^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+([a-zA-Z]{2}|net|NET|com|COM|gov|GOV|mil|MIL|org|ORG|edu|EDU|int|INT)$"
	var re = new RegExp(regu);
	if (tsEMail.search(re) != -1)
		return true;
	else
		return false;
}
function ValidateEMail(tsFileName,toObject,tbNotAllowEmpty){    
    if(toObject != null)
    {
        if(tbNotAllowEmpty)
           { if(!ValidateEmpty(tsFileName,toObject))return false;}
        else 
            if(IsEmpty(toObject.value)) return true;      
        if(!IsEMail(toObject.value))
        {
            alert("��������Ч��"+ tsFileName +"��");
            toObject.focus();
            toObject.select();
            return false;
        }
    }
    return true;
}
function ValidateEmpty(tsFileName,toObject){
    if(toObject != null)
    {
        if(IsEmpty(toObject.value))
        {
            alert("������"+ tsFileName +"��");
            toObject.focus();
            return false;
        }
    }
    return true;
}
function ValidateEmptyList(tsFileName,toObject){
    if(toObject != null)
    {
        if(IsEmpty(toObject.value))
        {
            alert("��ѡ��"+ tsFileName +"��");
            toObject.focus();
            return false;
        }
    }
    return true;
}

function ValidateDouble(tsFileName,toObject,tbNotAllowEmpty){    
    if(toObject != null)
    {
        if(tbNotAllowEmpty)
            if(!ValidateEmpty(tsFileName,toObject))return false;
        if(!IsDouble(toObject.value))
        {
            alert("��������ȷ��"+ tsFileName +"��");
            toObject.focus();
            return false;
        }
    }
    return true;
}

function ValidateInteger(tsFileName,toObject,tbNotAllowEmpty){    
    if(toObject != null)
    {
        if(tbNotAllowEmpty)
            if(!ValidateEmpty(tsFileName,toObject))return false;
        if(!IsInteger(toObject.value))
        {
            alert("��������ȷ��"+ tsFileName +"��");
            toObject.focus();
            return false;
        }
    }
    return true;
}
//��֤��������󳤶ȣ����TextBox �� TextMode="MultiLine" MaxLengthʧЧ�����
function  checkTextLength(toObject,tnlength)   
{   
    if(toObject != null)
    {   
        if(toObject.value.length>tnlength-1)   
        {   
            window.alert("��ȷ���ı��������������󳤶�Ϊ"+tnlength+"���ַ����������ֽ��Զ��ض�");   
            toObject.value   =   toObject.value.substring(0,tnlength-1);  
        }   
    }               
}
//��֤ʱ������
function ValidateDate(tsFileName,toObject,tbNotAllowEmpty){
   if(toObject != null)
    {
        if(tbNotAllowEmpty)
            if(!ValidateEmpty(tsFileName,toObject))return false;
        if(!IsDate(toObject.value))
        {
            alert("��������ȷ��"+ tsFileName +"��");
            toObject.focus();
            toObject.select();
            return false;
        }
    }
    return true;
}
//tsSDateField ��ʼʱ���ֶα��⣬��ʼʱ��Ŀؼ��������ֶα��⣬����ʱ��Ŀؼ���
function ValidataDateRange(tsSDateField,toSDateCtr,tsEDateField,toEDateCtr){
    var ldSDate = CDate(toSDateCtr.value);
    if(ldSDate == null || ldSDate =="")
    {
        alert("������" +tsSDateField +"��");
        toSDateCtr.focus();
        return false;
    }
    var ldEDate = CDate(toEDateCtr.value);
    if(ldEDate == null || ldEDate =="")
    {
        alert("������" +tsEDateField +"��");
        toEDateCtr.focus();
        return false;
    }
    if(DateDiff(ldEDate, ldSDate)<0){ 
        alert(tsEDateField +"��������" + tsSDateField  +"��");
        toEDateCtr.focus();
        return false;   
    }
    return true;
}

//�ж�һ���ֻ����Ƿ���ȷ
String.prototype.isMobile = function() {
  return (/^13[0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$|^147[0-9]{8}$|^18[0-9]{1}[0-9]{8}$/.test(this.Trim()));
}
//Trim()
String.prototype.Trim = function() {
  var m = this.match(/^\s*(\S+(\s+\S+)*)\s*$/);
  return (m == null) ? "" : m[1];
}

//�Ǹ�����
String.prototype.isNotNegativeInt = function(){
if( this.Trim() == "" )
return true ;
return (/^\d+$/.test(this.Trim()));
}

//�Ǹ�������
String.prototype.isNotNegativeFloat = function(){
if( this.Trim() == "" )
return true ;
return (/^\d+(\.\d+)?$/.test(this.Trim()));
}

//������
String.prototype.isFloat = function(){
if( this.Trim() == "" )
return true ;
return (/^(-?\d+)(\.\d+)?$/.test(this.Trim()));
}

//����
String.prototype.isInt = function(){
if( this.Trim() == "" )
return true ;
return (/^-?\d+$/.test(this.Trim()));
}

//ֻ��Ϊ������ĸ
function  checkTextPattern(toObject,Falg)   
{   
    if(toObject != null)
    {       
        var patterns = /^[a-zA-Z0-9]+$/g;
        if(!patterns.test(toObject)==true)
        {
            alert(""+Falg+"ֻ��Ϊ������ĸ��"); 
            return false;
        }
    }
    return true;               
}
//����Ϊȫ��
function  checkTextSbccase(toObject,Falg)   
{   
    if(toObject != null)
    {       
        var patterns = /[^\uff00-\uffff]/g;
        if(!patterns.test(toObject)==true)
        {
            alert(""+Falg+"����Ϊȫ�ǣ�"); 
            return false;
        }
    }
    return true;               
}
//�����������󳤶�
function CheckMaxLength(strTemp,mlt,name)    
{    
    var i,sum;    
    sum=0;    
    for(i=0;i<strTemp.length;i++)    
    {    
      if ((strTemp.charCodeAt(i)>=0) && (strTemp.charCodeAt(i)<=255))    
        sum=sum+1;    
      else   
        sum=sum+2;    
    }    
    if(mlt < sum)
    {
        alert(""+name+"�����������󳤶�Ϊ"+mlt+"���ַ���"); 
        return false;  
    }
    else
        return true;    
}
//ֻ��������ĸ��  
function  checkTextboxPattern(toObject,Falg)   
{   
    if(toObject != null)
    {    
        var patterns = /^[a-zA-Z]+$/g;
        if(!patterns.test(toObject)==true)
        {
            alert(""+Falg+"ֻ��Ϊ��ĸ��"); 
            return false;
        }
    }
    return true;               
}  




var aCity =
{
'11' : "����",
'12' : "���",
'13' : "�ӱ�",
'14' : "ɽ��",
'15' : "���ɹ�",
'21' : "����",
'22' : "����",
'23' : "������",
'31' : "�Ϻ�",
'32' : "����",
'33' : "�㽭",
'34' : "����",
'35' : "����",
'36' : "����",
'37' : "ɽ��",
'41' : "����",
'42' : "����",
'43' : "����",
'44' : "�㶫",
'45' : "����",
'46' : "����",
'50' : "����",
'51' : "�Ĵ�",
'52' : "����",
'53' : "����",
'54' : "����",
'61' : "����",
'62' : "����",
'63' : "�ຣ",
'64' : "����",
'65' : "�½�",
'71' : "̨��",
'81' : "���",
'82' : "����",
'91' : "����"
}

function cidInfo(sId){
var iSum=0
var info=""
if(!/^\d{17}(\d|x)$/i.test(sId))return "Error:�������֤��";
sId=sId.replace(/x$/i,"a");
if(aCity[parseInt(sId.substr(0,2))]==null)return "Error:�Ƿ�����";
sBirthday=sId.substr(6,4)+"-"+Number(sId.substr(10,2))+"-"+Number(sId.substr(12,2));
var d=new Date(sBirthday.replace(/-/g,"/"))
if(sBirthday!=(d.getFullYear()+"-"+ (d.getMonth()+1) + "-" + d.getDate()))return "Error:�Ƿ�����";
for(var i = 17;i>=0;i --) iSum += (Math.pow(2,i) % 11) * parseInt(sId.charAt(17 - i),11)
if(iSum%11!=1)return "Error:�Ƿ����֤��";
return "";//aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"��":"Ů")
}



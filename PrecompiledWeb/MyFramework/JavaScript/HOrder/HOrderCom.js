// JScript 文件
//得到客户的可担保信用卡号
function getCustomerCard(tnCustomerID,tnCarkTypeID)
{
    document.getElementById("txtCardNum").value = "";
    document.getElementById("txtCardHoldName").value = "";
    document.getElementById("txtLimitCard").value = "";
    var toResponse =  CRM.Buzlogic.Hotel.HOAjax.getCustomerCard(tnCustomerID,tnCarkTypeID);
    if(toResponse != null && toResponse.value != null)
    {
        document.getElementById("txtCardNum").value = toResponse.value[0];
        document.getElementById("txtCardHoldName").value = toResponse.value[1];
        document.getElementById("txtLimitCard").value = toResponse.value[2];
    }
}
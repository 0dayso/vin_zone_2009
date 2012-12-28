    /// Created by: yanght	Date: 2007/09/12		Version 02.00
	/// Change history:
	/// Ver   | Date       | Modifier | Description		
	/// 02.00 | 2007/09/12 | yanght   | The class is created.
	/// the class is used for operate the xml dom in client
	
function XMLDOMBase()
{    
    // 属性
    this.moXMLDom = null;    
        
    this.XslSheetName = "";
    this.ContentControl = null;
    this.XMLControl = null;
    
    //方法
    this.getXMLDom = HOgetXMLDom;               //得到Dom对象
    this.refreshContent = HOrefreshContent;     //刷新控件
    this.getRootDocument = HOgetRootDocument;   //得到根节点
    this.addNode = HOaddNode;                   //添加节点
    this.appendChildNode = HOappendChildNode;   //添加子节点
    this.getNodeValue = HOgetNodeValue;         //获得某一个节点的值
    this.setNodeValue = HOsetNodeValue;         //设置节点的值(如果此节点没有找到，则新加一个节点)
    this.getNode = HOgetNode;                   //得到一个节点
    this.deleteNode = HOdeleteNode;             //删除一个节点
    this.save = HOsave;                         //保存
    this.getRootDocumentEx = null;              //得到根节点的扩展，需要继承此类的子类来完成
    
    function HOrefreshContent(){   
        try{
            this.getRootDocument();
            var moXslSheet = new ActiveXObject("Microsoft.XMLDOM");
            moXslSheet.async = false
            moXslSheet.load(this.XslSheetName);
            this.ContentControl.innerHTML = this.getXMLDom().transformNode(moXslSheet);
        }catch(e){
            alert("客户端加载错误！"+"\r\n"+e.description);
       }
    }
    
    function HOgetXMLDom(){
        if(this.moXMLDom==null){
            this.moXMLDom = new ActiveXObject("Microsoft.XMLDOM");
            this.moXMLDom.loadXML(this.XMLControl.value);
        }
        if(this.moXMLDom==null){
            alert("订单数据加载错误！");
        }   
        return this.moXMLDom;
    }
        
    function HOgetRootDocument(){
        if(this.getXMLDom().childNodes[0].childNodes[0] != null)   
            return this.getXMLDom().childNodes[0].childNodes[0];
        else{     
            if(this.getRootDocumentEx != null)
                return this.getRootDocumentEx();    
        } 
        return null;    
    }
    
    function HOgetNodeValue(toParentNode,tsNodeName)
    {
        if(this.getNode(toParentNode,tsNodeName)!=null)
            return this.getNode(toParentNode,tsNodeName).text; 
        return "";
    }
    
    function HOgetNode(toParentNode,tsNodeName){
        if(toParentNode.selectNodes(tsNodeName).length != 0 )
            return toParentNode.selectNodes(tsNodeName).item(0);    
        return null;
    }
    
    function HOsetNodeValue(toParentNode,tsNodeName,toValue)
    {
        if(this.getNode(toParentNode,tsNodeName)!=null){
           this.getNode(toParentNode,tsNodeName).text = toValue; 
        }else{
            this.appendChildNode(toParentNode,tsNodeName,toValue)
        } 
    }
    
    function HOdeleteNode(toChildNode)
    {
        if(toChildNode!= null){
            if(toChildNode.parentNode!=null)
               toChildNode.parentNode.removeChild(toChildNode);
            else
                alert("要删除节点的父节点没有找到！");
        }
    
    }    
    
    function HOaddNode(tsNodeName){
	    var loNewNode = this.getXMLDom().createElement(tsNodeName);
        return loNewNode;
    }
    
    function HOappendChildNode(toParentNode,tsNodeName,toValue)
    {
        if(IsEmpty(toValue)) return;
        var loChildNode = this.getNode(toParentNode,tsNodeName);
        if(loChildNode == null)
            loChildNode = this.addNode(tsNodeName);            
        loChildNode.text = toValue;
        toParentNode.appendChild(loChildNode);
    } 
    
    function HOsave()
    {
        this.XMLControl.value = this.getXMLDom().xml;    
    }
}

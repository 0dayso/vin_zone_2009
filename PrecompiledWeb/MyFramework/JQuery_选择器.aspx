<%@ page language="C#" autoeventwireup="true" inherits="JQuery_选择器, App_Web_x03uetc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>


    <style type="text/css">
        .red
        {
            background-color: Red;
        }
        .green
        {
            background-color: Green;
        }
        .blue
        {
            background-color: Blue;
            font-size: x-small;
            font-weight: bold;
        }
        .yellow
        {
            background-color: Yellow;
            font-size: x-large;
        }
        li
        {
            padding: 0 3px;
        }
        .horizontal
        {
            float: left;
            list-style: none;
            margin: 10px;
        }
        .sub-level
        {
            background: #ffc;
        }
        a
        {
            color: #00f;
        }
        a.mailto
        {
            color: #f00;
        }
        a.pdflink
        {
            color: #090;
        }
        a.mysite
        {
            text-decoration: none;
            border-bottom: 1px dotted #00f;
        }
        .table-heading
        {
            background-color: #ff0;
        }
        th
        {
            text-align: left;
        }
        .odd
        {
            background-color: #ffc;
        }
        .even
        {
            background-color: #eee;
        }
        .highlight
        {
            color: #f00;
            font-weight: bold;
        }
        .italic
        {
            font-style: italic;
        }
        .bold
        {
            font-weight: bold;
        }
        .table-heading
        {
            background: #0066ff;
            color: #ffffff;
            line-height: 20px; /*  */
            height: 30px;
        }
    </style>

    <script type="text/javascript">

        //我们使用  $(document).ready()  包住我们的  jQuery 代码，DOM 加载完毕后就可以使它所有东西都可用。
        $(document).ready(function ()
        {
            $('span:contains(冯瑞涛)').addClass('red');
        });

        // 添加风格，让list横向排列
        $(document).ready(function ()
        {
            //选择#selected-plays    下面的li元素
            $('#selected-plays > li').addClass('horizontal');
            //递归所有li，自定义选择器:not 排除.horizontal类的元素
            $('#selected-plays li:not(.horizontal)').addClass('sub-level');
        });

        // 使用XPath 属性选择器 为链接分配Class
        $(document).ready(function ()
        {
            //正则表达式，获得所有内容为mailto:开始的
            $('a[href^="mailto:"]').addClass('mailto');
            //正则表达式，内容为.pdf 结尾的
            $('a[href$=".pdf"]').addClass('pdflink');
            //正则表达式，内容任何位置为finehappy的
            $('a[href*="finehappy"]').addClass('mysite');
        });

        //
        $(document).ready(function ()
        {
            //为th的父对象tr添加类
            $('th').parent().addClass('table-heading');
            //tr，除了内容存在th 属性 的并且TR索引值匹配为偶数的元素
            $('tr:not([th]):even').addClass('even');
            //奇数
            $('tr:not([th]):odd').addClass('odd');
            //$('tr:not([th])').filter(':odd').addClass('odd'); //同样可以实现

            //发现存在WPF的td
            $('td:contains("WPF")').addClass('highlight');
            //自定义选择器，内容中带有WPF 的同辈（同级别）为td的元素 高亮显示
            $('td:contains("WPF")~ td').addClass('highlight');
            /* 一下这些实现可以得到上面同样的结果 ，代表了jQuery选择器的灵活和链接能力
            1．得到包含  Henry 的单元格，然后它的兄弟（不只是下一个的兄弟）。加入这个类： 
            $('td:contains("Henry")').siblings().addClass('highlight'); 
            2．得到包含  Henry 的单元格，得到它的父亲，然后查找所有在它里面大于0的单元格（0 
            是第一个单元格），加入这个类： 
            $('td:contains("Henry")').parent().find('td:gt(0)') .addClass('highlight'); 
            3．得到包含  Henry 的单元格，得到它的父亲，查找所有在它里面，然后过滤那些除了包 
            含  Henry的，加入这个类： 
            $('td:contains("Henry")').parent().find('td').not(': contains("Henry")') ).addClass('highlight'); 
            4．得到包含  Henry 的单元格，得到它的父亲，查找在它的孩子里面的第二个单元格，然 
            后加入这个类，取消上一个  .find() ，在孩子里查找第三个单元格，并加入这个类： 
            $('td:contains("Henry")').parent().find('td:eq(1)').addClass( 'highlight').end().find('td:eq(2)').addClass('highlight');
            */


        });




    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>冯瑞涛，祝愿 找到我博客的同学 2009年 身体健康</span>
        <ul id="selected-plays">
            <li>北京
                <ul>
                    <li><a href="http://www.finehappy.com">finehappy 网站</a></li>
                    <li>宣武</li>
                </ul>
            </li>
            <li>上海
                <ul>
                    <li><a href="finemoon.pdf">书籍下载</a></li>
                    <li>浦东</li>
                </ul>
            </li>
            <li>广州
                <ul>
                    <li>Mailto：<a href="mailto:fengruitao@gmail.com">我的邮件</a>
                        <ul>
                            <li>级别 1</li>
                            <li>级别 2</li>
                        </ul>
                    </li>
                    <li>天河</li>
                </ul>
            </li>
        </ul>
        <h2>
            图书阅读</h2>
        <table border="0" cellspacing="1" cellpadding="5">
            <tr>
                <th>
                    图书名称
                </th>
                <th>
                    作者
                </th>
                <th>
                    出版日期
                </th>
            </tr>
            <tr>
                <td>
                    WPF 揭秘
                </td>
                <td>
                    Adam Nathan
                </td>
                <td>
                    2007年1月
                </td>
            </tr>
            <tr>
                <td>
                    WCF 揭秘
                </td>
                <td>
                    ****
                </td>
                <td>
                    2007年2月
                </td>
            </tr>
            <tr>
                <td>
                    SharePoint Service 3.0 开发指南
                </td>
                <td>
                    Todd C. Bleeker
                </td>
                <td>
                    2007年3月
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
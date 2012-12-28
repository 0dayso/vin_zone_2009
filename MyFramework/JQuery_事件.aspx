<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JQuery_事件.aspx.cs" Inherits="JQuery_事件" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
  

    <style type="text/css">
        .poem
        {
            margin: 0 5em;
        }
        .chapter
        {
            margin: 1em;
        }
        #switcher
        {
            float: right;
            background-color: #ddd;
            border: 1px solid #000;
            margin: 10px;
            padding: 10px;
            font-size: .9em;
        }
        #switcher h3
        {
            margin: 0;
        }
        /*  按钮类 */#switcher .button
        {
            width: 100px;
            float: left;
            text-align: center;
            margin: 10px;
            padding: 10px;
            background-color: #fff;
            border-top: 3px solid #888;
            border-left: 3px solid #888;
            border-bottom: 3px solid #444;
            border-right: 3px solid #444;
        }
        #header
        {
            clear: both;
        }
        body.large #container .chapter
        {
            font-size: 1.5em;
        }
        body.narrow #container .chapter
        {
            width: 400px;
        }
        .selected
        {
            font-weight: bold;
        }
        .hidden
        {
            display: none;
        }
        #switcher .hover
        {
            cursor: pointer;
            background-color: #afa;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function ()
        {
            //#switcher .button 和上面的Style对应，这句话的意思是为每一个按钮类都绑定单击事件
            $('#switcher .button').click(function (event)
            {
                $('body').removeClass();
                if (this.id == 'switcher-narrow')
                {
                    $('body').addClass('narrow');
                }
                else if (this.id == 'switcher-large')
                {
                    $('body').addClass('large');
                }
                $('#switcher .button').removeClass('selected');
                $(this).addClass('selected');
                //阻止冒泡
                event.stopPropagation();

            });
        });

        $(document).ready(function ()
        {
            //鼠标移入和移出响应hover自定义方法有两个参数，分别是移入和移出时所对应的响应方法
            $('#switcher .button').hover(function ()
            {
                $(this).addClass('hover');
            }, function ()
            {
                $(this).removeClass('hover');
            });
        });
        //隐藏按钮和移入移出实现
        $(document).ready(function ()
        {
            //定义一个方法名字，方便多次调用
            var toggleStyleSwitcher = function ()
            {
                //toggleClass()如果存在类则删除，不存在则添加
                $('#switcher .button').toggleClass('hidden');
            };

            //注册单击事件的行为
            $('#switcher').click(toggleStyleSwitcher);


            //toggle方法会交替操作参数1和参数2
            $('#switcher').toggle(function ()
            {
                $('#switcher .button').addClass('hidden'); //隐藏所有按钮，因为jquery有迭代能力所有可以使所有button有效
            },
            function ()
            {
                $('#switcher .button').removeClass('hidden'); //去除所有按钮的隐藏css
            })


            //下面的两段是演示如何注册和移出dom元素上的事件
            //移除单击事件的行为
            $('#switcher-narrow, #switcher-large').click(function ()
            {
                $('#switcher').unbind('click', toggleStyleSwitcher);
                //#switcher 此时已经没有了单击的响应方法

            });

            //注册单击事件的行为，这个时候再点击#switcher才能好使
            $('#switcher-normal').click(function ()
            {
                $('#switcher').click(toggleStyleSwitcher);
            });
        });
        //模拟Switcher被单击
        $(document).ready(function ()
        {
            $('#switcher').click();
        });


    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="switcher">
            <h3>
                风格更改</h3>
            <!-- 标准按钮拥有两个类风格 用空格分开 -->
            <div class="button selected" id="switcher-normal">
                标准（还原）</div>
            <div class="button" id="switcher-narrow">
                缩小宽度</div>
            <div class="button" id="switcher-large">
                字体加大</div>
        </div>
        <div id="header">
            <h2>
                博客园</h2>
            <h2 class="subtitle">
                微软程序员的大本营</h2>
            <div class="author">
                冯瑞涛</div>
        </div>
        <div class="chapter" id="chapter-preface">
            <h3 class="chapter-title">
                jQuery 一步一步从入门到精通
            </h3>
            <p>
                摘要: 随着像Silverlight·和Flex 这样的技术不断的成熟，丰富的Web 应用程序已经不简单的局限在DHtml级别上的一些脚本动态。Ajax更是昙花一现成为了一项再普通不过的“小把戏”，虽然RIA的下一个发展方向已经很明朗，但现在（至少是近几年）“呆板”的HTML依然是无法取代的，我们依然要使用HTML，CSS，Javascript。
                那么我下面将要开始的jQuery想必你已经非常的了解，因为对他的赞誉真是铺天盖地，如果你想了解她的介绍、入门、教程、在网上随处可见，我没有打算再重复这些文字，因为对于jQuery这样非常容易上手的js库，也许一些典型的例子就足够了。
                今天开始我将记录jQuery的一些使用技巧（虽然我还是用一步一步从入门到精通做标题），希望能对找到我文章的朋友一点帮助。</p>
        </div>
    </div>
    </form>
</body>
</html>

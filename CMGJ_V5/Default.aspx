<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hidistro.UI.Web.Vshop.Default" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.SaleSystem.CodeBehind" Assembly="Hidistro.UI.SaleSystem.CodeBehind" %>
<%@ Register TagPrefix="H2" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<!DOCTYPE html>
<html lang="en-US">
<head>
    <meta charset="UTF-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=1" name="viewport" />
    <meta http-equiv="content-script-type" content="text/javascript">
    <meta name="format-detection" content="telephone=no" />
    <!-- uc强制竖屏 -->
    <meta name="screen-orientation" content="portrait">
    <!-- QQ强制竖屏 -->
    <meta name="x5-orientation" content="portrait">
    <title><%=htmlTitleName %></title>
    <meta name="description" content="<%=Server.HtmlEncode(Desc) %>" />
    <link rel="stylesheet" href="/Admin/Shop/PublicMob/css/dist/style.css?20160816">
    <link rel="stylesheet" href="/Admin/Shop/PublicMob/css/SpsBtn.css">
    <link rel="stylesheet" href="/Admin/Shop/PublicMob/css/Menu.css">
    <link rel="stylesheet" href="<%=cssSrc %>">
    <style type="text/css">
        /*隐藏美洽客服手机端右下角层*/
        .mc_button {
            display: none;
        }

        #followtx {
            width: 100%;
            height: 36px;
            padding: 2px 0;
            max-width: 640px;
            background: #333;
            position: absolute;
            line-height: 18px;
            display: none;
            z-index: 999;
            color: #fff;
            opacity: 0.9;
            left: 50%;
            transform: translateX(-50%);
            -webkit-transform: translateX(-50%);
            -moz-transform: translateX(-50%);
        }

            #followtx input {
                padding: 2px 6px;
                float: right;
                margin: 6px 10px 0 0px;
                display: block;
                background: #02b8b1;
                color: #fff;
                border: none;
                height: 24px;
            }

            #followtx .closeFlow {
                cursor: pointer;
                position: absolute;
                top: 8px;
                left: 10px;
                color: #fff;
                font-size: 24px;
            }

            #followtx .little-logo {
                width: 30px;
                height: 30px;
                border-radius: 50%;
                border: 1px solid #fff;
                float: left;
                margin: 2px 10px 0 40px;
            }

        .icon_buy {
            display: none !important;
        }

        .textBox {
            width: 54%;
            float: left;
            display: table;
            height: 100%;
        }

            .textBox em {
                display: table-cell;
                vertical-align: middle;
            }

        .members_con:nth-child(2) {
            position: absolute;
            top: 1em;
            width: 70%;
            margin: -10px 15%;
        }

            .members_con:nth-child(2) .members_search {
                border-radius: 2em;
                background-color: #fff;
                padding: 5px;
                height: 2em;
            }

        .members_search button {
            float: left;
            background-size: auto;
            width: 2em;
            height: 2em;
            margin: .3em 0 .3em 1em;
            background: url(/Admin/shop/PublicMob/images/hui3.png) no-repeat;
            background-size: 70%;
        }

        .members_search input {
            padding-left: 0px;
            height: 2em;
            font-size: 1em;
        }

        .new_message {
            background-color: #fff;
            overflow: hidden;
            font-size: 2vw;
            line-height: 2.5em;
            border: 1px solid #e0e0e0;
            border-radius: 2em;
        }

            .new_message a {
                color: #000;
            }

        .gg_hot {
            background-color: #e52a63;
            padding: 0 .5em;
            color: #fff;
            margin: 0 1em;
        }

        .fulltext {
            padding: 0 !important;
        }

        .gg_more {
            color: #000;
            position: absolute;
            top: 0;
            right: 2%;
        }

        .members_nav1 ul li {
            width: 20%;
        }

        .members_goodspic ul {
            background-color: #fff !important;
        }

            .members_goodspic ul li {
                width: 33.3% !important;
                background-color: #fff !important;
            }

                .members_goodspic ul li.mingoods a {
                    background-color: #fff !important;
                }

        .replace {
            color: #e52a63 !important;
            font-size: 3vw !important;
            margin: 5px 10px !important;
        }

        .alink {
            margin: 2px 10px;
            font-size: 3vw;
            color: #000;
            overflow: hidden;
            height: 2.6em;
        }
        .customer-service {
        display:none !important;
        }
    </style>
    <style type="text/css">
        ._div_title {
            margin: 2px 10px;
            font-size: 3vw;
            color: #000;
            overflow: hidden;
            height: 2.6em;
        }

        ._div_price {
            color: #e52a63 !important;
            font-size: 3vw !important;
            margin: 5px 10px !important;
        }

        ._div_xinpin {
            margin: 5px;
            background: #762f6b;
            color: #f8f6f7;
            padding: 1px 5px;
            display: inline-block;
            position: absolute;
        }

        ._div_shop {
            width: 5vw !important;
            height: 5vw !important;
            border-radius: 50% !important;
            padding: .5vw !important;
            bottom: 5% !important;
            right: 5% !important;
        }

        .original_price {
            display: none !important;
        }
    </style>
    <script src="/Utility/bootflat/js/jquery-1.10.1.min.js"></script>
    <script src="/Utility/WeixinApi.js?v=3.7"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <H2:WeixinSet ID="weixin" runat="server"></H2:WeixinSet>
    <script type="text/javascript">
        var wxinshare_title = '<%=Server.HtmlEncode(siteName)%>'; var wxinshare_desc = '<%=Server.HtmlEncode(Desc.Replace("\n","").Replace("\r",""))%>';
        var wxinshare_link = window.location.href; var fxShopName = '<%=Server.HtmlEncode(siteName)%>'; var wxinshare_imgurl = '<%=imgUrl%>';
    </script>
    <script>
        function closeFlowConcernLayer() {
            $("#followtx").css("display", "none");
            $.cookie("wxfollow", "true", { expires: 1 });/*24小时提醒一次*/
        }

        function closeBtnConcernLayer() {
            $.cookie("closefollow", "true", { expires: 1 });/*24小时提醒一次*/
            if (isMustConcern != "true") {
                $('.popup').remove();
                $('.popup-bg').remove();
            }
        }
        if (window.top.location != window.location) {
            window.top.location.href = window.location.href;
        }
    </script>
</head>
<body>
    <div class="membersbox pad50 " id="divCommon" style="max-width: 640px; margin: 0 auto; position: relative;">
        <Hi:HomePage runat="server" ID="H_Page"></Hi:HomePage>
        <section class="members_bottom">
            <%if (EnabeHomePageBottomLink)
                { %>
            <section>
                <a href="/Default.aspx">商城首页<i></i></a>
                <a href="/ProductList.aspx">所有商品<i></i></a>
                <a href="/Vshop/MemberCenter.aspx">会员中心<i></i></a>
                <a href="/Vshop/ShoppingCart.aspx">购物车<i></i></a>
                <%if (!string.IsNullOrEmpty(DistributionLinkName))
                    { %>  <a href="<%= DistributionLink %>"><%=DistributionLinkName %><i></i></a><%} %>
            </section>
            <%} %>
        </section>
        <section class="members_bottom">
            <%if (EnableHomePageBottomCopyright)
                { %>
            <section style="height: 48px;"><a href="<%= CopyrightLink %>" target="_blank" style="color: #b3b3b3;"><%= CopyrightLinkName %> </a></section>
            <%} %>
        </section>
    </div>
    <div id="mmexport">
        <img src="/Admin/Shop/PublicMob/images/mmexport.png" width="100%" alt="">
    </div>
    <!--关注-->

    <!-- 悬浮按钮 -->
    <div class="mask_menu" id="menubtn" style="display: none;"></div>
    <div class="menu" id="menufloat" style="display: none;"><i class="icon-menu"></i></div>
    <div class="menu-c notel nofocus" id="menufloat-c" style="">
        <div class="menu-c-out">
            <div class="menu-c-inner" id="menuIn">
            </div>
        </div>
    </div>

    <!-- 收藏 -->
    <div class="collectbg">
        <img src="/Admin/Shop/PublicMob/images/collectbg.png" width="100%" alt=""><a href="javascript:;" class="a-know">我知道了</a>
    </div>
    <!-- 分享 -->
    <div class="sharebg">
        <img src="/Admin/Shop/PublicMob/images/mmexport.png" width="100%" alt=""><a href="javascript:;" class="a-know">我知道了</a>
    </div>
    <script src="/Admin/Shop/PublicMob/js/dist/lib-min.js"></script>
    <script src="/Admin/Shop/PublicMob/js/dist/main.js?20160420"></script>
    <H2:MeiQiaSet ID="MeiQiaSet" runat="server"></H2:MeiQiaSet>
    <script src="/templates/common/script/WeiXinShare.js?2016"></script>
    <script>WinXinShareMessage(wxinshare_title, wxinshare_desc, wxinshare_link, wxinshare_imgurl);</script>
    <script src="/Admin/shop/Public/js/dist/underscore.js"></script>
    <script src="/Utility/ShowIndex.js?20160818"></script>
    <script src="/Utility/IndexSuspendMenu.js"></script>
    <script src="/Utility/jquery.cookie.js"></script>
    <script src="/Utility/lazyload.js"></script>
    <script>
        $(function () {
            $(".biggoods a.goodsimg img,.members_imgad a.goodsimg img").each(function () {
                var obj = this;
                if ($(obj).attr("data-original")) {
                    $(obj).attr("data-original", $(obj).attr("data-original").replace("thumbs310/310_", "images/"));
                }

            })
            $("img.imgLazyLoading").lazyload();
            /*检查视频参数是否出错*/
            if ($(".diyShowVideo").length > 0) {
                $(".diyShowVideo").each(function () {
                    $(this).find(".diy-video").each(function () {
                        if ($(this).attr("src") != null && $(this).attr("src").indexOf("false") == 0) {
                            $(this).hide();/*如果为false 则隐藏*/
                        }
                    });
                });
            }
            /*点击我要分销功能*/
            $(document).on('click', '#fxBtn', function () {
                $("#mmexport").show();
            });
            $(document).on('touchend', '#fxBtn', function () {
                $('#fxBtn').click();
            })
            $(document).on('touchend', "#mmexport", function () {
                $(this).hide();
            });

            $('.j-swipe').each(function (index, el) {
                var me = $(this);
                me.attr('id', 'Swiper' + index);
                var id = me.attr('id');
                var elem = document.getElementById(id);
                window.mySwipe = Swipe(elem, {
                    startSlide: 0,
                    auto: 3000,
                    callback: function (m) {
                        $(elem).find('.members_flash_time').children('span').eq(m).addClass('cur').siblings().removeClass('cur')
                    },
                });
            });

            (function () {
                /*
                控制添加商品的图片显示高度，确保商品布局正常*/
                $(".mingoods img").each(function () {
                    if ($(this).attr("src") == "") {
                        $(this).attr("src", "/Utility/pics/none.gif");
                    }
                });



                $('.b_mingoods,.mingoods').each(function (index, el) {
                    var me = $(this),
                        imgHeight = me.find('img').width();
                    me.find('img').closest('a').height(imgHeight);
                });
                $('.board3').each(function (index, el) {
                    var me = $(this);
                    var bwidth = me.width();
                    if (me.hasClass('small_board') || !me.hasClass('big_board')) {
                        me.children('span').attr('style', 'height:' + bwidth + 'px !important;overflow:hidden;');
                    }
                    if (me.hasClass('big_board')) {
                        me.children('span').attr('style', 'height:' + (bwidth * 2 + 10) + 'px !important;overflow:hidden;');
                    }
                });
                $('.mdetail_goodsimg ul li').each(function (index, el) {
                    var me = $(this);
                    var imgWidth = me.width();
                    me.height(imgWidth);
                });
                <%if (IsHomeShowFloatMenu)
        {%>
                /*悬浮按钮*/
                var SpsBtn = {
                    config: {
                        menu: document.getElementById('menufloat'),
                        menusub: document.getElementById('menufloat-c'),
                        menubtn: document.getElementById('menubtn')
                    },
                    init: function () {
                        this.SpsbtnClick();
                        this.touchMove();
                    },
                    touchMove: function () {
                        var _this = this;
                        _this.config.menu.addEventListener('touchmove', function (e) {
                            e.preventDefault();
                            var touch = e.touches[0],
                                moveX, moveY,
                                winWh = window.innerWidth - 50,
                                winHt = window.innerHeight - 50;
                            moveX = touch.clientX - 25;
                            moveY = touch.clientY - 25;
                            moveY = moveY < 0 ? 0 : moveY;
                            moveX = moveX < 0 ? 0 : moveX;
                            moveY = moveY > winHt ? winHt : moveY;
                            moveX = moveX > winWh ? winWh : moveX;
                            _this.config.menu.style.left = moveX + 'px';
                            _this.config.menu.style.top = moveY + 'px';
                            _this.config.menusub.style.left = moveX + 10 + 'px';
                            _this.config.menusub.style.top = moveY - 190 + 'px';
                        }, false)
                    },
                    SpsbtnClick: function () {
                        var _this = this;
                        this.config.menu.addEventListener('click', function () {
                            var me = $(this);
                            if (!me.hasClass('show')) {
                                me.addClass('show');
                                me.siblings('.mask_menu,#menufloat-c').show();
                                me.siblings('#menufloat-c').find('.menu-c-inner').addClass('in').removeClass('outer')
                            } else {
                                me.removeClass('show');
                                me.siblings('.mask_menu,#menufloat-c').hide();
                                me.siblings('#menufloat-c').find('.menu-c-inner').removeClass('in').addClass('outer')
                            }
                        }, false);
                        this.config.menubtn.addEventListener('click', function () {
                            $(_this.config.menu).removeClass('show');
                            $(_this.config.menu).siblings('.mask_menu,#menufloat-c').hide();
                            $(_this.config.menu).siblings('#menufloat-c').find('.menu-c-inner').removeClass('in').addClass('outer')
                        })
                    }
                }
                SpsBtn.init();
                $("#menufloat").show();
                <%}%>
                $('.a-know').click(function (event) {
                    $(this).parent('.collectbg,.sharebg').hide();
                });




                /*关注检查，没有关注，就引出提示，每天弹一次*/
                //$("#followtx").hide();
                var followtype = "";
                var WeixinfollowUrl = "<%=WeixinfollowUrl%>";
                var AlinfollowUrl = "<%=AlinfollowUrl%>";
                var enableAliPayFuwuGuidePageSet = "<%=EnableAliPayFuwuGuidePageSet%>".toLowerCase();
                var followurl = "";
                if (/micromessenger/i.test(navigator.userAgent.toLowerCase())) {
                    followtype = "wx"; /*微信端打开*/
                    followurl = WeixinfollowUrl;
                }
                else if (/alipay/i.test(navigator.userAgent.toLowerCase())) {
                    followtype = "fw"; /*服务窗口打开*/
                    followurl = AlinfollowUrl;
                }
                else {
                    return;
                }
                var isFollowWeixin = '<%=IsFollowWeiXin%>';//是否关注公证号
                var isFollowAliFuwu = '<%=IsFollowAliFuwu%>';//是否关注公证号

                //alert($.cookie(followtype + "follow"));
                //是否已经关闭过
                if ($.cookie(followtype + "follow")) {
                    return;
                }

                LoadForceFollowCss();

                ///获取配置文件
                $.post("/api/VshopProcess.ashx",
                { action: "GetSiteSettings" }, function (msg) {
                    enableGuidePageSet = msg.EnableGuidePageSet;
                    isAutoGuide = msg.IsAutoGuide;
                    isMustConcern = msg.IsMustConcern;
                    guideConcernType = msg.GuideConcernType;
                    guidePageSet = msg.GuidePageSet;
                    concernMsg = msg.ConcernMsg;
                    console.log("关注信息：" + msg.FollowInfo);
                    if (msg.FollowInfo != 4) {
                        //加载强制关注层
                        console.log(enableGuidePageSet);
                        if (enableGuidePageSet) {
                            LoadFollowtx(isMustConcern, enableGuidePageSet);
                            if (isAutoGuide) {
                                if (guideConcernType == 0) {
                                    OpenFollowMe(true);
                                }
                                else {
                                    window.location.href = guidePageSet;
                                }
                            }
                        } else {
                            if (isMustConcern) {
                                if (guideConcernType == 0) {
                                    if ($.cookie("closefollow")) {
                                        return;
                                    }
                                    OpenFollowMe(true);
                                }
                                else {
                                    window.location.href = guidePageSet;
                                }
                            }
                        }
                    }
                });


                $(document).on('click', '#FollowMe', function () {
                    OpenFollowMe(false);
                })

                /*公众号关注检查END*/
            })();
        });

        //弹出二维码窗口
        function OpenFollowMe(checkfollow) {
            if (checkfollow) {
                if ($.cookie("closefollow")) {
                    return;
                }
            }

            var isMustConcernHtml = "";
            if (!isMustConcern) {
                isMustConcernHtml = '<div class="closeBtn" onclick="closeBtnConcernLayer()">X</div>';
            }
            concernMsg == '' ? "随时获取优惠资讯" : concernMsg;
            var html = '<div class="popup">' +
                            '<div class="img"><img src="/api/qrcode/" style="width:100%;height:100%"></div>' +
                                '<p style="margin-top:10px">长按图片识别二维码</p>' +
                                '<p style="text-align:center; margin:0 -20px;">' + concernMsg + '</p>' +
                                  isMustConcernHtml +
                            '</div>' +
                        '<div class="popup-bg"></div>';
            $('body').append(html);
        }

        //获取分销商信息
        function LoadFollowtx(isMustConcern, enableGuidePageSet) {
            var isFollowTw = "";
            if (enableGuidePageSet && $.cookie("wxfollow") == null) {
                isFollowTw = "block";
            }
            else {
                isFollowTw = "none";
            }

            var closeFlowHtml = "";
            if (!isMustConcern) {
                closeFlowHtml = '<div class="closeFlow" onclick="closeFlowConcernLayer()">×</div>';
            }
            var followtxHtml = '<div id="followtx" style="display:' + isFollowTw + '">'
            + closeFlowHtml
            + '<div class="little-logo">'
            + '<img  id="imgSiteLogo" width="30" height="30" src="<%=imgUrl%>" style="border-radius: 50%;" />'
            + '</div>'
            + '<input type="button" id="FollowMe" value="一键关注" />'
            + '<div class="textBox">'
            + '<em>欢迎您加入【<%=siteName%>】</em>'
            + '</div>'
            + '</div>'
            $('body').append(followtxHtml);
        }
        //加载css文件
        function LoadForceFollowCss() {
            var link = document.createElement('link');
            link.type = 'text/css';
            link.rel = 'stylesheet';
            link.href = '../../Admin/Shop/PublicMob/css/Followtx.css?20160818';
            document.getElementsByTagName("head")[0].appendChild(link);
        }
    </script>
    <script type="text/javascript" src="/Admin/Shop/PublicMob/plugins/swipe/swipe.js"></script>
    <script>
        $(function () {
            var t = new Date().getMinutes();
            $.getJSON("/api/Hi_Ajax_GetProductsCount.ashx?t=" + t, function (data) {
                $("#goodsCount").html(data.count);
                if ($("#StoreName").length > 0) {
                    $("#StoreName").html(data.storeName);
                    if (data.logoUrl != "") {
                        $("#imglogo").attr("src", data.logoUrl);
                    }
                }
            })
        });
    </script>
    <script type="text/j-template" id="menu">
      <div class="menuNav" id="menuNav">
          <ul id="ul">
                    <# _.each(menuList,function(item){ #>
                    <li>
                   
                          <# if(item.SubMenus.length > 0){ #>
                            <div class="navcontent" data="1">
                            <# if(item.ShopMenuPic.length > 0){ #>
                                <img src="<#= item.ShopMenuPic#>" style="width :20px;height:20px;" />
                            <# } #>
                             <#= item.Name #>
                        
    	                    <div class="childNav">
                            <ul>
                                 <# _.each(item.SubMenus,function(chlitem){ #>
                                    <li><a href="<#= chlitem.Content #>"><#= chlitem.Name #></a></li>
                                 <# }) #>
                           </ul>
                           </div>
                
                        <# } else{ #>
                               <div class="navcontent" data="0">
                                 <a href="<#= item.Content #>">
                               <# if(item.ShopMenuPic.length > 0){ #>
                                <img src="<#= item.ShopMenuPic#>" style="width :20px;height:20px;" />
                                <# } #>
                               <#= item.Name #></a>
                         <# } #>
                        </div>
                    </li>
                    <# }) #>
            </ul>
    </div>
    </script>
    <script type="text/javascript">
        $(function () {
            $(".mingoods").each(function (x, y) {
                $(y).find("a:first").prepend('<label class="_div_xinpin">新品</label>');
                $(y).append('<span class="_div_shop" style="background: #e52a63 !important;"><img src="/Storage/template/file/gwc.png" width="100%" height="100%"></span>');
            })
        })
        function zxkf() {
            $("#meiqia_serviceico").click();
        }
    </script>
</body>
</html>

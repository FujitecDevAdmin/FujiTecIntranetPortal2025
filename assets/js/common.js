$(document).ready(function () {
    pageScript();
    $("#cssmenu").accordion();
    $('[data-toggle-second="tooltip"],.action-btn a').tooltip();
    $('[data-toggle-second="tooltip"]').on('click', function () {
        $(this).tooltip('hide');
    });
    $(document).on('click', '.filter-container a[data-toggle="collapse"]', function () {
        $('.filter-container a').next('div').removeClass('show');
        $(this).next('div').addClass('show');
    });
    $(document).on('click', '.ico-left.collapsed', function () {
        $('.body').animate({ marginLeft: 0 }, 350);
        $(this).removeClass('collapsed').addClass('expanded');
    });
    $(document).on('click', '.ico-left.expanded', function () {
        $('.body').animate({ marginLeft: 220 }, 350);
        $(this).removeClass('expanded').addClass('collapsed');
    });
    function pageScript() {
        var totHeight = $('body').outerHeight(true);
        var hdrHeight = $('header').outerHeight(true);
        var menuHeight = $('.main-menu').outerHeight(true);
        var ftrHeight = $('footer').outerHeight(true);
        var titleHeight = $('.page-title').outerHeight(true);
        var bdyHeight1 = totHeight - hdrHeight - menuHeight;
        $('.page-container').attr('style', 'height:' + bdyHeight1 + 'px;');
        //alert(hdrHeight);

        var URL = window.location.href;
        var PageName = URL.substring(URL.lastIndexOf('/') + 1);
        //alert(URL);

        //alert(ourLocation);
        $('.nav li a').each(function () {
            if ($(this).attr('href') === URL) {
                $(this).parents('li').addClass('open').parent('.submenu').show();
                $('.has-sub.open > a').addClass('submenu-indicator-minus');
                $(this).toggleClass('select');
            }
        });
    }
    $(window).bind('resize', function () {
        pageScript();
    });
});
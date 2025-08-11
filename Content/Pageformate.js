window.onload = function () {
    var w = window.open("about:blank", "main", "width=640,height=480", true)
    window.opener = "main";
    window.open("", "_parent", "");
    w.opener.close();
}
/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

if (navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/Android/i)
|| navigator.userAgent.match(/BlackBerry/i) || navigator.userAgent.match(/IEMobile/i)) {
    $("#viewpoint_device").attr("content", "initial-scale = 1");//Wert ändern wenn zu groß
}
else if (navigator.userAgent.match(/iPad/i)) {
    $("#viewpoint_device").attr("content", "initial-scale = 1.00");
}


function DisplayErrorCode(ErrorCode) {
    DisplayError("Es ist ein fehler aufgetreten (" + ErrorCode + ")");
}
function DisplayError(Error) {
    jQuery("#ErrorDisplay").css("display", "block");
    jQuery("#ErrorText").text(Error);
}
function CloseError() {
    jQuery("#ErrorDisplay").css("display", "none");
}

var WantedModalClose = false;
function OpenLoadingIndicator(BackDrop) {
    jQuery("#LoadingModal").modal({
        backdrop: (BackDrop == "true"),
        keyboard: false,
        show: true
    });
}
function CloseLoadingIndicator() {
    jQuery("#LoadingModal").modal("hide");
}
function ClearLoadingIndicator()
{
    jQuery("#LoadingModal").modal("hide");
}
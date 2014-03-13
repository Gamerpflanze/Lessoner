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
    jQuery("#LoadingModal").on("hide.bs.modal", function () {
        if (WantedModalClose) {
            WantedModalClose = false;
            return true;
        }
        else {
            return false;
        }
    });
    jQuery("#LoadingModal").modal({
        backdrop: (BackDrop == "true"),
        keyboard: false,
        show: true
    });
    return true;
}
function CloseLoadingIndicator() {
    WantedModalClose = true;
    jQuery("#LoadingModal").modal("hide");
}
function ClearLoadingIndicator()
{
    jQuery("body").removeClass("modal-open");
    jQuery(".modal-backdrop").remove();
}
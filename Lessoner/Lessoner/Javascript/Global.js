/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

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
function OpenLoadingIndicator() {
    jQuery("#LoadingModal").modal({
        backdrop: true,
        keyboard: false,
        show: true
    });
    jQuery("#LoadingModal").on("hide.bs.modal", function () {
        if (WantedModalClose) {
            WantedModalClose = false;
            return true;
        }
        else {
            return false;
        }
    });
}
function CloseLoadingIndicator() {
    WantedModalClose = true;
    jQuery("#LoadingModal").modal("hide");
}
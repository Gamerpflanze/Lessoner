/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function DisplayErrorCode(ErrorCode)
{
    DisplayError("Es ist ein fehler aufgetreten (" + ErrorCode + ")");
}
function DisplayError(Error)
{
    jQuery("#ErrorDisplay").css("display", "block");
    jQuery("#ErrorText").text(Error);
}
function CloseError()
{
    jQuery("#ErrorDisplay").css("display", "none");
}
function OpenFormDropdown()
{

}
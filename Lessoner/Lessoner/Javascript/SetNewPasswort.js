/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function SetPassword()
{
    if(jQuery("#Passwort").val() == jQuery("#RePasswort").val())
    {
        if(jQuery("#Passwort").val() != "")
        {
            jQuery("LoadingModal").modal({ backdrop: false, keyboard: false });
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        jQuery("#PasswordsNotMatching").css("display", "initial");
        jQuery("#SetPasswordContainer").addClass("has-error");
        jQuery("#RePasswort").val("");
        return false;
    }
}
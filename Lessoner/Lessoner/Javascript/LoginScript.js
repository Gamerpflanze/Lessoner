/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function CheckLoggedin(Page)
{
    $.ajax({
        type: "POST",
        url: Page + "/CheckLoggedin",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) {
            if(data.d!="")
            {
                jQuery("#LoginError").css("visibility", "none");
                var LoginForm = jQuery("#LoginForm");
                LoginForm.empty();

                var ProfileLink = jQuery("<a></a>");
                ProfileLink.text(data.d);
                LoginForm.append(ProfileLink);
            }
        },
        error: function (message) {
            LoginError('2002');
        }
    });
}
function SendLoginData(Page)
{
    var Username = jQuery("#Username").val();
    var Passwort = jQuery("#Password").val();

    if(Username==""||Passwort=="")
    {
        jQuery("#LoginError").removeAttr("class");
        jQuery("#LoginError").attr("class", "label label-danger");
        jQuery("#LoginError").text("Benutzername und Passwort angeben");
    }
    else
    {
        jQuery("#LoginError").removeAttr("class");
        jQuery("#LoginError").attr("class", "label label-default");
        jQuery("#LoginError").text("Lade...");
        $.ajax({
            type: "POST",
            url: Page+"/GetLoginData",
            async: true,
            contentType: "application/json; charset=utf-8;",
            dataType:"json",
            data: JSON.stringify({ 'Username': Username, 'Passwort': Passwort }),
            success: function (data) { LoginRecieve(data) },
            error: function (message) { LoginError('2001') }
        });
    }
}
function LoginRecieve(data)
{
    //TODO: Fehlerausgabe und weiterleitung
    jQuery("#LoginError").removeAttr("class");
    jQuery("#LoginError").attr("class", "label label-danger");
    if(data.d == "LoginDenited")
    {
        jQuery("#LoginError").text("Benutzername oder Passwort falsch");
    }
    else if (data.d == "ExeptionError")
    {
        jQuery("#LoginError").text("Es ist ein Fehler aufgetreten (1001)");
    }
    else if(data.d == "MultipleUserError")
    {
        jQuery("#LoginError").text("Es ist ein Fehler aufgetreten (1002)");
    }
    else
    {
        var LoginForm = jQuery("#LoginForm");
        LoginForm.empty();

        var ProfileLink = jQuery("<a></a>");
        ProfileLink.text(data.d);
        LoginForm.append(ProfileLink);
    }
}
function LoginError(message)
{
    jQuery("#LoginError").removeAttr("class");
    jQuery("#LoginError").attr("class", "label label-danger");
    jQuery("#LoginError").text("Es ist ein Fehler aufgetreten ("+message+")");
}
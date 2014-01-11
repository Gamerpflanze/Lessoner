/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />


function SendLoginData()
{
    var Username = jQuery("#Username").val();
    var Passwort = jQuery("#Password").val();

    if(Username==""||Passwort=="")
    {
        //TODO: Fehlermeldung
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetLoginData",
            async: true,
            contentType: "application/json; charset=utf-8;",
            dataType:"json",
            data: JSON.stringify({'Username':Username, 'Passwort':Passwort}),
            success: function (data) { LoginRecieve(data) },
            error: function (message) { AjaxError(message) }
        });
    }
}

function LoginRecieve(data)
{
    //TODO: Fehlerausgabe und weiterleitung
    if(data.d == "LoginDenited")
    {

    }
    else if(data.d == "Error")
    {

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

function AjaxError(message)
{
    alert("error");
}
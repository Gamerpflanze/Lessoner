/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />


function SendLoginData()
{
    var Username = jQuery("#Username").val();
    var Passwort = jQuery("#Password").val();

    if(Username==""||Passwort=="")
    {
        //Fehlermeldung hier
        var Error = jQuery("<b></b>");
        Error.text("Fehler");
        Error.appendTo("#maincontainer");
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetLoginData",
            async:false,
            contentType: "application/json; charset=utf-8;",
            dataType:"json",
            data: JSON.stringify({'user':Username, 'pass':Passwort}),
            success: function (data) { LoginRecieve(data) },
            error: function (message) { AjaxError(message) }
        });
    }
}

function LoginRecieve(data)
{
    //TODO: Fehlerausgabe und weiterleitung
}

function AjaxError(message)
{
    alert("error");
}
/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" />

function leher()
{
    
    //TODO: Abfrage ob User Lehrer oder Schüler ist
    
}

function information()
{
    $.ajax({
        type: "POST",
        url: "profile.aspx/information",
        async: false,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data)
        {
            jQuery("#username").text(data.d[0] + " " + data.d[1] + " " + data.d[2]);
        },
        error: function (message) {
            LoginError('2003');
        }
    });
}
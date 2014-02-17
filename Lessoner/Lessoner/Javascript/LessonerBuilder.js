/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" 
/// <reference path="Global.js" />
var Dates;
var CurrentIndex=0;
function GetData()
{
    $.ajax({
        type: "POST",
        url: "LessonerBuilder.aspx/GetData",
        async: true,
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (data) { AddData(data)},
        error:function(data){
            DisplayErrorCode(2004)}
    });
}

function AddData(data)
{
    if(data.d=="NoAccessAllowed")
    {
        DisplayError("Sie haben keinen erlaubten Zugriff auf diese Seite (3001)")
    }
    else if (data.d == "NotLoggedIn")
    {
        DisplayError("Sie sind nicht Angemeldet (0001)")
    }
    else
    {
        Dates = data.d[0];
        jQuery("#WeekBegin").val(data.d[0][0]);
        var TBody = jQuery("#Lessoner");
        for (var i = 0; i < data.d[1].length;i++)
        {
            var row = jQuery("<tr></tr>");
            var time = jQuery("<td></td>");
            var Days = new Array();
            //TODO: Taginformationen holen
            for (var j = 0; j < data.d[1]; j++) {
                Days.push(jQuery("<td></td>"));
            }

            // column.text(data.d[1][i][0] + "<br>" + data.d[1][i][0] + "<br>" + data.d[1][i][0]);
            var Number = jQuery("<p></p>");
            var Begin = jQuery("<p></p>");
            var End = jQuery("<p></p>");

            Number.text(data.d[1][i][0]);
            Number.addClass("LessonerText");

            Begin.text(data.d[1][i][1]);
            Begin.addClass("LessonerText");

            End.text(data.d[1][i][2]);
            End.addClass("LessonerText");

            time.append(Number);
            time.append(Begin);
            time.append(End);

            row.append(time);
            TBody.append(row);
        }
    }
}

function NextDate()
{
    jQuery("#LastDate").removeAttr("disabled");
    CurrentIndex++;
    jQuery("#WeekBegin").val(Dates[CurrentIndex]);
    if(CurrentIndex==5)
    {
        jQuery("#NextDate").attr("disabled", "disabled");
    }
}

function LastDate() {
    jQuery("#NextDate").removeAttr("disabled");
    CurrentIndex--;
    jQuery("#WeekBegin").val(Dates[CurrentIndex]);
    if (CurrentIndex == 0) {
        jQuery("#LastDate").attr("disabled", "disabled");
    }
}
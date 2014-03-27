/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" 
/// <reference path="Global.js" />
var SelectedRow = jQuery("lel");
var SelectedIndex = -1;
var DoneChange = false;
var HasError = false;
function EditStudent(sender)
{
    var Sender = jQuery(sender);
    if (Sender.parent().index() == SelectedIndex)
    {
        return;
    }
    jQuery("#StudentRights").appendTo(jQuery("#StudentRightsKeeper"));
    jQuery("#StudentOptions").remove();
    for (var i = 0; i < SelectedRow.children().length; i++) {
        var Input = SelectedRow.first().children(":eq(" + i + ")").children().first();//After 2 hours of trying random stuff, this line of the script works. We are never going to find out why.
        Input.replaceWith(jQuery("<span>" + Input.val() + "</span>"));
    }

    SelectedRow = Sender.parent();
    SelectedIndex = Sender.parent().index();
    for (var i = 0; i < SelectedRow.children().length; i++) {
        var Label = SelectedRow.first().children(":eq(" + i + ")").children().first();
        Label.replaceWith(jQuery("<input type='text' class='form-control' onkeyup='TextChanged(this)' value='" + Label.html() + "' />"));
    }
    var TableRow = jQuery("<tr></tr>");
    var TableCell = jQuery("<td></td>");
    TableCell.attr("colspan", "7");
    jQuery("#StudentRights").appendTo(TableCell);
    TableCell.attr("id", "StudentOptions");
    TableRow.append(TableCell);
    TableRow.insertAfter(SelectedRow);
    for (var i = 0; i < jQuery("#StudentRights").children().length; i++)
    {
        var Current = jQuery("#StudentRights").children(":nth-child(" + (i+1) + ")").children().children("input");
        Current.attr("data-awfaopwjfafaibfauibf", i);
        if(SelectedRow.attr("data-rights")[parseInt(Current.parent().attr("data-location"))]==1)
        {
            Current.prop('checked', true);;
        }
        else
        {
            Current.prop('checked', false);
        }
    }
}
function RightChanged(Sender)
{
    DoneChange = true;
    jQuery(Sender).parent().parent().parent().parent().prev().addClass("warning");
    jQuery(Sender).parent().parent().parent().parent().prev().attr("data-changed", "true");
}
function TextChanged(Sender)
{
    DoneChange = true;
    var Input = jQuery(Sender);
    Input.parent().parent().attr("data-changed", "true");
    if(Input.val()=="")
    {
        HasError = true;
        Input.parent().addClass("has-error");
        Input.parent().parent().removeClass("warning");
        Input.parent().parent().addClass("danger");
    }
    else
    {
        if (!Input.parent().parent().hasClass("warning")) {
            var Current = Input.parent().parent();
            for (var i = 0; i < Input.parent().parent().children().length; i++)
            {
                if(Current.children(":nth-child(" + (i+1) + ")").children().val()=="")
                {
                    return;
                }
            }
            HasError = false;
            Input.parent().removeClass("has-error");
            Input.parent().parent().removeClass("danger");
            Input.parent().parent().addClass("warning");
        }
    }
}

function SaveStudents()
{
    var Table = jQuery("#StudentList").children("tbody");
    var Submit = new Array();
    for(var i = 1; i<=Table.children().length; i++)
    {
        var Current = Table.children(":nth-child(" + i + ")");
        Submit.push(new Array());
        //      S IT I 
        //Submit[][][]
        Submit[i - 1].push(Current.attr("data-newstudent"));
        Submit[i - 1].push(Current.attr("data-changed"));
        Submit[i - 1].push(Current.attr("data-id"));
        Submit[i - 1].push(Current.attr("data-rights"));
        Submit[i - 1].push(new Array());
        for(var j = 1; j<=Current.children().length; j++)
        {
            var Input = Current.children(":nth-child(" + j + ")").children();
            Submit[i - 1][4].push(Input.text());
        }
    }
    $.ajax({
        async: true,
        type: "POST",
        url: "Schuelerverwaltung.aspx/SaveStudent",
        data: JSON.stringify({ "StudentData": Submit }),//Converting JSON to JSON works,                                                                  because fuck dis
        dataType: "JSON",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            //make everything look guts and glory
        },
        error: function (message) {
            //sum error
        }
    });
}

window.onbeforeunload = function ()
{
    if(DoneChange)
    {
        return "Sie haben änderungen an einigen Schülern gemacht, die möglicherweise verloren gehen können.";
    }
}
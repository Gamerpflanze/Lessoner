/// <reference path="../JQuery/jquery-1.10.2.js" />
/// <reference path="../Bootstrap/js/bootstrap.js" 
/// <reference path="Global.js" />
var SelectedRow = jQuery([]);
var SelectedIndex = -1;
var DoneChange = false;
var HasError = false;
var CantEdit = false;
function EditTeacher(sender)
{
    if (CantEdit) { return; }
    var Sender = jQuery(sender);
    if (Sender.parent().index() == SelectedIndex)
    {
        return;
    }
    jQuery("#TeacherRights").appendTo(jQuery("#TeacherRightsKeeper"));
    jQuery("#TeacherOptions").parent().remove();
    for (var i = 0; i < SelectedRow.children().length; i++) {
        if (SelectedRow.children(":nth-child(" + (i+1) + ")").attr("data-ignoretransform") == "true") { continue; }
        var Input = SelectedRow.first().children(":eq(" + i + ")").children().first();//After 2 hours of trying random stuff, this line of the script works. We are never going to find out why.
        Input.replaceWith(jQuery("<span>" + Input.val() + "</span>"));
    }
    SelectedRow.removeAttr("id");
    SelectedRow = Sender.parent();
    SelectedRow.attr("id", "EditingRow");
    SelectedIndex = Sender.parent().index();
    for (var i = 0; i < SelectedRow.children().length; i++) {
        if (SelectedRow.children(":nth-child(" + (i+1) + ")").attr("data-ignoretransform") == "true") { continue;}
        var Label = SelectedRow.first().children(":eq(" + i + ")").children().first();
        Label.replaceWith(jQuery("<input type='text' class='form-control' onkeyup='TextChanged(this)' id='' value='" + Label.html() + "' />"));
    }
    var TableRow = jQuery("<tr></tr>");
    var TableCell = jQuery("<td></td>");
    TableCell.attr("colspan", "9");
    jQuery("#TeacherRights").appendTo(TableCell);
    TableCell.attr("id", "TeacherOptions");
    TableRow.append(TableCell);
    TableRow.insertAfter(SelectedRow);
    for (var i = 0; i < jQuery("#TeacherRights").children().length; i++)
    {
        var Current = jQuery("#TeacherRights").children(":nth-child(" + (i+1) + ")").children().children("input");
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
    if (CantEdit) { return; }
    var sender = jQuery(Sender);
    DoneChange = true;
    SelectedRow.addClass("warning");
    SelectedRow.attr("data-changed", "true");
    var Rights = SelectedRow.attr("data-rights");
    Rights = SetCharAt(Rights, (sender.children("input").prop("checked") ? 1 : 0), parseInt(sender.attr("data-location")));
    SelectedRow.removeAttr("data-rights");
    SelectedRow.attr("data-rights", Rights);
}
function TextChanged(Sender)
{
    DoneChange = true;
    var Input = jQuery(Sender);
    Input.parent().parent().attr("data-changed", "true");
    if(Input.val()=="")
    {
        if (Input.parent().attr("data-alowedempty") == "true")
        {
            return;
        }
        HasError = true;
        Input.parent().addClass("has-error");
        Input.parent().parent().removeClass("warning");
        Input.parent().parent().addClass("danger");
    }
    else
    {
        if (!Input.parent().parent().hasClass("warning")) {
            var Current = Input.parent().parent();
            Input.parent().removeClass("has-error");
            for (var i = 1; i < Input.parent().parent().children().length-1; i++)//1 Da titel übersprungen
            {
                var Value = Current.children(":nth-child(" + (i + 1) + ")").children().val();

                if (i == Input.parent().parent().children().length - 2) {
                    if (!IsValidEmailAddress(Value)) {
                        return;
                    }
                }
                if(Value=="")
                {
                    return;
                }
            }
            HasError = false;
            Input.parent().parent().removeClass("danger");
            Input.parent().parent().addClass("warning");
        }
    }
}

function SaveTeachers()
{
    if (CantEdit) { return; }
    if (!DoneChange || HasError)
    {
        return;
    }
    jQuery("#LoadingModal").modal({ backdrop: "static", keyboard: false });
    var EditRow = jQuery("#EditingRow");
    for (var i = 1; i <= EditRow.children().length; i++) {
        if (EditRow.children(":nth-child(" + i + ")").attr("data-ignoretransform") == "true") { continue; }
        var Input = EditRow.first().children(":nth-child(" + i + ")").children().first();
        Input.replaceWith(jQuery("<span>" + Input.val() + "</span>"));
    }
    EditRow.removeAttr("id");
    jQuery("#TeacherRights").appendTo(jQuery("#TeacherRightsKeeper"));
    jQuery("#TeacherOptions").parent().remove();
    SelectedRow = jQuery([]);
    SelectedIndex = -1;
    var Table = jQuery("#TeacherList").children("tbody");
    var Submit = new Array();
    for(var i = 1; i<=Table.children().length; i++)
    {
        var Current = Table.children(":nth-child(" + i + ")");
        Submit.push(new Array());
        Submit[i - 1].push(Current.attr("data-newTeacher"));
        Submit[i - 1].push(Current.attr("data-changed"));
        Submit[i - 1].push(Current.attr("data-id"));
        Submit[i - 1].push(Current.attr("data-rights"));
        Submit[i - 1].push(new Array());
        for(var j = 1; j<=Current.children().length; j++)
        {
            if (Current.children(":nth-child(" + j + ")").attr("data-ignoretransform") == "true") { continue; }
            var Input = Current.children(":nth-child(" + j + ")").children();
            Submit[i - 1][4].push(Input.text());
        }
    }
    $.ajax({
        async: true,
        type: "POST",
        url: "Lehrerverwaltung.aspx/SaveTeacher",
        data: JSON.stringify({ "TeacherData": Submit }),//Converting JSON to JSON works,                                                                  because fuck dis
        dataType: "JSON",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            if (data.d == 0)
            {
                for (var i = 1; i <= Table.children().length; i++)
                {
                    var CurrentRow=Table.children(":nth-child("+i+")");
                    CurrentRow.removeClass("warning");
                    CurrentRow.attr("data-newTeacher", "false");
                    CurrentRow.attr("data-changed", "false");
                }
                HasError = false;
                DoneChange = false;
                __doPostBack('MainList', '');
            }
            jQuery("#LoadingModal").modal("hide");
        },
        error: function (message) {
            jQuery("#LoadingModal").modal("hide");
            alert("error");
            alert(message);
            //TODO:sum error
        }
    });
}
function AddNewTeacher()
{
    if (CantEdit) { return; }
    HasError = true;
    DoneChange = true;
    var NewRow = jQuery('<tr data-changed="true" data-newTeacher="true" data-id="-1" class="danger" data-rights="0000000000000">\
            <td onclick="EditTeacher(this)" class="has-error" data-alowedempty="true"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td onclick="EditTeacher(this)" class="has-error"><span></span></td>\
            <td data-ignoretransform="true" class="has-error"><span></span></td>\
        </tr>');
    NewRow.appendTo(jQuery("#TeacherList"));
    NewRow.children(":first").click();
}
function DeleteTeacher(sender)
{
    jQuery("#DeleteTarget").val(jQuery(sender).attr("data-id"));
    jQuery("#DeleteConfirmModal").modal("show");
}
function SetCharAt(string, replacement, index) {
    return string.substring(0, index) + replacement + string.substring(index + String(replacement).length, string.length);
}

function IsValidEmailAddress(Email) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
    return pattern.test(Email);
};


function CantEditSetter() {
    CantEdit = true;
}


window.onbeforeunload = function ()
{
    if(DoneChange)
    {
        return "Sie haben änderungen an einigen Lehrern gemacht, die möglicherweise verloren gehen können.";
    }
}
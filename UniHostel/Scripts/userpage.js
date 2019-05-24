/// <reference path="jquery-3.3.1.min.js" />

$(document).ready(function () {
    var panels = $('.user-infos');
    var panelsButton = $('.dropdown-user');
    panels.hide();

    //Click dropdown
    panelsButton.click(function () {
        //get data-for attribute
        var dataFor = $(this).attr('data-for');
        var idFor = $(dataFor);

        //current button
        var currentButton = $(this);
        idFor.slideToggle(400, function () {
            //Completed slidetoggle
            if (idFor.is(':visible')) {
                currentButton.html('<i class="glyphicon glyphicon-chevron-up text-muted"></i>');
            }
            else {
                currentButton.html('<i class="glyphicon glyphicon-chevron-down text-muted"></i>');
            }
        })
    });

    //$('[data-toggle="tooltip"]').tooltip();

    var editButton = $("#edit-button");
    editButton.click(function () {
        var form = $("#edit-form");
        var fullname = $("#fullname").text().trim();
        var inputFullname = document.createElement("input");
        $(inputFullname).attr("type", "text")
            .attr("name", "Fullname")
            .attr("class", "form-control")
            .attr("value", fullname);
        var row = createRow("Fullname", inputFullname);

        document.getElementById("table").tBodies[0].appendChild(row);
        $(".update-field").each(function (i, object) {
            var content = object.getAttribute("data-content");
            var name = object.getAttribute("data-name");
            var input = document.createElement("input");
            $(input).attr("type", "text")
                .attr("name", name)
                .attr("class", "form-control")
                .attr("value", content);
            if (object.getAttribute("date-picker") != null) {
                $(input).datepicker();
            }
            $(input).appendTo(form);
            $(object).text("");
            $(object).append(input);
        });

        var submitButton = $("#submit-button");
        $(submitButton).attr("type", "submit");
        $("#footer").remove();
    });

    var changRoomButton = $("#change-room-button");
    changRoomButton.click(function () {
        var form = $("#edit-form");
        $(form).attr("action", "/Users/ChangeRoom");
        var inputRoomID = document.createElement("input");
        $(inputRoomID).attr("type", "text")
            .attr("name", "RoomID")
            .attr("class", "form-control")
            .attr("value", "");
        var row = createRow("RoomID", inputRoomID);

        document.getElementById("table").tBodies[0].innerHTML = "";
        document.getElementById("table").tBodies[0].appendChild(row);

        var submitButton = $("#submit-button");
        $(submitButton).attr("type", "submit");
        $("#footer").remove();
    });


});

var createRow = function (name, value) {
    var tr = document.createElement("tr");
    var td = document.createElement("td");
    td.textContent = name;
    var td2 = document.createElement("td");
    td2.appendChild(value);
    tr.appendChild(td);
    tr.appendChild(td2);
    return tr;
}
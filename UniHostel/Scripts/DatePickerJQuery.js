/// <reference path="jquery-3.3.1.min.js" />
/// <reference path="bootstrap-datepicker.min.js" />

$(document).ready(function () {
    $(".date-picker").each(function (index, object) {
        $(object).datepicker();
        console.log(object);
    });
   
})
$(document).ready(function () {
    $("#RoomID").change(function () {
        getPreviousNumber();
        getRoomPrice();
    });
    getPreviousNumber();
    getRoomPrice();
});

getPreviousNumber = function () {
    var RoomID = $("#RoomID option:selected").val();
    var list = $("input[data-prenum]");
    for (var i = 0; i < list.length; i++) {
        var serviceID = $(list[i]).attr("data-prenum");
        var resource = 'http://localhost:61368/Bills/GetPreviousNumber';
        var result = callAjax(resource, RoomID, serviceID, list[i]);
    }
};


getRoomPrice = function () {
    var ID = $("#RoomID option:selected").val();
    
    $.ajax({
        url: "http://localhost:61368/Bills/GetRoomPrice",
        data: {
            RoomID: ID
        },
        success: function (result) {
            if (result.success == true) {
                $("#RoomPrice").val(result.data);
            } else {
                console.log(result.message);
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}


callAjax = function (resource, roomID, serviceID, component) {
    $.ajax({
        url: resource + "?CompServiceId=" + serviceID + "&roomID=" + roomID,
        success: function (result) {
            $(component).val(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
};


function calculateCompulServiceMoney(obj) {
    var curNumList = $(".CurNum");
    var i = $(curNumList).index(obj);
    var curNum = +$(curNumList).eq(i).val();
    var preNum = +$(".PreNum").eq(i).val();
    var price = +$(".unit-price").eq(i).text();
    var amount = (curNum - preNum) * price;
    if (amount >= 0) {
        $(".amount").eq(i).text(amount);    
    }
    calculateTotal(null);
}

function calculateAdvancedServiceMoney(obj) {
    var quantity = +$(obj).val();
    var price = +$(obj).parents('tr').find(".unit-price").text();
    if (quantity >= 0 && price > 0) {
        console.log(obj);
        var amount = quantity * price;
        $(obj).parent('td').siblings(".amount").text(amount);
        calculateTotal(null);
    }
}

var calculateTotal = function (obj) {
    var otherFee = 0;
    if (obj != null) {
        otherFee = parseInt(obj.value);
    }
    var amountList = $(".amount");
    var total = otherFee;
    for (var i = 0; i < amountList.length; i++) {
        total += +$(amountList).eq(i).text();
    }
    total += +$("#RoomPrice").val();
    $(".total").val(total);
}

$("#bill-form").submit(function () {
    var compServiceList = new Array();
    var advancedServiceList = new Array();
    var today = new Date();
    $(".CompulsoryServiceID").each(function (i, each) {
        var serviceID = $(each).val();
        var preNum = +$(".PreNum").eq(i).val();
        var curNum = +$(".CurNum").eq(i).val();
        var amount = +$(each).parent("td").siblings(".amount").text();
        var details = {
            CompulsoryServiceID: serviceID,
            PreNum: preNum,
            CurNum: curNum,
            Time: today,
            Amount: amount
        };
        compServiceList.push(details);
    });

    $(".AdvancedServiceID").each(function (i, each) {
        var serviceID = $(each).val();
        var quantity = +$(".Quantity").eq(i).val();
        var amount = +$(each).parent("td").siblings(".amount").text();
        if (quantity != 0) {
            var details = {
                AdvancedServiceID: serviceID,
                Quantity: quantity,
                Time: today,
                Amount: amount
            };
            advancedServiceList.push(details);
        }
    });

    var ICollectionCompServicesJSON = JSON.stringify(compServiceList);
    var ICollectionAdvancedServicesJSON = JSON.stringify(advancedServiceList);

    $('<input />').attr('type', 'hidden')
        .attr('name', "ICollectionCompServicesJSON")
        .attr('value', ICollectionCompServicesJSON)
        .appendTo('#bill-form');

    $('<input />').attr('type', 'hidden')
        .attr('name', "ICollectionAdvancedServicesJSON")
        .attr('value', ICollectionAdvancedServicesJSON)
        .appendTo('#bill-form');

});

//checkValid = function () {
//    var returnValue = true;
//    var error = {};
//    var otherFee = +$("#OtherFee").val();
//    if (otherFee < 0) {
//        error.success = false;
//        error.message = "Other fee cannot be negative";
//        return error;
//    }
//    var amountList = $(".amount");
//    var total = otherFee;
//    for (var i = 0; i < amountList.length; i++) {
//        var num = +$(amountList).eq(i).text();
//        if (num < 0) {
//            break;
//        }
//    }
//    if (+$("#RoomPrice").val() < 0) {
//        returnValue = false;
//    }
//    return returnValue;
//}

function beforeUpdateIsPaid(object) {
    var value = object.value;
    var input = $("<input>")
        .attr("type", "hidden")
        .attr("name", "value").val(value);
    swal({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, sure!',
        reverseButtons: true
    }).then(
        value => {
            var form = document.getElementById("update-state-form");
            input.appendTo(form);
            form.submit();
        },
        dismiss => {
            swal(
                'Cancelled',
                "Your action is cancelled!!!",
                'error'
            )
        }
    );
    return false;
}

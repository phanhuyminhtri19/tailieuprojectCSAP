$(document).ready(function () {
    $("#recoverform").submit(function (e) {
        e.preventDefault();
        window.swal({
            title: "Checking...",
            text: "Please wait",
            //imageUrl: "images/ajaxloader.gif",
            showConfirmButton: false,
            allowOutsideClick: false
        });
        var username = $("#recoverform :input").eq(0).val();
        $.ajax({
            url: "http://localhost:61368/Home/RecoverPassword",
            data: {
                username: username,
            },
            success: function (result) {
                console.log(result);
                if (result != null && result.success == true) {
                    window.swal({
                        title: "Finished!",
                        type: "success",
                        html: result.message,
                        showConfirmButton: false,
                        timer: 2000
                    });
                } else {
                    window.swal({
                        title: "Error!",
                        type: "error",
                        text: result.message,
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            },
            error: function (err) {
                window.swal({
                    title: "Error!",
                    type: "error",
                    text: err.message,
                    showConfirmButton: false,
                    timer: 2000
                });
            }
        });
    });


});

function checkMatchPassword() {
    var password = $("#password").val();
    var confirm = $("#confirm-password").val();
    if (password !== confirm) {
        $("#message").text("Password does not match!!!");
        return false;
    }
    return true;
}
function confirmDeleteContent(id) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then(function (result) {
        alert(result.value);
    });
}

////send request to server and delete corresponding record
//function deleteContent(id) {
//    $.ajax({
//        type: "Post",
//        url: "/Admin/Delete/" + id,
//        success: function (data) {
//            if (data == "success") {
//                swal({
//                    title: "Delete Successfully!",
//                    text: "This record has been deleted",
//                    type: data,
//                    showConfirmButton: true,
//                    timer: 1000
//                }).then(() => {
//                    location.reload();
//                });
//            }
//        }
//    });
//}
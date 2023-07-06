var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    try {
        dataTable = $("#tblData").DataTable({
            "ajax": { url: "/Admin/Company/GetAll" },
            "columns": [
                { data: 'name', width: "25%" },
                { data: 'city', width: "20%" },
                { data: 'country', width: "15%" },
                {
                    data: 'id',
                    width: "25%",
                    render: function (data) {
                        return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/company/upsert?id=${data}" class='btn btn-sm btn-primary mx-2'/><i class='bi bi-pencil-square'></i>Edit</a>
                                <a onClick="Delete('/admin/company/delete?id=${data}')" class='btn btn-sm btn-danger mx-2'/><i class='bi bi-trash-fill'></i>Delete</a>
                            </div>`
                    }
                }
            ]
        })
    }
    catch (e) {
        console.log("EXCEPTION E ==> ", e)
    }
}

function Delete(url) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url,
                type: 'DELETE',
                success: function (data) {
                    toastr.success(data.msg)
                    dataTable.ajax.reload()
                }
            })
        }
    })

}
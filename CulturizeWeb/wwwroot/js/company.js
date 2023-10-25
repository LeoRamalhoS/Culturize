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
                    render: function (data, event, row) {

                        let statusButton;
                        if (row.active) {
                            statusButton = `<button onclick="Deactivate('${data}')" class='btn btn-sm btn-danger mx-2 rounded-pill'/><i class='bi bi-trash-fill'></i>Deactivate</button>`;
                        }
                        else {
                            statusButton = `<button onclick="Reactivate('${data}')" class='btn btn-sm btn-success mx-2 rounded-pill'/><i class='bi bi-play-fill'></i>Reactivate</button>`;
                        }

                        return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/company/upsert?id=${data}" class='btn btn-sm btn-primary mx-2 rounded-pill'/><i class='bi bi-pencil-square'></i>Edit</a>
                                ${statusButton}
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

function Deactivate(id) {
    Swal.fire({
        title: 'Deactivate Company?',
        text: "Users from this company won't be able to login!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, deactivate it!'
    })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/admin/company/deactivate?id=${id}`,
                    type: 'POST',
                    success: function (data) {
                        if (data.success)
                            toastr.success(data.msg)
                        else
                            toastr.error(data.msg)

                        dataTable.ajax.reload()
                    }
                })
            }
        })
}

function Reactivate(id) {
    Swal.fire({
        title: 'Reactivate Company?',
        text: "Users from this company will be able to login again!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, activate it!'
    })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/admin/company/activate?id=${id}`,
                    type: 'POST',
                    success: function (data) {
                        if (data.success)
                            toastr.success(data.msg)
                        else
                            toastr.error(data.msg)

                        dataTable.ajax.reload()
                    }
                })
            }
        })
}

function CreateDemoCompany() {
    Swal.fire({
        title: 'Generate demo company?',
        text: "This company will have placeholder data for demo purposes!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, generate it!'
    })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/admin/company/GenerateDemoCompany",
                    type: 'POST',
                    success: function (data) {
                        if (data.success)
                            toastr.success(data.msg)
                        else
                            toastr.error(data.msg)

                        location.reload(true);
                    }
                })
            }
        })
}
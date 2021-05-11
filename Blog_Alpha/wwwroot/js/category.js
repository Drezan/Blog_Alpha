var oTable;

$(document).ready(function () {
    loadDataTable();
    
});

function loadDataTable() {
    oTable = $("#tbCategories").DataTable({
        "ajax": {
            "url": "/Admin/Categories/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name" },
            { "data": "order" },
            //{ "data": "Created By" },
            //{ "data": "Created At" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class='text-center'> 
                                <a href='/Admin/Categories/Edit/${data}' class='btn btn-outline-warning'><i class='far fa-edit'></i></a>
                                &nbsp;
                                <a onclick='Delete(/Admin/Categories/Delete/${data})' class='btn btn-outline-danger'><i class='far fa-minus-square'></i></a>
                            </div>
                           `;
                },
                "width":"15%"
            }

        ],
        "language": {
            "emptyTable": "No hay registros."
        },
        "width":"100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure to delete this category?",
        text: "This content cannot be recovered!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6855",
        confirmButtonText: "Yes, i do",
        closeOnConfirm: true
    }, function () {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        oTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
                
            });
    });
}
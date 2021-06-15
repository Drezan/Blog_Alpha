var oTable;

$(document).ready(function () {
    loadDataTable()
});

function loadDataTable() {
    oTable = $("#tbArticles").DataTable({
        "ajax": {
            "url": "/Admin/Articles/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "title" },
            { "data": "description" },
            { "data": "category.name" },
            { "data": "created_At", "render": function (data) {
                    return (moment(data).isValid()) ? moment(data).format('LLL') : ""; } },
            { "data": "modified_At", "render": function (data) {
                    return (moment(data).isValid()) ? moment(data).format('LLL') : ""; } },
            //{ "data": "Autor" },
            //{ "data": "Modified By" }
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class='text-center'> 
                                <a href='/Admin/Articles/Edit/${data}' class='btn btn-outline-warning'><i class='far fa-edit'></i></a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Articles/Delete/${data}") class='btn btn-outline-danger'><i class='far fa-minus-square'></i></a>
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
    swal.fire({
        title: "Are you sure to delete this category?",
        text: "This content cannot be recovered!",
        showCancelButton: true,
        confirmButtonColor: "#DD6855",
        confirmButtonText: "Yes, i do"
    }).then((result) => {
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
var dataTable;
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    // код из вкладки Ajax сайта DataTables
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' }, // путь к методу getall
        // код из вкладки Data сайта DataTables
        "columns": [
            { data: 'carName', "width": "10%" },
            { data: 'description', "width": "40%" },
            { data: 'basicEquipmentPrice', "width": "10%" },
            { data: 'fullEquipmentPrice', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            { data: 'brand.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                         <a href="/admin/product/upsert?id=${data}"  class="btn btn-primary" style="height: 40px; width: 160px; border-radius: 10px;"> 
                         <i class="bi bi-pencil-square"></i> Edit</a>
                          <a onclick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2" style="height: 40px; width: 160px; border-radius: 10px;"> 
                          <i class="bi bi-trash-fill"></i>Delete</a>
                    </div>`
                },              
                "width": "10%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}


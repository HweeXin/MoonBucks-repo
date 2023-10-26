var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "id", "width": "15%" },
            { "data": "role", "width": "15%" },
            { "data": "email", "width": "15%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center">
                             <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:120px;">
                                    <i class="bi bi-lock-fill"></i>  Suspend
                                </a> 
                                <a href="user/UserManagement?userId=${data.id}" class="btn btn-primary text-white" style="cursor:pointer; width:150px;">
                                     <i class="bi bi-pencil-square"></i> Update
                                </a>
                        </div>
                    `
                    }
                    else {
                        return `
                        <div class="text-center">
                              <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:120px;">
                                    <i class="bi bi-unlock-fill"></i>  Unsuspend
                                </a>
                                <a href="user/UserManagement?userId=${data.id}" class="btn btn-primary text-white" style="cursor:pointer; width:150px;">
                                     <i class="bi bi-pencil-square"></i> Update
                                </a>
                        </div>
                    `
                    }


                },
                "width": "30%"
            }
        ]
    });
}
function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}
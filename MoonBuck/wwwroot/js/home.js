var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/staff/home/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'role.name', "width": "15%" },
            { data: 'startTime', "width": "15%" },
            { data: 'endTime', "width": "15%" },
            { data: 'isFilled', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/slot/upsert?id=${data}" class="btn btn-primary mx-2">Details</a>               
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

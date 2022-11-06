$(document).ready(function () {
    $("#combinedDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "Combined/AllEmployeeList2",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "EmployeeCode", "name": "Employee Code", "autoWidth": true },
            { "data": "FullName", "name": "Full Name", "autoWidth": true },
            { "data": "Email", "name": "Email", "autoWidth": true },
            { "data": "Address", "name": "Address", "autoWidth": true },
            { "data": "Salary", "name": "Salary", "autoWidth": true },
            { "data": "PositionName", "name": "Position", "autoWidth": true },
            { "data": "StartDate", "name": "Start Date", "autoWidth": true },
            { "data": "EndDate", "name": "End Date", "autoWidth": true },
            
        ]
    });
});  
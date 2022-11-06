var table;

$(document).ready(function () {
    $.fn.dataTable.moment("DD/MM/YYYY HH:mm:ss");
    $.fn.dataTable.moment("DD/MM/YYYY");

    table = $("combinedDatatable").DataTable({
        // Design Assets
        stateSave: true,
        autoWidth: true,
        // ServerSide Setups
        processing: true,
        serverSide: true,
        // Paging Setups
        paging: true,
        // Searching Setups
        searching: { regex: true },
        // Ajax Filter
        ajax: {
            url: "/Combined/AllEmployeeList2",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        // Columns Setups
        columns: [
            { data: "employeeCode" },
            { data: "fullName" },
            { data: "email" },
            { data: "address" },
            { data: "salary" },
            { data: "position" },
            {
                data: "startDate",
                render: function (data, type, row) {
                    // If display or filter data is requested, format the date
                    if (type === "display" || type === "filter") {
                        return moment(data).format("DD/MM/YYYY");
                    }
                    // Otherwise the data type requested (`type`) is type detection or
                    // sorting data, for which we want to use the raw date value, so just return
                    // that, unaltered
                    return data;
                }
            }
            {
                data: "endDate",
                render: function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        return moment(data).format("DD/MM/YYYY");
                    }
                    return data;
                }
            }
        ],
        // Column Definitions
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },
            {
                targets: 10,
                data: null,
                defaultContent: "<a class='btn btn-link' role='button' href='#' onclick='edit(this)'>Edit</a>",
                orderable: false
            },
        ]
    });
});

function strtrunc(str, num) {
    if (str.length > num) {
        return str.slice(0, num) + "...";
    }
    else {
        return str;
    }
}

function edit(rowContext) {
    if (table) {
        var data = table.row($(rowContext).parents("tr")).data();
        alert("Example showing row edit with id: " + data["id"] + ", name: " + data["name"]);
    }
}

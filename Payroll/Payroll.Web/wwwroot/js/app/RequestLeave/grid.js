function Grid() {
    $("#jsGrid1").jsGrid({

        width: "100%",
        pageSize: 15,
        sorting: true,
        paging: true,
        autoload: true,
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: 'GetData',
                    data: filter,
                    dataType: "JSON",
                });
            }
        },

        fields: [
            {
                name: "leave_date", title: "Date", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return '<a href="#" onclick="return getbyID(' + item.request_leave_id + ')">' + val + '</a>';
                },
            },
            { name: "ref_leave_type_.ref_leave_type_code", title: "Type", width: 40 },
            {
                name: "leave_day", title: "Day", type: "number", width: 20,
                itemTemplate: function (value) {
                    return value.toFixed(2);
                },
            },
            { name: "reason", title: "Reason", type: "text", width: 30 },
            { name: "ref_status_.ref_status_name", title: "Status", type: "text", width: 30 },


        ]
    });
}

function Grid2() {
    $("#jsGrid2").jsGrid({

        width: "100%",
        pageSize: 15,
        sorting: true,
        paging: true,
        autoload: true,
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: 'GetEmployeeBalance',
                    data: filter,
                    dataType: "JSON",
                });
            }
        },

        fields: [
            { name: "leave_type_name", title: "Leave Type", type: "text", width: 30 },
            { name: "earned", title: "Earned", type: "text", width: 30 },
            { name: "used", title: "Used", type: "text", width: 30 },
            { name: "balance", title: "Balance", type: "text", width: 30 },

        ]
    });
}
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
                    url: 'GetList',
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            {
                name: "employee_id", title: "Employee", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.employee_balance_id + ')">' + item.employee_.last_name + ', ' + item.employee_.first_name + '</a>';
                },
            },
            {
                name: "leave_type_id", title: "Type", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return item.ref_leave_type_.ref_leave_type_name;
                },
            },
            {
                name: "expire_date", title: "Expired", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return val;
                },
            },
            { name: "quantity", title: "Qty", type: "text", width: 30 },
            { name: "remarks", title: "Remarks", type: "text", width: 30 },
        ]
    });
}
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
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            {
                name: "overtime_date", title: "Date", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return '<a href="#" onclick="return getbyID(' + item.request_overtime_id + ')">' + val + '</a>';
                },
            },
            { name: "ref_overtime_type_.ref_overtime_type_name", title: "Type", width: 40 },
            { name: "time_in", title: "In", type: "text", width: 30 },
            { name: "time_out", title: "In", type: "text", width: 30 },
            { name: "reason", title: "Reason", type: "text", width: 30 },
            { name: "ref_status_.ref_status_name", title: "Status", type: "text", width: 30 },


        ]
    });
}
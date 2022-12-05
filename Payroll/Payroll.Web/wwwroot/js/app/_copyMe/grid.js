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
                    url: 'RefShift/GetData',
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            {
                name: "shift_name", title: "Shift", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.ref_shift_id + ')">' + item.shift_name + '</a>';
                },
            },
            { name: "shift_in", title: "Shift In", type: "text", width: 30 },
            { name: "shift_out", title: "Shift Out", type: "text", width: 30 },
            { name: "break_in", title: "Break In", type: "text", width: 30 },
            { name: "break_out", title: "Break Out", type: "text", width: 30 },
            { name: "required_hour", title: "Req Hr", type: "text", width: 30 },
        ]
    });
}
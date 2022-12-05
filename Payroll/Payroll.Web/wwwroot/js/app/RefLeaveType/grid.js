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
                name: "ref_leave_type_name", title: "Leave Type", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.ref_leave_type_id + ')">' + item.ref_leave_type_name + '</a>';
                },
            },
            { name: "ref_leave_type_code", title: "Code", type: "text", width: 30 },
            { name: "with_pay", title: "With Pay", type: "checkbox", width: 30 },

        ]
    });
}
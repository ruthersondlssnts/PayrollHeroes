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
                name: "employee_serial", title: "Employee No", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.employee_id + ')">' + item.employee_serial + '</a>';
                },
            },
            { name: "last_name", title: "Last Name", type: "text", width: 30 },
            { name: "first_name", title: "First Name", type: "text", width: 30 },
            { name: "role_.role_name", title: "Role", type: "text", width: 30 },

        ]
    });
}
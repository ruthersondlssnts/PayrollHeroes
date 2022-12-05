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
            { name: "role_name", title: "Role", type: "text", width: 20 },
            {
                name: "display_name", title: "Display Name", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.role_menu_id + ')">' + item.display_name + '</a>';
                }
            },
            { name: "controller_name", title: "Controller", type: "text", width: 30 },
            { name: "action_name", title: "Action", type: "text", width: 30 },
            { name: "display_icon", title: "Display Icon", type: "text", width: 30 },
            { name: "is_enable", title: "Active", type: "checkbox", width: 10, },
        ]
    });
}

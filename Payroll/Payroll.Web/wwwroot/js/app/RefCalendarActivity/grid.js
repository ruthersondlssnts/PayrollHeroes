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
                name: "work_date", title: "Holiday Date", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return '<a href="#" onclick="return getbyID(' + item.ref_calendar_activity_id + ')">' + val + '</a>';
                },
            },
            { name: "ref_day_type_.date_type_name", title: "Type", type: "text", width: 30 },
            { name: "description", title: "Description", type: "text", width: 30 },
           
        ]
    });
}
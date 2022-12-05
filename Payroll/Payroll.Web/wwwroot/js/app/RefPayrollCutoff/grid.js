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
                name: "cutoff_date_start", title: "Cutoff Start", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return '<a href="#" onclick="return getbyID(' + item.ref_payroll_cutoff_id + ')">' + val + '</a>';
                },
            },
            {
                name: "cutoff_date_end", title: "Cutoff End", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    var d = value.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    return val;
                },
            },
            { name: "is_processed", type: "checkbox", title: "Processed", sorting: false },
            { name: "government", type: "checkbox", title: "Government", sorting: false },
        ]
    });
}
function Grid() {
    var id = $('#cutoff').val();

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
                    url: 'GetListEarnings/' + id,
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            //{
            //    name: "ref_leave_type_name", title: "Leave Type", type: "text", width: 40,
            //    itemTemplate: function (value, item) {
            //        return '<a href="#" onclick="return getbyID(' + item.ref_leave_type_id + ')">' + item.ref_leave_type_name + '</a>';
            //    },
            //},
            { name: "ref_payroll_details_type_.ref_payroll_details_type_code", title: "Name", type: "text", width: 30 },
            { name: "qty", title: "Qty", type: "number", width: 10 },
            {
                name: "amount", title: "Amount", type: "number", width: 10,
                itemTemplate: function (value) {
                    return getAmount(value);
                }
            },

        ]
    });
}
function Grid2() {
    var id = $('#cutoff').val();

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
                    url: 'GetListDeduction/' + id,
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            //{
            //    name: "ref_leave_type_name", title: "Leave Type", type: "text", width: 40,
            //    itemTemplate: function (value, item) {
            //        return '<a href="#" onclick="return getbyID(' + item.ref_leave_type_id + ')">' + item.ref_leave_type_name + '</a>';
            //    },
            //},
            { name: "ref_payroll_details_type_.ref_payroll_details_type_code", title: "Name", type: "text", width: 30 },
            { name: "qty", title: "Qty", type: "number", width: 10 },
            {
                name: "amount", title: "Amount", type: "number", width: 10,
                itemTemplate: function (value) {
                    return getAmount(value);
                }
            },

        ]
    });
}
function getAmount(amount) {
    return Number(amount).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

function GetCutoff() {
    $.ajax({
        type: "GET",
        url: 'GetCutoff',
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_payroll_cutoff_id + '">' + data[i].ref_payroll_cutoff_name + '</option>';
            }
            $("#cutoff").html(s);
            Grid();
            Grid2();
        }

    });


}
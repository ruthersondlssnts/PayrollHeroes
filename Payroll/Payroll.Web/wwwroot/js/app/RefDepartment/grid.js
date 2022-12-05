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
                name: "ref_department_id", title: "Name", type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.ref_department_id + ')">' + item.department_name + '</a>';
                },
            },
        ]
    });
}

function Grid2(id) {
    
    $("#jsGrid2").jsGrid({

        width: "100%",
        pageSize: 15,
        paging: true,
        sorting: false,
        autoload: true,
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: 'GetApprover/' + id,
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },
        rowClass: function(item, itemIndex) {
            return "client-" + itemIndex;
        },
        onRefreshed: function() {
                    var $gridData = $("#jsGrid2 .jsgrid-grid-body tbody");
 
                    $gridData.sortable({
                        update: function(e, ui) {
                            var clientIndexRegExp = /\s*client-(\d+)\s*/;
                            var indexes = $.map($gridData.sortable("toArray", { attribute: "class" }), function(classes) {
                                return clientIndexRegExp.exec(classes)[1];
                            });
                            // arrays of items
                            var items = $.map($gridData.find("tr"), function(row) {
                                return $(row).data("JSGridItem");
                            });
                            console && console.log("Reordered items", items);
                        }
                    });
        },
        deleteConfirm: function(item) {
            return "The approver will be removed. Are you sure?";
        },
        fields: [
            { name: "name", title: "Name", type: "text", width: 70},
            {
                type: "control",
                modeSwitchButton: false,
                editButton: false, width: 30
            },
        ]
    });
}

//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    
    var items = $("#jsGrid2").jsGrid("option", "data");
    var arrayLength = items.length;
    for (var i = 0; i < arrayLength; i++) {
      alert(JSON.stringify(items[i]));

    }
    if (items == '')
    {
        swal_error("Please assign approvers.");
        return false;
    }
    var emp = JSON.stringify({
        'ref_department_id': $('#ref_department_id').val(),
        'department_name': $('#department_name').val(),
        'ref_department_approver_': JSON.stringify(items)
    });

    
    
    Swal.fire({
        title: 'Please Wait..!',
        text: 'Is working..',
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        onOpen: () => {
            swal.showLoading();
        }
    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        traditional: true,
        url: 'Update',
        data: emp,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            Grid();
            $('#myModal').modal('hide');
            Swal.hideLoading();
            swal_success();
        },
        error: function (response) {
            Swal.hideLoading();
            var err = JSON.parse(response);
            swal_error(err.errorMessage);
        }
    });
} 

function Add()
{
    var check = false;
    var emp_id = $('#approver_list').val();
    var name = $("#approver_list option:selected").text();

    var gridData = $("#jsGrid2").jsGrid("option", "data");
                                	 
    for (i = 0; i < gridData.length; i++) {                                		 
        if(emp_id == gridData[i].employee_id ){
            check=true;
        }
    }

    if (check==false)
    {
        // insert item
        $("#jsGrid2").jsGrid("insertItem", { employee_id: emp_id, name: name }).done(function() {
            console.log("insertion completed");
        });
    }
}
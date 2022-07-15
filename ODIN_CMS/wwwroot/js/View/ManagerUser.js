// Lấy thông tin User
function ViewUserData(id) {
    utils.Loading();
    $.ajax({
        type: 'GET',
        url: UrlRoot + "Home/GetUserInfo",
        data: {
            'id': id
        },
        dataType:'html',
        success: function (data) {
            utils.unLoading();
            $("#PopupThongBao").html(data).modal('show');
        }
    });
}

function ClosePopup() {
    $("#PopupThongBao").modal('hide');
}

//Save Data
function SaveData() {
    var param = {
        'UserID': $("#txtUserID").val(),
        'Username': $("#txtUserName").val(),
        'FullName': $("#txtFullName").val(),
        'Email': $("#txtEmail").val(),
        'Password': $("#txtpassword").val(),
        'Status': $("#cbxIsActive").is(":checked") ? true : false,
        'GroupID': $('#ddlGroupInfo').val(),
        'MerchantID': $('#ddlMerchantInfo').val(),
    };
    utils.Loading();
    $.ajax({
        type: 'POST',
        url: UrlRoot + "Home/SaveDataUser",
        data: param,
        async: true,
        success: function (data) {
            debugger;
            utils.unLoading();
            if (data.responseCode >= 0) {
                $('#PopupThongBao').modal('hide');
                bootbox.alert(data.responseDesc);
                //setTimeout(function () { location.reload(); }, 2500);
            }
            else {
                bootbox.alert(data.responseDesc);
            }
        }
    });
};

//Update Status User
function UpdateStatusUser(id, status) {
    var type = "Vô hiệu hóa";
    if (status == 0) {
        type = "Kích hoạt";
    }
    bootbox.confirm("Bạn muốn " + type + " Tài khoản này ?", function (conf) {
        if (conf == 1) {
            var urlUpstatus = UrlRoot + "Home/UpdateActiveUser";
            utils.Loading();
            $.ajax({
                type: 'POST',
                url: urlUpstatus,
                data: { id: id },
                success: function (data) {
                    utils.unLoading();
                    if (data.ResponseCode >= 0) {
                        bootbox.alert(data.ResponseDesc);
                        setTimeout(function () {
                            ListUsers();
                        }, 1000);
                    }
                    else {
                        bootbox.alert(data.ResponseDesc);
                        return;
                    }
                },
                error: function () {
                    utils.UnLoading();
                }
            });
        }
        return;
    });
};


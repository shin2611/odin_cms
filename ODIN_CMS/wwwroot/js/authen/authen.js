$(document).ready(function () {
    $('#loginForm').bootstrapValidator({
        message: 'This value is not valid',
        fields: {
            username: {
                message: 'The username is not valid',
                validators: {
                    notEmpty: {
                        message: 'Username không được bỏ trống'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9\.]+$/,
                        message: 'Username nhập vào sai định dạng'
                    },
                    stringLength: {
                        min: 3,
                        max: 30,
                        message: 'Username trong khoảng 3 - 30 ký tự'
                    }
                }
            },
            password: {
                message: 'The password is not empty',
                validators: {
                    notEmpty: {
                        message: 'password không thể bỏ trống'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'Password phải từ 6 đến 30 ký tự'
                    }
                }
            }
        }
    })
        .on('success.form.bv', function (e) {
            debugger;
            // Prevent submit form
            e.preventDefault();

            var $form = $(e.target),
                validator = $form.data('bootstrapValidator');
            onLogin();
        })
        .on('error.form.bv', function (e) {
            debugger;
            // Active the panel element containing the first invalid element
            e.preventDefault();
            var $form = $(e.target),
                validator = $form.data('bootstrapValidator');
            //$(".login").css("display", "block")
            //$(".alert-danger label").text("Có một vài lỗi trong khung đăng nhập");
            //$(".alert-danger").addClass("show");
           
            swal("Có một vài lỗi trong khung đăng nhập");
            var Username = $("#username").val();
            if (Username == null || Username == '')
                $("#username").focus();
            else
                $("#password").focus();
        });

    var onLogin = function () {
        var param = {
            Username: $("#username").val(),
            Password: $("#password").val()
        }
        var url = UrlRoot + "Account/Login";
        utils.Loading();
        $.ajax({
            type: 'POST',
            url: url,
            data: param,
            async: true,
            success: function (data) {
                utils.unLoading();
                if (data.responseCode > 0) {
                    swal("Đăng nhập thành công!","", "success")
                    setTimeout(function () {
                        location.href = UrlRoot + "Home/Index";
                    }, 2000);
                }
                else {
                    swal(data.responseDesc, "", "error");
                    $("#password").focus();
                }
            }
        });
    };

})
//身份证号码的验证规则
function isIdCardNo(num) {
    //if (isNaN(num)) {alert("输入的不是数字！"); return false;} 
    var len = num.length, re;
    if (len == 15)
        re = new RegExp(/^(\d{6})()?(\d{2})(\d{2})(\d{2})(\d{2})(\w)$/);
    else if (len == 18)
        re = new RegExp(/^(\d{6})()?(\d{4})(\d{2})(\d{2})(\d{3})(\w)$/);
    else {
        //alert("输入的数字位数不对。"); 
        return false;
    }
    var a = num.match(re);
    if (a != null) {
        if (len == 15) {
            var D = new Date("19" + a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        }
        else {
            var D = new Date(a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getFullYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        }
        if (!B) {
            //alert("输入的身份证号 "+ a[0] +" 里出生日期不对。"); 
            return false;
        }
    }
    if (!re.test(num)) {
        //alert("身份证最后一位只能是数字和字母。");
        return false;
    }
    return true;
}

$.validator.setDefaults({
    highlight: function (e) {
        $(e).closest(".form-groups").removeClass("has-success").addClass("has-error");
    },
    success: function (e) {
        e.closest(".form-groups").removeClass("has-error").addClass("has-success");
    },
    errorElement: "span",
    errorPlacement: function (e, r) {
        e.appendTo(r.is(":radio") || r.is(":checkbox") ? r.parent().parent().parent() : r.parent());
    },
    errorClass: "help-block m-b-none",
    validClass: "help-block m-b-none"
}),
$().ready(function () {
    var e = "<i class='fa fa-times-circle'></i> ";
    $("#birthday").datepicker({
        todayBtn: "linked"
    });
    $("#expireOfId").datepicker({
        todayBtn: "linked"
    });
    // 身份证号码验证 
    jQuery.validator.addMethod("isIdCardNo", function (value, element) {
        return this.optional(element) || isIdCardNo(value);
    }, "请正确输入您的身份证号码");
    // 手机号码验证    
    jQuery.validator.addMethod("isMobile", function (value, element) {
        var length = value.length;
        return this.optional(element) || (length == 11 && /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/.test(value));
    }, "请正确填写您的手机号码。");
    // 电话号码验证    
    jQuery.validator.addMethod("isPhone", function (value, element) {
        var tel = /^(\d{3,4}-?)?\d{7,9}$/g;
        return this.optional(element) || (tel.test(value));
    }, "请正确填写您的电话号码。");
    var icon = "<i class='fa fa-times-circle'></i>  ";
    $("#commentForm").validate({
        rules: {
            IdCard: {
                isIdCardNo: true
            },
            mobile: {
                isMobile: true
            },
            telphone: {
                required: true,
                isPhone: true
            }
        },
        messages: {
            //错误消息
            IdCard: {
                isIdCardNo: icon+"请正确输入您的身份证号码"
            },
            mobile: {
                isMobile: icon+"请正确填写您的手机号码"
            },
            telphone: {
                isPhone: icon+"请正确填写您的电话号码"
            }
        },
        submitHandler: function(form) {
            ajaxSubmit(form, function (data) {
                alert(data.msg);
                if (data.success) {
                    window.location.href = "/Admin/Customer/AllCustomer";
                }
            });
        }
    });
});
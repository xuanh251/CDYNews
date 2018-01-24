var common = {
    init: function () {
        common.registerEvents();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "slideDown",
            "hideMethod": "slideUp"
        };
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Post/GetListProductByKeyWord",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<a>" + item.label + "</a>")
                .appendTo(ul);
        };
        $('#btnFeedbackPopup').off('click').on('click', function () {
            common.getUserInfo();
            $('#fbname').val('');
            $('#fbemail').val('');
            $('#fbmessage').val('');
        });
        $('#btnSendFeedback').off('click').on('click', function () {
            common.sendFeedback();
        })
    },
    getUserInfo: function () {
        $.getJSON('//freegeoip.net/json/?callback=?', function (data) {
            $('#uf').val(JSON.stringify(data, null, 2));
        });
    },
    sendFeedback: function () {
        var mydata = new Object();
        mydata.Name = $('#fbname').val();
        mydata.Email = $('#fbemail').val();
        mydata.Message = $('#fbmessage').val();
        mydata.Status = true;
        mydata.UserInfo = $('#uf').val();


        $.post('/api/feedback', mydata, function (res) {
            toastr.success("Gửi thành công!");
            $('.md-modal').removeClass('md-show');
        }, 'json')
    }
}
common.init();
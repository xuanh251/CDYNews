$('.button').click(function () {
    var buttonId = $(this).attr('id');
    $('#modal-container').removeAttr('class').addClass(buttonId);
    $('body').addClass('modal-active');
})

$('#btnCancel').off('click').on('click', function () {
    $('#modal-container').addClass('out');
    $('body').removeClass('modal-active');
});
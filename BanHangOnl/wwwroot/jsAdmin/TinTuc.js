$('#tbodyTT').on('click', 'input[name="active"]', function () {
    var idTT = $(this).closest('tr').find('input[name="idTT"]').val();
    $.ajax({
        url: '/TinTuc/UpdateAcTive', // Đường dẫn đến action xử lý form
        method: 'POST',
        data: {
            idTT: idTT,
        },
    });
});

$(document).ready(function () {
    $('#LoginUser').on('submit', function (e) {
        //e.preventDefault();
        var form = document.getElementById('LoginUser');
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
        } else {
            var formData = $(this).serialize();
            console.log(formData);
            $.ajax({
              url: '/DangNhapTaiKhoan',
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.statusCode == 200) {
                        $('#divMessage').empty();
                        window.location.href = response.url;
                    } else {
                        $('#divMessage').text(response.message);
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
            e.preventDefault();
        }
    })
})
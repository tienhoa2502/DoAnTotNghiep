$(document).ready(function () {
    $('#LoginUser').on('submit', function (e) {
        e.preventDefault();
        var form = document.getElementById('LoginUser');
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
        } else {
            var formData = $(this).serialize();
            console.log(formData);
            $('#btnLogin').prop('disabled', true);

            $.ajax({
                url: '/DangNhap',
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.statusCode == 200) {
                        $('#message').remove();
                        $('#divMessage').empty();
                        window.location.href = response.url;
                    } else {
                        $('#divMessage').text(response.message);
                    }
                    $('#btnLogin').prop('disabled', false);
                },
                error: function (error) {
                    console.log(error);
                }
            });
            e.preventDefault();
        }
    })
})
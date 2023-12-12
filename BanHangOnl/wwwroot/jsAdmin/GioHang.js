    function updateTong() {
        var tong = 0;
    $('.conHang .giaBan').each(function () {
            var sl = $(this).closest('.conHang').find('.sl').val();
    if ($(this).val()) {
        tong += parseFloat($(this).val()) * parseFloat(sl);
            }
        });
    var formattedNumber = tong.toLocaleString('en-US', {style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 });
    $('#tongGia').text(formattedNumber + ' VNÐ');
    }
    updateTong();
    $(document).on('input', '.sl', function () {
        updateTong();
    })
    $(document).on('click', '.up', function () {
        this.parentNode.querySelector('input[type=number]').stepUp();
    $(this.parentNode.querySelector('input[type=number]')).trigger('input');
    })
    $(document).on('click', '.down', function () {
        this.parentNode.querySelector('input[type=number]').stepDown();
    $(this.parentNode.querySelector('input[type=number]')).trigger('input');
    })
    $(document).on('click', '.remove-cookies', function (event) {
        event.preventDefault();

    var a = $(this);
    $.ajax({
        url: '/removeCookies',
    type: 'POST',
    data: 'idHh=' + a.data('idhh'),
    success: function (response) {
        showNumberOfCart(response);
    a.closest('.row-item').prev('hr').remove();
    a.closest('.row-item').remove();
    updateTong();
            },
    error: function (error) {
        console.log(error);
            }
        });
    });
    $('#thanhToan').click(function (event) {
        event.preventDefault();
    var phieuXuatCT = [];
    $('.conHang').each(function(){
            var row = $(this);
    var ct = {
        Idctpx: 0,
    Idhh: parseInt(row.find('.idHh').val()),
    SoLuong: parseFloat(row.find('.sl').val()),
    Gia: parseFloat(row.find('.giaBan').val()),
    Idsize: parseInt(row.find('.cbSize').val()),
    Idmau: parseInt(row.find('.cbMau').val())
            }
    phieuXuatCT.push(ct);
        })
    $.ajax({
        url: '/thanhToanHoaDon',
    type: 'POST',
    data: JSON.stringify(phieuXuatCT),
    contentType: "application/json",
        success: function (response) {
            console.log(response);
                if(response.statusCode == 403){
        window.location.href = "/DangNhap"
            }
            if (response.statusCode == 200) {
                localStorage.setItem('data', JSON.stringify(response.ctx));
                localStorage.setItem('datakh', JSON.stringify(response.kh));

                window.location.href = "/ThanhToan"
            }
        showToast(response.message, response.statusCode);
            },
    error: function (error) {
        console.log(error);
            }
        });
    });

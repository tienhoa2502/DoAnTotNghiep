    function updateTong() {
        var tong = 0;
    $('.conHang .giaBan').each(function () {
            var sl = $(this).closest('.conHang').find('.sl').val();
    if ($(this).val()) {
        tong += parseFloat($(this).val()) * parseFloat(sl);
            }
        });
    var formattedNumber = tong.toLocaleString('en-US', {style: 'decimal', minimumFractionDigits: 0, maximumFractionDigits: 0 });
    $('#tongGia').text(formattedNumber + ' VNĐ');
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
                localStorage.setItem('tyLeGiam', JSON.stringify($('#voucher').val()));
                localStorage.setItem('tongGia', JSON.stringify($('#tongGia').text()));

                $.ajax({
                    url: '/ganTyLeGiam',
                    type: 'POST',
                    data: 'tyleGiam=' + $('#voucher').val(),
                });
                window.location.href = "/ThanhToan"
            }
        showToast(response.message, response.statusCode);
            },
    error: function (error) {
        console.log(error);
            }
        });
    });
// Define a flag to track whether the voucher has been applied
var voucherApplied = false;

function getMaGiam() {
    // Check if the voucher has already been applied
    if (voucherApplied) {
        // Display a message or take appropriate action for attempting to apply the voucher again
        showToast("Voucher đang được áp dụng!", 400); // Assuming 400 is an appropriate error code
        return;
    }

    $.ajax({
        url: '/getMaGiamGia',
        type: 'POST',
        data: 'ma=' + $('#form3Examplea2').val(),
        success: function (response) {
            showToast(response.message, response.statusCode);
            if (response.statusCode == 200) {
                // Log the original total price
                console.log('Original Total Price:', $('#tongGia').text());

                // Update voucher value in the input field
                $('#voucher').val(response.tyLeGiam);

                // Calculate and update the new total price
                updateTotalPrice();

                // Set the flag to indicate that the voucher has been applied
                voucherApplied = true;
            } else {
                // Clear the voucher value if there is an error
                $('#voucher').val('');
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

// Function to update total price based on the voucher value
function updateTotalPrice() {
    // Get the original total price
    var originalTotalPrice = parseFloat($('#tongGia').text().replace(/[^\d.]/g, ''));

    // Get the voucher value
    var voucherValue = parseFloat($('#voucher').val());

    // Calculate the new total price after applying the voucher
    var newTotalPrice = originalTotalPrice - (originalTotalPrice * (voucherValue / 100));

    // Update the #tongGia element with the new total price
    $('#tongGia').text(newTotalPrice.toFixed(2)); // Assuming 2 decimal places for the total price
}


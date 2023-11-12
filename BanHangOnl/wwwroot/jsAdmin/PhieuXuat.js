$(document).ready(function () {
//    formatNumberInput()

});
$('#groupTTKH').on('change', 'select[name="sdt"]', function () {
    var sdt = $(this).val();
    $.ajax({
        url: '/DonDatHang/GetThongTinKhachHang', // Đường dẫn đến action xử lý form
        method: 'POST',
        data: {
            sdt: sdt,
        },
        success: function (response) {
            console.log(response);
            if (response != null) {
                $('input[name="maKH"]').val(response.maKh);
                $('input[name="tenKH"]').val(response.tenKh);
                $('input[name="diaChi"]').val(response.diaChi);
                $('input[name="email"]').val(response.email);
            } else {
                $('input[name="maKH"]').val('Auto');
                $('input[name="tenKH"]').val('');
                $('input[name="diaChi"]').val('');
                $('input[name="email"]').val('');
            }
        }
    });
});
$('#groupTTChiTiet').on('change', 'select[name="idHangHoa"], select[name="mau"], select[name="size"]', function () {
    
    var idHH = $('select[name = "idHangHoa"] option:selected').val();
    var idMau = $('select[name = "mau"] option:selected').val();
    var idSize = $('select[name = "size"] option:selected').val();

    if (idHH != 0 && idMau != 0 && idSize!= 0) {
        $.ajax({
            url: '/DonDatHang/getDonViTinhVaSL', // Đường dẫn đến action xử lý form
            method: 'POST',
            data: {
                idHH: idHH,
                idMau: idMau,
                idSize: idSize
            },
            success: function (response) {
                $('#groupTTChiTiet #dVT').val(response.donViTinh);
                $('#groupTTChiTiet #DonGia').val(formatTotal(response.donGia));
                $('#groupTTChiTiet #sLCon').val(formatTotal(getSoLuongCon(idHH, idMau, idSize, response.soLuong)));
            }
        });
    }
});
$('#groupTTChiTiet').on('input', '#SLHH', function () {
    kiemTraSoLuong();
});
function getSoLuongCon(idHH, idMau, idSize, soLuong) {
    var $rowsWithSameId = $('#tbodyChiTietDonDatHang tr[data-idhh="' + idHH + '"][data-idMau="' + idMau + '"][data-idSize="' + idSize + '"]');
    if ($rowsWithSameId.length == 0) {
        return soLuong;
    } else {
        var soluongdacap = 0;
        $rowsWithSameId.each(function (index) {
            soluongdacap += Number($(this).find('td input[name="soLuong"]').val());
        });
        return soLuong - Number(soluongdacap);
    }
}
function kiemTraSoLuong() {
    var sLCon = parseInt($('#groupTTChiTiet #sLCon').val().replace(/,/g, ''));
    var sl = parseInt($('#groupTTChiTiet #SLHH').val().replace(/,/g, ''));
    if (sl > sLCon) {
        alert("Số lượng vượt quá số lượng tồn");
        $('#groupTTChiTiet #SLHH').val("");
        return;
    } else {
        tinhThanhTien()
    }
}
function tinhThanhTien() {
    var soLuong = parseInt($('#groupTTChiTiet #SLHH').val().replace(/,/g, ''));
    var donGia = parseInt($('#groupTTChiTiet #DonGia').val().replace(/,/g, ''));
    if (!isNaN(soLuong) && !isNaN(donGia)) { // Kiểm tra xem giá trị soLuong và donGia có phải là số hợp lệ
        $('#groupTTChiTiet #ThanhTien').val(formatTotal(soLuong * donGia));
    }
}
function addChiTietDonDatHang() {
    var tenHangHoa = $('#groupTTChiTiet select[name="idHangHoa"] option:selected').text();
    var idHangHoa = $('#groupTTChiTiet select[name="idHangHoa"] option:selected').val();
    var tensize = $('#groupTTChiTiet select[name="size"] option:selected').text();
    var idSize = $('#groupTTChiTiet select[name="size"] option:selected').val();
    var tenMau = $('#groupTTChiTiet select[name="mau"] option:selected').text();
    var idMau = $('#groupTTChiTiet select[name="mau"] option:selected').val();
    var donViTinh = $('#groupTTChiTiet #dVT').val();
    var soLuong = $('#groupTTChiTiet #SLHH').val();
    var donGia = $('#groupTTChiTiet #DonGia').val();
    var thanhTien = $('#groupTTChiTiet #ThanhTien').val();
    var soLuongTon = $('#groupTTChiTiet #sLCon').val();
    if (idHangHoa == '') {
        showToast("Vui lòng chọn hàng hóa", 500);
        return;
    }
    if (soLuong == '') {
        showToast("Vui lòng chọn số lượng", 500);
        return;
    }
    if (donGia == '') {
        showToast("Vui lòng chọn đơn giá", 500);
        return;
    }
    var newRow = $(`<tr data-idhh = "${idHangHoa}" data-idSize="${idSize}" data-idMau="${idMau}">
        <td class="first-td-column text-center p-1 td-sticky">
            <input autocomplete="off" type="text" class="form-control form-table text-center stt" readonly value="${GanSTT()}" style="width:32px;z-index:2;" />
            <input type="hidden" name="idHangHoa" value="${idHangHoa}" />
        </td>
        <td class="p-1 td-sticky" style="position: sticky;left: 33px;background-color: #fff !important; z-index:2">
                    <input type="hidden" name="size" value="${idHangHoa}" />
        ${tenHangHoa}
        </td>
        <td class="p-1 td-sticky" style="position: sticky;left: 33px;background-color: #fff !important; z-index:2">
                    <input type="hidden" name="mau" value="${idMau}" />
        ${tenMau}
        </td>
        <td class="p-1 td-sticky" style="position: sticky;left: 33px;background-color: #fff !important; z-index:2">
                    <input type="hidden" name="idHangHoa" value="${idSize}" />
        ${tensize}
        </td>
        <td class="p-1">
            <input autocomplete="off" readonly type="text" class="w-100 form-control form-table input-date-short-mask" style="width:90px;" value="${donViTinh}" id="dVT" name="dVT" />
        </td>
        <td class="p-1">
            <input autocomplete="off" type="text" class="w-100 form-control form-table formatted-number" style="width:55px;" value="${soLuong}" name="soLuong" />
            <input autocomplete="off" type="hidden" class="w-100 form-control form-table formatted-number" style="width:55px;" value="${soLuongTon}" name="soLuongTon" />

        </td>
        <td class="p-1">
            <input autocomplete="off" t type="text" class="w-100 form-control form-table formatted-number" style="width:80px;" value="${donGia}" name="donGia" />
        </td>
        <td class="p-1">
            <input autocomplete="off" type="text" readonly class="w-100 form-control form-table formatted-number" style="width:100px;" value="${thanhTien}" name="thanhTien" />
        </td>
        <td class="text-center p-1 last-td-column">
            <button type="button" class="btn btn-icon btn-sm text-red remove-DonDatHangCt">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                    <path d="M18 6l-12 12"></path>
                    <path d="M6 6l12 12"></path>
                </svg>
            </button>
        </td>
    </tr>
`)
    $('#tbodyChiTietDonDatHang').append(newRow);
    TinhTongTienDonDatHang();
    clearFormThemChiTietPhieuNhap();
}
function GanSTT() {
    var stt = $('#tbodyChiTietDonDatHang tr').length;

    return Number(Number(stt) + 1);
}
function clearFormThemChiTietPhieuNhap() {
    $('#groupTTChiTiet #SLHH').val('');
    $('#groupTTChiTiet #DonGia').val('');
    $('#groupTTChiTiet #ThanhTien').val('');
    $('#groupTTChiTiet #dVT').val('');
    $('#groupTTChiTiet #sLCon').val('');
    $('#groupTTChiTiet select[name="idHangHoa"]')[0].selectize.clear();

}
function TinhTongTienDonDatHang() {
    var tongTien = 0;
    $('#tbodyChiTietDonDatHang tr').each(function () {
        var thanhTien = parseInt($(this).find('input[name="thanhTien"]').val().replace(/,/g, ''));
        if (!isNaN(thanhTien)) {
            tongTien += thanhTien;
        }

    });
    $('#TienThanhToan').val(formatTotal(tongTien));
}
$('#tbodyChiTietDonDatHang').on("click", ".remove-DonDatHangCt", function (event) {
    event.preventDefault();
    var $row = $(this).closest('tr');
    $row.remove();
    // Cập nhật số thứ tự trên các hàng cùng idToaThuoc
    var $rowsWithSameId = $('#tbodyChiTietDonDatHang tr');
    $rowsWithSameId.each(function (index) {
        $(this).find('td input.stt').val(index + 1);
    });
    TinhTongTienDonDatHang();
});
function themDonDatHang() {
    var tableData = [];
    var dataPhieuNhapMaster = new FormData();
    var dataKhachHang = new FormData();

    var rows = $('#tbodyChiTietDonDatHang tr');
    if (rows.length == 0) {
        showToast("Vui lòng thêm thông tin phiếu nhập", 500);
        return;
    }
    rows.each(function () {
        var row = $(this);
        var rowData = {};
        rowData.Idhh = row.find('input[name="idHangHoa"]').val();
        rowData.Idmau = row.find('input[name="mau"]').val();
        rowData.Idsize = row.find('input[name="size"]').val();
        rowData.SoLuong = row.find('input[name="soLuong"]').val();
        rowData.Gia = row.find('input[name="donGia"]').val();
        tableData.push(rowData);
    });

    //var idNCC = $('select[name="nhaCungCap"]').val();
    //if (idNCC == '') {
    //    showToast("Vui lòng chọn nhà cung cấp", 500);
    //}
    //var ngayNhap = $('input[name="ngayNhap"]').val();
    var ghiChu = $('input[name="ghiChu"]').val();
    dataPhieuNhapMaster.append("GhiChu", ghiChu);
    //dataPhieuNhapMaster.append("NgayNhap", ngayNhap);
    //dataPhieuNhapMaster.append("Idncc", idNCC);
    var sdt = $('select[name="sdt"]').text();
    var email = $('input[name="email"]').val();
    var diaChi = $('input[name="diaChi"]').val();
    var tenKH = $('input[name="tenKH"]').val();

    dataKhachHang.append("Phone", sdt);
    dataKhachHang.append("Email", email);
    dataKhachHang.append("DiaChi", diaChi);
    dataKhachHang.append("TenKh", tenKH);

    var data = {
        DonDatHang: queryStringToData(dataPhieuNhapMaster),
        ChiTietDonDatHang: tableData,
        KhachHang: queryStringToData(dataKhachHang)
    };
    console.log(data);
    $.ajax({
        url: '/DonDatHang/ThemDonDatHang', // Đường dẫn đến action xử lý form
        method: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            if (response.statusCode == 200) {
                $('#tbodyChiTietDonDatHang').empty();
                $('#TienThanhToan').val('');

            }
            showToast(response.message, response.statusCode);
        }
    });
}
function traHang(id) {
    $.ajax({
        url: '/DonDatHang/TraHang', // Đường dẫn đến action xử lý form
        method: 'POST',
        data: {
            idPX : id,
        },
        success: function (response) {
            if (response.statusCode == 200) {
                $('#traHang').prop('disabled', true);
            }
            showToast(response.message, response.statusCode);
        }
    });
}
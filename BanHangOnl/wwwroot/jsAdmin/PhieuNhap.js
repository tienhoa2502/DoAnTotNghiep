﻿$(document).ready(function () {
    /*formatNumberInput()*/
});
$('#groupThemHangHoa').on('input', 'input[name="soLuong"], input[name="donGia"]', function () {
    var soLuong = parseInt($('#groupThemHangHoa input[name="soLuong"]').val().replace(/,/g, ''));
    var donGia = parseInt($('#groupThemHangHoa input[name="donGia"]').val().replace(/,/g, ''));
    if (!isNaN(soLuong) && !isNaN(donGia)) { // Kiểm tra xem giá trị soLuong và donGia có phải là số hợp lệ
        $('#groupThemHangHoa #thanhTien').val(formatTotal(soLuong * donGia));
    }
});
$('#groupThemHangHoa').on('change', 'select[name = "hangHoa"],#mau,#size', function () {
    var idhh = $(this).val();
    if (idhh != '') {
        $.ajax({
            url: '/NhapKho/getDonViTinh', // Đường dẫn đến action xử lý form
            method: 'POST',
            data: {
                idHH: idhh,
                idSize: $('#groupThemHangHoa #size').val(),
                idMau: $('#groupThemHangHoa #mau').val()
            },
            success: function (response) {
                console.log(response);
                $('#groupThemHangHoa #donViTinh').val(response.donViTinh);
                $('#groupThemHangHoa #sLCon').val(response.soLuong);
            }
        });
    }
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

function TinhTongTien() {
    var tongTien = 0;
    $('#tBody-ThemChiTietPhieuNhap tr').each(function () {
        var thanhTien = parseInt($(this).find('input[name="thanhTien"]').val().replace(/,/g, ''));
        if (!isNaN(thanhTien)) {
            tongTien += thanhTien;
        }

    });
    $('#tongTra').val(formatTotal(tongTien));
}
function AddRowPhieuNhapKho() {
    var tenHangHoa = $('#groupThemHangHoa select[name="hangHoa"] option:selected').text();
    var idHangHoa = $('#groupThemHangHoa select[name="hangHoa"]').val();
    var donViTinh = "Thùng";
    var soLuong = $('#groupThemHangHoa #soLuong').val();
    var donGia = $('#groupThemHangHoa #donGia').val();
    var thanhTien = $('#groupThemHangHoa #thanhTien').val();
    var size = $('#groupThemHangHoa #size').val();
    var mau = $('#groupThemHangHoa #mau').val();
    var tensize = $('#groupThemHangHoa #size').text();
    var tenmau = $('#groupThemHangHoa #mau').text();
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
    if (size == '') {
        showToast("Vui lòng chọn ngày sản xuất", 500);
        return;
    }
    if (mau == '') {
        showToast("Vui lòng chọn hạn sử dụng", 500);
        return;
    }
    var newRow = $(`<tr>
        <td class="first-td-column text-center p-1 td-sticky">
            <input autocomplete="off" type="text" class="form-control form-table text-center stt" readonly value="${GanSTT()}" style="width:32px;z-index:2;" />
            <input type="hidden" name="idHangHoa" value="${idHangHoa}" />
        </td>
        <td class="p-1 td-sticky" style="position: sticky;left: 33px;background-color: #fff !important; z-index:2">
        ${tenHangHoa}
        </td>
         <td class="p-1">${tenmau}
            <input autocomplete="off" type="hidden" class="w-100 form-control form-table input-date-short-mask" style="width:90px;text-align: center;" value="${mau}" id="mau1" name="mau" />
        </td>
        <td class="p-1">${tensize}
            <input autocomplete="off" type="hidden" class="w-100 form-control form-table input-date-short-mask" style="width:90px;text-align: center;" value="${size}" id="size1" name="size" />
        </td>
        <td class="p-1">
            <input autocomplete="off" readonly type="text" class="w-100 form-control form-table input-date-short-mask" style="width:90px;text-align: center;" value="${donViTinh}" id="dVT" name="dVT" />
        </td>

        <td class="p-1">
            <input autocomplete="off" readonly type="text" class="w-100 form-control form-table formatted-number" style="width:55px;text-align: center;" value="${soLuong}" name="soLuong" />

        </td>
        <td class="p-1">
            <input autocomplete="off" readonly type="text" class="w-100 form-control form-table formatted-number" style="width:80px;text-align: end;" value="${donGia}" name="donGia" />
        </td>
        <td class="p-1">
            <input readonly autocomplete="off" type="text" class="w-100 form-control form-table formatted-number" style="width:100px;text-align: end;" value="${thanhTien}" name="thanhTien" />
        </td>
       
        <td class="text-center p-1 last-td-column">
            <button type="button" class="btn btn-icon btn-sm text-red remove-phieuNhapCt">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                    <path d="M18 6l-12 12"></path>
                    <path d="M6 6l12 12"></path>
                </svg>
            </button>
        </td>
    </tr>
`)
    $('#tBody-ThemChiTietPhieuNhap').append(newRow);
    TinhTongTien();
    clearFormThem();
}
function GanSTT() {
    var stt = $('#tBody-ThemChiTietPhieuNhap tr').length;

    return Number(Number(stt) + 1);
}
function clearFormThem() {
    var soLuong = $('#groupThemHangHoa #soLuong').val('');
    var donGia = $('#groupThemHangHoa #donGia').val('');
    var thanhTien = $('#groupThemHangHoa #thanhTien').val('');
    //var size = $('#groupThemHangHoa #size').val('');
    //var mau = $('#groupThemHangHoa #mau').val('');
}
$('#tBody-ThemChiTietPhieuNhap').on("click", ".remove-phieuNhapCt", function (event) {
    event.preventDefault();
    var $row = $(this).closest('tr');
    $row.remove();
    // Cập nhật số thứ tự trên các hàng cùng idToaThuoc
    var $rowsWithSameId = $('#tBody-ThemChiTietPhieuNhap tr');
    $rowsWithSameId.each(function (index) {
        $(this).find('td input.stt').val(index + 1);
    });
    TinhTongTien();
});
function ThemPhieuNhap() {
    var tableData = [];
    var dataPhieuNhapMaster = new FormData();
    var rows = $('#tBody-ThemChiTietPhieuNhap tr');
    if (rows.length == 0) {
        showToast("Vui lòng thêm thông tin phiếu nhập", 500);
        return;
    }
    rows.each(function () {
        var row = $(this);
        var rowData = {};
        rowData.Idhh = row.find('input[name="idHangHoa"]').val();
        rowData.SoLuong = row.find('input[name="soLuong"]').val();
        rowData.Gia = row.find('input[name="donGia"]').val();
        rowData.Idsize = row.find('input[name="size"]').val();
        rowData.Idmau = row.find('input[name="mau"]').val();
        tableData.push(rowData);
    });
    var idNCC = $('select[name="nhaCungCap"]').val();
    if (idNCC == 0) {
        showToast("Vui lòng chọn nhà cung cấp", 500);
        return;
    }
    var ngayNhap = $('input[name="ngayNhap"]').val();
    var ghiChu = $('input[name="ghiChu"]').val();
    dataPhieuNhapMaster.append("GhiChu", ghiChu);
    dataPhieuNhapMaster.append("NgayNhap", ngayNhap);
    dataPhieuNhapMaster.append("Idncc", idNCC);
    var data = {
        PhieuNhap: queryStringToData(dataPhieuNhapMaster),
        ChiTietPhieuNhap: tableData
    };
    $.ajax({
        url: '/NhapKho/ThemPhieuNhap', // Đường dẫn đến action xử lý form
        method: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            if (response.statusCode == 200) {
                $('#tBody-ThemChiTietPhieuNhap').empty();
                $('#tongTra').val('');

            }
            showToast(response.message, response.statusCode);
        }
    });
}
$('#tBody-ThemChiTietPhieuNhap').on('input', 'input[name="soLuong"], input[name="donGia"]', function () {
    var tr = $(this).closest('tr');

    var soLuong = parseInt(tr.find('input[name="soLuong"]').val().replace(/,/g, ''));
    var donGia = parseInt(tr.find('input[name ="donGia"]').val().replace(/,/g, ''));
    if (!isNaN(soLuong) && !isNaN(donGia)) { // Kiểm tra xem giá trị soLuong và donGia có phải là số hợp lệ
        tr.find('input[name="thanhTien"]').val(formatTotal(soLuong * donGia));
        TinhTongTien();
    }
});
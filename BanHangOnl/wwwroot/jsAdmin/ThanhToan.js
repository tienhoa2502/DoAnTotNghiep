// Trang "/ThanhToan"
document.addEventListener("DOMContentLoaded", function () {
    var myData = JSON.parse(localStorage.getItem('data'));
    var kh = JSON.parse(localStorage.getItem('datakh'));
    var tyLeGiam = JSON.parse(localStorage.getItem('tyLeGiam'));

    GanThongTinKH(kh);
    var tongTien = 0;
    myData.forEach(function (data) {
        addHH(data);
        tongTien += data.idhhNavigation.giaBan;
    });
    $('#Amount').val(tongTien);
    console.log(tyLeGiam);
    //localStorage.removeItem('datakh');
    // Sau khi sử dụng xong, bạn có thể xóa dữ liệu khỏi localStorage (nếu cần)
//    localStorage.removeItem('myData');
});
//    <img class="product-thumbnail-image" alt="${data.idhhNavigation.tenHh}" src="${data.idhhNavigation.imgHangHoas[0].img}">
function addHH(data) {
    var tr = `    <tr class="product" data-product-id="${data.idhh}" data-variant-id="${data.idhh}">
                            <td class="product-description">
                                <span class="product-description-name order-summary-emphasis">${data.idhhNavigation.tenHh}</span>

                                <span class="product-description-variant order-summary-small-text">
                                    Trắng / M
                                </span>

                            </td>
                            <td class="product-quantity visually-hidden">${data.soLuong}</td>
                            <td class="product-price">
                                <span class="order-summary-emphasis">${data.idhhNavigation.giaBan}</span>
                            </td>
                   </tr>`
    $('#divHH').append(tr);
}
function GanThongTinKH(kh) {
    $('#hoTen').val(kh.tenKh);
    $('#email').val(kh.email);
    $('#sdt').val(kh.phone);
    $('#add').val(kh.diaChi);

}

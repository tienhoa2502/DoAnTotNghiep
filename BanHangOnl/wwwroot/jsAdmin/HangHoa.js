function UpdateHangHoa() {
    var form = $('#formThemHH');
    var hangHoa = form.serialize();
    var dataPhieuNhapMaster = new FormData();
    var MaHh = $('input[name="MaHh"]').val();
    var TenHh = $('input[name="TenHh"]').val();
    dataPhieuNhapMaster.append("MaHh", MaHh);
    dataPhieuNhapMaster.append("TenHh", TenHh);
    $.ajax({
        url: "/HangHoa/updateHangHoa",
        type: "POST",
        data: {
            hangHoa: queryStringToData(dataPhieuNhapMaster)
        },
        success: function (response) {
            const files = $("#upload-input")[0].files;
            const fileFormData = new FormData(); // Đặt tên biến khác

            // Thêm các tệp đã chọn vào FormData
            for (let i = 0; i < files.length; i++) {
                fileFormData.append("files", files[i]);
            }
            console.log(fileFormData);
            $.ajax({
                url: "/HangHoa/UpdateAnh?idHangHoa=" + response.id,
                type: "POST",
                data: fileFormData, // Sử dụng biến khác
                processData: false,
                contentType: false,
                success: function (response) {
                    window.location.href = "/HangHoa";
                },
                error: function (error) {
                    console.error("Lỗi khi cập nhật hàng hóa: " + error);
                }
            });
        },
        error: function (error) {
            console.error("Lỗi khi cập nhật hàng hóa: " + error);
        }
    });

}
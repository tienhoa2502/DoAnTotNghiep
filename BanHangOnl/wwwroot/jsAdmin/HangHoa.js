function UpdateHangHoa() {
    var form = $('#formThemHH');
    var formData = form.serialize();
    $.ajax({
        url: "/HangHoa/updateHangHoa",
        type: "POST",
        data: formData,
        success: function (response) {
            const files = $("#fileInput")[0].files;
            const fileFormData = new FormData(); // Đặt tên biến khác

            // Thêm các tệp đã chọn vào FormData
            for (let i = 0; i < files.length; i++) {
                fileFormData.append("files", files[i]);
            }

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
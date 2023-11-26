function luuSlide() {
    const files = $("#upload-input")[0].files;
    const fileFormData = new FormData(); // Đặt tên biến khác

    // Thêm các tệp đã chọn vào FormData
    for (let i = 0; i < files.length; i++) {
        fileFormData.append("files", files[i]);
    }
    console.log(fileFormData);
    $.ajax({
        url: "/SideQuangCao/Them",
        type: "POST",
        data: fileFormData, // Sử dụng biến khác
        processData: false,
        contentType: false,
        success: function (response) {
            console.log("ok");
            window.location.href = "/SideQuangCao";
        },
        error: function (error) {
            console.error("Lỗi khi cập nhật hàng hóa: " + error);
        }
    });
}

var imgList = [];
var imgAvt = null;
var imgTemp = [];

$(document).ready(function () {
    $(".upload-area").click(function () {
        $('#upload-input').trigger('click');
    });

    $('#upload-input').change(event => {
        if (event.target.files) {
            let filesAmount = event.target.files.length;
            $('.upload-img').html("");

            for (let i = 0; i < filesAmount; i++) {
                let reader = new FileReader();
                reader.onload = function (event) {
                    let html = `
                        <div class = "uploaded-img">
                            <img src = "${event.target.result}">
                            <input type="radio" class="form-input-check checkAvt remove-btn" name="checkAvt" ${i == 0 ? "checked" : ""}>
                        </div>
                    `;
                    $(".upload-img").append(html);
                    imgList.push(event.target.result);
                    if (i == 0) {
                        imgAvt = event.target.result
                    } else {
                        imgTemp.push(event.target.result);
                    }
                }
                reader.readAsDataURL(event.target.files[i]);
            }

            $('.upload-info-value').text(filesAmount);
            $('.upload-img').css('padding', "20px");
        }
    });


    $(document).on('click', 'input[name=checkAvt]', function (event) {
        if ($(event.target).hasClass('checkAvt')) {
            let removedFileSrc = $(event.target).parent().find('img').attr('src');
            imgAvt = imgList.filter(file => file === removedFileSrc);
            imgTemp = imgList.filter(file => file !== removedFileSrc);
        } else if ($(event.target).parent().hasClass('checkAvt')) {
            let removedFileSrc = $(event.target).parent().parent().find('img').attr('src');
            imgAvt = imgList.filter(file => file === removedFileSrc);
            imgTemp = imgList.filter(file => file !== removedFileSrc);
        }
    });



});
var imgList = [];
var imgAvt = null;
var imgTemp = [];

$(document).ready(function(){
    $(".upload-area").click(function(){
        $('#upload-input').trigger('click');
    });

    $('#upload-input').change(event => {
        if(event.target.files){
            let filesAmount = event.target.files.length;
            $('.upload-img').html("");

            for(let i = 0; i < filesAmount; i++){
                let reader = new FileReader();
                reader.onload = function(event){
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

    // $(window).click(function(event){
    //     if($(event.target).hasClass('remove-btn')){
    //         $(event.target).parent().remove();
    //     } else if($(event.target).parent().hasClass('remove-btn')){
    //         $(event.target).parent().parent().remove();
    //     }
    // })

     // <button type = "button" class = "remove-btn">
     //                            <i class = "fas fa-times"></i>
     //                        </button>

    // $(window).click(function (event) {
    //     if ($(event.target).hasClass('checkAvt')) {
    //         let removedFileSrc = $(event.target).parent().find('img').attr('src');
    //         imgAvt = imgList.filter(file => file === removedFileSrc);
    //         imgList = imgList.filter(file => file !== removedFileSrc);
    //     } else if ($(event.target).parent().hasClass('checkAvt')) {
    //         let removedFileSrc = $(event.target).parent().parent().find('img').attr('src');
    //         imgAvt = imgList.filter(file => file === removedFileSrc);
    //         imgList = imgList.filter(file => file !== removedFileSrc);
    //     }
    //  //   $('.upload-info-value').text(imgList.length);
    // });


    $(document).on('click', 'input[name=checkAvt]', function (event) {
        console.log('1233');
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




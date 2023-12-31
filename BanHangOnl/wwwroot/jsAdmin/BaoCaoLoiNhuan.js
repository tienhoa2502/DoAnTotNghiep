﻿var _myChart = null;
$(document).ready(function () {
    baoCaoLoiNhuan();
    //formatNumberInput();

});
function baoCaoLoiNhuan() {
    var tuNgay = $('#tuNgay').val();
    var denNgay = $('#denNgay').val();
    $.ajax({
        url: '/baoCaoLoiNhuan',
        data: {
            TuNgay: tuNgay,
            DenNgay: denNgay,
        },
        method: 'POST',
        success: function (data) {
            console.log(data);
            _labels = [];
            _values = [];
            // Trích xuất dữ liệu từ phản hồi
            var doanhThuData = data.doanhThu;
            var giaVonData = data.giaVon;
            if (_myChart !== null) {
                _myChart.destroy();
            }
            // Tiếp tục xây dựng biểu đồ với dữ liệu này
            buildBarChart(doanhThuData, giaVonData);
            $('#tbodyBaoCaoLoiNhuan:not(#tongTien)').empty();
            data.listLoiNhuan.forEach(function (data, i) {
                
                addRowTableBCLN(data,i);
            });
            TinhTongDoanhThu();
            TinhTongGiaVon();
            TinhTongLoiNhuan();
        },
        error: function (error) {
            console.log(error);
        }
    });
}
//function buildBarChart(doanhThuData, giaVonData) { 
//    var labels = doanhThuData.map(item => item.label);
//    console.log(labels);
//    var doanhThuValues = doanhThuData.map(item => item.doanhthu);
//    var giaVonValues = giaVonData.map(item => item.doanhthu);
//    var loiNhuanValues = doanhThuValues.map((doanhThu, index) => doanhThu - giaVonValues[index]);
//new Chart(document.querySelector('#myBarChart'), {
//            type: 'bar',
//            data: {
//                labels: labels,
//                datasets: [
//            {
//                label: 'Doanh Thu',
//                backgroundColor: 'rgba(75, 192, 192, 0.2)',
//                borderColor: 'rgba(75, 192, 192, 1)',
//                borderWidth: 1,
//                data: doanhThuValues,
//            },
//            {
//                label: 'Giá Vốn',
//                backgroundColor: 'rgba(255, 99, 132, 0.2)',
//                borderColor: 'rgba(255, 99, 132, 1)',
//                borderWidth: 1,
//                data: giaVonValues,
//            },
//            {
//                label: 'Lợi Nhuận',
//                backgroundColor: 'rgba(255, 206, 86, 0.2)',
//                borderColor: 'rgba(255, 206, 86, 1)',
//                borderWidth: 1,
//                data: loiNhuanValues,
//            },
//        ],            },
//            options: {
//                scales: {
//                    y: {
//                        beginAtZero: true
//                    }
//                }
//            }
//        });
//    };
function buildBarChart(doanhThuData, giaVonData) {
    var labels = doanhThuData.map(item => item.label);
    var doanhThuValues = doanhThuData.map(item => item.doanhthu);
    var giaVonValues = giaVonData.map(item => item.doanhthu);
    var loiNhuanValues = doanhThuValues.map((doanhThu, index) => doanhThu - giaVonValues[index]);


    var data = {
        labels: labels,
        datasets: [
            {
                label: 'Doanh Thu',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1,
                data: doanhThuValues,
            },
            {
                label: 'Giá Vốn',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
                data: giaVonValues,
            },
            {
                label: 'Lợi Nhuận',
                backgroundColor: 'rgba(255, 206, 86, 0.2)',
                borderColor: 'rgba(255, 206, 86, 1)',
                borderWidth: 1,
                data: loiNhuanValues,
            },
        ],
    };


    var options = {
        scales: {
            x: {
                beginAtZero: true,
            },
            y: {
                beginAtZero: true,
            },
        },
    };

    var ctx = document.getElementById('myBarChart').getContext('2d');
    _myChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: options,
    });
}


//            //            document.addEventListener("DOMContentLoaded", () => {
//            //new Chart(document.querySelector('#barChart'), {
//            //    type: 'bar',
//            //    data: {
//            //        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
//            //        datasets: [{
//            //            label: 'Bar Chart',
//            //            data: [65, 59, 80, 81, 56, 55, 40],
//            //            backgroundColor: [
//            //                'rgba(255, 99, 132, 0.2)',
//            //                'rgba(255, 159, 64, 0.2)',
//            //                'rgba(255, 205, 86, 0.2)',
//            //                'rgba(75, 192, 192, 0.2)',
//            //                'rgba(54, 162, 235, 0.2)',
//            //                'rgba(153, 102, 255, 0.2)',
//            //                'rgba(201, 203, 207, 0.2)'
//            //            ],
//            //            borderColor: [
//            //                'rgb(255, 99, 132)',
//            //                'rgb(255, 159, 64)',
//            //                'rgb(255, 205, 86)',
//            //                'rgb(75, 192, 192)',
//            //                'rgb(54, 162, 235)',
//            //                'rgb(153, 102, 255)',
//            //                'rgb(201, 203, 207)'
//            //            ],
//            //            borderWidth: 1
//            //        }]
//            //    },
//            //    options: {
//            //        scales: {
//            //            y: {
//            //                beginAtZero: true
//            //            }
//            //        }
//            //    }
//            //});
//            //            });

//}
//function buildBarChart(doanhThuData, giaVonData) {
//    var labels = doanhThuData.map(item => item.label);
//    var doanhThuValues = doanhThuData.map(item => item.doanhthu);
//    var giaVonValues = giaVonData.map(item => item.doanhthu);
//    var loiNhuanValues = doanhThuValues.map((doanhThu, index) => doanhThu - giaVonValues[index]);

//    var data = {
//        labels: labels,
//        datasets: [
//            {
//                label: 'Doanh Thu',
//                backgroundColor: 'rgba(75, 192, 192, 0.2)',
//                borderColor: 'rgba(75, 192, 192, 1)',
//                borderWidth: 1,
//                data: doanhThuValues,
//                fill: false,
//            },
//            {
//                label: 'Giá Vốn',
//                backgroundColor: 'rgba(255, 99, 132, 0.2)',
//                borderColor: 'rgba(255, 99, 132, 1)',
//                borderWidth: 1,
//                data: giaVonValues,
//                fill: false,
//            },
//            {
//                label: 'Lợi Nhuận',
//                backgroundColor: 'rgba(255, 206, 86, 0.2)',
//                borderColor: 'rgba(255, 206, 86, 1)',
//                borderWidth: 1,
//                data: loiNhuanValues,
//                fill: false,
//            },
//        ],
//    };

//    var options = {
//        scales: {
//            x: {
//                beginAtZero: true,
//            },
//            y: {
//                beginAtZero: true,
//            },
//        },
//    };

//    var ctx = document.getElementById('myBarChart').getContext('2d');
//    var myChart = new Chart(ctx, {
//        type: 'line',
//        data: data,
//        options: options,
//    });
//}
function addRowTableBCLN(data, i) {
    var stt = i + 1;
    var newRow = $(`<tr>
                <td>${stt}</td>
                <td>${data.ngay}</td>
                <td><input type="text" readonly class="form-control formatted-number" name="doanhThu" value="${data.doanhThu}" /></td>
                <td><input type="text" readonly class="form-control formatted-number" name="giaVon" value="${data.giaVon}" /></td>
                <td><input type="text" readonly class="form-control formatted-number" name="loiNhuan" value="${data.doanhThu - data.giaVon}" /></td>
    </tr>`)
    $('#tbodyBaoCaoLoiNhuan').append(newRow);
//    formatNumberInput();
}
function TinhTongDoanhThu() {
    var tongTien = 0;
    $('#tbodyBaoCaoLoiNhuan tr').each(function () {
        var thanhTien = parseInt($(this).find('input[name="doanhThu"]').val().replace(/,/g, ''));
        if (!isNaN(thanhTien)) {
            tongTien += thanhTien;
        }

    });
    $('#doanhThu').val(formatTotal(tongTien));
}
function TinhTongGiaVon() {
    var tongTien = 0;
    $('#tbodyBaoCaoLoiNhuan tr').each(function () {
        var thanhTien = parseInt($(this).find('input[name="giaVon"]').val().replace(/,/g, ''));
        if (!isNaN(thanhTien)) {
            tongTien += thanhTien;
        }

    });
    $('#giaVon').val(formatTotal(tongTien));
}
function TinhTongLoiNhuan() {
    var tongTien = 0;
    $('#tbodyBaoCaoLoiNhuan tr').each(function () {
        var thanhTien = parseInt($(this).find('input[name="loiNhuan"]').val().replace(/,/g, ''));
        if (!isNaN(thanhTien)) {
            tongTien += thanhTien;
        }

    });
    $('#loiNhuan').val(formatTotal(tongTien));
}

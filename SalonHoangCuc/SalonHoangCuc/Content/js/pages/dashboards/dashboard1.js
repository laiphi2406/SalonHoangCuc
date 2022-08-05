
$(function () {
    "use strict";

});


function showThangDiem() {

    $.ajax({
        type: "POST",
        url: "/Home/BieuDoDiem",
        data: '{checkThang: 1}',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            chart.data.labels = response.TieuDe;
            chart.data.series[0] = response.DiemSo;
            chart.update();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function showThangCongViec() {
    $.ajax({
        type: "POST",
        url: "/Home/BieuDoSoLuong",
        data: '{checkThang: 1}',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            chart2.data.labels = response.TieuDe;
            chart2.data.series[0] = response.CongViec;
            chart2.update();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function showNamDiem() {
    $.ajax({
        type: "POST",
        url: "/Home/BieuDoDiem",
        data: '{checkThang: 2}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            chart.data.labels = response.TieuDe;
            chart.data.series[0] = response.DiemSo;
            chart.update();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function showNamCongViec() {
    $.ajax({
        type: "POST",
        url: "/Home/BieuDoSoLuong",
        data: '{checkThang: 2}',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            chart2.data.labels = response.TieuDe;
            chart2.data.series[0] = response.CongViec;
            chart2.update();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
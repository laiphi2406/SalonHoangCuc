/*Get quan huyen */
function GetQuanHuyen(log) {
    if (log.value == '') {
        $('#quanhuyen option').remove();
        var select = document.getElementById("quanhuyen");
        var option = document.createElement("option");
        option.text = ' --Quận/Huyện-- ';
        option.value = '';
        if (option.value == "") {
            select.appendChild(option);
            $('#xaphuong option').remove();
            var select = document.getElementById("xaphuong");
            var option = document.createElement("option");
            option.text = ' --Xã/Phường-- ';
            option.value = '';
            select.appendChild(option);
        }
        
    } else {
        $.ajax({
            type: "Get",
            url: "/HoGiaDinhs/GetQuanHuyen?TinhTpId=" + log.value,
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            success: function (response) {
                $('#xaphuong option').remove();
                var select = document.getElementById("xaphuong");
                var option = document.createElement("option");
                option.text = ' --Xã/Phường-- ';
                option.value = '';
                select.appendChild(option);
                $('#quanhuyen option').remove();
                var select = document.getElementById("quanhuyen");
                var option = document.createElement("option");
                option.text = ' --Quận/Huyện-- ';
                option.value = '';
                select.appendChild(option);
                for (var i = 0; i < response.length; i++) {
                    var option = document.createElement("option");
                    option.text = response[i].Name;
                    option.value = response[i].ID;
                    select.appendChild(option);
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}

/*Get xa phuong */
function GetXaPhuong(logg) {
    if (logg.value == '') {
        $('#xaphuong option').remove();
        var select = document.getElementById("quanhuyen");
        var option = document.createElement("option");
        option.text = ' --Xã/Phường-- ';
        option.value = '';
        select.appendChild(option);
        return;
    } else {
        $.ajax({
            type: "Get",
            url: "/HoGiaDinhs/GetXaPhuong?QuanHuyenId=" + logg.value,
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            success: function (response) {
                $('#xaphuong option').remove();
                var select = document.getElementById("xaphuong");
                var option = document.createElement("option");
                option.text = ' --Chọn Xã Phường-- ';
                option.value = '';
                select.appendChild(option);

                for (var i = 0; i < response.length; i++) {
                    var option = document.createElement("option");
                    option.text = response[i].Name;
                    option.value = response[i].ID;
                    select.appendChild(option);
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}
//get địa chỉ để hiển thị phần edit
function getdiachi() {
    $.ajax({
        type: "Get",
        url: "/HoGiaDinhs/GetXaPhuong?QuanHuyenId=" + logg.value,
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            $('#xaphuong option').remove();
            var select = document.getElementById("xaphuong");
            var option = document.createElement("option");
            option.text = ' --Chọn Xã Phường-- ';
            option.value = '';
            select.appendChild(option);

            for (var i = 0; i < response.length; i++) {
                var option = document.createElement("option");
                option.text = response[i].Name;
                option.value = response[i].ID;
                select.appendChild(option);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


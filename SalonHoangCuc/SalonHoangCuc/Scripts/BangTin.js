function displayDate() {
    console.log('xxxx');
    debugger;
    var urlInsert = '@Url.Action("Index")';
    $.get(urlInsert, function () {
    });
}

function cancelCreate() {
    document.location = '@Url.Action("Index","BangTinsTest")';

}
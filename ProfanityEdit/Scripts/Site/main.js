

function addTextFileToInput(targetId) {
    var e = window.event;
    var file = event.srcElement.files[0];
    var reader = new FileReader();
    var $target = $(targetId);
    reader.onloadend = function () {
        var txt = reader.result;
        $target.val(txt);
    }
    reader.readAsText(file);
}

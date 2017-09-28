

function addTextFileToInput(targetId) {
    var e = window.event;
    var file = event.srcElement.files[0];
    var reader = new FileReader();
    var $target = $(targetId);
    reader.onloadend = function () {
        var txt = reader.result;
        $target.val(txt);
    };
    reader.readAsText(file);
}

// select first preset radio button
$('.presets input[name="preset"]').first().attr('checked', 'true');

function showClass(className) {
    $('.' + className).removeClass('hidden');
}

function hideClass(className) {
    $('.' + className).addClass('hidden');
}
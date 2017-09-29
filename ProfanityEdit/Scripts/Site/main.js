﻿

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
function setPreset() {
    var presetId = $('#EditListId').val();
    var $preset = $('.presets input[name="preset"][value="' + presetId + '"]');
    if ($preset.length == 0) {
        $('.presets input[name="preset"][value="Custom"]').attr('checked', 'true');
        showClass('customPreferences');
    }
    else {
        $preset.attr('checked', 'true');
    }
};

function showClass(className) {
    $('.' + className).removeClass('hidden');
}

function hideClass(className) {
    $('.' + className).addClass('hidden');
}
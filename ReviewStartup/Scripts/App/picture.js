﻿$(document).ready(function () {
    $('#chosePicture').click(function () {
        $('#picture').click();
        return false;
    });
    $('#picture').change(function (event) {
        if (event.target.files && event.target.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#pictureDispalay').attr('src', e.target.result);

            }
            reader.readAsDataURL(event.target.files[0]);
        }
    });
})
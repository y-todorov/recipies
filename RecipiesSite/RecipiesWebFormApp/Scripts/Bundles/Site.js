﻿kendo.culture("en-IE");

function error_handler(e) {
    if (e.errors) {
        debugger;
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}
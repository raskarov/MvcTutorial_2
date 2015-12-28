function str_rand() {
    var result = '';
    var words = '0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM';
    var max_position = words.length - 1;
    for (i = 0; i < 8; ++i) {
        position = Math.floor(Math.random() * max_position);
        result = result + words.substring(position, position + 1);
    }

    document.getElementById('password').value = result;
};

($(function () {
    $("#dialog-delete-admin").dialog({
        autoOpen: false,
        height: 350,
        width: 500
    });

    $(".delete-admin-opener").click(function () {
        $("#dialog-delete-admin").dialog("open");
    });
}));

($(function () {
    $("#dialog-edit-admin").dialog({
        autoOpen: false,
        height: 400,
        width: 500
    });

    $(".edit-admin-opener").click(function () {
        $("#dialog-edit-admin").dialog("open");
    });
}));

($(function () {
    $("#dialog-create-admin").dialog({
        autoOpen: false,
        height: 400,
        width: 600
    });

    $(".create-admin-opener").click(function () {
        $("#dialog-create-admin").dialog("open");
    });
}));

($(function () {
    $("#dialog-create-patient").dialog({
        autoOpen: false,
        height: 400,
        width: 650
    });

    $(".create-patient-opener").click(function () {
        $("#dialog-create-patient").dialog("open");
    });
}));

($(function () {
    $("#dialog-edit-patient").dialog({
        autoOpen: false,
        height: 400,
        width: 650
    });

    $(".edit-patient-opener").click(function () {
        $("#dialog-edit-patient").dialog("open");
    });
}));

($(function () {
    $("#dialog-delete-patient").dialog({
        autoOpen: false,
        height: 400,
        width: 650
    });

    $(".delete-patient-opener").click(function () {
        $("#dialog-delete-patient").dialog("open");
    });
}));

($(function () {
    $("#dialog-delete-doctor").dialog({
        autoOpen: false,
        height: 400,
        width: 700
    });

    $(".delete-doctor-opener").click(function () {
        $("#dialog-delete-doctor").dialog("open");
    });
}));

($(function () {
    $("#dialog-edit-doctor").dialog({
        autoOpen: false,
        height: 400,
        width: 700
    });

    $(".edit-doctor-opener").click(function () {
        $("#dialog-edit-doctor").dialog("open");
    });
}));

($(function () {
    $("#dialog-create-doctor").dialog({
        autoOpen: false,
        height: 400,
        width: 700
    });

    $(".create-doctor-opener").click(function () {
        $("#dialog-create-doctor").dialog("open");
    });
}));

($(function () {
    $("#dialog-show-patients").dialog({
        autoOpen: false,
        height: 400,
        width: 700
    });

    $(".show-patients-opener").click(function () {
        $("#dialog-show-patients").dialog("open");
    });
}));
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

    $("#delete-admin-opener").click(function () {
        $("#dialog-delete-admin").dialog("open");
    });
}));

($(function () {
    $("#dialog-edit-admin").dialog({
        autoOpen: false,
        height: 400,
        width: 500
    });

    $("#edit-admin-opener").click(function () {
        $("#dialog-edit-admin").dialog("open");
    });
}));

($(function () {
    $("#dialog-create-admin").dialog({
        autoOpen: false,
        height: 400,
        width: 600
    });

    $("#create-admin-opener").click(function () {
        $("#dialog-create-admin").dialog("open");
    });
}));


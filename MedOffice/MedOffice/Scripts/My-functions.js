($(function () {
    $("#dialog-delete").dialog({
        autoOpen: false,
        height: 350,
        width:500
    });

    $("#delete-opener").click(function () {
        $("#dialog-delete").dialog("open");
    });
}));

(function str_rand() {
    var result = '';
    var words = '0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM';
    var max_position = words.length - 1;
    for (i = 0; i < 8; ++i) {
        position = Math.floor(Math.random() * max_position);
        result = result + words.substring(position, position + 1);
    }

    document.getElementById('password').value = result;
});

($(function () {
    $("#dialog-edit").dialog({
        autoOpen: false,
        height: 350,
        width: 500
    });

    $("#edit-opener").click(function () {
        $("#dialog-edit").dialog("open");
    });
}));


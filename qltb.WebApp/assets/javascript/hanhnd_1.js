const pageSize = 10;

$(document).ajaxStart(function () {
    $.LoadingOverlay("show", {
        background: "rgba(169,169,169,0.6)",
        image: "",
        fontawesome: "far fa-spinner-third fa-spin",
        fontawesomeColor: "#6D6D6D",
        size: 30,
        maxSize: 50
    });
});
$(document).ajaxStop(function () {
    $.LoadingOverlay("hide");
});

function generateId(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() *
            charactersLength));
    }
    return result;
}

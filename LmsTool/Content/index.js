$(document).ready(function () {
    $('#Showmodal').click(function () {
        var url = $('#CreateModal').data('url');

        $.get(url, function (data) {
            $('#CreateContainer').html(data);

            $('#CreateModal').modal('show');
        });
    });
});
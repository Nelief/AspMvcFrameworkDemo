$(document).ready(function () {
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc: ""
        },
        columns: [
            {
                data: "title",
                render: function (data, type, movie) {
                    return "<div class='d-flex justify-content-center'><a class='text-dark text-decoration-none' href='Movie/Details/" + movie.id + "'>" + movie.title + "</a></div>";
                }
            },
            {
                data: "genre.name",
                render: function (data, type, movie) {
                    return "<div class='d-flex justify-content-center'>" + movie.genre.name + "</div>";
                }
            }
        ]
        ,
        initComplete: function () {
            updatepaginationstyle();
        },
        drawCallback: function () {
            updatepaginationstyle();
        }
    });

    function updatepaginationstyle() {
        $('.pagination').addClass('d-flex gap-2 justify-content-end')
        $('.paginate_button').addClass('btn rounded');
        $('.paginate_button a').addClass('text-dark text-decoration-none');


        $('.paginate_button').each(function () {
            var $this = $(this);
            if ($this.hasClass('disabled')) {
                $this.addClass('btn-secondary');
            }
            else if ($this.hasClass('active')) {
                $this.addClass('btn-primary');
            }
            else {
                $this.addClass('btn-info');
            }
        })
    }
});
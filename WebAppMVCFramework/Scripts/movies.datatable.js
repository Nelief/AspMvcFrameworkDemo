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
            },
            {
                data: "id",
                render: function (data) {
                    var editButton = "<a class='btn btn-secondary' href='/Movie/Edit/" + data + "'>EDIT</a>";
                    var deleteButton = "<button class='btn btn-danger js-delete' data-movie-id=" + data + ">DELETE</button>";

                    return "<div class='d-flex justify-content-end gap-2'>" + editButton + "  " + deleteButton + "</div>";
                },
                orderable: false
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

    $("#movies").on("click", ".js-delete", function () {
        var button = $(this);
        var movieId = $(this).attr("data-movie-id");
        bootbox.confirm("Deleting customer with id :" + movieId, function (result) {
            if (result) {
                $.ajax({
                    url: "api/movies/" + movieId,
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    })
});
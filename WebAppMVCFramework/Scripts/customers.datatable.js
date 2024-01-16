//creazione tabella con richieste AJAX e datatable 
$(document).ready(function () {
    var table = $("#customers").DataTable({
        ajax: {
            url: "/api/customers",
            dataSrc: ""
        },
        columns: [
            {
                data: "Name",
                render: function (data, type, customer) {
                    return "<a class='text-dark text-decoration-none' href='Customer/Details/" + customer.id + "'>" + customer.name + " " + customer.cognome + "</a>";
                }
            },
            {
                data: "membershipType.name"
            },
            {
                data: "membershipType.discoutRate",
                render: function (data) {
                    return data + " %"
                }
            },
            {
                data: "id",
                render: function (data) {
                    var editButton = "<a class='btn btn-secondary' href='/Customer/Edit/" + data + "'>EDIT</a>";
                    var deleteButton = "<button class='btn btn-danger js-delete' data-customer-id=" + data + ">DELETE</button>";

                    return "<div class='d-flex justify-content-center gap-2'>" + editButton + "  " + deleteButton + "</div>";
                },
                orderable: false
            }
        ],
        initComplete: function () {
            UpdatePaginationStyle();
        },
        drawCallback: function () {
            UpdatePaginationStyle();
        }
    });

    function UpdatePaginationStyle() {
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

    //Delete Ajax function
    $("#customers").on("click", ".js-delete", function () {
        var button = $(this);
        var customerId = $(this).attr("data-customer-id");
        bootbox.confirm("Deleting customer with id :" + $(this).attr("data-customer-id"), function (result) {
            if (result) {
                $.ajax({
                    url: "api/customers/" + customerId,
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    })
});

//Base delete ajax script
//$(document).ready(function () {
//    $("#customers .js-delete").on("click", function () {
//        var button = $(this);
//        if (confirm("You sure you want to delete the selected Customer ?")) {
//            $.ajax({
//                url: "api/customers/" + $(this).attr("data-customer-id"), //Fetching the ID from a custom attribute in the delete button
//                method: "DELETE",
//                success: function () {
//                    //questo rimuove solo la riga dalla tabella, non modifica il DB
//                    button.parents("tr").remove();
//                }
//            });
//        }
//    })
//});


//Ajax script con bootbox per il modale
//$(document).ready(function () {
//    $("#customers .js-delete").on("click", function () {
//        var button = $(this);

//        bootbox.confirm("Deleting customer with id :" + $(this).attr("data-customer-id"), function (result) {
//            if (result) {
//                $.ajax({
//                    url: "api/customers/" + $(this).attr("data-customer-id"),
//                    method: "DELETE",
//                    success: function () {
//                        button.parents("tr").remove();
//                    }
//                });
//            }
//        });
//    })
//});


//Ajax script ottimizzato per non creare N funzioni di delete
//$(document).ready(function () {
//    $("#customers").on("click", ".js-delete", function () {
//        var button = $(this);

//        bootbox.confirm("Deleting customer with id :" + $(this).attr("data-customer-id"), function (result) {
//            if (result) {
//                $.ajax({
//                    url: "api/customers/" + $(this).attr("data-customer-id"),
//                    method: "DELETE",
//                    success: function () {
//                        button.parents("tr").remove();
//                    }
//                });
//            }
//        });
//    })
//});


//Ajax script con datatable costruita client-side in base alla tabella originale (lento)
//$(document).ready(function () {
//    //definizione lenta: datables itera la tabella e ricrea la propria versione
//    $("#customers").DataTable();

//$("#customers").on("click", ".js-delete", function () {
//                var button = $(this);

//bootbox.confirm("Deleting customer with id :" + $(this).attr("data-customer-id"), function (result) {
//                    if (result) {
//    $.ajax({
//        url: "api/customers/" + $(this).attr("data-customer-id"),
//        method: "DELETE",
//        success: function () {
//            button.parents("tr").remove();
//        }
//    });
//                    }
//                });
//            })
//        });


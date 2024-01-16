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
});


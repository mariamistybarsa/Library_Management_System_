$(document).ready(function () {
    BookDataTable();
    console.log("Page is fully loaded!");
});

function BookDataTable() {
    $('#BookTable').DataTable({
        processing: true,
        serverSide: false,
        ajax: {
            url: '/AdminArea/Book/GetBook',
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            { data: 'title' },
            { data: 'author' },
            { data: 'isbn' },
            { data: 'publisher' },
            {
                data: 'publishedDate',
                render: function (data) {
                    return new Date(data).toLocaleDateString();
                }
            },
            { data: 'categoryName' },
            {
                data: 'coverImagePath',
                render: function (data) {
                    return `<img src="${data}" width="50" height="60" class="img-thumbnail" />`;
                }
            },
            { data: 'totalCopies' },
            { data: 'availableCopies' },
            {
                data: 'bookId',
                render: function (data) {
                    return `
                        <a href="/Book/Edit/${data}" class="btn btn-sm btn-primary">Edit</a>
                        <a href="/Book/Delete/${data}" class="btn btn-sm btn-danger">Delete</a>
                        <a href="javascript:void(0);" class="btn btn-sm btn-info viewDetails" data-id="${data}">Details</a>
                    `;
                }
            }
        ]
    });
}

$(document).on('click', '.viewDetails', function () {
    var id = $(this).data('id');
    console.log("Clicked Details for ID:", id);

    $.ajax({
        url: '/AdminArea/Ajax/GetBookDetails'
,
        type: 'GET',
        data: { id: id },
        success: function (result) {
            $('#bookDetailsContent').html(result);
            $('#bookDetailsModal').modal('show'); // Bootstrap 5 modal
        },
        error: function () {
            alert("❌ Error loading book details.");
        }
    });
});
//$(document).on('click', '.editBook', function () {
//    var id = $(this).data('id');

//    $.ajax({
//        url: '/AdminArea/Book/EditPartial',
//        type: 'GET',
//        data: { id: id },
//        success: function (result) {
//            $('#bookEditContent').html(result);
//            $('#bookEditModal').modal('show');
//        }
//    });
//});

//$(document).on('submit', '#editBookForm', function (e) {
//    e.preventDefault();
//    var form = $(this);

//    $.ajax({
//        url: '/AdminArea/Book/SaveBook',
//        type: 'POST',
//        data: form.serialize(),
//        success: function (res) {
//            if (res.success) {
//                $('#bookEditModal').modal('hide');
//                $('#BookTable').DataTable().ajax.reload(); // Reload table
//            } else {
//                $('#bookEditContent').html(res); // re-render form with errors
//            }
//        }
//    });
//});

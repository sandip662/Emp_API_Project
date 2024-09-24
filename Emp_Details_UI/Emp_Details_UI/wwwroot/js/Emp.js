function TagDelete(Id) {
    console.log("TagDelete called with ID:", Id);
    debugger;
    Swal.fire({
        title: 'Do you want to Delete?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Make a POST request to delete the record
            $.ajax({
                url: "/EmpUI/DeleteEmp",
                method: 'POST',
                data: {
                    id: Id
                },
                success: function (data) {
                    console.log("Delete response received:", data);
                    if (data.msg === "success") {
                        Swal.fire("Done", "Record Deleted Successfully!", "success");
                    }
                    else {
                        Swal.fire("Oops!!!", "Please Contact Admin", "error");
                    }
                    // Reload the page after deletion
                    window.location.reload();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error("Error occurred while deleting:", textStatus, errorThrown);
                    Swal.fire("Oops!!!", "An error occurred while deleting the record.", "error");
                }
            });
        } else if (result.isDenied) {
            Swal.fire('Welcome', '', 'info');
        }
    });
}


function EditEmp(id) {
    console.log("EditEmp called with ID:", id);
    $.getJSON("/EmpUI/EditEmp/" + id, function (d) {
        console.log("Data received:", d); // Log the data received

        // Check if data is correctly fetched
        if (d) {
            $("#Id").val(d.id);
            $("#FirstName").val(d.firstName);
            $("#LastName").val(d.lastName);
            $("#Email").val(d.email);
            $("#Salary").val(d.salary);


            // Display the modal
            $("#exampleModal").modal('show');

            $("#exampleModalLabel").html('Section Edit Form:: [Edit]');
        } else {
            console.error("No data received for editing.");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Failed to fetch data for editing:", textStatus, errorThrown);
    });
}

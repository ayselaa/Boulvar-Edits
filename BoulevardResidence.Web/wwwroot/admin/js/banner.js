$(function () {
    //delete 
  

    RemoveItem(".delete-banner", "/Admin/Banner/Delete");
    RemoveItem(".delete-blog", "/Admin/Blog/Delete");
    RemoveItem(".delete-bannergift", "/Admin/BannerGift/Delete");
    RemoveItem(".delete-sliderdiscovery", "/Admin/SliderDiscovery/Delete");
    RemoveItem(".delete-sliderdiscoveryvideo", "/Admin/SliderDiscoveryVideo/Delete");
    /* RemoveItem(".delete-footermenu", "/Admin/FooterMenu/Delete");*/
   /* RemoveItem(".delete-menuitem", "/Admin/FooterMenuItem/Delete");*/


    


    function RemoveItem(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
            let deleteButton = $(this);
            let id = deleteButton.attr("data-id");
            let data = { id: id };
            let tbody = deleteButton.closest("tbody");

            
            Swal.fire({
                icon: "warning",
                title: "Are you sure?",
                text: "This action cannot be undone.",
                showCancelButton: true,
                confirmButtonColor: "#dc3545",
                confirmButtonText: "Delete",
                cancelButtonText: "Cancel",
            }).then((result) => {
                if (result.isConfirmed) {
                    
                    $.ajax({
                        url: url,
                        type: "Post",
                        data: data,
                        success: function () {
                            let rowsToDelete = tbody.find(`tr[data-id="${id}"]`);
                            rowsToDelete.remove();

                            if (tbody.children().length === 0) {
                                $(".table").remove();
                                $(".paginate-area").remove();
                            }

                            // Display SweetAlert success notification
                            Swal.fire({
                                icon: "success",
                                title: "Success",
                                text: "Deleted successfully!",
                                position: "end",
                                showConfirmButton: false,
                                timer: 2000,
                            });
                        },
                    });
                }
            });
        });
    }
   
  
})
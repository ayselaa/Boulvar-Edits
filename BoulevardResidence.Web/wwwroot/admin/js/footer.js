
// Footer Menu
$(function () {
    $(document).on("click", ".menu-status", function () {
        let menuId = $(this).parent().attr("data-id");
        let elements = $("[data-id='" + menuId + "'] .menu-status");

        console.log("test");

        $.ajax({
            url: "footerMenu/setstatus",
            type: "POST",
            data: { id: menuId },
            success: function (res) {
                $(elements).toggleClass("active-status", !res);
                $(elements).toggleClass("deActive-status", res);
            }
        });
    });
});




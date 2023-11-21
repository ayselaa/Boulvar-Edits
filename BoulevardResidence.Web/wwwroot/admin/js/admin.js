  //Banner
$(function () {
    $(document).on("click", ".banner-status", function () {
        let bannerId = $(this).parent().attr("data-id");
        let elements = $("[data-id='" + bannerId + "'] .banner-status");

        console.log("test");

        $.ajax({
            url: "banner/setstatus",
            type: "POST",
            data: { id: bannerId },
            success: function (res) {
                $(elements).toggleClass("active-status", !res);
                $(elements).toggleClass("deActive-status", res);
            }
        });
    });
});


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


//Slider Delete




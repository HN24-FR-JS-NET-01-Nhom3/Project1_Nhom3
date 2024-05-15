$(function () {
    "use strict";
    var url = window.location.href;

    // Function to handle the activation of sidebar links
    function activateSidebarLink(element) {
        // Remove 'active' and 'selected' classes from all links and list items
        $("#sidebarnav a").removeClass("active");
        $("#sidebarnav li").removeClass("selected");

        // Add 'active' class to the clicked link
        element.addClass("active");

        // Add 'selected' class to the parent 'li' element of the clicked link
        element.closest('li').addClass("selected");
    }

    // Select the current sidebar link based on the URL
    var element = $("#sidebarnav a").filter(function () {
        return this.href === url;
    });

    // Activate the found element if it exists
    if (element.length) {
        activateSidebarLink(element);
    }

    // Add click event handler to all sidebar links
    $("#sidebarnav a").on("click", function (e) {
        activateSidebarLink($(this));
    });
});

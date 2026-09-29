document.addEventListener("DOMContentLoaded", () => {

    const menuButton =
        document.getElementById("mobileMenuButton");

    const mobileNavigation =
        document.getElementById("mobileNavigation");


    if (!menuButton || !mobileNavigation) {
        return;
    }


    menuButton.addEventListener("click", () => {

        const isOpen =
            mobileNavigation.classList.toggle("is-open");

        menuButton.setAttribute(
            "aria-expanded",
            isOpen.toString()
        );

    });

});
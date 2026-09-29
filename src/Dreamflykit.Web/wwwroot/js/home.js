(() => {
    "use strict";

    const prefersReducedMotion =
        window.matchMedia(
            "(prefers-reduced-motion: reduce)"
        ).matches;

    if (prefersReducedMotion) {
        return;
    }

    const revealElements =
        document.querySelectorAll(
            "[data-dfk-reveal]"
        );

    revealElements.forEach((element) => {
        element.animate(
            [
                {
                    opacity: 0,
                    transform: "translateY(24px)"
                },
                {
                    opacity: 1,
                    transform: "translateY(0)"
                }
            ],
            {
                duration: 700,
                easing: "cubic-bezier(0.22, 1, 0.36, 1)",
                fill: "both"
            }
        );
    });

    const heroVisual =
        document.querySelector(
            "[data-dfk-hero-visual]"
        );

    if (heroVisual) {
        heroVisual.animate(
            [
                {
                    opacity: 0,
                    transform:
                        "translateY(20px) scale(0.98)"
                },
                {
                    opacity: 1,
                    transform:
                        "translateY(0) scale(1)"
                }
            ],
            {
                duration: 900,
                delay: 120,
                easing: "cubic-bezier(0.22, 1, 0.36, 1)",
                fill: "both"
            }
        );
    }
})();(() => {
    "use strict";

    const prefersReducedMotion =
        window.matchMedia(
            "(prefers-reduced-motion: reduce)"
        ).matches;

    if (prefersReducedMotion) {
        return;
    }

    const revealElements =
        document.querySelectorAll(
            "[data-dfk-reveal]"
        );

    revealElements.forEach((element) => {
        element.animate(
            [
                {
                    opacity: 0,
                    transform: "translateY(24px)"
                },
                {
                    opacity: 1,
                    transform: "translateY(0)"
                }
            ],
            {
                duration: 700,
                easing: "cubic-bezier(0.22, 1, 0.36, 1)",
                fill: "both"
            }
        );
    });

    const heroVisual =
        document.querySelector(
            "[data-dfk-hero-visual]"
        );

    if (heroVisual) {
        heroVisual.animate(
            [
                {
                    opacity: 0,
                    transform:
                        "translateY(20px) scale(0.98)"
                },
                {
                    opacity: 1,
                    transform:
                        "translateY(0) scale(1)"
                }
            ],
            {
                duration: 900,
                delay: 120,
                easing: "cubic-bezier(0.22, 1, 0.36, 1)",
                fill: "both"
            }
        );
    }
})();
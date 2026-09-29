import Alpine from "alpinejs";

import { gsap } from "gsap";

import { ScrollTrigger } from "gsap/ScrollTrigger";

gsap.registerPlugin(ScrollTrigger);

window.Alpine = Alpine;

Alpine.start();

document.addEventListener("DOMContentLoaded", () => {

    document.documentElement.classList.add("js-ready");

});
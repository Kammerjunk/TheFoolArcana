if (window.matchMedia("(prefers-color-scheme: dark)").media === "not all") {
    document.documentElement.style.display = "none";
    document.head.insertAdjacentHTML(
        "beforeend",
        "<link rel=\"stylesheet\" href=\"~/lib/bootstrap-dark-5/dist/css/bootstrap-night.min.css\" media=\"(prefers-color-scheme: dark)\" />"
    );
}

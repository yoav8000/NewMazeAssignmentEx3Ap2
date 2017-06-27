
$(function () {
    if (localStorage == null) {
        localStorage.setItem("defaultRows", 20);
        localStorage.setItem("defaultCols", 20);
        localStorage.setItem("defaultSearchAlgo", "bfs");
    }
});
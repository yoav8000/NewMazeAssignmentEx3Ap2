
$(function () {
    document.getElementById("rows").value = localStorage.getItem("defaultRows");
    document.getElementById("cols").value = localStorage.getItem("defaultCols");
    document.getElementById("searchAlgorithm").value = localStorage.getItem("defaultSearchAlgo")
});


$(function () {
    $("#saveSettings").click(function () {
        var tempRows = document.getElementById("rows").value;
        var tempCols = document.getElementById("cols").value;
        var searchAlgo = $("#searchAlgorithm").val();
        var validInputFlag = 1;
        if (tempRows == "") {
            validInputFlag = 0;
            alert("Please enter rows.");
        }

        if (tempCols == "") {
            validInputFlag = 0;
            alert("Please enter cols.");
        }

        if (validInputFlag) {
            localStorage.setItem("defaultRows", tempRows);
            localStorage.setItem("defaultCols", tempCols);
            localStorage.setItem("defaultSearchAlgo", searchAlgo);
            window.location.replace("../HomePage/HomePage.html");
        }

    });
});
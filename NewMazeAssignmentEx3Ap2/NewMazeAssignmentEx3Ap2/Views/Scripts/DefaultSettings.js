
// define the value of rows, cols and algo to the default
$(function () {
    document.getElementById("rows").value = localStorage.getItem("defaultRows");
    document.getElementById("cols").value = localStorage.getItem("defaultCols");
    document.getElementById("searchAlgorithm").value = localStorage.getItem("defaultSearchAlgo")
});


// get the input of rows, cols and algo from the user and save it in local storage
$(function () {
    $("#saveSettings").click(function () {
        // rows input
        var tempRows = document.getElementById("rows").value;
        // cols input
        var tempCols = document.getElementById("cols").value;
        // searchAlgo input
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

        // save the input in local Storage
        if (validInputFlag) {
            localStorage.setItem("defaultRows", tempRows);
            localStorage.setItem("defaultCols", tempCols);
            localStorage.setItem("defaultSearchAlgo", searchAlgo);
            window.location.replace("../HomePage/HomePage.html");
        }

    });
});
var finishedGame = 0;
var mazeBoard;
var enabled = true;
var gotMaze = false;

// put the default details on the text box
$(function () {
    document.getElementById("rows").value = localStorage.getItem("defaultRows");
    document.getElementById("cols").value = localStorage.getItem("defaultCols");
    document.getElementById("searchAlgorithm").value = localStorage.getItem("defaultSearchAlgo")
});


jQuery(function ($) {

    // define a function when push the start new game button
    $(startNewGame).click(function () {
        if (enabled) {
            gotMaze = true;

            var fillFieldsFlag = 0;
            // check that the rows text is not empty
            if ($("#rows").val() == "") {
                fillFieldsFlag = 1;
                alert("Please enter rows")

            }
            // check that the cols text is not empty
            if ($("#cols").val() == "") {
                fillFieldsFlag = 1;
                alert("Please enter cols")
            }
            // check that the maze Name text is not empty
            if ($("#mazeName").val() == "") {
                fillFieldsFlag = 1;
                alert("Please enter a name for the maze.")
            }
            // if there are all details
            if (!fillFieldsFlag) {


                var apiUrl = "/api/Mazes";

                // ajax call.
                $("#loader").show();
                mazeName = $("#mazeName").val();
                document.title = mazeName;


                if (mazeBoard != undefined && mazeName != mazeBoard.data("mazeName")) {
                    var tempCanvas = document.getElementById("mazeCanvas");
                    $("mazeCanvas").clearCanvas(tempCanvas);

                }

                if (mazeBoard == undefined || mazeName != mazeBoard.data("mazeName")) {
                    rowsAmmount = $("#rows").val();
                    colsAmmount = $("#cols").val();

                    // sent the request to the server (Get request)
                    $.getJSON(apiUrl + "/" + mazeName + "/" + rowsAmmount + "/" + colsAmmount)

                        // when done with the ajax call.
                        .done(function (maze) {
                            // get information from the object returned.
                            var jsonObject = JSON.parse(maze);
                            var mazeString = jsonObject.Maze;
                            var startPoint = jsonObject.Start;
                            var startRow = startPoint.Row;
                            var startCol = startPoint.Col;
                            var endPoint = jsonObject.End;
                            var endRow = endPoint.Row;
                            var endCol = endPoint.Col;
                            var id = "mazeCanvas";
                            var canvas = document.getElementById("mazeCanvas");
                            var playerImage = document.getElementById("playerImage");
                            var destImage = document.getElementById("destinationImage");
                            var mazeArray = [];
                            var mazeRow = [];
                            for (var i = 0; i < rowsAmmount; ++i) {
                                for (var j = 0; j < colsAmmount; ++j) {
                                    mazeRow[j] = mazeString[i * colsAmmount + j];
                                }
                                mazeArray[i] = mazeRow;
                                mazeRow = [];
                            }
                            // draw the Maze on the canvaas
                            mazeBoard = $("#mazeCanvas").drawMaze(canvas, id, mazeArray, mazeName, rowsAmmount, colsAmmount, startPoint.Row, startPoint.Col, endPoint.Row, endPoint.Col, playerImage, destImage);

                            // hide the loader
                            $("#loader").hide();

                            $("#mazeCanvas").focus();
                            enabled = true;
                            finishedGame = 0;

                        });
                } else {

                    mazeBoard.restartImagesLocation(document.getElementById("mazeCanvas"));
                    $("#mazeCanvas").focus();
                    enabled = true;
                    finishedGame = 0;
                    $("#loader").hide();
                }

            }
        }
    });

    // define a function when the user press any key
    $("#mazeCanvas").keydown(function (e) {
        // if the maze drawed on the canvas
        if (enabled) {
            switch (e.which) {
                // case the user press left
                case 37:
                    if (mazeBoard.data("IsEnabled")) {
                        // check that the move is valid
                        if ((mazeBoard.data("currentColPos") - 1 >= 0) &&
                            mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos")][mazeBoard.data("currentColPos") - 1] == 0) {
                            MovePlayer("0");//left

                        }
                    }
                    break;
                case 38:
                    // case the user press up
                    if (mazeBoard.data("IsEnabled")) {
                        // check that the move is valid
                        if ((mazeBoard.data("currentRowPos") - 1 >= 0) &&
                            mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos") - 1][mazeBoard.data("currentColPos")] == 0) {
                            MovePlayer("2");//up

                        }
                    }
                    break;
                case 39:
                    // case the user press right
                    if (mazeBoard.data("IsEnabled")) {
                        // check that the move is valid
                        if ((mazeBoard.data("currentColPos") + 1 < mazeBoard.data("cols")) &&
                            mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos")][mazeBoard.data("currentColPos") + 1] == 0) {
                            MovePlayer("1");//right

                        }
                    }
                    break;
                case 40:
                    // case the user press down
                    if (mazeBoard.data("IsEnabled")) {
                        // check that the move is valid
                        if ((mazeBoard.data("currentRowPos") + 1 < mazeBoard.data("rows")) &&
                            mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos") + 1][mazeBoard.data("currentColPos")] == 0) {
                            MovePlayer("3");//down
                        }
                    }
                    break;
            }

            if (mazeBoard.data("currentRowPos") == mazeBoard.data("exitRow") && mazeBoard.data("currentColPos") == mazeBoard.data("exitCol")) {
                mazeBoard.data("IsEnabled", false);
                if (finishedGame == 0) {
                    setTimeout(function () { alert("Congratulations! you have reached the destination!"); }, 5);
                    //alert("Congratulations! you have reached the destination!");
                    finishedGame = 1;
                }
            }
        }
    });


    // define a function when the user press on solveGame buttopn
    $(solveGame).click(function () {
        // if the maze drawed on the canvas
        if (enabled) {
            if(gotMaze){
            enabled = false;
            var apiUrl = "/api/Mazes";

            // get the canvas 
            var tempCanvas = document.getElementById("mazeCanvas");
            //mazeBoard = $("#mazeCanvas").drawMaze(tempCanvas);
            mazeBoard.restartImagesLocation(tempCanvas);

            // ajax request.
            $("#loader").show();
            mazeName = mazeBoard.data("mazeName");
            var searchAlgo = $("#searchAlgorithm").val();
            $.getJSON(apiUrl + "/" + mazeName + "/" + searchAlgo)

                // when done with the ajax request.
                .done(function (solution) {
                    // get information from the object returned.
                    var jsonObject = JSON.parse(solution);

                    var mazeSolution = jsonObject.Solution;
                    $("#loader").hide();
                    var length = mazeSolution.length;
                    var interval;
                    var index = 0;


                    var timer = setInterval(function () {
                        MovePlayer(mazeSolution[index++]);
                        if (index == length) {
                            clearInterval(timer);
                            enabled = true;
                            mazeBoard.data("IsEnabled", false);
                        }
                    }, 250);



                });
        }
    }
    });
});

// function that move the player
function MovePlayer(direction) {


    switch (direction) {
        case "0":
            {
                mazeBoard.moveLeft();
                break;
            }
        case "1":
            {
                mazeBoard.moveRight();
                break;
            }
        case "2":
            {
                mazeBoard.moveUp();
                break;
            }
        case "3":
            {
                mazeBoard.moveDown();
                break;
            }
        default:
            {
                break;
            }
    }
}
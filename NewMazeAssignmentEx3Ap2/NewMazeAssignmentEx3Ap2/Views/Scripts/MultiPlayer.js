var mazeBoard;
var opponentBoard;
var enabled = true;
var gotMaze = false;
var mazeName;
var usersUri = "/api/Users";
var enableButtons = true;
var multiPlayerHub = $.connection.multiPlayerHub;


$(function () {
    if (sessionStorage.getItem("userName") == null) {
        alert("please sign up and login so you could play against an opponent");
        window.location.replace("../LoginPage/LoginPage.html");
    }
});


jQuery(function ($) {


    $.connection.hub.start().done(function () {

        $(function () {
            multiPlayerHub.server.listCommand();
            document.getElementById("rows").value = localStorage.getItem("defaultRows");
            document.getElementById("cols").value = localStorage.getItem("defaultCols");
        });

        $(startNewGame).click(function () {
            if (enableButtons) {
                enableButtons = false;
                if (enabled) {
                    gotMaze = true;

                    var fillFieldsFlag = 0;
                    if ($("#rows").val() == "") {
                        fillFieldsFlag = 1;
                        alert("Please enter rows")

                    }

                    if ($("#cols").val() == "") {
                        fillFieldsFlag = 1;
                        alert("Please enter cols")
                    }
                    if ($("#mazeName").val() == "") {
                        fillFieldsFlag = 1;
                        alert("Please enter a name for the maze.")
                    }
                    if (!fillFieldsFlag) {
                        var apiUrl = "/api/Mazes";

                        // ajax call.
                        $("#loader").show();
                        mazeName = $("#mazeName").val();
                        document.title = mazeName;

                        rowsAmmount = $("#rows").val();
                        colsAmmount = $("#cols").val();

                        multiPlayerHub.server.startCommand(mazeName, rowsAmmount, colsAmmount);
                    }
                }
            }
        });



        $(joinGame).click(function () {
            if (enabled) {
                if (enableButtons) {
                    enableButtons = false;
                    mazeName = $("#selectGames option:selected").text();
                    document.title = mazeName;
                    multiPlayerHub.server.joinCommand(mazeName);
                }
            }
        });


        $(selectGames).click(function () {
            if (enabled) {
                if (enableButtons) {
                    multiPlayerHub.server.listCommand();
                }
            }
        });



        $("#mazeCanvas").keydown(function (e) {
            if (enabled) {
                var movedFlag = false;
                var direction;
                var playerType = "me";
                switch (e.which) {
                    case 37:
                        if (mazeBoard.data("IsEnabled")) {
                            if ((mazeBoard.data("currentColPos") - 1 >= 0) &&
                                mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos")][mazeBoard.data("currentColPos") - 1] == 0) {
                                MovePlayer("0");//left
                                direction = "0";
                                movedFlag = true;
                            }
                        }
                        break;
                    case 38:
                        if (mazeBoard.data("IsEnabled")) {

                            if ((mazeBoard.data("currentRowPos") - 1 >= 0) &&
                                mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos") - 1][mazeBoard.data("currentColPos")] == 0) {
                                MovePlayer("2");//up
                                direction = "2";
                                movedFlag = true;
                            }
                        }
                        break;
                    case 39:
                        if (mazeBoard.data("IsEnabled")) {

                            if ((mazeBoard.data("currentColPos") + 1 < mazeBoard.data("cols")) &&
                                mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos")][mazeBoard.data("currentColPos") + 1] == 0) {
                                MovePlayer("1");//right
                                direction = "1";
                                movedFlag = true;
                            }
                        }
                        break;
                    case 40:
                        if (mazeBoard.data("IsEnabled")) {

                            if ((mazeBoard.data("currentRowPos") + 1 < mazeBoard.data("rows")) &&
                                mazeBoard.data("mazeData")[mazeBoard.data("currentRowPos") + 1][mazeBoard.data("currentColPos")] == 0) {
                                MovePlayer("3");//down
                                direction = "3";
                                movedFlag = true;
                            }
                        }
                        break;
                }
                if (movedFlag) {
                    multiPlayerHub.server.playCommand(mazeName, direction);
                }

                if (mazeBoard.data("currentRowPos") == mazeBoard.data("exitRow") && mazeBoard.data("currentColPos") == mazeBoard.data("exitCol")) {
                    mazeBoard.data("IsEnabled", false);

                    winnerUri = usersUri;
                    winnerUri += ("/won/" + sessionStorage.getItem("userName"));

                    $.getJSON(winnerUri).done(function (data) {
                        enabled = false;
                        setTimeout(function () { window.location.replace("../RankingPage/Ranking.html"); }, 20);

                    });
                    enabled = false;
                    mazeBoard.data("IsEnabled", false);
                    setTimeout(function () { alert("Congratulations! you have reached the destination!"); }, 5);

                }
            }
        });
    });



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
});


multiPlayerHub.client.getMaze = function (maze) {
    // get information from the object returned.
    var jsonObject = JSON.parse(maze);
    var mazeString = jsonObject.Maze;
    var startPoint = jsonObject.Start;
    var startRow = startPoint.Row;
    var startCol = startPoint.Col;
    var endPoint = jsonObject.End;
    var endRow = endPoint.Row;
    var endCol = endPoint.Col;
    var id1 = "mazeCanvas";
    var id2 = "opponentCanvas";
    var canvas = document.getElementById("mazeCanvas");
    var opponentCanvas = document.getElementById("opponentCanvas")
    var playerImage = document.getElementById("playerImage");
    var destImage = document.getElementById("destinationImage");
    var opponentImage = document.getElementById("opponentImage");
    var rowsAmmount = jsonObject.Rows;
    var colsAmmount = jsonObject.Cols;
    var mazeArray = [];
    var mazeRow = [];
    for (var i = 0; i < rowsAmmount; ++i) {
        for (var j = 0; j < colsAmmount; ++j) {
            mazeRow[j] = mazeString[i * colsAmmount + j];
        }
        mazeArray[i] = mazeRow;
        mazeRow = [];
    }

    mazeBoard = $("#mazeCanvas").drawMaze(canvas, id1, mazeArray, mazeName, rowsAmmount, colsAmmount, startPoint.Row, startPoint.Col, endPoint.Row, endPoint.Col, playerImage, destImage);
    opponentBoard = $("#opponentCanvas").drawMaze(opponentCanvas, id2, mazeArray, mazeName, rowsAmmount, colsAmmount, startPoint.Row, startPoint.Col, endPoint.Row, endPoint.Col, opponentImage, destImage);

    $("#loader").hide();
    $("#mazeCanvas").focus();
    enabled = true;

}



multiPlayerHub.client.updateJoinablMazes = function (list) {
    // var select = document.getElementById("selectGames");
    $("#selectGames").empty();

    for (var i = 0; i < list.length; ++i) {
        $(selectGames).append("<option " + "value = " + list[i] + ">" + list[i] + "</option>");
    }

}


multiPlayerHub.client.moveOpponent = function (direction) {


    switch (direction) {
        case "0":
            {
                opponentBoard.moveLeft();
                break;
            }
        case "1":
            {
                opponentBoard.moveRight();

                break;
            }
        case "2":
            {
                opponentBoard.moveUp();

                break;
            }
        case "3":
            {
                opponentBoard.moveDown();

                break;
            }
        default:
            {
                break;
            }
    }

    if (opponentBoard.data("currentRowPos") == opponentBoard.data("exitRow") && opponentBoard.data("currentColPos") == opponentBoard.data("exitCol")) {
        opponentBoard.data("IsEnabled", false);

        loserUri = usersUri;
        loserUri += ("/lost/" + sessionStorage.getItem("userName"));

        $.getJSON(loserUri).done(function (data) {
            setTimeout(function () { window.location.replace("../RankingPage/Ranking.html"); }, 52);

        });

        enabled = false;
        mazeBoard.data("IsEnabled", false);
        setTimeout(function () { alert("Unfortunatly you lost the game!"); }, 50);

    }

}



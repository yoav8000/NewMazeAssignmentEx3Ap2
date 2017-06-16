

var apiUrl = "/api/Mazes";

var playerImage;
var destImage;
var startRow;
var startCol;
var endRow;
var endCol;
var rowsAmmount;
var colsAmmount;
var mazeName;
var mazeString;
var mazeArray;
var startPoint;
var endPoint;
var currentRowPos;
var currentColPos;


jQuery(function ($) {
    $(startNewGame).click(function () {
        mazeName = $("#mazeName").val();
        rowsAmmount = $("#rows").val();
        colsAmmount = $("#cols").val();
        $.getJSON(apiUrl + "/" + mazeName + "/" + rowsAmmount + "/" + colsAmmount)

            .done(function (maze) {
                var jsonObject = JSON.parse(maze);
                mazeString = jsonObject.Maze;
                startPoint = jsonObject.Start;
                startRow = startPoint.Rows;
                startCol = startPoint.Col;
                endPoint = jsonObject.End;
                endRow = endPoint.Row;
                endCol = endPoint.Col;
                var id = "mazeCanvas";
                var canvas = document.getElementById("mazeCanvas");
                playerImage = document.getElementById("playerImage");
                destImage = document.getElementById("destinationImage");
                mazeArray = [];
                var mazeRow = [];
                for (var i = 0; i < rowsAmmount; ++i) {
                    for (var j = 0; j < colsAmmount; ++j) {
                        mazeRow[j] = mazeString[i * rowsAmmount + j];
                    }
                    mazeArray[i] = mazeRow;
                    mazeRow = [];
                }
                currentRowPos = startPoint.Row;
                currentColPos = startPoint.Col;
                $("#mazeCanvas").drawMaze(canvas, id, mazeArray, rowsAmmount, colsAmmount, startPoint, endPoint, playerImage, destImage);
                $("#mazeCanvas").focus();
            });
    });


    $("#mazeCanvas").keydown(function (e) {
        switch (e.which) {
            case 37:
                if (mazeArray[currentRowPos][currentColPos - 1] == 0) {
                    $("#mazeCanvas").moveLeft();
                    currentColPos -= 1;
                }
                break;
            case 38:
                if (mazeArray[currentRowPos-1][currentColPos ] == 0) {
                    $("#mazeCanvas").moveUp();
                    currentRowPos -= 1;
                }
                break;
            case 39:
                if (mazeArray[currentRowPos][currentColPos + 1] ==  0) {
                    $("#mazeCanvas").moveRight();
                    currentColPos += 1;
                }
                break;
            case 40:
                if (mazeArray[currentRowPos + 1][currentColPos] ==  0) {
                    $("#mazeCanvas").moveDown();
                    currentRowPos += 1;
                }
                break;
        }
    });

});
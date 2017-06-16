(function ($) {

    var currentRowsPos;
    var currentColPos;
    var mazeCanvas;
    var cellHeight
    var cellWidth;
    var myPlayerImage
    var destImage

    $.fn.drawMaze = function (canvas, canavasId, maze, rows, cols, start, end, playerImage, destinationImage) {
        mazeCanvas = canvas;
        var context = mazeCanvas.getContext("2d");
        cellWidth = mazeCanvas.width / cols;
        cellHeight = mazeCanvas.height / rows;
        myPlayerImage = playerImage;
        destImage = destinationImage;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (maze[i][j] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
        }


        startCol = start.Col;
        startRows = start.Row;
        context.drawImage(myPlayerImage, cellWidth * startCol, cellHeight * startRows, cellWidth, cellHeight);

        endCol = end.Col;
        endRows = end.Row;

        context.drawImage(destinationImage, cellWidth * endCol, cellHeight * endRows, cellWidth, cellHeight);
        currentRowsPos = startRows;
        currentColPos = startCol;

      
    }




    $.fn.moveRight = function () {

        var context = mazeCanvas.getContext("2d");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowsPos, cellWidth, cellHeight)
        currentColPos += 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowsPos, cellWidth, cellHeight);

    }


    $.fn.moveLeft = function () {

        var context = mazeCanvas.getContext("2d");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowsPos, cellWidth, cellHeight)
        currentColPos -= 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowsPos, cellWidth, cellHeight);

    }


    $.fn.moveUp = function () {

        var context = mazeCanvas.getContext("2d");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowsPos, cellWidth, cellHeight)
        currentRowsPos -= 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowsPos, cellWidth, cellHeight);

    }


    $.fn.moveDown = function () {


        var context = mazeCanvas.getContext("2d");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowsPos, cellWidth, cellHeight)
        currentRowsPos += 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowsPos, cellWidth, cellHeight);

    }

})(jQuery);
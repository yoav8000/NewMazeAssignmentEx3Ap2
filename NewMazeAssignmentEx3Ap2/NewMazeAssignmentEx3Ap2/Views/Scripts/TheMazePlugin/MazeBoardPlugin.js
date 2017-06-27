(function ($) {

    $.fn.drawMaze = function (canvas, canavasId, mazeData, mazeName, rows, cols, startRow, startCol, exitRow, exitCol, playerImage, destinationImage) {

        if ((canvas != undefined) && (canavasId != undefined) && (mazeData != undefined) && (mazeName != undefined)
            && (rows != undefined) && (cols != undefined) && (startRow != undefined) && (startCol != undefined)
            && (exitRow != undefined) && (exitCol != undefined) && (playerImage != undefined)
            && (destinationImage != undefined)) {

            $(this).data("canavas", canvas);
            $(this).data("canavasId", canavasId);
            $(this).data("mazeData", mazeData);
            $(this).data("mazeName", mazeName);
            $(this).data("rows", rows);
            $(this).data("cols", cols);
            $(this).data("startRow", startRow);
            $(this).data("startCol", startCol);
            $(this).data("exitRow", exitRow);
            $(this).data("exitCol", exitCol);
            $(this).data("playerImage", playerImage);
            $(this).data("destinationImage", destinationImage);
            $(this).data("currentRowPos", startRow);
            $(this).data("currentColPos", startCol);
            $(this).data("cellWidth", canvas.width / cols);
            $(this).data("cellHeight", canvas.height / rows);
            $(this).data("IsEnabled", true);

            var tempCanvas = $(this).data("canavas");
            var context = tempCanvas.getContext("2d");
            var cellWidth = tempCanvas.width / cols;
            var cellHeight = tempCanvas.height / rows;
            var maze = $(this).data("mazeData");
            context.fillStyle = "#000000";
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (maze[i][j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
        }
        $(this).restartImagesLocation(canvas);
        return this;
    }




    $.fn.restartImagesLocation = function (canvas) {

        if ($(this).data("currentRowPos") != $(this).data("startRow") || $(this).data("currentColPos") != $(this).data("startCol")) {

            var tempContext = canvas.getContext("2d");
            tempContext.fillRect($(this).data("cellWidth") * $(this).data("currentColPos"), $(this).data("cellHeight")
                * $(this).data("currentRowPos"), $(this).data("cellWidth"), $(this).data("cellHeight"));


            $(this).data("currentColPos", $(this).data("startCol"));
            $(this).data("currentRowPos", $(this).data("startRow"));

        }
        context = $(this).data("canavas").getContext("2d");

        context.drawImage($(this).data("playerImage"), $(this).data("cellWidth") * $(this).data("startCol"),
            $(this).data("cellHeight") * $(this).data("startRow"), $(this).data("cellWidth"), $(this).data("cellHeight"));




        context.drawImage($(this).data("destinationImage"), $(this).data("cellWidth") * $(this).data("exitCol"), $(this).data("cellHeight")
            * $(this).data("exitRow"), $(this).data("cellWidth"), $(this).data("cellHeight"));
        $(this).data("IsEnabled", true);

        }


    

    $.fn.clearCanvas = function (canvas) {

        $(this).removeData("mazeData");
        $(this).removeData("mazeName");
        $(this).removeData("rows");
        $(this).removeData("startRow");
        $(this).removeData("startCol");
        $(this).removeData("exitRow");
        $(this).removeData("exitCol");
        $(this).removeData("currentRowPos");
        $(this).removeData("currentColPos");
        $(this).removeData("cellWidth");
        $(this).removeData("cellHeight");
        $(this).data("IsEnabled", true);

        var context = canvas.getContext("2d");
        var cellWidth = canvas.width / cols;
        var cellHeight = canvas.height / rows;
        context.fillStyle = "#FFFFFF";
        context.fillRect(0, 0, canvas.width, canvas.height);

    }

    $.fn.moveRight = function () {

        var context = $(this).data("canavas").getContext("2d");
        var cellWidth = $(this).data("cellWidth");
        var cellHeight = $(this).data("cellHeight");
        var currentColPos = $(this).data("currentColPos");
        var currentRowPos = $(this).data("currentRowPos");
        var myPlayerImage = $(this).data("playerImage");
        context.fillStyle = "#FFFFFF";


        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowPos, cellWidth, cellHeight)
        currentColPos += 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowPos, cellWidth, cellHeight);
        $(this).data("currentColPos", currentColPos);

    }


    $.fn.moveLeft = function () {

        var context = $(this).data("canavas").getContext("2d");
        var cellWidth = $(this).data("cellWidth");
        var cellHeight = $(this).data("cellHeight");
        var currentColPos = $(this).data("currentColPos");
        var currentRowPos = $(this).data("currentRowPos");
        var myPlayerImage = $(this).data("playerImage");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowPos, cellWidth, cellHeight)
        currentColPos -= 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowPos, cellWidth, cellHeight);
        $(this).data("currentColPos", currentColPos);
    }


    $.fn.moveUp = function () {

        var context = $(this).data("canavas").getContext("2d");
        var cellWidth = $(this).data("cellWidth");
        var cellHeight = $(this).data("cellHeight");
        var currentColPos = $(this).data("currentColPos");
        var currentRowPos = $(this).data("currentRowPos");
        var myPlayerImage = $(this).data("playerImage");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowPos, cellWidth, cellHeight)
        currentRowPos -= 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowPos, cellWidth, cellHeight);
        $(this).data("currentRowPos", currentRowPos);
    }


    $.fn.moveDown = function () {

        var context = $(this).data("canavas").getContext("2d");
        var cellWidth = $(this).data("cellWidth");
        var cellHeight = $(this).data("cellHeight");
        var currentColPos = $(this).data("currentColPos");
        var currentRowPos = $(this).data("currentRowPos");
        var myPlayerImage = $(this).data("playerImage");
        context.fillStyle = "#FFFFFF";
        context.fillRect(cellWidth * currentColPos, cellHeight * currentRowPos, cellWidth, cellHeight)
        currentRowPos += 1;
        context.drawImage(myPlayerImage, cellWidth * (currentColPos), cellHeight * currentRowPos, cellWidth, cellHeight);
        $(this).data("currentRowPos", currentRowPos);
    }



})(jQuery);
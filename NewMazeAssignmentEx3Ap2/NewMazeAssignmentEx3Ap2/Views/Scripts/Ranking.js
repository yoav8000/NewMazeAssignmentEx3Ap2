
$(function () {
    var usersUri = "/api/Users";
    var users =[];

    $.getJSON(usersUri).done(function (data) {
        for (var i = 0; i < data.length; ++i) {
            users[i] = data[i];
        }
        users.sort(function (a, b) {
            var calcA = a.Wins - a.Losses;
            var calcB = b.Wins - b.Losses;
            if (calcA < calcB) {
                return 1;
            }
            if (calcA > calcB) {
                return -1;
            }
            return 0;
        });

        $("#rankingTable").append("<tr><th>Rank</th><th>Username</th><th>Wins</th><th>Losses</th></tr>");
        $("#rankingTable").append("<tbody>");
        for (var i = 0; i < users.length; ++i) {
            $("#rankingTable").append("<tr>" + "<td>" + (i + 1) + "</td>" +
                "<td>" + users[i].Name + "</td > " +
                "<td>" + users[i].Wins + "</td > " +
                "<td>" + users[i].Losses + "</td > " +
            "</tr>");
            
        }
        $("#rankingTable").append("</tbody>");
    

    });
});

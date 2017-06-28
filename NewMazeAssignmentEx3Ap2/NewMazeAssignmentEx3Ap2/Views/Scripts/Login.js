




var users;
var userClickedLogin = false;


var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    self.userName = ko.observable();
    self.password = ko.observable();
    var usersUri = "/api/Users";



    self.login = function () {
        if (!userClickedLogin) {
            userClickedLogin = true;;
            var usersUri = "/api/Users";
            usersUri += "/" + self.userName() + "/" + self.password();
            $("#loader").show();
            $.get(usersUri, self.userName(), self.password()).done(function (item) {
                if (item == "user wasn't in db") {
                    $("#loader").hide();
                    setTimeout(function () { alert("incorrect username\nplease enter a correct user name."); }, 50);


                    userClickedLogin = false;
                } else {

                    if (item == "incorrect password") {
                        $("#loader").hide();
                        setTimeout(function () { alert("incorrect password,\nplease enter the correct password."); }, 50);
                        userClickedLogin = false;
                    } else {
                        self.users.push(item);
                        sessionStorage.setItem("userName", item.Name);
                        window.location.replace("../HomePage/HomePage.html");
                    }
                }

            })
                .fail(function (xhr) {
                    $("#loader").hide();
                    setTimeout(function () { alert("ERROR: something went wrong in connecting to the server."); }, 50);

                    userClickedLogin = false;

                });
        }
    }

}
ko.applyBindings(new ViewModel()); // sets up the data binding

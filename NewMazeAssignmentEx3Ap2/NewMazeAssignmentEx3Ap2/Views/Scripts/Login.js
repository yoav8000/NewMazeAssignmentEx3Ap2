




var users;



var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    self.userName = ko.observable();
    self.password = ko.observable();
    var usersUri = "/api/Users";



    self.login = function () {
        var usersUri = "/api/Users";
        
        usersUri += "/" + self.userName() + "/" + self.password();
        $("#loader").show();
        $.get(usersUri, self.userName(), self.password()).done(function (item) {
            self.users.push(item);
            sessionStorage.setItem("userName", item.Name);
            if (item == "user wasn't in db") {
                alert("incorrect username\nplease enter a correct user name.");
            
            } else {
                $("#loader").hide();
                if (item == "incorrect password") {
                    alert("incorrect password,\nplease enter the correct password.");
                } else {
                    window.location.replace("../HomePage/HomePage.html");
                }
            }
        })
            .fail(function (xhr) {
                if (409 == xhr.status) {
                    alert("The user name is already taken,\nplease choose another one.");
                } else {
                    alert("ERROR: something went wrong in connecting to the server.");
                }
                $("#loader").hide();
            });
    }

}
ko.applyBindings(new ViewModel()); // sets up the data binding

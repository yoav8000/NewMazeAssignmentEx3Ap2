var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirmPassword");


function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords Don't Match");
    } else {
        confirm_password.setCustomValidity('');
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;




var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    self.userName = ko.observable();
    self.password = ko.observable();
    self.email = ko.observable();
    var usersUri = "/api/Users";

    function getAllUsers() {
        $.getJSON(usersUri).done(function (data) {
            self.users(data);
        });
    }

   
    self.addUser = function () {
        var usersUri = "/api/Users";

        var tempUserName = document.getElementById("userName").value;
        var user =
            {
                name: tempUserName,
                password: document.getElementById("password").value,
                email: document.getElementById("email").value,
                Wins: 0,
                Losses: 0
            };

        usersUri += "/" + tempUserName;


        $.post(usersUri, user).done(function (item) {
            self.users.push(item);
            sessionStorage.setItem("userName", item.Name);
            window.location.replace("../HomePage/HomePage.html");
        })
            .fail(function (xhr) {
                if (409 == xhr.status) {
                    alert("The user name is already taken,\nplease choose another one.");
                } else {
                    alert("ERROR: something went wrong in connecting to the server.");
                }
            });
    }

}
ko.applyBindings(new ViewModel()); // sets up the data binding
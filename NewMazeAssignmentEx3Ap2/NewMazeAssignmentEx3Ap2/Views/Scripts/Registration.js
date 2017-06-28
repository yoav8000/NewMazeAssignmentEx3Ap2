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

var userClickedRegister = false;


var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures
    self.users = ko.observableArray(); // enables data binding
    self.userName = ko.observable();
    self.password = ko.observable();
    self.email = ko.observable();
    var usersUri = "/api/Users";

   
    self.addUser = function () {
        if (!userClickedRegister) {
            userClickedRegister = true;
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

            $("#loader").show();
            $.post(usersUri, user).done(function (item) {
                self.users.push(item);
                sessionStorage.setItem("userName", item.Name);
                $("#loader").hide();
                window.location.replace("../HomePage/HomePage.html");
            })
                .fail(function (xhr) {
                    $("#loader").hide();
                    if (409 == xhr.status) {
                       
                        setTimeout(function () { alert("The user name is already taken,\nplease choose another one."); }, 50);
                        userClickedRegister = false;
                    } else {
                        setTimeout(function () { alert("ERROR: something went wrong in connecting to the server."); }, 50);
                        
                        userClickedRegister = false;
                    }
                 
                });
        }
    }

}
ko.applyBindings(new ViewModel()); // sets up the data binding
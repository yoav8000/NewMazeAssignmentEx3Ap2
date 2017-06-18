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


    self.checkIfUserNameExis = function () {
        var tempUser = ko.utils.arrayFirst(self.users(), function (tempUser) {
            return user.userName == tempUser.userName;
        });
        if (tempUser != null) {
            alert("user name already exist in the system");
        }
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


        self.checkIfUserNameExis();

       
          
                $.post(usersUri, user).done(function (item) {
                    self.users.push(item);
                    sessionStorage.setItem("userName", user.userName);
                    //     window.location.replace("HomePage.html");
                });

       
    }


}
ko.applyBindings(new ViewModel()); // sets up the data binding
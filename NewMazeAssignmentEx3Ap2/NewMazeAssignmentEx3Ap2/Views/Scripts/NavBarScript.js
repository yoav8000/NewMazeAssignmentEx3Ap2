

$("#navBar").load("../TheNavBar/NavBar.html", function (response, status, xhr) {

    if (status == "success") {

        // if the user log in, change the button register to hello + user name
        if (sessionStorage.getItem("userName") != null) {
            document.getElementById("register").innerHTML = "Hello " + sessionStorage.getItem("userName");
            document.getElementById("register").href = "#";
        }
        // if the user log in, change the button log in to log off
        if (sessionStorage.getItem("userName") != null) {
            document.getElementById("login").innerHTML = "Log Off";
            document.getElementById("login").href = "../RegistrationPage/Register.html";
        }
    }
    });
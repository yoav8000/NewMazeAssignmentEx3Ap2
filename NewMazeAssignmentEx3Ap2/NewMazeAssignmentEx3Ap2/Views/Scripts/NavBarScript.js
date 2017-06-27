

$("#navBar").load("../TheNavBar/NavBar.html", function (response, status, xhr) {

    if (status == "success") {


        if (sessionStorage.getItem("userName") != null) {
            document.getElementById("register").innerHTML = "Hello " + sessionStorage.getItem("userName");
            document.getElementById("register").href = "#";
        }
        if (sessionStorage.getItem("userName") != null) {
            document.getElementById("login").innerHTML = "Log Off";
            document.getElementById("login").href = "../RegistrationPage/Register.html";
        }
    }
    });
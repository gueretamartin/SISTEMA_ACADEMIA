$(document).ready(function () {

    cargarEventosLogin();

});

function cargarEventosLogin() {
    $('#btnLogin').click(function () {
        $("#form1").submit(function (e) {
            e.preventDefault();
            var user = $('#txtUser').val();
            var pass = $('#txtPass').val();
            if ((user === "" || user === null)  && (pass === "" || pass === null)) {
                $('#lblError').text("Falta ingresar el usuario y la contraseña");
                $('#txtUser').css("border", "3px solid #ff0000");
                $('#txtPass').css("border", "3px solid #ff0000");
            }
            else if (user == "") {
                $('#lblError').text("Falta ingresar el usuario");
                $('#txtUser').css("border", "3px solid #ff0000");
            }
            else if (pass == "") {
                $('#lblError').text("Falta ingresar la contraseña");
                $('#txtPass').css("border", "3px solid #ff0000");
            }
            else {
                this.submit();
            }
        });
        
    });

    $('#txtUser').click(function () {

        $('#txtUser').css("border", "0");
        $('#txtPass').css("border", "0");
        $('#lblError').html("");
    });

    $('#txtPass').click(function () {

        $('#txtUser').css("border", "0");
        $('#txtPass').css("border", "0");
        $('#lblError').html("");
    });
}
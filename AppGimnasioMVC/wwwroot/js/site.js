// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Tipos de datos

//let nombre = "jorge"; variable
//let lastname = 'peña';
//let nombreCompleto = nombre + lastname;
//const PI = 3.1415; constante
//console.log(nombre); mostar el valor por consola del navegador
//document.write(PI); escribir es el documento html
//let n1 = 77;
//let n2 = 3;
//let result = n1 + n2;
//let result = n1 > n2;

function validarLogin() {

    let contrasenia = document.getElementById("Password1").value.toString();
    let email = document.getElementById("Email1").value.toString();
    
    if (email === "" || contrasenia === "") {
        Swal.fire({
            icon: 'error',
            title: 'Datos incorrectos',
            text: 'Debe llenar todos los campos'
        });
    }
}

function bienvenida(usuario) {
    if (usuario != "") {
        Swal.fire({
            icon: 'success',
            title: 'Bienvenido',
            text: usuario
        });
    }
}

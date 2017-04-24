$(document).ready(function () {
    alert("prueba");
     var botonBuscador = $("#botonBuscar");
     var inputBuscador = $("#buscar");

        if (inputBuscador.is(":empty")) {
            botonBuscador.attr('disabled', 'disabled');
        }

});


$(document).ready(function () {
    var texto = $("#texto");
    var boton = $("#botonEnviar");

    deshabilitarBoton();
    texto.keydown(procesarKeypress);
});

function procesarKeypress(e) {
    var codigo = e.which;
    var textoActual = $("#texto").val() + String.fromCharCode(codigo);
    var charBackspace = String.fromCharCode(8);
    textoActual = textoActual.replace(charBackspace, "");

    var condicion = codigo == 8 ? backspacePressed : backspaceNotPressed;

    if (condicion(textoActual)) {
        habilitarBoton();
    }

    else {
        deshabilitarBoton();
    }
}

function habilitarBoton() {
    $("#botonEnviar").removeAttr("disabled");
}

function deshabilitarBoton() {
    $("#botonEnviar").attr("disabled", "disabled");
}

function backspacePressed(texto) {
    return texto.length > 1;
}

function backspaceNotPressed(texto) {
    return texto.length >= 1;
}

$(document).ready(function () {
    debugger;
    var nombreDeProducto = $("#datosNombreProducto");
    var talleDeProducto = $("#datosTalleProducto");
    var exportar = $("#exportar");
    if ($("#pedidos-tabla tr").length > 1) {
        exportar.show();
    } else {
        exportar.hide();
    }
});


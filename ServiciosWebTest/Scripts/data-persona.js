$(document).ready(function () {
    $('#btnGuardarPersona').click(function () {
        var persona = {};
        persona.tipo_persona = $("#tipo_persona").val();
        persona.nombre = $("#nombre").val();
        persona.tipo_documento = $("#tipo_documento").val();
        persona.num_documento = $("#num_documento").val();
        persona.direccion = $("#direccion").val();
        persona.telefono = $("#telefono").val();
        persona.email = $("#email").val();



        $.ajax({
            type: 'POST',
            url: '/api/persona/guardar',
            dateType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(persona), // { categoria : categoria}
            success: function (response) {
                toastr.success("Persona Agregada", "Guardar", { timeOut: 3000, closeButton: true });
                //console.log("Listo");

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("Error, Vehiculo no Agregado", "No guardado", { timeOut: 3000, closeButton: true });
                console.log(textStatus);
            }
        });
    });


});
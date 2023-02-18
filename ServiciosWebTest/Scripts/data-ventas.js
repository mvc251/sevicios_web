$(document).ready(function () {
    $('#btnGuardarVenta').click(function () {
        var ventas = {};
        ventas.idCliente = $("#idCliente").val();
        ventas.idUsuario = $("#idUsuario").val();
        ventas.tipo_comprobante = $("#tipo_comprobante").val();
        ventas.serie_comprobante = $("#serie_comprobante").val(); 
        ventas.num_comprobante = $("#num_comprobante").val();
        ventas.fecha_hora = $("#fecha_hora").val();
        ventas.estado = $("#estado").val();
        ventas.idVenta = $("#idVenta").val();
        ventas.idArticulo = $("#idArticulo").val();
        ventas.cantidad = $("#cantidad").val();
        ventas.descuento = $("#descuento").val();



        $.ajax({
            type: 'POST',
            url: '/api/ventas/guardar',
            dateType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(ventas), // { categoria : categoria}
            success: function (response) {
                toastr.success("Venta Agregada", "Guardar", { timeOut: 3000, closeButton: true });
                //console.log("Listo");

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("Error, Venta No Agregada", "No guardado", { timeOut: 3000, closeButton: true });
                console.log(textStatus);
            }
        });
    });


    $('#btnActualizarVentasAll').click(
        function MostrarAll() {
            $.ajax({
                type: "GET",
                url: "/api/Ventas",
                dataType: "JSON",
                success: function (data) {
                    toastr.success("Tabla de Ventas Actualizada", "Mostrar", { timeOut: 1000, closeButton: true });
                    get();
                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error("No se pudo procesar la información de forma correcta", "No mostrado", { timeOut: 1000, closeButton: true });
                }
            });
        });


    $('#btnActualizar').click(function () {

        var id = $('#identificador').val();

        var vehiculos = {};
        vehiculos.id = $("#identificador").val();
        vehiculos.marca = $("#marca").val();
        vehiculos.modelo = $("#modelo").val();
        vehiculos.anio = $("#anio").val();
        vehiculos.numeroSerie = $("#numeroSerie").val();
        vehiculos.totalPasajeros = $("#totalPasajeros").val();
        vehiculos.color = $("#color").val();
        vehiculos.descripcion = $("#descripcion").val();
        vehiculos.propietario = $("#propietario").val();

        $.ajax({
            type: 'PUT',
            url: '/api/Vehiculos/' + id,
            dateType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(vehiculos),
            success: function (data) {
                toastr.success("Venta Actulizada", "Actualizado", { timeOut: 1000, closeButton: true });

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta", "No actualizado", { timeOut: 1000, closeButton: true });

            }
        });
    });


    $(function () {
        get();
    });


    function get() {
        $("#gridVentasAll").empty();
        var $grid = $("#gridVentasAll");

        var $tr = $("<tr></tr>");
        $tr.append("<td>Id Venta</td>");
        $tr.append("<td>Id Cliente</td>");
        $tr.append("<td>Id Usuario</td>");
        $tr.append("<td>Tipo Comprobante</td>");
        $tr.append("<td>Serie de Comprobante</td>");
        $tr.append("<td>Numero de Comprobante</td>");
        $tr.append("<td>Fecha</td>");
        $tr.append("<td>Impuesto</td>");
        $tr.append("<td>Total</td>");
        $tr.append("<td>Estado</td>");

        $grid.append($tr);

        $.ajax({
            url: "/api/Ventas",
            type: "GET",
            success: function (data) {
                $.each(data, function (idx, item) {
                    var $tr = $("<tr></tr>");
                    $tr.append("<td>" + item.idVenta + "</td>");
                    $tr.append("<td>" + item.idCliente + "</td>");
                    $tr.append("<td>" + item.idUsuario + "</td>");
                    $tr.append("<td>" + item.tipo_comprobante + "</td>");
                    $tr.append("<td>" + item.serie_comprobante + "</td>");
                    $tr.append("<td>" + item.num_comprobante + "</td>");
                    $tr.append("<td>" + item.fecha_hora + "</td>");
                    $tr.append("<td>" + item.impuesto + "</td>");
                    $tr.append("<td>" + item.total + "</td>");
                    $tr.append("<td>" + item.estado + "</td>");

                    $grid.append($tr);
                });
            },
            error: function (request, msg, error) {
                toastr.error("Error, Venta No Agregada", "No guardado", { timeOut: 3000, closeButton: true });
                console.log(textStatus);
            },
        });
    }


});
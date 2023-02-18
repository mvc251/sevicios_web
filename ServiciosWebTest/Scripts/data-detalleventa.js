$(document).ready(function () {

    $('#btnBuscarVentasDia').click(function () {

        var ventaDia = $('#ventaDia').val();

        $("#tablaVentas").empty();
        var $tablaVentas = $("#tablaVentas");
        var $tr = $("<tr></tr>");
        $tr.append("<td>Id Venta</td>");
        $tr.append("<td>Id Cliente</td>");
        $tr.append("<td>Id Usuario</td>");
        $tr.append("<td>Fecha y Hora</td>");
        $tr.append("<td>Impuesto</td>");
        $tr.append("<td>Total</td>");
        $tr.append("<td>Estado</td>");
        $tablaVentas.append($tr);

        $("#tablaDetalleVenta").empty();
        var $tablaDetalleVenta = $("#tablaDetalleVenta");
        var $trdtv = $("<tr></tr>");
        $trdtv.append("<td>Detalle Venta1 (Id)</td>");
        $trdtv.append("<td>Id Articulo</td>");
        $trdtv.append("<td>Cantidad</td>");
        $trdtv.append("<td>Precio</td>");
        $trdtv.append("<td>Descuento</td>");
        $trdtv.append("<td>Nombre</td>");
        $trdtv.append("<td>Id Venta</td>");
        $tablaDetalleVenta.append($trdtv);  

        $.ajax({
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            url: '/api/ventas/date?fecha=' + ventaDia,
            success: function (data) {
                toastr.success("Ventas encontrada con la fecha: " + ventaDia, { timeOut: 3000, closeButton: true })

                $.each(data, function (idx, item1) {
                    var $tr = $("<tr></tr>");
                    $tr.append("<td>" + item1.idVenta + "</td>");
                    $tr.append("<td>" + item1.idCliente + "</td>");
                    $tr.append("<td>" + item1.idUsuario + "</td>");
                    $tr.append("<td>" + item1.fecha_hora + "</td>");
                    $tr.append("<td>" + item1.impuesto + "</td>");
                    $tr.append("<td>" + item1.total + "</td>");
                    $tr.append("<td>" + item1.estado + "</td>");
                    $tablaVentas.append($tr);
                });

                $.each(data, function (idx, item) {
                    var $trdtv = $("<tr></tr>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].detalle_venta1 + "</td>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].idArticulo + "</td>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].cantidad + "</td>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].precio + "</td>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].descuento + "</td>");
                    $trdtv.append("<td>" + item.Nombre + "</td>");
                    $trdtv.append("<td>" + item.detalleVentaDto[idx].idVenta + "</td>");
                    $tablaDetalleVenta.append($trdtv);
                });

            },
            error: function (xhr) {
                toastr.error("No existen ventas en la fecha solicitada", { timeOu: 3000, closeButton: true })
                console.log(textStatus);
            },
        });
    });

});
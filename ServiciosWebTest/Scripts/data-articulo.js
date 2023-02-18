$(document).ready(function () {
    $('#btnGuardarArticulo').click(function () {
        var articulo = {};
        articulo.idCategoria = $("#idCategoria").val();
        articulo.Codigo = $("#Codigo").val();
        articulo.Nombre = $("#Nombre").val();
        articulo.Precio_venta = $("#Precio_venta").val();
        articulo.Stock = $("#Stock").val();
        articulo.Descripcion = $("#Descripcion").val();
        articulo.Condicion = $("#Condicion").val();


        $.ajax({
            type: 'POST',
            url: '/api/articulo/guardar',
            dateType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(articulo), // { categoria : categoria}
            success: function (response) {
                toastr.success("Articulo Agregado", "Guardar", { timeOut: 3000, closeButton: true });
                //console.log("Listo");

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("Error, Vehiculo no Agregado", "No guardado", { timeOut: 3000, closeButton: true });
                console.log(textStatus);
            }
        });
    });


    $('#btnEliminarArticulo').click(function () {
        var idArticulo = $('#identificador').val();
        $.ajax({
            type: 'DELETE',
            url: '/api/Articulo/' + idArticulo,
            dataType: "JSON",
            success: function (data) {
                toastr.warning("Articulo Eliminado", "Eliminar", { timeOut: 3000, closeButton: true });
            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se puede procesar la información de forma correcta", "No eliminado",
                    { timeOut: 3000, closeButton: true });
            }
        });
    });


    $('#btnActualizarArticulo').click(function () {

        var idArticulo = $('#identificador').val();

        var articulo = {};
        articulo.idArticulo = $("#identificador").val();
        articulo.idCategoria = $("#idCategoria").val();
        articulo.Codigo = $("#Codigo").val();
        articulo.Nombre = $("#Nombre").val();
        articulo.Precio_venta = $("#Precio_venta").val();
        articulo.Stock = $("#Stock").val();
        articulo.Descripcion = $("#Descripcion").val();
        articulo.Condicion = $("#Condicion").val();
       

        $.ajax({
            type: 'PUT',
            url: '/api/Articulos/' + idArticulo,
            dateType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(articulo),
            success: function (data) {
                toastr.success("Articulo Actualizado", "Actualizado", { timeOut: 1000, closeButton: true });

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta", "No actualizado", { timeOut: 1000, closeButton: true });

            }
        });
    });


    $('#btnBuscarStockMax').click(function () {
        $.ajax({
            type: "GET",
            url: "/api/Articulos/max",
            dataType: "JSON",
            success: function (data) {
                toastr.success("Stock maximo encontrado", "Buscar", { timeOut: 3000, closeButton: true });
                //    $.each(data, function (i) {
                $("#maxStock").val(data);
                //    });

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta", "No buscado", { timeOut: 1000, closeButton: true });
            }
        });
    });


    $('#btnBuscarStockMin').click(function () {
        $.ajax({
            type: "GET",
            url: "/api/Articulos/min",
            dataType: "JSON",
            success: function (data) {
                toastr.success("Stock minimo mostrado", "Buscar", { timeOut: 3000, closeButton: true });
                //    $.each(data, function (i) {
                $("#minStock").val(data);
                //    });

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta", "No buscado", { timeOut: 1000, closeButton: true });
            }
        });
    });


    $('#btnSumarVentas').click(function () {
        $.ajax({
            type: "GET",
            url: "/api/Articulos/sumar",
            dataType: "JSON",
            success: function (data) {
                toastr.success("Suma de articulos", "Buscar", { timeOut: 3000, closeButton: true });
                //    $.each(data, function (i) {
                $("#sumVentas").val(data);
                //    });

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta", "No buscado", { timeOut: 1000, closeButton: true });
            }
        });
    });

});
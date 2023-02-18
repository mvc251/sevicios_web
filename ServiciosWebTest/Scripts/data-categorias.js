$(document).ready(function () {

    $("#btnGuardarCategoria").click(function () {

        var categoriaAdd = {};
        categoriaAdd.Nombre = $("#NombreCategoria").val();
        categoriaAdd.Descripcion = $("#DescripcionCategoria").val();
        categoriaAdd.Condicion = $("#CondicionCategoria").val();

        $.ajax({

            type: "POST",
            url: "api/Categoria/add",
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify(categoriaAdd),
            success: function (response) {
                toastr.success("Categoria Agregada", "Ejemplo", { timeOut: 3000, closeButton: true });
                console.log("Listo")
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus);
            }

        });
    });


    $("#btnActualizarCategoria").click(function () {
        var IdCategoria = $('#IdCategoria').val();
        var categoriaUpdate = {};
        categoriaUpdate.Nombre = $('#NombreCategoria').val(),
        categoriaUpdate.Descripcion = $('#DescripcionCategoria').val();
        categoriaUpdate.Id = $('#IdCategoria').val();

        $.ajax({
            type: "PUT",
            url: "api/Categorias/" + IdCategoria,
            dataType: 'Json',
            contentType: "application/json",
            data: JSON.stringify(categoriaUpdate),

            success: function (data) {
                toastr.success("Categoria Actualizado", "Ejemplo",
                    { timeOut: 3000, closeButton: true });
            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la informacion correcta",
                    "ejemplo", { timeOut: 3000, closeButton: true });
            }
        })
    });


    $("#btnEliminarCategoria").click(function () {
        var IdCategoria = $("#IdCategoria").val();
        $.ajax({
            type: "Delete",
            url: "/api/Categorias/" + IdCategoria,
            dataType: 'Json',
            success: function (data) {
                toastr.warning("Categoria Eliminada",
                    { timeOut: 3000, closeButton: true });
            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la informacion correcta",
                    "campaña dice", { timeOut: 3000, closeButton: true });

            }

        })
    });


});
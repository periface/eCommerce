$(document).ready(function () {
    $("body").on("click",".collapsible",function () {
        var this1 = $(this); // Obtiene el elemento
        var data = {
            id: $(this).attr('pid')
        };
        var isLoaded = $(this1).attr('data-loaded'); // Revisa si los valores ya fueron cargados
        if (isLoaded == "false") {
            $(this1).addClass("loadingP");   // Muestra el panel de carga
            $(this1).removeClass("collapse");
            // Ahora cargamos los datos
            $.ajax({
                url: "/Administrador/Categorias/CargaHijos",
                type: "GET",
                data: data,
                dataType: "json",
                success: function (d) {
                    $(this1).removeClass("loadingP");
                    if (d.length > 0) {

                        var $ul = $("<ul></ul>");
                        $.each(d, function (i, ele) {
                            $ul.append(
                                    $("<li><br/></li>").append(
                                        "<span class='collapse collapsible' data-loaded='false' pid='" + ele.idCategoria + "'>&nbsp;</span>" +
                                        "<span><a class='button btn btn-sm btn-success' data-id='" + ele.idCategoria + "' >" + ele.nombreCategoria + "</a>&nbsp;&nbsp; <a href='#' data-id='"+ele.idCategoria+"' class='eliminarCategoria'>Eliminar</a></span>"
                                    )
                                )
                        });
                        $(this1).parent().append($ul);
                        $(this1).addClass('collapse');
                        $(this1).toggleClass('collapse expand');
                        $(this1).closest('li').children('ul').slideDown();
                    }
                    else {
                        // no sub menu
                        $(this1).css({ 'display': 'inline-block', 'width': '15px' });
                    }
                    $(this1).attr('data-loaded', true);
                },
                error: function () {
                    alert("Error!");
                }
            });
        }
        else {
            // Si ya hay datos cargados
            $(this1).toggleClass("collapse expand");
            $(this1).closest('li').children('ul').slideToggle();
        }

    });
})
$(document).ready(function () {
    $("body").on("click", ".button", function () {
        $("#contenedorP").html("");
        $("#contenedorP").addClass("#tree .loadingP");
        var id = 0;
        id = $(this).data("id");
        $("#contenedorP").load("/Administrador/Categorias/EditarCategoria/" + id);
        $("#contenedorP").removeClass("#tree .loadingP");
    });
    //$("#agregarCat").click(function () {
    //    $("#contenedorP").load("/Administrador/Categorias/NuevaCategoria/");
    //});
    function cargaArbol() {

        $("#arbolCat").load("arbolCategorias");

    }
});

namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CajaComentarios",
                c => new
                    {
                        idComentario = c.Int(nullable: false, identity: true),
                        idCliente = c.Int(),
                        idProducto = c.Int(nullable: false),
                        calificacion = c.Int(nullable: false),
                        comentario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idComentario)
                .ForeignKey("dbo.Cliente", t => t.idCliente)
                .ForeignKey("dbo.Producto", t => t.idProducto, cascadeDelete: true)
                .Index(t => t.idCliente)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        idCliente = c.Int(nullable: false, identity: true),
                        nombreUsuario = c.String(nullable: false),
                        nombre = c.String(nullable: false),
                        apPaterno = c.String(nullable: false),
                        apMaterno = c.String(),
                        idCiudad = c.Int(nullable: false),
                        direccionEnvioLinea1 = c.String(nullable: false),
                        direccionEnvioLinea2 = c.String(),
                        direccionFacturacionLinea1 = c.String(),
                        direccionFacturacionLinea2 = c.String(),
                        rfc = c.String(),
                        telefono = c.String(),
                        cp = c.String(nullable: false),
                        facturacionCapturada = c.Boolean(nullable: false),
                        datosCapturados = c.Boolean(nullable: false),
                        edad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCliente);
            
            CreateTable(
                "dbo.Orden",
                c => new
                    {
                        idOrden = c.Int(nullable: false, identity: true),
                        idCliente = c.Int(),
                        ordenNombre = c.String(),
                        ordenApPaterno = c.String(),
                        ordenApMaterno = c.String(),
                        ordenDireccion = c.String(),
                        ordenPais = c.String(),
                        ordenEstado = c.String(),
                        ordenCiudad = c.String(),
                        ordenCodigoPostal = c.String(),
                        ordenTelefono = c.String(),
                        ordenMail = c.String(),
                        ordenTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ordenFecha = c.DateTime(nullable: false),
                        ordenRevisado = c.Boolean(nullable: false),
                        ordenFechaPago = c.DateTime(nullable: false),
                        ordenPagado = c.Boolean(nullable: false),
                        ordenCodigoRastreo = c.String(),
                        ordenCodigoCupon = c.String(),
                        ordenSubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ordenCodigoPayPal = c.String(),
                        ordenDescuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ordenNombreEnvio = c.String(),
                        ordenTipoPago = c.String(),
                        ordenEstadoPedido = c.String(),
                        ordenImagenTicket = c.String(),
                        ordenBase64Imagen = c.String(),
                        ordenComentarioEstado = c.String(),
                    })
                .PrimaryKey(t => t.idOrden)
                .ForeignKey("dbo.Cliente", t => t.idCliente)
                .Index(t => t.idCliente);
            
            CreateTable(
                "dbo.OrdenDetalle",
                c => new
                    {
                        idDetalle = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        idProducto = c.Int(),
                        nombreProducto = c.String(),
                        cantidad = c.Int(nullable: false),
                        precioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idDetalle)
                .ForeignKey("dbo.Orden", t => t.idOrden, cascadeDelete: true)
                .Index(t => t.idOrden);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        idProducto = c.Int(nullable: false, identity: true),
                        idCategoria = c.Int(),
                        nombreProducto = c.String(),
                        descripcionProducto = c.String(),
                        precioProducto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fechaCreacion = c.DateTime(nullable: false),
                        habilitado = c.Boolean(nullable: false),
                        imgProducto = c.String(),
                        descripcionCorta = c.String(),
                        tags = c.String(),
                        habilitarComentarios = c.Boolean(nullable: false),
                        fechaModificacion = c.DateTime(nullable: false),
                        slugs = c.String(),
                        stock = c.Int(nullable: false),
                        mostrarStock = c.Boolean(nullable: false),
                        mostrarSinStock = c.Boolean(nullable: false),
                        habilitarCompraSinStock = c.Boolean(nullable: false),
                        sku = c.String(),
                        precio_idPrecio = c.Int(),
                    })
                .PrimaryKey(t => t.idProducto)
                .ForeignKey("dbo.Categorias", t => t.idCategoria)
                .ForeignKey("dbo.Precios", t => t.precio_idPrecio)
                .Index(t => t.idCategoria)
                .Index(t => t.precio_idPrecio);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        nombreCategoria = c.String(nullable: false, maxLength: 200),
                        idPadre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.Diferenciadores",
                c => new
                    {
                        idDiferenciador = c.Int(nullable: false, identity: true),
                        mensajeDiferenciador = c.String(),
                        tipoDiferenciador = c.String(),
                        idProducto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idDiferenciador)
                .ForeignKey("dbo.Producto", t => t.idProducto, cascadeDelete: true)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Mutadores",
                c => new
                    {
                        idMut = c.Int(nullable: false, identity: true),
                        caract = c.String(),
                        valorAgregado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        defecto = c.Boolean(nullable: false),
                        porcentual = c.Boolean(nullable: false),
                        idDiferenciador = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idMut)
                .ForeignKey("dbo.Diferenciadores", t => t.idDiferenciador, cascadeDelete: true)
                .Index(t => t.idDiferenciador);
            
            CreateTable(
                "dbo.GaleriaProducto",
                c => new
                    {
                        idImagen = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(nullable: false),
                        fechaRegistro = c.DateTime(nullable: false),
                        imagen = c.String(),
                    })
                .PrimaryKey(t => t.idImagen)
                .ForeignKey("dbo.Producto", t => t.idProducto, cascadeDelete: true)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Precios",
                c => new
                    {
                        idPrecio = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(nullable: false),
                        precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descuentoActivo = c.Boolean(nullable: false),
                        fechaInicio = c.DateTime(),
                        fechaVencimiento = c.DateTime(),
                    })
                .PrimaryKey(t => t.idPrecio);
            
            CreateTable(
                "dbo.CarritoDeCompra",
                c => new
                    {
                        idRecord = c.Int(nullable: false, identity: true),
                        idCarro = c.String(maxLength: 50),
                        idProducto = c.Int(nullable: false),
                        contadorCarro = c.Int(nullable: false),
                        fechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idRecord)
                .ForeignKey("dbo.Producto", t => t.idProducto, cascadeDelete: true)
                .Index(t => t.idProducto);
            
            CreateTable(
                "dbo.Ciudad",
                c => new
                    {
                        idCiudad = c.Int(nullable: false, identity: true),
                        nombreCiudad = c.String(nullable: false),
                        idEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCiudad)
                .ForeignKey("dbo.Estados", t => t.idEstado, cascadeDelete: true)
                .Index(t => t.idEstado);
            
            CreateTable(
                "dbo.Estados",
                c => new
                    {
                        idEstado = c.Int(nullable: false, identity: true),
                        nombreEstado = c.String(nullable: false),
                        idPais = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idEstado)
                .ForeignKey("dbo.Pais", t => t.idPais, cascadeDelete: true)
                .Index(t => t.idPais);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        idPais = c.Int(nullable: false, identity: true),
                        nombrePais = c.String(),
                        langCode = c.String(),
                    })
                .PrimaryKey(t => t.idPais);
            
            CreateTable(
                "dbo.CostosEnvio",
                c => new
                    {
                        idEnvio = c.Int(nullable: false, identity: true),
                        nombreMetodoEnvio = c.String(nullable: false),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        detalle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idEnvio);
            
            CreateTable(
                "dbo.CuponDescuentoes",
                c => new
                    {
                        idCupon = c.Int(nullable: false, identity: true),
                        codigoCupon = c.String(maxLength: 50),
                        tipoDesc = c.String(),
                        idCliente = c.Int(),
                        descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        usado = c.Boolean(nullable: false),
                        cantidadesUso = c.Int(nullable: false),
                        fechaCaducidad = c.DateTime(nullable: false),
                        minimoRequerido = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.idCupon)
                .ForeignKey("dbo.Cliente", t => t.idCliente)
                .Index(t => t.idCliente);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        idEmpresa = c.Int(nullable: false, identity: true),
                        nombreEmpresa = c.String(nullable: false, maxLength: 100),
                        misionEmpresa = c.String(nullable: false, maxLength: 400),
                        visionEmpresa = c.String(nullable: false, maxLength: 400),
                        direccionEmpresa = c.String(nullable: false, maxLength: 200),
                        telefonoEmpresa = c.String(nullable: false),
                        correoOficialEmpresa = c.String(nullable: false),
                        correoVentasEmpresa = c.String(nullable: false),
                        correoAyudaEmpresa = c.String(nullable: false),
                        correoEnviosEmpresa = c.String(nullable: false),
                        facebookEmpresa = c.String(),
                        facebookId = c.String(),
                        twitterEmpresa = c.String(),
                        twitterId = c.String(),
                        infoActiva = c.Boolean(nullable: false),
                        slogan = c.String(),
                        imgPrincipalEmpresa = c.String(),
                        empLat = c.String(),
                        empLong = c.String(),
                    })
                .PrimaryKey(t => t.idEmpresa);
            
            CreateTable(
                "dbo.Politicas",
                c => new
                    {
                        idPolitica = c.Int(nullable: false, identity: true),
                        nombrePolitica = c.String(),
                        contenidoPolitica = c.String(),
                        slugPolitica = c.String(),
                        fechaPublicacion = c.DateTime(nullable: false),
                        fechaActualizacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idPolitica);
            
            CreateTable(
                "dbo.PreguntasFrecuentes",
                c => new
                    {
                        idPregunta = c.Int(nullable: false, identity: true),
                        pregunta = c.String(),
                        respuesta = c.String(),
                        fechaPublicacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idPregunta);
            
            CreateTable(
                "dbo.RegistroDeActividad",
                c => new
                    {
                        idRegistro = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(),
                        ip = c.String(),
                        usuario = c.String(),
                        fecha = c.DateTime(nullable: false),
                        nombreUsuario = c.String(),
                        edadUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idRegistro);
            
            CreateTable(
                "dbo.Seccion",
                c => new
                    {
                        idSeccion = c.Int(nullable: false, identity: true),
                        nombreSeccion = c.String(nullable: false, maxLength: 100),
                        estadoSeccion = c.Boolean(nullable: false),
                        imagenSeccion = c.String(),
                        contenidoSeccion = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.idSeccion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuponDescuentoes", "idCliente", "dbo.Cliente");
            DropForeignKey("dbo.Ciudad", "idEstado", "dbo.Estados");
            DropForeignKey("dbo.Estados", "idPais", "dbo.Pais");
            DropForeignKey("dbo.CarritoDeCompra", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "precio_idPrecio", "dbo.Precios");
            DropForeignKey("dbo.GaleriaProducto", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Diferenciadores", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Mutadores", "idDiferenciador", "dbo.Diferenciadores");
            DropForeignKey("dbo.CajaComentarios", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "idCategoria", "dbo.Categorias");
            DropForeignKey("dbo.CajaComentarios", "idCliente", "dbo.Cliente");
            DropForeignKey("dbo.OrdenDetalle", "idOrden", "dbo.Orden");
            DropForeignKey("dbo.Orden", "idCliente", "dbo.Cliente");
            DropIndex("dbo.CuponDescuentoes", new[] { "idCliente" });
            DropIndex("dbo.Estados", new[] { "idPais" });
            DropIndex("dbo.Ciudad", new[] { "idEstado" });
            DropIndex("dbo.CarritoDeCompra", new[] { "idProducto" });
            DropIndex("dbo.GaleriaProducto", new[] { "idProducto" });
            DropIndex("dbo.Mutadores", new[] { "idDiferenciador" });
            DropIndex("dbo.Diferenciadores", new[] { "idProducto" });
            DropIndex("dbo.Producto", new[] { "precio_idPrecio" });
            DropIndex("dbo.Producto", new[] { "idCategoria" });
            DropIndex("dbo.OrdenDetalle", new[] { "idOrden" });
            DropIndex("dbo.Orden", new[] { "idCliente" });
            DropIndex("dbo.CajaComentarios", new[] { "idProducto" });
            DropIndex("dbo.CajaComentarios", new[] { "idCliente" });
            DropTable("dbo.Seccion");
            DropTable("dbo.RegistroDeActividad");
            DropTable("dbo.PreguntasFrecuentes");
            DropTable("dbo.Politicas");
            DropTable("dbo.Empresa");
            DropTable("dbo.CuponDescuentoes");
            DropTable("dbo.CostosEnvio");
            DropTable("dbo.Pais");
            DropTable("dbo.Estados");
            DropTable("dbo.Ciudad");
            DropTable("dbo.CarritoDeCompra");
            DropTable("dbo.Precios");
            DropTable("dbo.GaleriaProducto");
            DropTable("dbo.Mutadores");
            DropTable("dbo.Diferenciadores");
            DropTable("dbo.Categorias");
            DropTable("dbo.Producto");
            DropTable("dbo.OrdenDetalle");
            DropTable("dbo.Orden");
            DropTable("dbo.Cliente");
            DropTable("dbo.CajaComentarios");
        }
    }
}

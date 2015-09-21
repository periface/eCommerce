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
                        comentario = c.String(),
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
                        nombreUsuario = c.String(),
                        nombre = c.String(),
                        apPaterno = c.String(),
                        apMaterno = c.String(),
                        idCiudad = c.Int(nullable: false),
                        direccionEnvioLinea1 = c.String(),
                        direccionEnvioLinea2 = c.String(),
                        direccionFacturacionLinea1 = c.String(),
                        direccionFacturacionLinea2 = c.String(),
                        rfc = c.String(),
                        telefono = c.String(),
                        cp = c.String(),
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
                        idCategoria = c.Int(nullable: false),
                        nombreProducto = c.String(),
                        descripcionProducto = c.String(),
                        precioProducto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fechaCreacion = c.DateTime(nullable: false),
                        habilitado = c.Boolean(nullable: false),
                        imgProducto = c.String(),
                        descripcionCorta = c.String(),
                        tags = c.String(),
                        habilitarComentarios = c.String(),
                        fechaModificacion = c.String(),
                        slugs = c.String(),
                    })
                .PrimaryKey(t => t.idProducto)
                .ForeignKey("dbo.Categorias", t => t.idCategoria, cascadeDelete: true)
                .Index(t => t.idCategoria);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        nombreCategoria = c.String(maxLength: 200),
                        idPadre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCategoria);
            
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
                        nombreCiudad = c.String(),
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
                        nombreEstado = c.String(),
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
                        nombreMetodoEnvio = c.String(),
                        costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        detalle = c.String(),
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
                        nombreEmpresa = c.String(maxLength: 100),
                        misionEmpresa = c.String(maxLength: 400),
                        visionEmpresa = c.String(maxLength: 400),
                        direccionEmpresa = c.String(maxLength: 200),
                        telefonoEmpresa = c.String(),
                        correoOficialEmpresa = c.String(),
                        correoVentasEmpresa = c.String(),
                        correoAyudaEmpresa = c.String(),
                        correoEnviosEmpresa = c.String(),
                        facebookEmpresa = c.String(),
                        facebookIdEmpresa = c.String(),
                        facebookSecretEmpresa = c.String(),
                        twitterEmpresa = c.String(),
                        twitterIdEmpresa = c.String(),
                        twitterSecretEmpresa = c.String(),
                        infoActiva = c.Boolean(nullable: false),
                        slogan = c.String(),
                    })
                .PrimaryKey(t => t.idEmpresa);
            
            CreateTable(
                "dbo.GaleriaProducto",
                c => new
                    {
                        idImagen = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(nullable: false),
                        fechaRegistro = c.DateTime(nullable: false),
                        imagen = c.String(),
                    })
                .PrimaryKey(t => t.idImagen);
            
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
                        nombreSeccion = c.String(maxLength: 100),
                        estadoSeccion = c.String(),
                        imagenSeccion = c.String(),
                        contenidoSeccion = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.idSeccion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuponDescuentoes", "idCliente", "dbo.Cliente");
            DropForeignKey("dbo.Ciudad", "idEstado", "dbo.Estados");
            DropForeignKey("dbo.Estados", "idPais", "dbo.Pais");
            DropForeignKey("dbo.CarritoDeCompra", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.CajaComentarios", "idProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "idCategoria", "dbo.Categorias");
            DropForeignKey("dbo.CajaComentarios", "idCliente", "dbo.Cliente");
            DropForeignKey("dbo.OrdenDetalle", "idOrden", "dbo.Orden");
            DropForeignKey("dbo.Orden", "idCliente", "dbo.Cliente");
            DropIndex("dbo.CuponDescuentoes", new[] { "idCliente" });
            DropIndex("dbo.Estados", new[] { "idPais" });
            DropIndex("dbo.Ciudad", new[] { "idEstado" });
            DropIndex("dbo.CarritoDeCompra", new[] { "idProducto" });
            DropIndex("dbo.Producto", new[] { "idCategoria" });
            DropIndex("dbo.OrdenDetalle", new[] { "idOrden" });
            DropIndex("dbo.Orden", new[] { "idCliente" });
            DropIndex("dbo.CajaComentarios", new[] { "idProducto" });
            DropIndex("dbo.CajaComentarios", new[] { "idCliente" });
            DropTable("dbo.Seccion");
            DropTable("dbo.RegistroDeActividad");
            DropTable("dbo.PreguntasFrecuentes");
            DropTable("dbo.Politicas");
            DropTable("dbo.GaleriaProducto");
            DropTable("dbo.Empresa");
            DropTable("dbo.CuponDescuentoes");
            DropTable("dbo.CostosEnvio");
            DropTable("dbo.Pais");
            DropTable("dbo.Estados");
            DropTable("dbo.Ciudad");
            DropTable("dbo.CarritoDeCompra");
            DropTable("dbo.Categorias");
            DropTable("dbo.Producto");
            DropTable("dbo.OrdenDetalle");
            DropTable("dbo.Orden");
            DropTable("dbo.Cliente");
            DropTable("dbo.CajaComentarios");
        }
    }
}

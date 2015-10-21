using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.Repos;
using PymeTamFinal.Controllers;
using Microsoft.AspNet.Identity;
using PymeTamFinal.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.MetodosPago.PayPal.Servicios;
using PymeTamFinal.Repositorios.Implementaciones;

namespace PymeTamFinal.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepositorioBase<Empresa>, RepositorioEmpresa>();
            container.RegisterType<IRepositorioBase<Seccion>,RepositorioSecciones>();
            container.RegisterType<IRepositorioBase<Categoria>, RepositorioCategorias>();
            container.RegisterType<IRepositorioBase<Producto>, RepositorioProductos>();
            container.RegisterType<IRepositorioBase<GaleriaProducto>, RepositorioGaleria>();
            container.RegisterType<IRepositorioBase<Precios>, RepositorioPrecios>();
            container.RegisterType<IRepositorioBase<CajaComentarios>, RepositorioComentarios>();
            container.RegisterType<IRepositorioBase<Cliente>, RepositorioClientes>();
            container.RegisterType<IRepositorioBase<Pais>, RepositorioPaises>();
            container.RegisterType<IRepositorioBase<Estados>, RepositorioEstados>();
            container.RegisterType<IRepositorioBase<Ciudad>, RepositorioCiudades>();
            container.RegisterType<IRepositorioBase<CostosEnvio>, RepositorioEnvios>();
            container.RegisterType<IRepositorioBase<CuponDescuento>, RepositorioCupones>();
            container.RegisterType<IRepositorioBase<Cliente>, RepositorioClientes>();
            container.RegisterType<IRepositorioBase<Banco>,RepositorioBancos>();
            container.RegisterType<IOrdenGeneradorBase<compraModel>, ManejadorDePedidos>();
            container.RegisterType<IPaypalCryptBase<PaypalConfig>, EncryptarPayPalECB>();
            container.RegisterType<IRepositorioBase<PaypalConfig>, RepositorioPayPalConfig>();
            container.RegisterType<IRepositorioBase<Orden>, RepositorioPedidos>();
            container.RegisterType<ITransaccionExterna<paypalPagoClienteModel>,PayPalImplementacionServidor>();
            container.RegisterType<ITransaccionExterna<stripeTarjetaModel>,MetodosPago.Stripe.Servicios.StripeImplementacion>();
            //Para user el accountController
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<EmailService>(new InjectionConstructor());
            //Para poder usar identity
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
        }
    }
}

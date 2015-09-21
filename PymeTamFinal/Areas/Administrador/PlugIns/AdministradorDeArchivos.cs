using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PymeTamFinal.Areas.Administrador.PlugIns
{
    public static class AdministradorDeArchivos
    {
        public static string guardarArchivo(HttpPostedFileBase archivo, string carpetaArchivos, string carpetaDelElemento)
        {
            var _carpeta = HttpContext.Current.Server.MapPath(carpetaArchivos + "/" + carpetaDelElemento + "/");
            var _carpetaSql = carpetaArchivos + carpetaDelElemento + "/" + archivo.FileName;
            var _carpetaCombinada = _carpeta + archivo.FileName;
            GenerarCarpetas(_carpeta);
            archivo.SaveAs(_carpetaCombinada);
            return _carpetaSql;
        }
        public static void eliminarArchivo(string ruta)
        {
            try
            {
                var servArchivo = HttpContext.Current.Server.MapPath(ruta);
                File.Delete(servArchivo);
            }
            catch (Exception)
            {

                return;
            }
        }
        public static void eliminarCarpeta(string carpeta)
        {
            try
            {
                var servCarpeta = HttpContext.Current.Server.MapPath(carpeta);
                Directory.Delete(servCarpeta);
            }
            catch (Exception)
            {
                return;
            }
        }
        private static void GenerarCarpetas(string carpeta)
        {
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
    }
}
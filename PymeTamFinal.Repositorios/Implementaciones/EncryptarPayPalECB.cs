using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.PayPalEncryptBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    public class EncryptarPayPalECB : PaypalEncryptBase<PaypalConfig>
    {
        public EncryptarPayPalECB(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override string Desencriptar(string valor, bool usarHash)
        {
            byte[] arregloLlave;
            byte[] aEncriptar = Convert.FromBase64String(valor);
            System.Configuration.AppSettingsReader rd = new System.Configuration.AppSettingsReader();
            string _llave = (string)rd.GetValue("llave", typeof(string));
            if (usarHash) {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                arregloLlave = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(_llave));
            }
            else
            {
                arregloLlave = Encoding.UTF8.GetBytes(_llave);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = arregloLlave;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultado = cTransform.TransformFinalBlock(aEncriptar,0,aEncriptar.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultado);

        }
        public override string Encriptar(string valor, bool usarHash)
        {
            byte[] llave;
            byte[] aEnctriptar = Encoding.UTF8.GetBytes(valor);
            System.Configuration.AppSettingsReader rd = new System.Configuration.AppSettingsReader();
            string _llave = (string)rd.GetValue("llave", typeof(string));
            if (usarHash)
            {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                llave = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(_llave));
                hashMD5.Clear();
            }
            else
            {
                llave = Encoding.UTF8.GetBytes(_llave);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = llave;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultados = cTransform.TransformFinalBlock(aEnctriptar, 0, aEnctriptar.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultados, 0, resultados.Length);
        }
        public override void Guardar(PaypalConfig model)
        {
            context.Paypal.Add(model);
            context.SaveChanges();
        }
    }
}

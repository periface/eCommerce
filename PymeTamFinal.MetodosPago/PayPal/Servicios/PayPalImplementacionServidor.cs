﻿using PayPal.Api;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.PayPal.Servicios
{
    public class PayPalImplementacionServidor:ITransaccionExternaPayPalBase<paypalPagoClienteModel>
    {
        string secret;
        string api;
        public PayPalImplementacionServidor(string api,string secret)
        {
            this.api = api;
            this.secret = secret;
        }
        public override bool ComprobarConexion(string apiKey, string apiSecret, out string error)
        {

            error = string.Empty;
            try
            {
                var opciones = new Dictionary<string, string>();
                opciones.Add("mode", "sandbox");
                string tokenAcceso = new OAuthTokenCredential(apiKey, apiSecret, opciones).GetAccessToken();
                if (string.IsNullOrEmpty(tokenAcceso))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public override object GenerarContexto()
        {
            var opciones = new Dictionary<string, string>();
            opciones.Add("mode", "sandbox");
            string tokenAcceso = new OAuthTokenCredential(api, secret, opciones).GetAccessToken();
            APIContext apiContext = new APIContext(tokenAcceso);
            return apiContext;
        }
        public override string GenerarToken(paypalPagoClienteModel modeloRequerido)
        {
            var apiContext = GenerarContexto();
            return "";
        }
    }
}

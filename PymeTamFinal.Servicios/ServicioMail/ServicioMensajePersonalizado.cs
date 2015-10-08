using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;

namespace PymeTamFinal.Servicios.ServicioMail
{
    public class ServicioMensajePersonalizado : IIdentityMessageService
    {
        SmtpClient cliente;
        public virtual async Task SendAsync(IdentityMessage message)
        {
            
        }
    }
}

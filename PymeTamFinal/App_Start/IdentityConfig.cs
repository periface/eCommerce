﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PymeTamFinal.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;

namespace PymeTamFinal
{
    public class EmailService : IIdentityMessageService
    {
        
        public async Task SendAsync(IdentityMessage message)
        {

            try
            {
                using (var cliente = new SmtpClient())
                {
                    var webMensaje = new MailMessage();
                    var credencial = new NetworkCredential()
                    {
                        UserName = WebConfigurationManager.AppSettings["usuario"],
                        Password = WebConfigurationManager.AppSettings["contrasena"],
                        Domain = WebConfigurationManager.AppSettings["dominio"] == null ? null : WebConfigurationManager.AppSettings["dominio"],
                    };
                    cliente.Credentials = credencial;
                    cliente.Host = "smtp-mail.outlook.com";
                    cliente.Port = 587;
                    cliente.EnableSsl = true;
                    try
                    {
                        await cliente.SendMailAsync(webMensaje);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            // Plug in your email service here to send an email.
            //message.Destination = "aats91@outlook.com";
            //message.Body = "Mensaje de prueba";
            //message.Subject = "Mensaje de prueba";
            //var _message = new MailMessage();
            //_message.Sender = new MailAddress("temp_peri@hotmail.com");
            //_message.From = new MailAddress("temp_peri@hotmail.com");
            //_message.To.Add(new MailAddress(message.Destination));
            //_message.Body = message.Body;
            //using (var smtp = new SmtpClient())
            //{
            //    var credential = new NetworkCredential
            //    {
            //        UserName = "temp_peri@hotmail.com",  // replace with valid value
            //        Password = "TorresSanchez"  // replace with valid value
            //    };
            //    smtp.Credentials = credential;
            //    smtp.Host = "smtp-mail.outlook.com";
            //    smtp.Port = 587;
            //    smtp.EnableSsl = true;
            //    await smtp.SendMailAsync(_message);
            //}
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}

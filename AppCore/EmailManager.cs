using Entities_DTOs;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AppCore
{
    public class EmailManager
    {
        private readonly IConfiguration _config;

        public EmailManager(IConfiguration config)
        {
            _config = config;
        }

        private void SendEmail(string to, string subject, string body)
        {
            try
            {
                var server = _config["SmtpSettings:Server"];
                var port = int.Parse(_config["SmtpSettings:Port"]);
                var user = _config["SmtpSettings:User"];
                var password = _config["SmtpSettings:Password"];
                var enableSsl = bool.Parse(_config["SmtpSettings:EnableSsl"]);

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(user);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;

                    using (var smtp = new SmtpClient(server, port))
                    {
                        smtp.Credentials = new NetworkCredential(user, password);
                        smtp.EnableSsl = enableSsl;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error enviando correo: " + ex.Message);
            }
        }

        // Bienvenida al registrarse
        public void SendWelcomeEmail(Usuario u)
        {
            string subject = "Bienvenido al sistema";
            string body = $"Hola {u.Name},\n\n¡Bienvenid@! Gracias por registrarte en nuestra Biblioteca.";
            SendEmail(u.Email, subject, body);
        }

        // Confirmación de préstamo
        public void SendPrestamoConfirmacion(Prestamo p, Usuario u)
        {
            string subject = "Confirmación de préstamo";
            string body = $"Hola {u.Name},\n\nTu préstamo del libro con ISBN {p.Isbn} ha sido registrado.\n" +
                          $"Fecha límite de devolución: {p.FechaLimite:dd/MM/yyyy}.\n\n¡Disfruta tu lectura!";
            SendEmail(u.Email, subject, body);
        }

        // Recordatorio de devolución
        public void SendRecordatorioDevolucion(Prestamo p, Usuario u)
        {
            string subject = "Recordatorio de devolución";
            string body = $"Hola {u.Name},\n\nRecuerda devolver el libro con ISBN {p.Isbn} antes del {p.FechaLimite:dd/MM/yyyy}.\n\nGracias por usar nuestra biblioteca.";
            SendEmail(u.Email, subject, body);
        }

        // Confirmación de devolución
        public void SendConfirmacionDevolucion(Prestamo p, Usuario u)
        {
            string subject = "Confirmación de devolución";
            string body = $"Hola {u.Name},\n\nHas devuelto el libro con ISBN {p.Isbn} correctamente.\n\nGracias por tu responsabilidad.";
            SendEmail(u.Email, subject, body);
        }
    }
}

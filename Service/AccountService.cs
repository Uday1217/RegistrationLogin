using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Org.BouncyCastle.Crypto.Generators;
using RegistrationLogin.Mvc.Models;
using System.Net.Mail;
using System.Net;
using RegistrationLogin.Mvc.Configuration;
using Microsoft.Extensions.Options;

namespace RegistrationLogin.Mvc.Service
{
    public class AccountService : IAccountService
    {
        private readonly RegContext _context;
        private readonly SmtpSettings _smtpSettings;

        public AccountService(RegContext context, IOptions<SmtpSettings> smtpSettings)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;
        }



        public async Task<Login> LogIn(string email, string password)
        {
            var data = await _context.Registers.Where(x => x.Email == email || x.Password == password).FirstOrDefaultAsync();

            if (data != null && BCrypt.Net.BCrypt.EnhancedVerify(password, data.Password))
            {
                var loginData = new Login
                {
                    Email = data.Email,
                    Password = data.Password
                };
                return loginData;

            }
            else
            {

                return null;
            }
        }


        public async Task<bool> Registration(Register register)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(register.Password);

                register.Password = register.Confirm_Password = hashedPassword;


                var data = _context.Registers.Add(register);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Not Registored");
                return false;
            }
        }

        public async Task<bool> SendRegistrationConfirmationEmail(string userEmail)
        {
            var result = false;

            using (SmtpClient smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.SenderEmail),
                    Subject = "Registration Confirmation",
                    Body = "Thank you for registering!",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(userEmail);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    result = true;
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it)
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }

                return result;
            }
        }
    }
}

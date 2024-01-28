using RegistrationLogin.Mvc.Models;


namespace RegistrationLogin.Mvc.Service
{
    public interface IAccountService
    {
        Task<bool> Registration(Register register);

        Task<Login> LogIn(string email, string password);

        Task<bool> SendRegistrationConfirmationEmail(string userEmail);
    }
}

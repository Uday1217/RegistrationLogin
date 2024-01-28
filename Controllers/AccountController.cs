using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RegistrationLogin.Mvc.Models;
using RegistrationLogin.Mvc.Service;

namespace RegistrationLogin.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _account;
        private readonly RegContext _context;

        public AccountController(IAccountService account, RegContext context)
        {
            _account = account;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Register register)
        {
            if (register.Password != register.Confirm_Password)
            {
                ViewBag.ErrorMessage = "Password Mismatch";
                return View();
            }

            var isRegistered = await _account.Registration(register);
            if (isRegistered)
            {
                ViewBag.SuccessMessage = "Registration Successful";
                // Send registration confirmation email
                await _account.SendRegistrationConfirmationEmail(register?.Email);
                return RedirectToAction("Registration", "Account");
            }
            else
            {

                ViewBag.ErrorMessage = "Registration Failed";
                return View();
            }
        }



        public IActionResult Login(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            try
            {

                var data = await _account.LogIn(email, password);

                if (data == null)
                {

                    ViewBag.ErrorMessage = "Incorrect email or password";
                    return View("Login");
                }

                ViewBag.Message = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }
        }


    }

}


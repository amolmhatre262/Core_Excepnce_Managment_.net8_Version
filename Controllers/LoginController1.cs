using Expence_Managment_Core_WebApplication.Models;
using Expence_Managment_Core_WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expence_Managment_Core_WebApplication.Controllers
{
    public class LoginController1 : Controller
    {

        private readonly UserServices _authService; // Inject your service here

        public LoginController1(UserServices authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users model)
        {
            if (ModelState.IsValid)
                return View("Index", model);

            var result = await _authService.AuthenticateUser(model.UserName, model.PasswordHash);

            if (result != null)
            {
                // Login successful
                // You can set session, redirect to dashboard, etc.

                HttpContext.Session.SetString("UserName", model.UserName);
                HttpContext.Session.SetInt32("UserId", model.UserID);

                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LoginController1");
        }
    }
}


    


using Expence_Managment_Core_WebApplication.Models;
using Expence_Managment_Core_WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expence_Managment_Core_WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices _userService;

        public UserController(UserServices userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index", "LoginController1");
            }

            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUsersAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View("Details", user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(Users user)
        {
            if (ModelState.IsValid)
            {
                bool updated = await _userService.UpdateUserAsync(user.UserID, user);
                if (updated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update user. Please try again.");
                    return View("Details", user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user data. Please check your input.");
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Users user)
        {
            if (!ModelState.IsValid)
            {
                bool added = await _userService.AddUser(user);
                if (added)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add user. Please try again.");
                    return View("Details", user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user data. Please check your input.");
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUsersAsync(id);
            if (user != null)
            {
                bool deleted = await _userService.DeleteUser(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        public IActionResult UserAuthenticate(string username, string Password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("", "Username and Password cannot be empty.");
                return View("Index");
            }
            var user = _userService.AuthenticateUser(username, Password).Result;
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View("Index");
            }
        }


    }
}

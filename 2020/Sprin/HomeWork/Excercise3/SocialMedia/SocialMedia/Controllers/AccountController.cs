using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.ViewModels;


namespace SocialMedia.Controllers
{
    public class AccountController : Controller
    {
        private UsersContext db;
        public AccountController(UsersContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    Authenticate(model.Email, model.Password, user.Id); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                    User usr = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                    Authenticate(model.Email, model.Password, usr.Id); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private void Authenticate(string userName, string password, int id)
        {
            var authToken = GetHash(userName + password);
            Response.Cookies.Append("token", authToken.ToString());
            Response.Cookies.Append("id", id);
        }

        public double GetHash(string inputString)
        {
            var fibonach1 = 1;
            var fibonach2 = 1;
            var resultHash = 0d; 

            foreach(var ch in inputString)
            {
                var fibonach3 = fibonach2 + fibonach1;
                
                var hashPart = ch * fibonach3 % 11 + 0.2512846;
                resultHash += hashPart;

                fibonach1 = fibonach2;
                fibonach2 = fibonach3;
            }

            return resultHash;
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("Login", "Account");
        }
    }
}

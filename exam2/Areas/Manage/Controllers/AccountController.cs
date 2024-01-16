using exam2.Areas.Manage.ViewModels;
using exam2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam2.Areas.Manage.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            { 
                Email = registerVM.Email,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.UserName,
            };

            IdentityResult result =await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", "Duzgun Daxil edin");
                }
                return View();
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index");


        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var exist= await _userManager.FindByNameAsync(loginVM.UserName);
            if (exist == null)
            {
                ModelState.AddModelError("", "Username duzgun daxil edin");
                return View();
            }

            var signInChech= _signInManager.CheckPasswordSignInAsync(exist, loginVM.Password,true);
            //if (!signInChech.Suc)
            //{

            //}

            //await _signInManager.SignInAsync(exist,true,)


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

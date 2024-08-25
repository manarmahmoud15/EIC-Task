using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (RegisterUserViewModel UserVM)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = UserVM.UserName,
                    PasswordHash = UserVM.Password

                };
                IdentityResult result =  await userManager.CreateAsync(user , UserVM.Password);
                if(result.Succeeded)
                {
                   await signInManager.SignInAsync(user, false); // session cookie
                    return RedirectToAction("AddNewCustomer", "customer");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", UserVM);
        }

        [HttpGet]
        public IActionResult Login ()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Login (LoginViewModel userVM)
        {
            if(ModelState.IsValid == true)
            {
               ApplicationUser userDB =
                    await userManager.FindByNameAsync(userVM.UserName);
                if (userDB != null)
                {
                    bool FoundPassword = 
                        await userManager.CheckPasswordAsync(userDB, userVM.Password);

                    if(FoundPassword)
                    {
                       await signInManager.SignInAsync(userDB, userVM.RememberMe);
                        return RedirectToAction("AddNewCustomer", "Customer");
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return View("Login" , userVM);
        }

        public async Task<IActionResult> SignOut ()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

       
    }
}

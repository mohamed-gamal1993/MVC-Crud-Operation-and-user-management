using DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Helper;
using PL.Models;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            } return View(model);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var password = await userManager.CheckPasswordAsync(user, model.Password);
                    if (password)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RemmberMe, false);
                        if (result.Succeeded) return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Sign Out
        public async new Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var resetpassword = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Title = "Reset Password",
                        To = model.Email,
                        Body = resetpassword
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError(string.Empty, "Email is not found");
            }
            return View(model);
        }

        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetpasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                         return RedirectToAction("ResetPasswordDone");
                    foreach (var error in result.Errors)
                         ModelState.AddModelError(string.Empty, error.Description);
                         return View(model);
                }
                
                    ModelState.AddModelError(string.Empty, "Email is not found");
           }
            
                return View(model);
       }

        public IActionResult ResetPasswordDone()
        {
            return View();
        }
        #endregion
    }
}

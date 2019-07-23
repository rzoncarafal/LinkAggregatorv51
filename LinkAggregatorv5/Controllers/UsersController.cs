using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LinkAggregatorv5.Models;
using LinkAggregatorv5.Models.UserViewModels;
using LinkAggregatorv5.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkAggregatorv5.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly LinkAggregatorv5Context _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public UsersController(LinkAggregatorv5Context context,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Where(u => u.Email.Equals(model.Email)).FirstOrDefault() != null)
                {
                    ModelState.AddModelError(string.Empty, "There is user with the given email.");
                    return View(model);
                };

                    var user = new User()
                {
                    Password = model.Password,
                    AddDate = DateTime.Today,
                    UpdateDate = DateTime.Today,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    UserName = model.Email
                };
                var hasher = new PasswordHasher<User>();
                user.PasswordHash = hasher.HashPassword(user, model.Password);

                _context.User.Add(user);
                _context.SaveChanges();

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Users/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme).Replace("Register", "ConfirmEmail");

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Thank you for register on our page. Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return View("ActivationLinkSent");
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Links");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            if (user.EmailConfirmed == false)
            {
                user.EmailConfirmed = true;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return View("ConfirmEmail", "Users");
            }
            else
            {
                return View("ConfirmEmailDoneBefore", "Users");
            }
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == null)
                {
                    return RedirectToAction("Index", "Links");
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "There is no user with the given email.");
                    return View(model);
                }

                await _emailSender.SendEmailAsync(user.Email, "Your password",
                    $"Your password is: {user.Password}");

                return View("ForgotPasswordConfirmation", "Users");
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            var existsUser = _context.User.Where(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password)).SingleOrDefault();
            if (existsUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. There is no user with the given email or password.");
                return View(model);
            }

            if (!existsUser.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Email is not confirmed!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You logged correct, but method 'PasswordSignInAsync' is not working :((((( and there is no other way to log in :(");
                return View(model);
            }
        }



        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(LinksController.Index), "Links");
            }
        }

    }
}
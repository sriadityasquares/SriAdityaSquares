﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LoginApp.Models;
using DataLayer;
using log4net;
using BusinessLayer;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSharp;
using ModelLayer;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LoginApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        CommonBL common = new CommonBL();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static readonly ILog log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            var _user = UserManager.FindByEmail(model.Email);
                            //common.UpdateAgentLocation(model.Latitude, model.Longitude, model.Email);
                            return RedirectToLocal(_user.Id, returnUrl);
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);

                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return View(model);
            }

        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }



        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AllowAnonymous]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email,PhoneNumber = model.PhoneNumber };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var roleadd = await UserManager.AddToRoleAsync(user.Id, model.Role);
                        var message = "Welcome to Sri Aditya Squares" + Environment.NewLine + "Below are your login credetials:" + Environment.NewLine + "Username :" + model.Email + Environment.NewLine + "Password :" + model.Password + Environment.NewLine + "Please use the link to login to the application :" + "https://sasinfra.in";
                        //var message = "Hi";
                        var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + model.PhoneNumber + "&smsContentType=english");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("Cache-Control", "no-cache");
                        IRestResponse response = client.Execute(request);

                        SMS sms = new SMS();
                        sms.MessageType = "Account Creation";
                        sms.Message = message;
                        sms.Recipients = model.PhoneNumber;
                        sms.CreatedBy = User.Identity.Name;
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                        sms.CreatedDate = indianTime;
                        CommonBL common = new CommonBL();
                        common.LogSMS(sms);
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        ModelState.Clear();
                        //return RedirectToAction("Success", "Account");
                        TempData["registermessage"] = "Registration Successful";
                    }
                    else
                    {
                        TempData["registermessage"] = "Registration Failed";
                    }
                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    var password = CreatePassword(6);
                    UserManager<IdentityUser> userManager =
    new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, password);
                    var message = "Welcome to Sri Aditya Squares \n Here is you new password :" + password + "\n Please Reset once logged in. :)";
                    var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + user.PhoneNumber + "&smsContentType=english");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Cache-Control", "no-cache");
                    IRestResponse response = client.Execute(request);
                    SMS sms = new SMS();
                    sms.MessageType = "Password Reset";
                    sms.Message = message;
                    sms.Recipients = user.PhoneNumber;
                    sms.CreatedBy = User.Identity.Name;
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                    sms.CreatedDate = indianTime;
                    CommonBL common = new CommonBL();
                    common.LogSMS(sms);
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
       

       
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        
       
        public ActionResult Success()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        public ActionResult MyProfile()
        {
            string role = "";
            if (User.IsInRole("Customer"))
                role = "Customer";
            if (User.IsInRole("Agent"))
                role = "Agent";
            CommonBL c = new CommonBL();
            var profile = c.GetMyProfile(User.Identity.Name, role);
            return View(profile);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string UserID, string returnUrl)
        {
            if (UserManager.IsInRole(UserID, "Admin"))
                return RedirectToAction("Home", "Dashboard");
            else if (UserManager.IsInRole(UserID, "DataEntry"))
                return RedirectToAction("Home", "Dashboard");
            else if (UserManager.IsInRole(UserID, "Agent"))
                return RedirectToAction("Home", "Dashboard");
            else if (UserManager.IsInRole(UserID, "Customer"))
                return RedirectToAction("FlatLifeCycle", "Dashboard");
            else if (UserManager.IsInRole(UserID, "Client"))
                return RedirectToAction("Index", "Dashboard");
            else if (UserManager.IsInRole(UserID, "Manager"))
                return RedirectToAction("Home", "Dashboard");
            else if (UserManager.IsInRole(UserID, "Employee"))
                return RedirectToAction("Home", "Dashboard");
            return null;
        }

        

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
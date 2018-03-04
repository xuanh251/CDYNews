using BotDetect.Web.Mvc;
using CDYNews.Common;
using CDYNews.Data;
using CDYNews.Model.Models;
using CDYNews.Web.App_Start;
using CDYNews.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CDYNews.Service;

namespace CDYNews.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;

        public AccountController(ApplicationUserManager userManager,
                                 ApplicationSignInManager signInManager,
                                 IApplicationGroupService appGroupService,
                                 IApplicationRoleService appRoleService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
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
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng!")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    ViewData["ErrorMsg"] = "Có lỗi xảy ra!";
                    return View(model);
                }
                var userByUsername = await _userManager.FindByNameAsync(model.UserName);
                if (userByUsername != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    ViewData["ErrorMsg"] = "Có lỗi xảy ra!";
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Birthday = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address

                };
                await _userManager.CreateAsync(user, model.Password);

                var groupId=_appGroupService.AddUserToGroup(user.Id);
                var listRole = _appRoleService.GetListRoleByGroupId(groupId);
                _appRoleService.Save();
                foreach (var role in listRole)
                {
                    await _userManager.RemoveFromRoleAsync(user.Id, role.Name);
                    await _userManager.AddToRoleAsync(user.Id, role.Name);
                }

                ViewData["SuccessMsg"] = "Đăng ký thành công!";
                MailHelper.SendMail(model.Email, "Đăng ký tài khoản thành công!", "Chúc mừng bạn đã trở thành thành viên chính thức của website chúng tôi!");
            }
            else
            {
                ViewData["ErrorMsg"] = "Có lỗi xảy ra!";
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return Redirect("/");
        }
    }
}
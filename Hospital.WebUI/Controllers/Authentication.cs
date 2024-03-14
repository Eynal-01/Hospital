using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.WebUI.Controllers
{
    public class Authentication : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly CustomIdentityDbContext _customIdentityDbContext;

        public Authentication(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager, CustomIdentityDbContext customIdentityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _customIdentityDbContext = customIdentityDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult Selected(string selected)
        {
            selected = selected.Trim();
            selected = selected.ToLower();

            return RedirectToAction("Login", "Authentication", new { selected });
        }

        [HttpGet]
        public IActionResult Login(string selected)
        {
            var viewModel = new LoginViewModel();
            viewModel.Selected = selected;
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            //var result = loginViewModel.UserNameOrEmail.EndsWith("@gmail.com");
            //var d=
            //var result = loginViewModel.Email != null ? loginViewModel.Email : loginViewModel.UserName;
            if (ModelState.IsValid)
            {
                var signIn = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, true, false);

                if (signIn.Succeeded)
                {
                    dynamic user;
                    if (loginViewModel.Selected == "patient")
                    {
                        user = _customIdentityDbContext.Patients.SingleOrDefault(i => i.UserName == loginViewModel.UserName);
                    }
                    else if (loginViewModel.Selected == "doctor")
                    {
                        user = _customIdentityDbContext.Doctors.SingleOrDefault(i => i.UserName == loginViewModel.UserName);
                    }
                    else
                    {
                        user = _customIdentityDbContext.Admins.SingleOrDefault(i => i.UserName == loginViewModel.UserName);
                    }

                    if (user != null)
                    {
                        _customIdentityDbContext.Update(user);
                        await _customIdentityDbContext.SaveChangesAsync();
                    }
                    if (loginViewModel.Selected == "patient")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (loginViewModel.Selected == "doctor")
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register(string selected)
        {
            var viewModel = new RegisterViewModel();
            viewModel.Selected = selected;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid && registerViewModel.ConfirmPassword == registerViewModel.Password)
            {
                dynamic signInUser = null;
                if (registerViewModel.Selected == "patient")
                {
                    signInUser = new Patient
                    {
                        Email = registerViewModel.Email,
                        UserName = registerViewModel.UserName,
                        PhoneNumber = registerViewModel.MobileNumber.ToString(),
                    };
                }
                else if (registerViewModel.Selected == "doctor")
                {
                    signInUser = new Doctor
                    {
                        Email = registerViewModel.Email,
                        UserName = registerViewModel.UserName,
                        PhoneNumber = registerViewModel.MobileNumber.ToString(),
                    };
                }
                else
                {
                    signInUser = new Admin
                    {
                        Email = registerViewModel.Email,
                        UserName = registerViewModel.UserName,
                        PhoneNumber = registerViewModel.MobileNumber.ToString(),
                    };
                }

                var v = new CustomIdentityUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.UserName,
                    PhoneNumber = registerViewModel.MobileNumber.ToString(),
                };

                if (signInUser != null)
                {
                    var result = await _userManager.CreateAsync(v, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        if (registerViewModel.Selected == "patient")
                        {
                            await _customIdentityDbContext.Patients.AddAsync(signInUser);
                        }
                        else if (registerViewModel.Selected == "doctor")
                        {
                            await _customIdentityDbContext.Doctors.AddAsync(signInUser);
                        }
                        else if (registerViewModel.Selected == "admin")
                        {
                            await _customIdentityDbContext.Admins.AddAsync(signInUser);
                        }
                        if (!await _roleManager.RoleExistsAsync(registerViewModel.Selected))
                        {
                            var role = new CustomIdentityRole
                            {
                                Name = registerViewModel.Selected,
                            };
                            var resul = await _roleManager.CreateAsync(role);
                            if (!resul.Succeeded)
                            {
                                ModelState.AddModelError("", "Error");
                                return View(registerViewModel);
                            }
                        }

                        await _userManager.AddToRoleAsync(v, registerViewModel.Selected);
                        return RedirectToAction("Login", "Authentication", new { registerViewModel.Selected });
                    }
                }
            }
            return View(registerViewModel);
        }
    }
}
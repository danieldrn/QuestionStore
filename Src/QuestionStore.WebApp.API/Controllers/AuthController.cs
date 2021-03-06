﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionStore.WebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> CreatUser(RegisterUserModel registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.PassWord);

            return CustomResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginUserModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.PassWord, false, true);

            if (result.Succeeded)
            {
               // _logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                return CustomResponse(login);
            }


            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return BadRequest();
        }
    }

    public class RegisterUserModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string PassWord { get; set; }

        [Compare("PassWord", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassWord { get; set; }
    }

    public class LoginUserModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string PassWord { get; set; }
    }
}

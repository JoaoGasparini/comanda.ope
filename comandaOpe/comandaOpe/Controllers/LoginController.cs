using comandaOpe.Models;
using comandaOpe.Models.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace comandaOpe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAutenticacao _autenticacao;
        public LoginController(IAutenticacao iAutenticacao)
        {
            _autenticacao = iAutenticacao;
        }
        
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([Bind] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string RegistroStatus = _autenticacao.RegistrarUsuario(usuario);
                if (RegistroStatus == "Registrado com sucesso")
                {
                    ModelState.Clear();
                    TempData["Sucesso"] = "Registro realizado com sucesso!";
                    return View();
                }
                else
                {
                    if(RegistroStatus.Contains("duplicate key value violates unique"))
                    {
                        TempData["Falhou"] = "O usuario ja foi cadastrado.";
                        return View();
                    }
                    TempData["Falhou"] = "O Registro do usuário falhou.";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario([Bind] Usuario usuario)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Email");
            ModelState.Remove("Cargo");

            if (ModelState.IsValid)
            {
                bool LoginStatus = _autenticacao.ValidarLogin(usuario);

                if (LoginStatus == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Login)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    if (User.Identity.IsAuthenticated)
                        return RedirectToAction("Painel", "Usuario");
                    else
                    {
                        TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas " + User.Identity.Name;
                        return RedirectToAction("LoginUsuario", "Login");
                    }
                }
                else
                {
                    TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}

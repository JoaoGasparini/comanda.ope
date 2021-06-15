using comandaOpe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using comandaOpe.Data;
using comandaOpe.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace comandaOpe.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public IActionResult Painel()
        {
            return View("PainelAdm", "Usuario");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoginUsuario", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

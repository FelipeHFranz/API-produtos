using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Produtos.API.Interfaces;
using Produtos.API.Models;
using Produtos.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly ILoginRepositorio _loginRepositorio;

        public LoginController(ILoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        [HttpPost()]
        public async Task<ActionResult> Login(Login login)
        {
            var usuario = await _loginRepositorio.login(login.Email,login.Senha);

            if (usuario == null || usuario.Senha != login.Senha)
            {
                return BadRequest("Email ou senha Incorreto");
            }
            var token = TokenService.GenerateToken(usuario);
            usuario.Senha = "";
            return Ok(JsonConvert.SerializeObject(new { token = token}));
        }
    }
}

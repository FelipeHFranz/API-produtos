using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Produtos.API.Interfaces;
using Produtos.API.Models;
using Produtos.API.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Controllers
{

    [ApiController]
    [Route("usuario")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioController;

        public UsuarioController(IUsuarioRepositorio usuarioController)
        {
            _usuarioController = usuarioController;
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(Usuario usuario)
        {
            var user = await _usuarioController.SelecionarByEmail(usuario.Email);
            if (user != null)
            {
                return BadRequest("Email já cadastrado");
            }
            _usuarioController.Incluir(usuario);
            if (await _usuarioController.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("Ocorreu um erro");
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarUsuario(Usuario usuario)
        {

            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var id = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);

            var user = await _usuarioController.SelecionarByID(id);

            if(user.Nome == usuario.Nome&& user.Email == usuario.Email)
            {
                return Ok();
            }

            user.Nome = usuario.Nome;
            user.Email = usuario.Email;
            if (await _usuarioController.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest(usuario);
        }


        [HttpPut("alterarsenha")]
        [Authorize]
        public async Task<ActionResult> AlteraSenha(Senha senha)
        {
           
            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var id = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);

            var user = await _usuarioController.SelecionarByID(id);
            if (user.Senha == senha.SenhaAtual)
            {
                user.Senha = senha.SenhaNova;

                if (await _usuarioController.SaveAllAsync())
                {
                    return Ok();
                }
                return BadRequest("Ocorreu um erro");
            }
            return BadRequest("Senha atual incoreta");

        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            var usuario = await _usuarioController.SelecionarByID(id);

            if (usuario == null)
            {
                return NotFound("Usuario não encontrado");
            }

            _usuarioController.Excluir(usuario);
            if (await _usuarioController.SaveAllAsync())
            {
                return Ok("Sucesso");
            }
            return BadRequest("Ocorreu um erro");
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> SelecionarUsuario()
        {
            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var id = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);

            var usuario = await _usuarioController.SelecionarByID(id);
            usuario.Senha = "0";
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado");
            }

            return Ok(usuario);
        }


        
    }
}

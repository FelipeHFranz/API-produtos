using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("produto")]
    public class ProdutoController : Controller
    {

        private readonly IProdutoRepositorio _produtoRepository;
        public ProdutoController(IProdutoRepositorio produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

      

        [HttpPost]
        [Authorize]
        public async Task<ActionResult>CadastrarProduto(Produto produto)
        {

            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                     
           
            
            produto.IdUser = int.Parse( token.Claims.First(c => c.Type == "nameid").Value);
            _produtoRepository.Incluir(produto);

            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("Ocorreu um erro");
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarProduto(Produto produto)
        {
            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var id = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);

            var produtocad = await _produtoRepository.SelecionarByID(produto.Id);
            
            if (produtocad == null)
            {
                return NotFound("Produto não encontrado");
            }
            if (produtocad.Descricao
                == produto.Descricao && produtocad.Valor == produto.Valor)
            {
                return Ok();
            }
            if (produtocad.IdUser == id)
            {
                produtocad.Descricao = produto.Descricao;
                produtocad.Valor = produto.Valor;
                if (await _produtoRepository.SaveAllAsync())
                {
                    return Ok();
                }
                return BadRequest("Ocorreu um erro");
            }
            return BadRequest("Sem autorização");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletarProduto(int id)
        {

            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var idusuario = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);

            var produtocad = await _produtoRepository.SelecionarByID(id);

            if (produtocad == null)
            {
                return NotFound("Produto não encontrado");
            }
            if (produtocad.IdUser == idusuario)
            {
                _produtoRepository.Excluir(produtocad);
                if (await _produtoRepository.SaveAllAsync())
                {
                    return Ok();
                }
                return BadRequest("Ocorreu um erro");
            }
            return BadRequest("Sem autorização");


          
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> SelecionarProduto(int id)
        {

            var produto = await _produtoRepository.SelecionarByID(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAllProdutos()
        {
            //pega o token e decodifica para obter o id do usuario
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            var jwtEncodedString = headerValue.ToString().Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);



           var id = int.Parse(token.Claims.First(c => c.Type == "nameid").Value);
            var produtos = await _produtoRepository.SelecionarTodos(id);
            if (produtos == null)
            {
                return NotFound("Produtos não encontrado");
            }
            return Ok(produtos);
        }
    }
}

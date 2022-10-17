using Produtos.API.Interfaces;
using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Produtos.API.Repositories
{
    public class ProdutoRepository : IProdutoRepositorio
    {
        private readonly ProdutosContext _context;

        public ProdutoRepository(ProdutosContext context)
        {
            _context = context;
        }

        public void Alterar(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
        }

        public void Excluir(Produto produto)
        {
            _context.Produto.Remove(produto);
        }

        public void Incluir(Produto produto)
        {
            _context.Produto.Add(produto);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Produto> SelecionarByID(int id)
        {
            return await _context.Produto.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> SelecionarTodos(int iduser)
        {
            return await _context.Produto.Where(x => x.IdUser == iduser).ToListAsync();
           
        }
    }
}

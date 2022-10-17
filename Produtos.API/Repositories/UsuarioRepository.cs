using Microsoft.EntityFrameworkCore;
using Produtos.API.Interfaces;
using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepositorio
    {
        private readonly ProdutosContext _context;

        public UsuarioRepository(ProdutosContext context)
        {
            _context = context;
        }
        public void Alterar(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }

        public void Excluir(Usuario usuario)
        {
            _context.Usuario.Remove(usuario);
        }

        public void Incluir(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public async Task<Usuario> SelecionarByEmail(string email)
        {
            return await _context.Usuario.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario> SelecionarByID(int id)
        {
            return await _context.Usuario.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}

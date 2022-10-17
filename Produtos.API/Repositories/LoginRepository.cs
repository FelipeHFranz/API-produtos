using Microsoft.EntityFrameworkCore;
using Produtos.API.Interfaces;
using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Repositories
{
    public class LoginRepository : ILoginRepositorio
    {
        private readonly ProdutosContext _context;

        public LoginRepository(ProdutosContext context)
        {
            _context = context;
        }
        public async Task<Usuario> login(string email, string senha)
        {
            return await _context.Usuario.Where(x => x.Email == email && x.Senha == senha).FirstOrDefaultAsync();
        }
    }
}

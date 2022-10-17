using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Interfaces
{
    public interface ILoginRepositorio
    {
        Task<Usuario> login(string email, string senha);
    }
}

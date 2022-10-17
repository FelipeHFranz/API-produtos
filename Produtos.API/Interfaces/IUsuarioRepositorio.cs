using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Interfaces
{
    public interface IUsuarioRepositorio
    {

        void Incluir(Usuario usuario);
        void Alterar(Usuario usuario);
        void Excluir(Usuario usuario);

        Task<Usuario> SelecionarByID(int id);
        Task<Usuario> SelecionarByEmail(string email);

        Task<bool> SaveAllAsync();
    }
}

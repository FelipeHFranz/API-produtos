using Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.API.Interfaces
{
    public interface IProdutoRepositorio
    {
        void Incluir(Produto produto);
        void Alterar(Produto produto);
        void Excluir(Produto produto);

        Task<Produto> SelecionarByID(int id);
        Task<IEnumerable<Produto>> SelecionarTodos(int iduser);

        Task<bool> SaveAllAsync();
    }
}

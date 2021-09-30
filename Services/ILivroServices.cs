using Projeto22.Input_Model;
using Projeto22.View_Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto22.Services
{
    public interface ILivroServices : IDisposable
    {
        Task<List<Livros_View_Model>> Obter(int pagina, int quantidade);
        Task<Livros_View_Model> Obter(Guid id);
        Task<Livros_View_Model> Inserir(Livros_Input_Model livro);
        Task Atualizar(Guid id, Livros_Input_Model livro);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}

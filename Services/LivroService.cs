using Projeto22.Entities;
using Projeto22.Exceptions;
using Projeto22.InputModel;
using Projeto22.Repositories;
using Projeto22.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto22.Services
{
    public class LivroService : ILivroServices
    {
        private readonly ILivroRepositorio _livroRepository;

        public LivroService(ILivroRepositorio livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<List<Livros_View_Model>> Obter(int pagina, int quantidade)
        {
            var livros = await _livroRepository.Obter(pagina, quantidade);

            return livros.Select(livro => new Livros_View_Model
                                {
                                    Id = livro.Id,
                                    nome_livro = livro.nome_livro,
                                    autor = livro.autor,
                                    preco = livro.preco
                                })
                               .ToList();
        }

        public async Task<Livros_View_Model> Obter(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                return null;

            return new Livros_View_Model
            {
                Id = livro.Id,
                nome_livro = livro.nome_livro,
                autor = livro.autor,
                preco = livro.preco
            };
        }

        public async Task<Livros_View_Model> Inserir(Livros_Input_Model jogo)
        {
            var entidade_livro = await _livroRepository.Obter(livro.nome_livro, livro.autor);

            if (entidade_livro.Count > 0)
                throw new Livro_Cadastrado_Exception();

            var livro_Insert = new Livro
            {
                Id = Guid.NewGuid(),
                nome_livro = livro.nome_livro,
                autor = livro.autor,
                preco = livro.preco
            };

            await _livroRepository.Inserir(livro_Insert);

            return new Livros_View_Model
            {
                Id = livro.Id,
                nome_livro = livro.nome_livro,
                autor = livro.autor,
                preco = livro.preco
            };
        }

        public async Task Atualizar(Guid id, Livros_Input_Model livro)
        {
            var entidade_livro = await _livroRepository.Obter(id);

            if (entidade_livro == null)
                throw new Livro_Nao_Cadastrado_Exception();

            entidade_livro.nome_livro = livro.nome_livro;
            entidade_livro.autor = livro.autor;
            entidade_livro.preco = livro.preco;

            await _livroRepository.Atualizar(entidade_livro);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidade_livro = await _livroRepository.Obter(id);

            if (entidade_livro == null)
                throw new Livro_Nao_Cadastrado_Exception();

            entidade_livro.preco = preco;

            await _livroRepository.Atualizar(entidade_livro);
        }

        public async Task Remover(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                throw new Livro_Nao_Cadastrado_Exception();

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}

using Projeto22.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto22.Repositoriio
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Livro>()
        {
            {Guid.Parse("01"), new Livro{ Id = Guid.Parse("01"), nome_livro = "Crônicas de Nárnia", autor = "C.S. Lewis", preco = 50} },
            {Guid.Parse("02"), new Livro{ Id = Guid.Parse("02"), nome_livro = "A Menina que Roubava Livros", autor = "Markus Zusak", preco = 70} },
            {Guid.Parse("03"), new Livro{ Id = Guid.Parse("03"), nome_livro = "O Mundo de Sofia", autor = "Josten Gaarder", preco = 60} },
            {Guid.Parse("04"), new Livro{ Id = Guid.Parse("04"), nome_livro = "O Iluminado", autor = "Stephen King", preco = 80} },
            {Guid.Parse("05"), new Livro{ Id = Guid.Parse("05"), nome_livro = "A Montanha Mágia", autor = "Thomas Mann", preco = 80} },
            {Guid.Parse("06"), new Livro{ Id = Guid.Parse("06"), nome_livro = "Crime e Castigo", autor = "Fyodor Dostoevsky", preco = 50} }
            {Guid.Parse("07"), new Livro{ Id = Guid.Parse("07"), nome_livro = "Sherlock Holmes", autor = "Arthur Conan Doyle", preco = 100} }

        };

        public Task<List<Livro>> Obter(int pg, int qtd)
        {
            return Task.FromResult(livros.Values.Skip((pg - 1) * qtd).Take(qtd).ToList());
        }

        public Task<Livro> Obter(Guid id)
        {
            if (!livros.ContainsKey(id))
                return Task.FromResult<Livro>(null);

            return Task.FromResult(livros[id]);
        }

        public Task<List<Livro>> Obter(string nome_livro, string produtora)
        {
            return Task.FromResult(livros.Values.Where(livro => livro.nome_livro.Equals(nome_livro) && livro.autor.Equals(autor)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome_livro, string autor)
        {
            var retorno = new List<Livro>();

            foreach(var livro in livros.Values)
            {
                if (livro.nome_livro.Equals(nome_livro) && jogo.autor.Equals(autor))
                    retorno.Add(livro);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Livro livro)
        {
            livros.Add(livro.Id, livro);
            return Task.CompletedTask;
        }

        public Task Atualizar(Livro livro)
        {
            livros[livro.Id] = livro;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            livros.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {



        }
    }

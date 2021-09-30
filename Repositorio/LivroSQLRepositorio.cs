using Projeto22.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Projeto22.Repositorio
{
    public class LivroSQLRepositorio : ILivroRepositorio
    {
        private readonly SqlConnection sqlConnection;

        public LivroSQLRepositorio(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            var livros = new List<Jogo>();

            var comandos = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comandos, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livros.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    nome_livro = (string)sqlDataReader["Nome do Livro"],
                    autor = (string)sqlDataReader["Nome do autor"],
                    preco = (double)sqlDataReader["Preco do livro"]
                });
            }

            await sqlConnection.CloseAsync();

            return livros;
        }

        public async Task<Livro> Obter(Guid id)
        {
            Livro livro = null;

            var comando = $"select * from Livros where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livro = new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    nome_livro = (string)sqlDataReader["Nome do livro"],
                    autor = (string)sqlDataReader["Nome do autor"],
                    preco = (double)sqlDataReader["Preco do livro"]
                };
            }

            await sqlConnection.CloseAsync();

            return livro;
        }

        public async Task<List<Livro>> Obter(string nome_livro, string autor)
        {
            var livros = new List<Livro>();

            var comando = $"select * from Livros where nome_livro = '{nome_livro}' and autor = '{autor}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livros.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    nome_livro = (string)sqlDataReader["Nome do Livro"],
                    autor = (string)sqlDataReader["Nome do autor"],
                    preco = (double)sqlDataReader["Preco do livro"]
                });
            }

            await sqlConnection.CloseAsync();

            return livros;
        }

        public async Task Inserir(Livro livro)
        {
            var comando = $"insert Livros (Id, Nome, Autor, Preco) values ('{livro.Id}', '{livro.nome_livro}', '{livro.autor}', {livro.preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Livro livro)
        {
            var comando = $"update Livros set Nome do livro = '{livro.nome_livro}', Autor = '{livro.autor}', Preco = {livro.preco.ToString().Replace(",", ".")} where Id = '{livro.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Livros where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }

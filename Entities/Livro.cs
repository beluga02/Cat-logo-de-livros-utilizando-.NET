using System;

namespace Projeto22.Entities
{
    public class Livro
    {
        public Guid Id { get; set; }
        public string nome_livro { get; set; }
        public string autor { get; set; }
        public double preco { get; set; }
    }
}

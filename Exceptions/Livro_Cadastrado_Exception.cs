using System;

namespace Projeto22.Exceptions
{
    public class Livro_Cadastrado_Exception : Exception
    {
        public Livro_Cadastrado_Exception()
            : base("Este livro já está cadastrado")
        { }
    }
}

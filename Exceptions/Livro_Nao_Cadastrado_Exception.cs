using System;

namespace Projeto22.Exceptions
{
    public class Livro_Nao_Cadastrado_Exception: Exception
    {
        public Livro_Nao_Cadastrado_Exception()
            :base("Este livro não está cadastrado")
        {}
    }
}

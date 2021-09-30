using System.ComponentModel.DataAnnotations;

namespace Projeto22.Input_Model
{
    public class Livros_Input_Model
    {
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "O nome do livro deve conter entre 1 a 30 caracteres!")]
        public string nome_livro { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "O nome do autor(a) deve conter entre 1 a 30 caracteres!")]
        public string autor { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "O preço mínimo para o livro é de 1 real e o preço máximo é de 10000 reais!")]
        public double preco { get; set; }
    }
}

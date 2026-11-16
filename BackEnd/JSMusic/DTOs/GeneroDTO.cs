
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class GeneroCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
    }

    public class GeneroAtualizarDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
    }

    public class GeneroRespostaDTO
    {
        public int IdGenero { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}

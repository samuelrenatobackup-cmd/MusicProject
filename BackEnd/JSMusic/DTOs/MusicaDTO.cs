
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class MusicaCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Duracao { get; set; }

        [Required]
        [Url]
        public string UrlAudio { get; set; } = string.Empty;
    }

    public class MusicaAtualizarDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Duracao { get; set; }

        [Required]
        [Url]
        public string UrlAudio { get; set; } = string.Empty;
    }

    public class MusicaRespostaDTO
    {
        public int IdMusica { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Duracao { get; set; }
        public string UrlAudio { get; set; } = string.Empty;
    }
}

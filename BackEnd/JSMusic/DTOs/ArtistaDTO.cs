
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class ArtistaCadastroDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int IdUsuario { get; set; }
    }

    public class ArtistaRespostaDTO
    {
        public int IdArtista { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
    }
}

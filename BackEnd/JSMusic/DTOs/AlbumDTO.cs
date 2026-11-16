
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class AlbumCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        public DateOnly? DataLancamento { get; set; }
    }

    public class AlbumAtualizarDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        public DateOnly? DataLancamento { get; set; }
    }

    public class AlbumRespostaDTO
    {
        public int IdAlbum { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly? DataLancamento { get; set; }
    }
}

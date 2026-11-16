
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class ArtistaMusicaDTO
    {
        [Range(1, int.MaxValue)]
        public int IdArtista { get; set; }

        [Range(1, int.MaxValue)]
        public int IdMusica { get; set; }
    }
}

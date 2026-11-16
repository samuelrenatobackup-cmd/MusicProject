
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class ArtistaAlbumDTO
    {
        [Range(1, int.MaxValue)]
        public int IdArtista { get; set; }

        [Range(1, int.MaxValue)]
        public int IdAlbum { get; set; }
    }
}

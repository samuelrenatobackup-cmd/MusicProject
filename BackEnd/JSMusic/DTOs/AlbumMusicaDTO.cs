
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class AlbumMusicaDTO
    {
        [Range(1, int.MaxValue)]
        public int IdAlbum { get; set; }

        [Range(1, int.MaxValue)]
        public int IdMusica { get; set; }
    }
}

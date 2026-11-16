
using System.ComponentModel.DataAnnotations;

namespace JSMusic.DTOs
{
    public class MusicaGeneroDTO
    {
        [Range(1, int.MaxValue)]
        public int IdMusica { get; set; }

        [Range(1, int.MaxValue)]
        public int IdGenero { get; set; }
    }
}

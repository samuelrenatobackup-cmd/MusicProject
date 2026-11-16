
using JSMusic.DTOs;
using JSMusic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumMusicaController : ControllerBase
    {
        private readonly IAlbumMusicaRepository _repository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMusicaRepository _musicaRepository;

        public AlbumMusicaController(
            IAlbumMusicaRepository repository,
            IAlbumRepository albumRepository,
            IMusicaRepository musicaRepository)
        {
            _repository = repository;
            _albumRepository = albumRepository;
            _musicaRepository = musicaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Associar(AlbumMusicaDTO dto)
        {
            var album = await _albumRepository.BuscarPorIdAsync(dto.IdAlbum);
            if (album == null)
                return NotFound("Álbum não encontrado.");

            var musica = await _musicaRepository.BuscarPorIdAsync(dto.IdMusica);
            if (musica == null)
                return NotFound("Música não encontrada.");

            if (album.IdMusicas.Any(m => m.IdMusica == dto.IdMusica))
                return Conflict("Esta música já está associada ao álbum.");

            await _repository.AssociarAsync(dto.IdAlbum, dto.IdMusica);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Desassociar(AlbumMusicaDTO dto)
        {
            var album = await _albumRepository.BuscarPorIdAsync(dto.IdAlbum);
            if (album == null)
                return NotFound("Álbum não encontrado.");

            if (!album.IdMusicas.Any(m => m.IdMusica == dto.IdMusica))
                return NotFound("Esta música não está associada ao álbum.");

            await _repository.DesassociarAsync(dto.IdAlbum, dto.IdMusica);

            return NoContent();
        }

        [HttpGet("album/{idAlbum}")]
        public async Task<IActionResult> ListarMusicasPorAlbum(int idAlbum)
        {
            var album = await _albumRepository.BuscarPorIdAsync(idAlbum);

            if (album == null)
                return NotFound("Álbum não encontrado.");

            var musicas = await _repository.ListarMusicasPorAlbumAsync(idAlbum);

            return Ok(musicas);
        }

        [HttpGet("musica/{idMusica}")]
        public async Task<IActionResult> ListarAlbunsPorMusica(int idMusica)
        {
            var musica = await _musicaRepository.BuscarPorIdAsync(idMusica);

            if (musica == null)
                return NotFound("Música não encontrada.");

            var albuns = await _repository.ListarAlbunsPorMusicaAsync(idMusica);

            return Ok(albuns);
        }
    }
}

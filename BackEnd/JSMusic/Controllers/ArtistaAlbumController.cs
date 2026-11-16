
using JSMusic.DTOs;
using JSMusic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistaAlbumController : ControllerBase
    {
        private readonly IArtistaAlbumRepository _repository;
        private readonly IArtistaRepository _artistaRepository;
        private readonly IAlbumRepository _albumRepository;

        public ArtistaAlbumController(
            IArtistaAlbumRepository repository,
            IArtistaRepository artistaRepository,
            IAlbumRepository albumRepository)
        {
            _repository = repository;
            _artistaRepository = artistaRepository;
            _albumRepository = albumRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Associar(ArtistaAlbumDTO dto)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(dto.IdArtista);
            if (artista == null)
                return NotFound("Artista não encontrado.");

            var album = await _albumRepository.BuscarPorIdAsync(dto.IdAlbum);
            if (album == null)
                return NotFound("Álbum não encontrado.");

            if (artista.IdAlbums.Any(a => a.IdAlbum == dto.IdAlbum))
                return Conflict("Este álbum já está associado ao artista.");

            await _repository.AssociarAsync(dto.IdArtista, dto.IdAlbum);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Desassociar(ArtistaAlbumDTO dto)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(dto.IdArtista);
            if (artista == null)
                return NotFound("Artista não encontrado.");

            if (!artista.IdAlbums.Any(a => a.IdAlbum == dto.IdAlbum))
                return NotFound("Este álbum não está associado ao artista.");

            await _repository.DesassociarAsync(dto.IdArtista, dto.IdAlbum);

            return NoContent();
        }

        [HttpGet("artista/{idArtista}")]
        public async Task<IActionResult> ListarAlbunsPorArtista(int idArtista)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(idArtista);

            if (artista == null)
                return NotFound("Artista não encontrado.");

            var albuns = await _repository.ListarAlbunsPorArtistaAsync(idArtista);

            return Ok(albuns);
        }

        [HttpGet("album/{idAlbum}")]
        public async Task<IActionResult> ListarArtistasPorAlbum(int idAlbum)
        {
            var album = await _albumRepository.BuscarPorIdAsync(idAlbum);

            if (album == null)
                return NotFound("Álbum não encontrado.");

            var artistas = await _repository.ListarArtistasPorAlbumAsync(idAlbum);

            return Ok(artistas);
        }
    }
}

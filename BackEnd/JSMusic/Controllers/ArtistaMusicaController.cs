
using JSMusic.DTOs;
using JSMusic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistaMusicaController : ControllerBase
    {
        private readonly IArtistaMusicaRepository _repository;
        private readonly IArtistaRepository _artistaRepository;
        private readonly IMusicaRepository _musicaRepository;

        public ArtistaMusicaController(
            IArtistaMusicaRepository repository,
            IArtistaRepository artistaRepository,
            IMusicaRepository musicaRepository)
        {
            _repository = repository;
            _artistaRepository = artistaRepository;
            _musicaRepository = musicaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Associar(ArtistaMusicaDTO dto)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(dto.IdArtista);
            if (artista == null)
                return NotFound("Artista não encontrado.");

            var musica = await _musicaRepository.BuscarPorIdAsync(dto.IdMusica);
            if (musica == null)
                return NotFound("Música não encontrada.");

            if (artista.IdMusicas.Any(m => m.IdMusica == dto.IdMusica))
                return Conflict("Esta música já está associada ao artista.");

            await _repository.AssociarAsync(dto.IdArtista, dto.IdMusica);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Desassociar(ArtistaMusicaDTO dto)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(dto.IdArtista);
            if (artista == null)
                return NotFound("Artista não encontrado.");

            if (!artista.IdMusicas.Any(m => m.IdMusica == dto.IdMusica))
                return NotFound("Esta música não está associada ao artista.");

            await _repository.DesassociarAsync(dto.IdArtista, dto.IdMusica);

            return NoContent();
        }

        [HttpGet("artista/{idArtista}")]
        public async Task<IActionResult> ListarMusicasPorArtista(int idArtista)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(idArtista);

            if (artista == null)
                return NotFound("Artista não encontrado.");

            var musicas = await _repository.ListarMusicasPorArtistaAsync(idArtista);

            return Ok(musicas);
        }

        [HttpGet("musica/{idMusica}")]
        public async Task<IActionResult> ListarArtistasPorMusica(int idMusica)
        {
            var musica = await _musicaRepository.BuscarPorIdAsync(idMusica);

            if (musica == null)
                return NotFound("Música não encontrada.");

            var artistas = await _repository.ListarArtistasPorMusicaAsync(idMusica);

            return Ok(artistas);
        }
    }
}

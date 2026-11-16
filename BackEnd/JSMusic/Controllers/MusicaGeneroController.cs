
using JSMusic.DTOs;
using JSMusic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicaGeneroController : ControllerBase
    {
        private readonly IMusicaGeneroRepository _repository;
        private readonly IMusicaRepository _musicaRepository;
        private readonly IGeneroRepository _generoRepository;

        public MusicaGeneroController(
            IMusicaGeneroRepository repository,
            IMusicaRepository musicaRepository,
            IGeneroRepository generoRepository)
        {
            _repository = repository;
            _musicaRepository = musicaRepository;
            _generoRepository = generoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Associar(MusicaGeneroDTO dto)
        {
            var musica = await _musicaRepository.BuscarPorIdAsync(dto.IdMusica);
            if (musica == null)
                return NotFound("Música não encontrada.");

            var genero = await _generoRepository.BuscarPorIdAsync(dto.IdGenero);
            if (genero == null)
                return NotFound("Gênero não encontrado.");

            if (musica.IdGeneros.Any(g => g.IdGenero == dto.IdGenero))
                return Conflict("Este gênero já está associado à música.");

            await _repository.AssociarAsync(dto.IdMusica, dto.IdGenero);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Desassociar(MusicaGeneroDTO dto)
        {
            var musica = await _musicaRepository.BuscarPorIdAsync(dto.IdMusica);
            if (musica == null)
                return NotFound("Música não encontrada.");

            if (!musica.IdGeneros.Any(g => g.IdGenero == dto.IdGenero))
                return NotFound("Este gênero não está associado à música.");

            await _repository.DesassociarAsync(dto.IdMusica, dto.IdGenero);

            return NoContent();
        }

        [HttpGet("musica/{idMusica}")]
        public async Task<IActionResult> ListarGenerosPorMusica(int idMusica)
        {
            var musica = await _musicaRepository.BuscarPorIdAsync(idMusica);

            if (musica == null)
                return NotFound("Música não encontrada.");

            var generos = await _repository.ListarGenerosPorMusicaAsync(idMusica);

            return Ok(generos);
        }

        [HttpGet("genero/{idGenero}")]
        public async Task<IActionResult> ListarMusicasPorGenero(int idGenero)
        {
            var genero = await _generoRepository.BuscarPorIdAsync(idGenero);

            if (genero == null)
                return NotFound("Gênero não encontrado.");

            var musicas = await _repository.ListarMusicasPorGeneroAsync(idGenero);

            return Ok(musicas);
        }
    }
}

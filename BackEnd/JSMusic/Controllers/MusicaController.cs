
using JSMusic.DTOs;
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicaController : ControllerBase
    {
        private readonly IMusicaRepository _repository;

        public MusicaController(IMusicaRepository repository)
        {
            _repository = repository;
        }

        private static MusicaRespostaDTO Mapear(Musica musica)
        {
            return new MusicaRespostaDTO
            {
                IdMusica = musica.IdMusica,
                Nome = musica.Nome,
                Duracao = musica.Duracao,
                UrlAudio = musica.UrlAudio
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicaRespostaDTO>>> Listar()
        {
            var musicas = await _repository.ListarAsync();

            return Ok(musicas.Select(Mapear));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MusicaRespostaDTO>> BuscarPorId(int id)
        {
            var musica = await _repository.BuscarPorIdAsync(id);

            if (musica == null)
                return NotFound("Música não encontrada.");

            return Ok(Mapear(musica));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<MusicaRespostaDTO>>> BuscarPorNome(
            [FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Informe o nome da música.");

            var musicas = await _repository.BuscarPorNomeAsync(nome);

            return Ok(musicas.Select(Mapear));
        }

        [HttpGet("genero/{idGenero}")]
        public async Task<ActionResult<IEnumerable<MusicaRespostaDTO>>> ListarPorGenero(
            int idGenero)
        {
            var musicas = await _repository.ListarPorGeneroAsync(idGenero);

            return Ok(musicas.Select(Mapear));
        }

        [HttpGet("artista/{idArtista}")]
        public async Task<ActionResult<IEnumerable<MusicaRespostaDTO>>> ListarPorArtista(
            int idArtista)
        {
            var musicas = await _repository.ListarPorArtistaAsync(idArtista);

            return Ok(musicas.Select(Mapear));
        }

        [HttpGet("album/{idAlbum}")]
        public async Task<ActionResult<IEnumerable<MusicaRespostaDTO>>> ListarPorAlbum(
            int idAlbum)
        {
            var musicas = await _repository.ListarPorAlbumAsync(idAlbum);

            return Ok(musicas.Select(Mapear));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(MusicaCadastroDTO dto)
        {
            var musica = new Musica
            {
                Nome = dto.Nome,
                Duracao = dto.Duracao,
                UrlAudio = dto.UrlAudio
            };

            await _repository.CadastrarAsync(musica);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = musica.IdMusica }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, MusicaAtualizarDTO dto)
        {
            var musica = await _repository.BuscarPorIdAsync(id);

            if (musica == null)
                return NotFound("Música não encontrada.");

            musica.Nome = dto.Nome;
            musica.Duracao = dto.Duracao;
            musica.UrlAudio = dto.UrlAudio;

            await _repository.AtualizarAsync(musica);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var musica = await _repository.BuscarPorIdAsync(id);

            if (musica == null)
                return NotFound("Música não encontrada.");

            await _repository.ExcluirAsync(id);

            return NoContent();
        }
    }
}

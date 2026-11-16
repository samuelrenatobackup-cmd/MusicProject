
using JSMusic.DTOs;
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumRepository _repository;

        public AlbumController(IAlbumRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumRespostaDTO>>> Listar()
        {
            var albuns = await _repository.ListarAsync();

            return Ok(albuns.Select(a => new AlbumRespostaDTO
            {
                IdAlbum = a.IdAlbum,
                Nome = a.Nome,
                DataLancamento = a.DataLancamento
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumRespostaDTO>> BuscarPorId(int id)
        {
            var album = await _repository.BuscarPorIdAsync(id);

            if (album == null)
                return NotFound("Álbum não encontrado.");

            return Ok(new AlbumRespostaDTO
            {
                IdAlbum = album.IdAlbum,
                Nome = album.Nome,
                DataLancamento = album.DataLancamento
            });
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(AlbumCadastroDTO dto)
        {
            var album = new Album
            {
                Nome = dto.Nome,
                DataLancamento = dto.DataLancamento
            };

            await _repository.CadastrarAsync(album);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = album.IdAlbum }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AlbumAtualizarDTO dto)
        {
            var album = await _repository.BuscarPorIdAsync(id);

            if (album == null)
                return NotFound("Álbum não encontrado.");

            album.Nome = dto.Nome;
            album.DataLancamento = dto.DataLancamento;

            await _repository.AtualizarAsync(album);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var album = await _repository.BuscarPorIdAsync(id);

            if (album == null)
                return NotFound("Álbum não encontrado.");

            await _repository.ExcluirAsync(id);

            return NoContent();
        }
    }
}

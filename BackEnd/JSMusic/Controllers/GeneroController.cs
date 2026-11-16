
using JSMusic.DTOs;
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _repository;

        public GeneroController(IGeneroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroRespostaDTO>>> Listar()
        {
            var generos = await _repository.ListarAsync();

            return Ok(generos.Select(g => new GeneroRespostaDTO
            {
                IdGenero = g.IdGenero,
                Nome = g.Nome
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroRespostaDTO>> BuscarPorId(int id)
        {
            var genero = await _repository.BuscarPorIdAsync(id);

            if (genero == null)
                return NotFound("Gênero não encontrado.");

            return Ok(new GeneroRespostaDTO
            {
                IdGenero = genero.IdGenero,
                Nome = genero.Nome
            });
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(GeneroCadastroDTO dto)
        {
            var genero = new Genero
            {
                Nome = dto.Nome
            };

            await _repository.CadastrarAsync(genero);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = genero.IdGenero }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, GeneroAtualizarDTO dto)
        {
            var genero = await _repository.BuscarPorIdAsync(id);

            if (genero == null)
                return NotFound("Gênero não encontrado.");

            genero.Nome = dto.Nome;

            await _repository.AtualizarAsync(genero);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var genero = await _repository.BuscarPorIdAsync(id);

            if (genero == null)
                return NotFound("Gênero não encontrado.");

            await _repository.ExcluirAsync(id);

            return NoContent();
        }
    }
}

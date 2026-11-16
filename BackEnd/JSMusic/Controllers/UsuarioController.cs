
using JSMusic.DTOs;
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static UsuarioRespostaDTO Mapear(Usuario usuario)
        {
            return new UsuarioRespostaDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRespostaDTO>>> Listar()
        {
            var usuarios = await _repository.ListarAsync();

            return Ok(usuarios.Select(Mapear));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRespostaDTO>> BuscarPorId(int id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(Mapear(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroDTO dto)
        {
            if (await _repository.EmailExisteAsync(dto.Email))
                return Conflict("Este e-mail já está cadastrado.");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                TipoUsuario = "Usuario"
            };

            await _repository.CadastrarAsync(usuario);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = usuario.IdUsuario }, Mapear(usuario));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, UsuarioAtualizarDTO dto)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            if (!string.Equals(usuario.Email, dto.Email,
                StringComparison.OrdinalIgnoreCase) &&
                await _repository.EmailExisteAsync(dto.Email))
            {
                return Conflict("Este e-mail já está cadastrado.");
            }

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;

            await _repository.AtualizarAsync(usuario);

            return NoContent();
        }
    }
}

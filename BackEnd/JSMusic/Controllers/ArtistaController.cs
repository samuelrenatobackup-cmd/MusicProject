
using JSMusic.DTOs;
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistaController : ControllerBase
    {
        private readonly IArtistaRepository _artistaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ArtistaController(
            IArtistaRepository artistaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _artistaRepository = artistaRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistaRespostaDTO>>> Listar()
        {
            var artistas = await _artistaRepository.ListarAsync();

            return Ok(artistas.Select(a => new ArtistaRespostaDTO
            {
                IdArtista = a.IdArtista,
                IdUsuario = a.IdUsuario,
                NomeUsuario = a.IdUsuarioNavigation.Nome
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistaRespostaDTO>> BuscarPorId(int id)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(id);

            if (artista == null)
                return NotFound("Artista não encontrado.");

            return Ok(new ArtistaRespostaDTO
            {
                IdArtista = artista.IdArtista,
                IdUsuario = artista.IdUsuario,
                NomeUsuario = artista.IdUsuarioNavigation.Nome
            });
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ArtistaCadastroDTO dto)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(dto.IdUsuario);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var artistaExistente =
                await _artistaRepository.BuscarPorUsuarioIdAsync(dto.IdUsuario);

            if (artistaExistente != null)
                return Conflict("Este usuário já possui um cadastro de artista.");

            var artista = new Artistum
            {
                IdUsuario = dto.IdUsuario
            };

            await _artistaRepository.CadastrarAsync(artista);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = artista.IdArtista }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ArtistaCadastroDTO dto)
        {
            var artista = await _artistaRepository.BuscarPorIdAsync(id);

            if (artista == null)
                return NotFound("Artista não encontrado.");

            var usuario = await _usuarioRepository.BuscarPorIdAsync(dto.IdUsuario);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var outroArtista =
                await _artistaRepository.BuscarPorUsuarioIdAsync(dto.IdUsuario);

            if (outroArtista != null && outroArtista.IdArtista != id)
                return Conflict("Este usuário já possui outro cadastro de artista.");

            artista.IdUsuario = dto.IdUsuario;

            await _artistaRepository.AtualizarAsync(artista);

            return NoContent();
        }
    }
}

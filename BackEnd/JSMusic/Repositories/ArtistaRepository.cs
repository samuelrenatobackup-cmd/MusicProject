
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de acesso a dados dos artistas.
    /// </summary>
    public class ArtistaRepository : IArtistaRepository
    {
        private readonly AppDbContext _context;

        public ArtistaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca um artista pelo identificador.
        /// </summary>
        public async Task<Artistum?> BuscarPorIdAsync(int id)
        {
            return await _context.Artista
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdMusicas)
                .Include(a => a.IdAlbums)
                .FirstOrDefaultAsync(a => a.IdArtista == id);
        }

        /// <summary>
        /// Busca um artista pelo identificador do usuário vinculado.
        /// </summary>
        public async Task<Artistum?> BuscarPorUsuarioIdAsync(int idUsuario)
        {
            return await _context.Artista
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdMusicas)
                .Include(a => a.IdAlbums)
                .FirstOrDefaultAsync(a => a.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Lista todos os artistas cadastrados.
        /// </summary>
        public async Task<List<Artistum>> ListarAsync()
        {
            return await _context.Artista
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdMusicas)
                .Include(a => a.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo artista.
        /// </summary>
        public async Task<Artistum> CadastrarAsync(Artistum artista)
        {
            await _context.Artista.AddAsync(artista);
            await _context.SaveChangesAsync();

            return artista;
        }

        /// <summary>
        /// Atualiza os dados de um artista.
        /// </summary>
        public async Task AtualizarAsync(Artistum artista)
        {
            _context.Artista.Update(artista);
            await _context.SaveChangesAsync();
        }
    }
}

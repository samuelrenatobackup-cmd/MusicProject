
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de acesso a dados dos álbuns.
    /// </summary>
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _context;

        public AlbumRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca um álbum pelo identificador.
        /// </summary>
        public async Task<Album?> BuscarPorIdAsync(int id)
        {
            return await _context.Albums
                .Include(a => a.IdArtista)
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdAlbum == id);
        }

        /// <summary>
        /// Lista todos os álbuns cadastrados.
        /// </summary>
        public async Task<List<Album>> ListarAsync()
        {
            return await _context.Albums
                .Include(a => a.IdArtista)
                .Include(a => a.IdMusicas)
                .ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo álbum.
        /// </summary>
        public async Task<Album> CadastrarAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();

            return album;
        }

        /// <summary>
        /// Atualiza os dados de um álbum.
        /// </summary>
        public async Task AtualizarAsync(Album album)
        {
            _context.Albums.Update(album);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui um álbum e suas associações.
        /// </summary>
        public async Task ExcluirAsync(int id)
        {
            var album = await _context.Albums
                .Include(a => a.IdArtista)
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdAlbum == id);

            if (album == null)
                return;

            album.IdArtista.Clear();
            album.IdMusicas.Clear();

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }
    }
}

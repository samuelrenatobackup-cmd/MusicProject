
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de associação entre álbuns e músicas.
    /// </summary>
    public class AlbumMusicaRepository : IAlbumMusicaRepository
    {
        private readonly AppDbContext _context;

        public AlbumMusicaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Associa uma música a um álbum.
        /// </summary>
        public async Task AssociarAsync(int idAlbum, int idMusica)
        {
            var album = await _context.Albums
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdAlbum == idAlbum);

            var musica = await _context.Musicas
                .FirstOrDefaultAsync(m => m.IdMusica == idMusica);

            if (album == null || musica == null)
                return;

            if (album.IdMusicas.Any(m => m.IdMusica == idMusica))
                return;

            album.IdMusicas.Add(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a associação entre um álbum e uma música.
        /// </summary>
        public async Task DesassociarAsync(int idAlbum, int idMusica)
        {
            var album = await _context.Albums
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdAlbum == idAlbum);

            if (album == null)
                return;

            var musica = album.IdMusicas
                .FirstOrDefault(m => m.IdMusica == idMusica);

            if (musica == null)
                return;

            album.IdMusicas.Remove(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Lista os IDs das músicas associadas a um álbum.
        /// </summary>
        public async Task<List<int>> ListarMusicasPorAlbumAsync(int idAlbum)
        {
            return await _context.Albums
                .Where(a => a.IdAlbum == idAlbum)
                .SelectMany(a => a.IdMusicas.Select(m => m.IdMusica))
                .ToListAsync();
        }

        /// <summary>
        /// Lista os IDs dos álbuns associados a uma música.
        /// </summary>
        public async Task<List<int>> ListarAlbunsPorMusicaAsync(int idMusica)
        {
            return await _context.Musicas
                .Where(m => m.IdMusica == idMusica)
                .SelectMany(m => m.IdAlbums.Select(a => a.IdAlbum))
                .ToListAsync();
        }
    }
}

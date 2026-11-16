
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de associação entre artistas e álbuns.
    /// </summary>
    public class ArtistaAlbumRepository : IArtistaAlbumRepository
    {
        private readonly AppDbContext _context;

        public ArtistaAlbumRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Associa um artista a um álbum.
        /// </summary>
        public async Task AssociarAsync(int idArtista, int idAlbum)
        {
            var artista = await _context.Artista
                .Include(a => a.IdAlbums)
                .FirstOrDefaultAsync(a => a.IdArtista == idArtista);

            var album = await _context.Albums
                .FirstOrDefaultAsync(a => a.IdAlbum == idAlbum);

            if (artista == null || album == null)
                return;

            if (artista.IdAlbums.Any(a => a.IdAlbum == idAlbum))
                return;

            artista.IdAlbums.Add(album);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a associação entre um artista e um álbum.
        /// </summary>
        public async Task DesassociarAsync(int idArtista, int idAlbum)
        {
            var artista = await _context.Artista
                .Include(a => a.IdAlbums)
                .FirstOrDefaultAsync(a => a.IdArtista == idArtista);

            if (artista == null)
                return;

            var album = artista.IdAlbums
                .FirstOrDefault(a => a.IdAlbum == idAlbum);

            if (album == null)
                return;

            artista.IdAlbums.Remove(album);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Lista os IDs dos artistas associados a um álbum.
        /// </summary>
        public async Task<List<int>> ListarArtistasPorAlbumAsync(int idAlbum)
        {
            return await _context.Albums
                .Where(a => a.IdAlbum == idAlbum)
                .SelectMany(a => a.IdArtista.Select(ar => ar.IdArtista))
                .ToListAsync();
        }

        /// <summary>
        /// Lista os IDs dos álbuns associados a um artista.
        /// </summary>
        public async Task<List<int>> ListarAlbunsPorArtistaAsync(int idArtista)
        {
            return await _context.Artista
                .Where(a => a.IdArtista == idArtista)
                .SelectMany(a => a.IdAlbums.Select(al => al.IdAlbum))
                .ToListAsync();
        }
    }
}

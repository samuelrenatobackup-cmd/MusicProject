
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de associação entre artistas e músicas.
    /// </summary>
    public class ArtistaMusicaRepository : IArtistaMusicaRepository
    {
        private readonly AppDbContext _context;

        public ArtistaMusicaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Associa uma música a um artista.
        /// </summary>
        public async Task AssociarAsync(int idArtista, int idMusica)
        {
            var artista = await _context.Artista
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdArtista == idArtista);

            var musica = await _context.Musicas
                .FirstOrDefaultAsync(m => m.IdMusica == idMusica);

            if (artista == null || musica == null)
                return;

            if (artista.IdMusicas.Any(m => m.IdMusica == idMusica))
                return;

            artista.IdMusicas.Add(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a associação entre um artista e uma música.
        /// </summary>
        public async Task DesassociarAsync(int idArtista, int idMusica)
        {
            var artista = await _context.Artista
                .Include(a => a.IdMusicas)
                .FirstOrDefaultAsync(a => a.IdArtista == idArtista);

            if (artista == null)
                return;

            var musica = artista.IdMusicas
                .FirstOrDefault(m => m.IdMusica == idMusica);

            if (musica == null)
                return;

            artista.IdMusicas.Remove(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Lista os IDs das músicas associadas a um artista.
        /// </summary>
        public async Task<List<int>> ListarMusicasPorArtistaAsync(int idArtista)
        {
            return await _context.Artista
                .Where(a => a.IdArtista == idArtista)
                .SelectMany(a => a.IdMusicas.Select(m => m.IdMusica))
                .ToListAsync();
        }

        /// <summary>
        /// Lista os IDs dos artistas associados a uma música.
        /// </summary>
        public async Task<List<int>> ListarArtistasPorMusicaAsync(int idMusica)
        {
            return await _context.Musicas
                .Where(m => m.IdMusica == idMusica)
                .SelectMany(m => m.IdArtista.Select(a => a.IdArtista))
                .ToListAsync();
        }
    }
}

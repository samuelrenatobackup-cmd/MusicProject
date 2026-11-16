
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de associação entre músicas e gêneros.
    /// </summary>
    public class MusicaGeneroRepository : IMusicaGeneroRepository
    {
        private readonly AppDbContext _context;

        public MusicaGeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Associa um gênero a uma música.
        /// </summary>
        public async Task AssociarAsync(int idMusica, int idGenero)
        {
            var genero = await _context.Generos
                .Include(g => g.IdMusicas)
                .FirstOrDefaultAsync(g => g.IdGenero == idGenero);

            var musica = await _context.Musicas
                .FirstOrDefaultAsync(m => m.IdMusica == idMusica);

            if (genero == null || musica == null)
                return;

            if (genero.IdMusicas.Any(m => m.IdMusica == idMusica))
                return;

            genero.IdMusicas.Add(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a associação entre uma música e um gênero.
        /// </summary>
        public async Task DesassociarAsync(int idMusica, int idGenero)
        {
            var genero = await _context.Generos
                .Include(g => g.IdMusicas)
                .FirstOrDefaultAsync(g => g.IdGenero == idGenero);

            if (genero == null)
                return;

            var musica = genero.IdMusicas
                .FirstOrDefault(m => m.IdMusica == idMusica);

            if (musica == null)
                return;

            genero.IdMusicas.Remove(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Lista os IDs dos gêneros associados a uma música.
        /// </summary>
        public async Task<List<int>> ListarGenerosPorMusicaAsync(int idMusica)
        {
            return await _context.Musicas
                .Where(m => m.IdMusica == idMusica)
                .SelectMany(m => m.IdGeneros.Select(g => g.IdGenero))
                .ToListAsync();
        }

        /// <summary>
        /// Lista os IDs das músicas associadas a um gênero.
        /// </summary>
        public async Task<List<int>> ListarMusicasPorGeneroAsync(int idGenero)
        {
            return await _context.Generos
                .Where(g => g.IdGenero == idGenero)
                .SelectMany(g => g.IdMusicas.Select(m => m.IdMusica))
                .ToListAsync();
        }
    }
}

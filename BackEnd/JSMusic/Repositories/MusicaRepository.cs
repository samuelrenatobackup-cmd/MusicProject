
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de acesso a dados das músicas.
    /// </summary>
    public class MusicaRepository : IMusicaRepository
    {
        private readonly AppDbContext _context;

        public MusicaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca uma música pelo identificador.
        /// </summary>
        public async Task<Musica?> BuscarPorIdAsync(int id)
        {
            return await _context.Musicas
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .FirstOrDefaultAsync(m => m.IdMusica == id);
        }

        /// <summary>
        /// Lista todas as músicas cadastradas.
        /// </summary>
        public async Task<List<Musica>> ListarAsync()
        {
            return await _context.Musicas
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Busca músicas pelo nome, permitindo correspondência parcial.
        /// </summary>
        public async Task<List<Musica>> BuscarPorNomeAsync(string nome)
        {
            return await _context.Musicas
                .Where(m => EF.Functions.Like(m.Nome, $"%{nome}%"))
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Lista as músicas associadas a um gênero.
        /// </summary>
        public async Task<List<Musica>> ListarPorGeneroAsync(int idGenero)
        {
            return await _context.Musicas
                .Where(m => m.IdGeneros.Any(g => g.IdGenero == idGenero))
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Lista as músicas associadas a um artista.
        /// </summary>
        public async Task<List<Musica>> ListarPorArtistaAsync(int idArtista)
        {
            return await _context.Musicas
                .Where(m => m.IdArtista.Any(a => a.IdArtista == idArtista))
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Lista as músicas pertencentes a um álbum.
        /// </summary>
        public async Task<List<Musica>> ListarPorAlbumAsync(int idAlbum)
        {
            return await _context.Musicas
                .Where(m => m.IdAlbums.Any(a => a.IdAlbum == idAlbum))
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .ToListAsync();
        }

        /// <summary>
        /// Cadastra uma nova música.
        /// </summary>
        public async Task<Musica> CadastrarAsync(Musica musica)
        {
            await _context.Musicas.AddAsync(musica);
            await _context.SaveChangesAsync();

            return musica;
        }

        /// <summary>
        /// Atualiza os dados de uma música.
        /// </summary>
        public async Task AtualizarAsync(Musica musica)
        {
            _context.Musicas.Update(musica);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui uma música e suas associações.
        /// </summary>
        public async Task ExcluirAsync(int id)
        {
            var musica = await _context.Musicas
                .Include(m => m.IdGeneros)
                .Include(m => m.IdArtista)
                .Include(m => m.IdAlbums)
                .FirstOrDefaultAsync(m => m.IdMusica == id);

            if (musica == null)
                return;

            musica.IdGeneros.Clear();
            musica.IdArtista.Clear();
            musica.IdAlbums.Clear();

            _context.Musicas.Remove(musica);
            await _context.SaveChangesAsync();
        }
    }
}

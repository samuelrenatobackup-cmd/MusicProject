
using JSMusic.Interfaces;
using JSMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de acesso a dados dos gêneros musicais.
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca um gênero pelo identificador.
        /// </summary>
        public async Task<Genero?> BuscarPorIdAsync(int id)
        {
            return await _context.Generos
                .Include(g => g.IdMusicas)
                .FirstOrDefaultAsync(g => g.IdGenero == id);
        }

        /// <summary>
        /// Lista todos os gêneros cadastrados.
        /// </summary>
        public async Task<List<Genero>> ListarAsync()
        {
            return await _context.Generos
                .Include(g => g.IdMusicas)
                .ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo gênero.
        /// </summary>
        public async Task<Genero> CadastrarAsync(Genero genero)
        {
            await _context.Generos.AddAsync(genero);
            await _context.SaveChangesAsync();

            return genero;
        }

        /// <summary>
        /// Atualiza os dados de um gênero.
        /// </summary>
        public async Task AtualizarAsync(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui um gênero e suas associações.
        /// </summary>
        public async Task ExcluirAsync(int id)
        {
            var genero = await _context.Generos
                .Include(g => g.IdMusicas)
                .FirstOrDefaultAsync(g => g.IdGenero == id);

            if (genero == null)
                return;

            genero.IdMusicas.Clear();

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }
    }
}

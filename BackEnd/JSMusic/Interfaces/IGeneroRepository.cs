
using JSMusic.Models;

namespace JSMusic.Interfaces
{
    public interface IGeneroRepository
    {
        Task<Genero?> BuscarPorIdAsync(int id);

        Task<List<Genero>> ListarAsync();

        Task<Genero> CadastrarAsync(Genero genero);

        Task AtualizarAsync(Genero genero);

        Task ExcluirAsync(int id);
    }
}

using JSMusic.Models;

namespace JSMusic.Interfaces
{
    public interface IArtistaRepository
    {
        Task<Artistum?> BuscarPorIdAsync(int id);

        Task<Artistum?> BuscarPorUsuarioIdAsync(int idUsuario);

        Task<List<Artistum>> ListarAsync();

        Task AtualizarAsync(Artistum artista);

        Task<Artistum> CadastrarAsync(Artistum artista);
    }
}

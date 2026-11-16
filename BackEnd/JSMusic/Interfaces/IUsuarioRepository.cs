using JSMusic.Models;

namespace JSMusic.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> BuscarPorIdAsync(int id);
        Task<Usuario?> BuscarPorEmailAsync(string email);
        Task<List<Usuario>> ListarAsync();
        Task<Usuario> CadastrarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task<bool> EmailExisteAsync(string email);
    }
}
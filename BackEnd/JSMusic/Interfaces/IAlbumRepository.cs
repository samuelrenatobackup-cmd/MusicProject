using JSMusic.Models;

namespace JSMusic.Interfaces
{
    public interface IAlbumRepository
    {
        Task<Album?> BuscarPorIdAsync(int id);
        Task<List<Album>> ListarAsync();
        Task<Album> CadastrarAsync(Album album);
        Task AtualizarAsync(Album album);
        Task ExcluirAsync(int id);
    }
}

using JSMusic.Models;

namespace JSMusic.Interfaces
{
    public interface IMusicaRepository
    {
        Task<Musica?> BuscarPorIdAsync(int id);

        Task<List<Musica>> ListarAsync();

        Task<List<Musica>> BuscarPorNomeAsync(string nome);

        Task<List<Musica>> ListarPorGeneroAsync(int idGenero);

        Task<List<Musica>> ListarPorArtistaAsync(int idArtista);

        Task<List<Musica>> ListarPorAlbumAsync(int idAlbum);

        Task<Musica> CadastrarAsync(Musica musica);

        Task AtualizarAsync(Musica musica);

        Task ExcluirAsync(int id);
    }
}

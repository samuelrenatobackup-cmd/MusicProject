
namespace JSMusic.Interfaces
{
    public interface IAlbumMusicaRepository
    {
        Task AssociarAsync(int idAlbum, int idMusica);

        Task DesassociarAsync(int idAlbum, int idMusica);

        Task<List<int>> ListarMusicasPorAlbumAsync(int idAlbum);

        Task<List<int>> ListarAlbunsPorMusicaAsync(int idMusica);
    }
}

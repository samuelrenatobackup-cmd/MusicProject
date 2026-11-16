
namespace JSMusic.Interfaces
{
    public interface IArtistaAlbumRepository
    {
        Task AssociarAsync(int idArtista, int idAlbum);

        Task DesassociarAsync(int idArtista, int idAlbum);

        Task<List<int>> ListarArtistasPorAlbumAsync(int idAlbum);

        Task<List<int>> ListarAlbunsPorArtistaAsync(int idArtista);
    }
}

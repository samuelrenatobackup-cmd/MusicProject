
using JSMusic.Models;

namespace JSMusic.Interfaces
{
    /// <summary>
    /// Define as operações de associação entre artistas e músicas.
    /// </summary>
    public interface IArtistaMusicaRepository
    {
        /// <summary>
        /// Associa uma música a um artista.
        /// </summary>
        Task AssociarAsync(int idArtista, int idMusica);

        /// <summary>
        /// Remove a associação entre um artista e uma música.
        /// </summary>
        Task DesassociarAsync(int idArtista, int idMusica);

        /// <summary>
        /// Lista os identificadores das músicas de um artista.
        /// </summary>
        Task<List<int>> ListarMusicasPorArtistaAsync(int idArtista);

        /// <summary>
        /// Lista os identificadores dos artistas de uma música.
        /// </summary>
        Task<List<int>> ListarArtistasPorMusicaAsync(int idMusica);
    }
}

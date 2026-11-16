namespace JSMusic.Interfaces
{
    public interface IMusicaGeneroRepository
    {
        Task AssociarAsync(int idMusica, int idGenero);

        Task DesassociarAsync(int idMusica, int idGenero);

        Task<List<int>> ListarGenerosPorMusicaAsync(int idMusica);

        Task<List<int>> ListarMusicasPorGeneroAsync(int idGenero);
    }
}

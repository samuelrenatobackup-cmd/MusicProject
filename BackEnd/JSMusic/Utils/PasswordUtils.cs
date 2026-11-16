
namespace JSMusic.Utils
{
    /// <summary>
    /// Responsável por gerar e verificar hashes de senhas.
    /// </summary>
    public static class PasswordUtils
    {
        public static string HashPassword(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool VerifyPassword(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}

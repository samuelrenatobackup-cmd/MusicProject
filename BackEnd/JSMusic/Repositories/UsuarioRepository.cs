
using JSMusic.Interfaces;
using JSMusic.Models;
using JSMusic.Utils;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Repositories
{
    /// <summary>
    /// Implementa as operações de acesso a dados dos usuários.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca um usuário pelo identificador.
        /// </summary>
        public async Task<Usuario?> BuscarPorIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        /// <summary>
        /// Busca um usuário pelo endereço de e-mail.
        /// </summary>
        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        public async Task<List<Usuario>> ListarAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        public async Task<Usuario> CadastrarAsync(Usuario usuario)
        {
            usuario.Senha = PasswordUtils.HashPassword(usuario.Senha);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        /// <summary>
        /// Atualiza os dados de um usuário.
        /// </summary>
        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza a senha de um usuário.
        /// </summary>
        public async Task AtualizarSenhaAsync(int id, string novaSenha)
        {
            var usuario = await BuscarPorIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Senha = PasswordUtils.HashPassword(novaSenha);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica se um endereço de e-mail já está cadastrado.
        /// </summary>
        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }
    }
}

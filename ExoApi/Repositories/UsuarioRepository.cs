using ExoApi.Contexts;
using ExoApi.Domains;

namespace ExoApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ExoApiContext _ctx;
        public UsuarioRepository(ExoApiContext ctx)
        {
            _ctx = ctx;
        }

        public List<Usuario> Listar()
        {
            return _ctx.Usuarios.ToList();
        }

        public void Criar(Usuario usuario)
        {
            _ctx.Usuarios.Add(usuario);
            _ctx.SaveChanges();
        }

        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _ctx.Usuarios.Find(id);

            if(usuario.Email != null)
            {
                usuarioBuscado.Email = usuario.Email;
            }
            if (usuario.Senha != null)
            {
                usuarioBuscado.Senha = usuario.Senha;
            }

            _ctx.Usuarios.Update(usuarioBuscado);
            _ctx.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            return _ctx.Usuarios.Find(id);
        }

        public void Deletar(int id)
        {
            Usuario usuario = _ctx.Usuarios.Find(id);
            _ctx.Usuarios.Remove(usuario);
            _ctx.SaveChanges();
        }

        public Usuario BuscarPorEmailESenha(Usuario usuario)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);
        }
    }
}

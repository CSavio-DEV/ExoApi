using ExoApi.Contexts;
using ExoApi.Domains;

namespace ExoApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoApiContext _ctx;

        public ProjetoRepository(ExoApiContext ctx)
        {
            _ctx = ctx;
        }

        public List<Projeto> Listar()
        {
            return _ctx.Projetos.ToList();
        }
    }
}

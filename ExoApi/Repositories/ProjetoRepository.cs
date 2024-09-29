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

        public void Criar(Projeto projeto)
        {
            _ctx.Projetos.Add(projeto);
            _ctx.SaveChanges();
        }

        public void Atualizar(int id, Projeto projeto)
        {
            Projeto projeto1 = _ctx.Projetos.Find(id);
            if (projeto.NomeDoProjeto != null)
            {
                projeto1.NomeDoProjeto = projeto.NomeDoProjeto;
            }
            if (projeto.Area != null)
            {
                projeto1.Area = projeto.Area;
            }
            if (projeto.Status != null)
            {
                projeto1.Status = projeto.Status;
            }

            _ctx.Projetos.Update(projeto1);
            _ctx.SaveChanges();
        }

        public Projeto BuscarPorId(int id)
        {
            return _ctx.Projetos.Find(id);
        }

        public void Deletar(int id)
        {
            Projeto projeto = _ctx.Projetos.Find(id);
            _ctx.Projetos.Remove(projeto);
            _ctx.SaveChanges();
        }

    }
}

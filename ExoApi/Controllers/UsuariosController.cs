using ExoApi.Domains;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            return StatusCode(200, _usuarioRepository.Listar());
        }

        [HttpPost]
        public IActionResult CriarUsuario(Usuario usuario)
        {
            _usuarioRepository.Criar(usuario);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            if(_usuarioRepository.BuscarPorId(id) == null)
            {
                return StatusCode(404);
            }

            _usuarioRepository.Atualizar(id, usuario);

            return StatusCode(204);
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            if(_usuarioRepository.BuscarPorId(id) == null)
            {
                return StatusCode(404);
            }

            _usuarioRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}

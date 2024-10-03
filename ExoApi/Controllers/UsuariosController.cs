using ExoApi.Domains;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            return StatusCode(200, _usuarioRepository.Listar());
        }

        //[HttpPost]
        //public IActionResult CriarUsuario(Usuario usuario)
        //{
        //    _usuarioRepository.Criar(usuario);
        //    return StatusCode(201);
        //}

        /// <summary>
        /// Faz Login
        /// </summary>
        /// <remarks>
        /// {
        ///     "email": "usuario@email.com,
        ///     "senha": "1234"
        /// }
        /// </remarks>
        /// <param name="usuario">Email e senha do usuario</param>
        /// <returns>Token de autenticacao</returns>
        /// <response code="200">Token gerado</response>
        /// <response code="401">Email ou senha incorretos</response>
        [HttpPost]
        public IActionResult FazerLogin(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(usuario);

            if(usuarioBuscado == null)
            {
                return StatusCode(401);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("asdfg_qwert@12345_asdfg_qwert@12345_asdfg_qwert@12345")); //esse texto é a chave

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "exoapi.webapi",
                audience: "exoapi.webapi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            return StatusCode(200, new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            
        }

        [Authorize]
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

        [Authorize]
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

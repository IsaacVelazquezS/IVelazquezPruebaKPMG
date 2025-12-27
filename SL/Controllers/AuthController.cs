using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BL.Login _login;

        public AuthController(IConfiguration config, BL.Login login)
        {
            _config = config;
            _login = login;
        }

        

        [HttpPost("login")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = _login.LoginUsuario(login.Username, login.Password);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                string token = GenerarToken(usuario);

                return Ok(new
                {
                    token
                });

            }
            else
            {
                return Unauthorized("Credenciales inválidas");
            }
        }


        private string GenerarToken(ML.Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresHours = double.Parse(_config["Jwt:ExpiresHours"]!);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiresHours"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

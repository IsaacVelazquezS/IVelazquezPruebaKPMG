using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Login
    {

        private readonly DL.IvelazquezPruebaKpmgContext _context;

        public Login(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result LoginUsuario(string username, string password)
        {
            ML.Result result = new ML.Result();

            try
            {
                var usuarioDL = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdEmpleadoNavigation)
                .FirstOrDefault(u => u.Username == username && u.IsActive);

                if (usuarioDL != null)
                {
                    bool valido = BCrypt.Net.BCrypt.Verify(password, usuarioDL.PasswordHash);

                    if (valido)
                    {
                        ML.Usuario usuarioML = new ML.Usuario
                        {
                            IdUsuario = usuarioDL.IdUsuario,
                            Username = usuarioDL.Username,
                            Email = usuarioDL.Email,
                            IsActive = usuarioDL.IsActive,

                            Rol = new ML.Rol
                            {
                                IdRol = usuarioDL.IdRol,
                                Name = usuarioDL.IdRolNavigation.Name
                            },

                            Empleado = new ML.Empleado
                            {
                                IdEmpleado = usuarioDL.IdEmpleado
                            }
                        };

                        result.Object = usuarioML;
                        result.Correct = true;

                    }                  
                }
            } catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}

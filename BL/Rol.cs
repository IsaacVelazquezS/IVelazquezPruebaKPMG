using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public Rol(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var roles = _context.Rols
                                    .FromSqlRaw("EXEC RolGetAll")
                                    .ToList();

                if (roles.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var entity in roles)
                    {
                        ML.Rol rol = new ML.Rol
                        {
                            IdRol = entity.IdRol,
                            Name = entity.Name
                        };

                        result.Objects.Add(rol);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron registros";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}



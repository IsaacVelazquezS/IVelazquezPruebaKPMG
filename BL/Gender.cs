using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Gender
    {

        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public Gender(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var Genders = _context.Genders
                                    .FromSqlRaw("EXEC GenderGetAll")
                                    .ToList();

                if (Genders.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var entity in Genders)
                    {
                        ML.Gender gender = new ML.Gender
                        {
                            IdGender = entity.IdGender,
                            Name = entity.Name
                        };

                        result.Objects.Add(gender);
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
using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Education
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public Education(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var Educations = _context.Educations
                                    .FromSqlRaw("EXEC EducationGetAll")
                                    .ToList();

                if (Educations.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var entity in Educations)
                    {
                        ML.Education education = new ML.Education
                        {
                            IdEducation = entity.IdEducation,
                            Name = entity.Name
                        };

                        result.Objects.Add(education);
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
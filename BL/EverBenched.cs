using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EverBenched
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public EverBenched(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                var Genders = _context.EverBencheds
                                    .FromSqlRaw("EXEC EverBenchedGetAll") 
                                    .ToList();

                if (Genders.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var entity in Genders)
                    {
                        ML.EverBenched  everBenched = new ML.EverBenched
                        {
                            IdEverBenche = entity.IdEverBenche,
                            Description = entity.Description
                        };

                        result.Objects.Add(everBenched);
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
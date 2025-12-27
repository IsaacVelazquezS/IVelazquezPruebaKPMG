using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class City
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public City(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                var Cities = (from CityDB in _context.Cities
                              select CityDB).ToList();

                if (Cities.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var entity in Cities)
                    {
                        ML.City city = new ML.City
                        {
                            IdCity = entity.IdCity,
                            Name = entity.Name
                        };
                        result.Objects.Add(city);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "no se encontraron registros";
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}

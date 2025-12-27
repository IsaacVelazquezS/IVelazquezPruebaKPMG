using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;
        public Empleado(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }
        public ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var FilasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC EmpleadoAdd @Age,@JoiningYear,@ExperienceInCurrentDomain,@LeaveORNot,@IdEducation,@IdCity,@IdGender,@PaymentTier,@IdEverBenche",
                    new SqlParameter("@Age", empleado.Age),
                    new SqlParameter("@JoiningYear", empleado.JoiningYear),
                    new SqlParameter("@ExperienceInCurrentDomain", empleado.ExperienceInCurrentDomain),
                    new SqlParameter("@LeaveORNot", empleado.LeaveOrnot),
                    new SqlParameter("@IdEducation", empleado.Education.IdEducation),
                    new SqlParameter("@IdCity", empleado.City.IdCity),
                    new SqlParameter("@IdGender", empleado.Gender.IdGender),
                    new SqlParameter("@PaymentTier", empleado.PaymentTier),
                    new SqlParameter("@IdEverBenche", empleado.EverBenched.IdEverBenche)
                );

                result.Correct = FilasAfectadas > 0;
                if (!result.Correct)
                    result.ErrorMessage = "Error al registrar";

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var empleados = _context.EmpleadoDTO
            .FromSqlRaw(
                "EXEC EmpleadoGetAll " +
                "@Age, @JoiningYear, @ExperienceInCurrentDomain, @LeaveOrNot, " +
                "@IdEducation, @IdCity, @IdGender, @PaymentTier, @IdEverBenched",

                new SqlParameter("@Age", empleado.Age ?? (object)DBNull.Value),
                new SqlParameter("@JoiningYear", empleado.JoiningYear ?? (object)DBNull.Value),
                new SqlParameter("@ExperienceInCurrentDomain",
                                 empleado.ExperienceInCurrentDomain ?? (object)DBNull.Value),
                new SqlParameter("@LeaveOrNot", empleado.LeaveOrnot ?? (object)DBNull.Value),

                new SqlParameter("@IdEducation",
                    empleado.Education?.IdEducation == 0 ? (object)DBNull.Value : empleado.Education?.IdEducation),

                new SqlParameter("@IdCity",
                    empleado.City?.IdCity == 0 ? (object)DBNull.Value : empleado.City?.IdCity),

                new SqlParameter("@IdGender",
                    empleado.Gender?.IdGender == 0 ? (object)DBNull.Value : empleado.Gender?.IdGender),

                new SqlParameter("@PaymentTier",
                    empleado.PaymentTier ?? (object)DBNull.Value),

                new SqlParameter("@IdEverBenched",
                    empleado.EverBenched?.IdEverBenche == 0 ? (object)DBNull.Value : empleado.EverBenched?.IdEverBenche)
            )
            .ToList();

                if (empleados.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var entity in empleados)
                    {
                        ML.Empleado empleadoObj = new ML.Empleado
                        {
                            IdEmpleado = entity.IdEmpleado,
                            Education = new ML.Education { Name = entity.EducationName },
                            JoiningYear = entity.JoiningYear,
                            City = new ML.City { Name = entity.CityName },
                            PaymentTier = entity.PaymentTier,
                            Age = entity.Age,
                            Gender = new ML.Gender { Name = entity.GenderName },
                            EverBenched = new ML.EverBenched { Description = entity.EverBenched },
                            ExperienceInCurrentDomain = entity.ExperienceInCurrentDomain,
                            LeaveOrnot = entity.LeaveORNot
                        };

                        result.Objects.Add(empleadoObj);
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
        public ML.Result GetByID(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var empleadoObj = _context.EmpleadoDTOGBY.FromSqlRaw("EXEC EmpleadoGetById @IdEmpleado", new SqlParameter("@IdEmpleado", IdEmpleado)).AsEnumerable().SingleOrDefault();

                if (empleadoObj != null)
                {
                    ML.Empleado empleado = new ML.Empleado
                    {
                        IdEmpleado = empleadoObj.IdEmpleado,
                        Age = empleadoObj.Age,
                        JoiningYear = empleadoObj.JoiningYear,
                        ExperienceInCurrentDomain = empleadoObj.ExperienceInCurrentDomain,
                        LeaveOrnot = empleadoObj.LeaveORNot,
                        PaymentTier = empleadoObj.PaymentTier,

                        Education = new ML.Education
                        {
                            IdEducation = empleadoObj.IdEducation,
                            Name = empleadoObj.EducationName
                        },
                        City = new ML.City
                        {
                            IdCity = empleadoObj.IdCity,
                            Name = empleadoObj.CityName
                        },
                        Gender = new ML.Gender
                        {
                            IdGender = empleadoObj.IdGender,
                            Name = empleadoObj.GenderName
                        },
                        EverBenched = new ML.EverBenched
                        {
                            IdEverBenche = empleadoObj.IdEverBenche,
                            Description = empleadoObj.EverBenchedDescription
                        }
                    };
                    result.Object = empleado;
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
        public ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var empleadoObj = _context.Empleados
                    .FirstOrDefault(e => e.IdEmpleado == empleado.IdEmpleado);

                if (empleadoObj != null)
                {
                    empleadoObj.Age = empleado.Age;
                    empleadoObj.JoiningYear = empleado.JoiningYear;
                    empleadoObj.ExperienceInCurrentDomain = empleado.ExperienceInCurrentDomain;
                    empleadoObj.LeaveOrnot = empleado.LeaveOrnot;
                    empleadoObj.IdEducation = empleado.Education.IdEducation;
                    empleadoObj.IdCity = empleado.City.IdCity;
                    empleadoObj.IdGender = empleado.Gender.IdGender;
                    empleadoObj.PaymentTier = empleado.PaymentTier;
                    empleadoObj.IdEverBenche = empleado.EverBenched.IdEverBenche;

                    int filasAfectadas = _context.SaveChanges();
                    result.Correct = filasAfectadas > 0;

                    if (!result.Correct)
                        result.ErrorMessage = "Error al actualizar empleado";
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Empleado no encontrado";
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
        public ML.Result Delete(int idEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var empleado = _context.Empleados
                    .FirstOrDefault(e => e.IdEmpleado == idEmpleado);

                if (empleado != null)
                {
                    _context.Empleados.Remove(empleado);
                    result.Correct = _context.SaveChanges() > 0;

                    if (!result.Correct)
                        result.ErrorMessage = "Error al eliminar empleado";
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Empleado no encontrado";
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

        public  ML.Result CargaMasivaCSV(IFormFile archivoCSV)
        {
            ML.Result result = new ML.Result();
            
            try
            {
                using (var reader = new StreamReader(archivoCSV.OpenReadStream()))
                {
                    string linea = reader.ReadLine();
                    int total = 0;
                    while ((linea = reader.ReadLine()) != null)
                    {
                        string[] columnas = linea.Split(',');
                        ML.Empleado empleado = MapearEmpleado(columnas);
                        Add(empleado);
                        total++;
                    }
                    result.Correct = true;
                    result.Object = $"Se cargaron {total} empleados correctamente";
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
        private static ML.Empleado MapearEmpleado(string[] columnas)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Age = int.Parse(columnas[4]);
            empleado.JoiningYear = int.Parse(columnas[1]);
            empleado.ExperienceInCurrentDomain = int.Parse(columnas[7]);
            empleado.PaymentTier = int.Parse(columnas[3]);
            empleado.LeaveOrnot = columnas[8] == "1";

            empleado.Education = new ML.Education
            {
                IdEducation = columnas[0] == "Bachelors" ? 1 : columnas[0] == "Masters" ? 2 : 3
            };
            empleado.City = new ML.City
            {
                IdCity = columnas[2] == "Bangalore" ? 1 : columnas[2] == "Pune" ? 2 : 3
            };
            empleado.Gender = new ML.Gender
            {
                IdGender = columnas[5] == "Male" ? 1 : 2
            };

            empleado.EverBenched = new ML.EverBenched
            {
                IdEverBenche = columnas[6] == "Yes" ? 1 : 2
            };
            return empleado;
        }
    }
}

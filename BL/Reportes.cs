using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Reportes
    {
        private readonly DL.IvelazquezPruebaKpmgContext _context;

        public Reportes(DL.IvelazquezPruebaKpmgContext context)
        {
            _context = context;
        }


        public ML.Result DistribucionPorGenero()
        {
            ML.Result result = new ML.Result();

            try
            {

                var data = _context.EmpleadoGeneroDTO
                                   .FromSqlRaw("EXEC EmpleadoGenero")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos de género.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result DistribucionPorEdad()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoRangoEdadDTO
                                   .FromSqlRaw("EXEC EmpleadoRangoEdad")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos por rango de edad.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result DistribucionPorCiudad()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoCiudadDTO
                                   .FromSqlRaw("EXEC EmpleadoCiudad")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos por ciudad.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result DistribucionNivelEducativo()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoNivelEducativoDTO
                                   .FromSqlRaw("EXEC EmpleadoNivelEducativo")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos de nivel educativo.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result HistorialEverBenched()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoEverBenchedDTO
                                   .FromSqlRaw("EXEC Empleado_EverBenched")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos Ever Benched.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result PrediccionAbandono()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoPrediccionAbandonoDTO
                                   .FromSqlRaw("EXEC Empleado_PrediccionAbandono")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos de abandono.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result PerfilesPorExperienciaDominio(int? minExperiencia, int? dominio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.PerfilExperienciaDominioDTO
                                   .FromSqlRaw(
                                       "EXEC PerfilesPorExperienciaDominio @MinExperiencia = {0}, @Dominio = {1}",
                                       minExperiencia, dominio)
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No se encontraron perfiles.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result ExperienciaVsNivelPago()
        {
            ML.Result result = new ML.Result();

            try
            {
                var data = _context.EmpleadoExperienciaPagoDTO
                                   .FromSqlRaw("EXEC Empleado_Experiencia_Pago")
                                   .ToList();

                result.Objects = data.Cast<object>().ToList();
                result.Correct = data.Any();
                result.ErrorMessage = data.Any() ? null : "No hay datos de experiencia por pago.";
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public ML.Result ExportarReporte(string tipoReporte)
        {
            ML.Result result = new ML.Result();

            try
            {
                switch (tipoReporte)
                {
                    case "DIVERSIDAD":
                        result.Objects = _context.ReporteDiversidadDTO
                            .FromSqlRaw("EXEC ExportarReporte @TipoReporte = {0}", tipoReporte)
                            .Cast<object>().ToList();
                        break;

                    case "ROTACION":
                        result.Objects = _context.ReporteRotacionDTO
                            .FromSqlRaw("EXEC ExportarReporte @TipoReporte = {0}", tipoReporte)
                            .Cast<object>().ToList();
                        break;

                    case "TALENTO":
                        result.Objects = _context.ReporteTalentoDTO
                            .FromSqlRaw("EXEC ExportarReporte @TipoReporte = {0}", tipoReporte)
                            .Cast<object>().ToList();
                        break;
                }

                result.Correct = result.Objects.Any();
                result.ErrorMessage = result.Correct ? null : "No hay datos para el reporte.";
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
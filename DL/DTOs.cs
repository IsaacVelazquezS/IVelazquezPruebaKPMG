using DL;

namespace DL
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public string? EducationName { get; set; }
        public int? JoiningYear { get; set; }
        public string? CityName { get; set; }
        public int? PaymentTier { get; set; }
        public int? Age { get; set; }
        public string? GenderName { get; set; }
        public string? EverBenched { get; set; }
        public int? ExperienceInCurrentDomain { get; set; }
        public bool? LeaveORNot { get; set; }
    }
    public class EmpleadoDTOGBY
    {
        public int IdEmpleado { get; set; }
        public int? Age { get; set; }
        public int? JoiningYear { get; set; }
        public int? ExperienceInCurrentDomain { get; set; }
        public bool? LeaveORNot { get; set; }
        public int? PaymentTier { get; set; }

        public int IdEducation { get; set; }
        public string? EducationName { get; set; }

        public int IdCity { get; set; }
        public string? CityName { get; set; }

        public int IdGender { get; set; }
        public string? GenderName { get; set; }

        public int IdEverBenche { get; set; }
        public string? EverBenchedDescription { get; set; }
    }
    public class EmpleadoGeneroDTO
    {
        public string Genero { get; set; }
        public int Total { get; set; }
    }
    public class EmpleadoRangoEdadDTO
    {
        public string RangoEdad { get; set; }
        public int Total { get; set; }
    }
    public class EmpleadoCiudadDTO
    {
        public string Ciudad { get; set; }
        public int Total { get; set; }
    }
    public class EmpleadoNivelEducativoDTO
    {
        public string NivelEducativo { get; set; }
        public int Total { get; set; }
    }
    public class EmpleadoEverBenchedDTO
    {
        public string EverBenched { get; set; }
        public int Total { get; set; }
    }
    public class EmpleadoPrediccionAbandonoDTO
    {
        public string Genero { get; set; }
        public string Ciudad { get; set; }
        public int TotalEmpleados { get; set; }
        public int TotalAbandonos { get; set; }
    }
    public class ReporteDiversidadDTO
    {
        public string Genero { get; set; }
        public string Ciudad { get; set; }
        public int TotalEmpleados { get; set; }
    }
    public class ReporteRotacionDTO
    {
        public string EstadoEmpleado { get; set; }
        public int Total { get; set; }
    }
    public class ReporteTalentoDTO
    {
        public string NivelEducativo { get; set; }
        public double PromedioExperiencia { get; set; }
        public int TotalEmpleados { get; set; }
    }
    public class PerfilExperienciaDominioDTO
    {
        public int IdEmpleado { get; set; }
        public int Age { get; set; }
        public int JoiningYear { get; set; }
        public int ExperienceInCurrentDomain { get; set; }
        public string EducationName { get; set; }
        public string GenderName { get; set; }
        public string CityName { get; set; }
        public int PaymentTier { get; set; }
        public string EverBenched { get; set; }
    }
    public class EmpleadoExperienciaPagoDTO
    {
        public int NivelPago { get; set; }
        public double PromedioExperiencia { get; set; }
    }
    public class LoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}


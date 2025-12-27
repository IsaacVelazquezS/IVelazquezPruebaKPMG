using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int? Age { get; set; }

    public int? JoiningYear { get; set; }

    public int? ExperienceInCurrentDomain { get; set; }

    public bool? LeaveOrnot { get; set; }

    public int? IdEducation { get; set; }

    public int? IdCity { get; set; }

    public int? IdGender { get; set; }

    public int? PaymentTier { get; set; }

    public int? IdEverBenche { get; set; }

    public virtual City? IdCityNavigation { get; set; }

    public virtual Education? IdEducationNavigation { get; set; }

    public virtual EverBenched? IdEverBencheNavigation { get; set; }

    public virtual Gender? IdGenderNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

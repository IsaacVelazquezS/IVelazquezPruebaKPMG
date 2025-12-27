using System;
using System.Collections.Generic;

namespace DL;

public partial class EverBenched
{
    public int IdEverBenche { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

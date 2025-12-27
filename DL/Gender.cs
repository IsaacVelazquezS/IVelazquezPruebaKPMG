using System;
using System.Collections.Generic;

namespace DL;

public partial class Gender
{
    public int IdGender { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

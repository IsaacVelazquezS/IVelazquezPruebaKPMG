using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public int? Age { get; set; }
        public int? JoiningYear { get; set; }
        public int? ExperienceInCurrentDomain { get; set; }
        public bool? LeaveOrnot { get; set; }
        public int? PaymentTier { get; set; }
        public List<object>? Empleados { get; set; }
        public Education? Education { get; set; }
        public City? City { get; set; }
        public Gender? Gender { get; set; }
        public EverBenched? EverBenched { get; set; }
        //public Rol? Rol { get; set; }
    }
}

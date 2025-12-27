using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
        public class Usuario
        {
            public int IdUsuario { get; set; }
            public string Username { get; set; } 
            public string PasswordHash { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public Empleado Empleado { get; set; }
            public Rol Rol { get; set; }
        }
    
}

# Sistema de Gestión de Empleados y Usuarios – .NET Core

Proyecto desarrollado en **.NET Core 8 con C#**, implementando una **arquitectura en N capas**, autenticación mediante **JWT**, acceso a datos con **Entity Framework Core 8** y **Stored Procedures**, así como un **Dashboard interactivo con Chart.js** para la visualización de información.

El sistema permite la gestión de **empleados y usuarios**, autenticación segura, **carga masiva de datos**, y generación de reportes gráficos basados en información almacenada en **SQL Server**.

---

##  Arquitectura

El proyecto está estructurado bajo una **arquitectura en N capas**, garantizando escalabilidad, mantenimiento y separación de responsabilidades:

- **PL (Presentation Layer)**  
  ASP.NET Core MVC, Razor Views, Bootstrap, AJAX y consumo de API protegida con JWT.
  
- **BL (Business Layer)**  
  Lógica de negocio, validaciones y reglas del sistema.
  
- **DL (Data Layer)**  
  Entity Framework Core 8, DbContext, entidades ,DTOs y ejecución de Stored Procedures.
  
- **ML (Model Layer)**  
   modelos de transferencia de datos.

---

##  Modelo de Base de Datos

Base de datos: **IVelazquezPruebaKPMG**

### Entidades principales:

### **Empleado**
- IdEmpleado (PK)
- Age
- JoiningYear
- ExperienceInCurrentDomain
- LeaveORNot
- PaymentTier
- IdCity (FK)
- IdEducation (FK)
- IdGender (FK)
- IdEverBenche (FK)

### **Usuario**
- IdUsuario (PK)
- Username
- PasswordHash
- Email
- IsActive
- IdEmpleado (FK)
- IdRol (FK)

### **Catálogos**
- **Rol** (IdRol, Name)
- **City** (IdCity, Name)
- **Education** (IdEducation, Name)
- **Gender** (IdGender, Name)
- **EverBenched** (IdEverBenche, Description)

Las relaciones están definidas mediante **llaves foráneas** y son consumidas a través de **Entity Framework Core** y **Stored Procedures**.

---

##  Funcionalidades

###  Autenticación
- Login de usuarios con **JWT**
- Validación de credenciales
- Contraseñas encriptadas
- Control de acceso a vistas y API

###  Gestión de Usuarios
- Alta, edición, eliminación y consulta
- Asociación con empleados y roles
- Tabla de usuarios con opciones de edición y eliminación

### Gestión de Empleados
- CRUD completo
- **Carga masiva de empleados desde archivo CSV**
- Validaciones en cliente y servidor

###  Dashboard y Reportes
Gráficas dinámicas desarrolladas con **Chart.js**, alimentadas desde **Stored Procedures**, que muestran:

- Distribución de empleados por **género**
- Distribución por **rango de edad**
- Empleados por **ciudad**
- Nivel **educativo**
- Indicadores de permanencia y rotación

---

## Tecnologías Utilizadas

### Backend
- .NET Core 8
- C#
- Entity Framework Core 8
- JWT (JSON Web Token)
- Stored Procedures

### Frontend
- ASP.NET Core MVC
- Razor Views
- Bootstrap
- JavaScript
- jQuery
- AJAX
- Chart.js

### Base de Datos
- SQL Server

---

##  Configuración del Proyecto

1. Restaurar la base de datos **IVelazquezPruebaKPMG** en SQL Server.
2. Configurar la cadena de conexión en el archivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=IVelazquezPruebaKPMG;User Id=USUARIO;Password=PASSWORD;"
   }

##Credenciales de ACCESO
Credenciales 
RRHH 
usuario : isaac
Contraseña : PassWord1

Gerente 
usuario : isaac02
Contraseña : PassWord1

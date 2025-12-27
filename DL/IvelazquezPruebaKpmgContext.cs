using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class IvelazquezPruebaKpmgContext : DbContext
{
    public IvelazquezPruebaKpmgContext()
    {
    }

    public IvelazquezPruebaKpmgContext(DbContextOptions<IvelazquezPruebaKpmgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EverBenched> EverBencheds { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    //DTOs
    public virtual DbSet<EmpleadoDTOGBY> EmpleadoDTOGBY { get; set; }
    public virtual DbSet<EmpleadoDTO> EmpleadoDTO { get; set; }
    public virtual DbSet<EmpleadoGeneroDTO> EmpleadoGeneroDTO { get; set; }
    public virtual DbSet<EmpleadoRangoEdadDTO> EmpleadoRangoEdadDTO { get; set; }
    public virtual DbSet<EmpleadoCiudadDTO> EmpleadoCiudadDTO { get; set; }
    public virtual DbSet<EmpleadoNivelEducativoDTO> EmpleadoNivelEducativoDTO { get; set; }
    public virtual DbSet<EmpleadoEverBenchedDTO> EmpleadoEverBenchedDTO { get; set; }
    public virtual DbSet<EmpleadoPrediccionAbandonoDTO> EmpleadoPrediccionAbandonoDTO { get; set; }
    public virtual DbSet<ReporteDiversidadDTO> ReporteDiversidadDTO { get; set; }
    public virtual DbSet<ReporteRotacionDTO> ReporteRotacionDTO { get; set; }
    public virtual DbSet<ReporteTalentoDTO> ReporteTalentoDTO { get; set; }
    public virtual DbSet<PerfilExperienciaDominioDTO> PerfilExperienciaDominioDTO { get; set; }
    public virtual DbSet<EmpleadoExperienciaPagoDTO> EmpleadoExperienciaPagoDTO { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=IVelazquezPruebaKPMG;TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmpleadoDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EmpleadoDTOGBY>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EmpleadoGeneroDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoRangoEdadDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoCiudadDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoNivelEducativoDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoEverBenchedDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoPrediccionAbandonoDTO>().HasNoKey();
        modelBuilder.Entity<ReporteDiversidadDTO>().HasNoKey();
        modelBuilder.Entity<ReporteRotacionDTO>().HasNoKey();
        modelBuilder.Entity<ReporteTalentoDTO>().HasNoKey();
        modelBuilder.Entity<PerfilExperienciaDominioDTO>().HasNoKey();
        modelBuilder.Entity<EmpleadoExperienciaPagoDTO>().HasNoKey();

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("PK__City__394B023A9C5E7E4F");

            entity.ToTable("City");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.IdEducation).HasName("PK__Educatio__5E748C7EB510F417");

            entity.ToTable("Education");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EA6E358A6");

            entity.ToTable("Empleado");

            entity.Property(e => e.LeaveOrnot).HasColumnName("LeaveORNot");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK__Empleado__IdCity__1B0907CE");

            entity.HasOne(d => d.IdEducationNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEducation)
                .HasConstraintName("FK__Empleado__IdEduc__1A14E395");

            entity.HasOne(d => d.IdEverBencheNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEverBenche)
                .HasConstraintName("FK__Empleado__IdEver__1DE57479");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdGender)
                .HasConstraintName("FK__Empleado__IdGend__1BFD2C07");
        });

        modelBuilder.Entity<EverBenched>(entity =>
        {
            entity.HasKey(e => e.IdEverBenche).HasName("PK__EverBenc__9C668F0AE5402A09");

            entity.ToTable("EverBenched");

            entity.Property(e => e.Description)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender).HasName("PK__Gender__0042D43B0A4EFBA0");

            entity.ToTable("Gender");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CD2BEEADB");

            entity.ToTable("Rol");

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9759678AED");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Username, "UQ__Usuario__536C85E4E1377F98").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534F2D48657").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__4222D4EF");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

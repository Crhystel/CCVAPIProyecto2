using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CCVAPIProyecto2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
        base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<ActividadEstudiante> ActividadEstudiantes { get; set; }
        public DbSet<ActividadProfesor> ActividadProfesores { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<ClaseActividad> ClaseActividades { get; set; }
        public DbSet<ClaseEstudiante> ClaseEstudiantes { get; set; }
        public DbSet<ClaseProfesor> ClaseProfesores { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Administrador> Administrador { get; set; } = default!;
        public DbSet<Materia> Materias { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Profesor>().ToTable("Profesor");
            modelBuilder.Entity<Administrador>().ToTable("Administrador");
            //modelBuilder.Entity<ClaseProfesor>()
            //    .HasKey(c => c.Id);
            //modelBuilder.Entity<ClaseProfesor>()
            //    .HasOne(c => c.Profesor)
            //    .WithMany(c => c.ClaseProfesores)
            //    .HasForeignKey(c => c.ProfesorId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<ClaseProfesor>()
            //    .HasOne(c => c.ClaseP)
            //    .WithMany(c => c.ClaseProfesores)
            //    .HasForeignKey(c => c.ClasePId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ClaseActividad>()
            //    .HasKey(c => c.Id);
            //modelBuilder.Entity<ClaseActividad>()
            //    .HasOne(c => c.Actividad)
            //    .WithMany(c => c.ClaseActividades)
            //    .HasForeignKey(c => c.ActividadId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<ClaseActividad>()
            //    .HasOne(c => c.Clase)
            //    .WithMany(c => c.ClaseActividades)
            //    .HasForeignKey(c => c.ClaseId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<ActividadEstudiante>()
            //    .HasKey(c => c.Id);
            //modelBuilder.Entity<ActividadEstudiante>()
            //    .HasOne(c => c.Estudiante)
            //    .WithMany(c => c.ActividadEstudiantes)
            //    .HasForeignKey(c => c.EstudianteId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<ActividadEstudiante>()
            //    .HasOne(c => c.Actividad)
            //    .WithMany(c => c.ActividadEstudiantes)
            //    .HasForeignKey(c => c.ActividadId);
            //modelBuilder.Entity<ActividadProfesor>()
            //    .HasKey(c => c.Id);
            //modelBuilder.Entity<ActividadProfesor>()
            //    .HasOne(c => c.Profesor)
            //    .WithMany(c => c.ActividadProfesores)
            //    .HasForeignKey(c => c.ProfesorId);
            //modelBuilder.Entity<ActividadProfesor>()
            //    .HasOne(c => c.Actividad)
            //    .WithMany(c => c.ActividadProfesores)
            //    .HasForeignKey(c => c.ActividadId);
            //modelBuilder.Entity<Profesor>()
            //    .Property(p => p.Materia)
            //    .HasConversion<string>();
            //modelBuilder.Entity<Estudiante>()
            //    .Property(p => p.Grado)
            //    .HasConversion<string>();
            modelBuilder.Entity<Administrador>().HasData(new Administrador
            {
                Id = 1,
                Cedula = "1234567890",
                Nombre = "Roberto",
                NombreUsuario = "admin",
                Contrasenia = "admin",
                Edad = 30,
                Rol = RolEnum.Administrador,
            });
            modelBuilder.Entity<Estudiante>().HasData(new Estudiante
            {
                Id = 2,
                Cedula = "0111111111",
                Nombre = "Crhystel",
                NombreUsuario = "crhys",
                Contrasenia = "crhys",
                Edad = 19,
                Grado = GradoEnum.Primer_Bachillerato_BGU,
                Rol = RolEnum.Estudiante,

            });
            modelBuilder.Entity<Profesor>().HasData(new Profesor
            {
                Id = 3,
                Cedula = "0111111122",
                Nombre = "Yuliana",
                NombreUsuario = "yuli",
                Contrasenia = "yuli",
                Edad = 19,
                Materia= MateriaEnum.Matematicas,
                Rol = RolEnum.Profesor,

            });

            modelBuilder.Entity<ActividadEstudiante>()
                .HasOne(ae => ae.Actividad)
                .WithMany(a => a.ActividadEstudiantes)
                .HasForeignKey(ae => ae.ActividadId);

            modelBuilder.Entity<ActividadEstudiante>()
                .HasOne(ae => ae.Estudiante)
                .WithMany(e => e.ActividadEstudiantes)
                .HasForeignKey(ae => ae.EstudianteId);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AdminApp.Models
{
    public partial class Registro : DbContext
    {
        public Registro()
            : base("name=Registro")
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .Property(e => e.dni_alu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.nombre_alu)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.apellidos_alu)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .HasMany(e => e.Seccion)
                .WithMany(e => e.Alumno)
                .Map(m => m.ToTable("Detalle_asig_alumno_seccion").MapLeftKey("id_alu").MapRightKey("id_sec"));

            modelBuilder.Entity<Curso>()
                .Property(e => e.descripcion_cur)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Seccion)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);
        }
    }
}

namespace AdminApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            Seccion = new HashSet<Seccion>();
        }

        [Key]
        public int id_cur { get; set; }

        [Required]
        [StringLength(25)]
        public string descripcion_cur { get; set; }

        public bool estado_cur { get; set; }
        public string horario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seccion> Seccion { get; set; }

        //---------M�TODOS---------------------------------------

        public List<Curso> listar()
        {
            var cursos = new List<Curso>();
            string cadena = "SELECT * FROM CURSO";
            try
            {
                using (var contenedor = new Registro())
                {
                    cursos = contenedor.Database.SqlQuery<Curso>(cadena).ToList();
                }
            }
            catch (Exception)
            {

                // throw;
            }
            return cursos;
        }

        public Boolean Insertar(string des, bool est)
        {
            bool estado = false;
            string cadena = "'" + des + "',";
            cadena = cadena + "'" + est + "'";

            try
            {
                using (var cnx = new Registro())
                {
                    int r = cnx.Database.ExecuteSqlCommand("INSERT INTO CURSO VALUES (" + cadena + ")");
                    if (r == 1)
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception)
            {
                estado = false;
                //throw;
            }
            return estado;
        }

        public Curso un_registro(int id)
        {
            var registro = new Curso();
            try
            {
                using (var cnx = new Registro())
                {
                    registro = cnx.Curso.Where(a => a.id_cur == id).Single();
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return registro;
        }

        public Boolean Actualizar(int id, string des, bool est)
        {
            bool estado = false;
            string cadena = "descripcion_cur='" + des + "', estado_cur='" + est + "'";
            try
            {
                using (var cnx = new Registro())
                {
                    int r = cnx.Database.ExecuteSqlCommand("UPDATE CURSO SET " + cadena + "WHERE id_cur=" + id);
                    if (r == 1)
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception)
            {
                estado = false;
                //throw;
            }
            return estado;
        }

        public Boolean Eliminar(int id)
        {
            bool estado = false;
            try
            {
                using (var cnx = new Registro())
                {
                    int r = cnx.Database.ExecuteSqlCommand("DELETE FROM CURSO WHERE id_cur =" + id);
                    if (r == 1)
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception)
            {
                estado = false;
                //throw;
            }
            return estado;
        }
    }
}

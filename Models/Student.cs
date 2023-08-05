using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT3045C_FinalProject.Models
{
    public class Student
    {
        [Key, Range(1, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        [StringLength(100)]
        public string Program { get; set; }
        public SchoolYear Year { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual Favorites Favorites { get; set; }
        public virtual Opinions Opinions { get; set; }
    }

    public class StudentDTO
    {
        [Key, Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        [StringLength(100)]
        public string Program { get; set; }
        public SchoolYear Year { get; set; }
    }

    public class NewStudentDTO
    {
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        [StringLength(100)]
        public string Program { get; set; }
        public SchoolYear Year { get; set; }
    }

    public enum SchoolYear
    {
        Freshman = 1,
        Sophomore,
        Junior,
        Senior,
        Graduate
    }
}

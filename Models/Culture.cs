using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT3045C_FinalProject.Models
{
    public class Culture
    {
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        [StringLength(30)]
        public string BirthCountry { get; set; }
        [StringLength(30)]
        public string Nationality { get; set; }
        [StringLength(30)]
        public string FirstLanguage { get; set; }
        [StringLength(30)]
        public string PrimaryLanguage { get; set; }
        public virtual Student Student { get; set; }
    }

    public class CultureDTO
    {
        [Key, Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        [StringLength(30)]
        public string BirthCountry { get; set; }
        [StringLength(30)]
        public string Nationality { get; set; }
        [StringLength(30)]
        public string FirstLanguage { get; set; }
        [StringLength(30)]
        public string PrimaryLanguage { get; set; }
    }
}

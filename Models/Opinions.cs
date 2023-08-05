using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT3045C_FinalProject.Models
{
    public class Opinions
    {
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        public bool IOSOverAdroid { get; set; }
        public bool PineappleOnPizza { get; set; }
        public bool CatsOverDogs { get; set; }
        public bool PlaystationOverXbox { get; set; }
        public virtual Student Student { get; set; }

    }

    public class OpinionsDTO
    {
        [Key, Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        public bool IOSOverAdroid { get; set; }
        public bool PineappleOnPizza { get; set; }
        public bool CatsOverDogs { get; set; }
        public bool PlaystationOverXbox { get; set; }
    }
}

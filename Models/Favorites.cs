using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT3045C_FinalProject.Models
{
    public class Favorites
    {
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        [StringLength(30)]
        public string FavoriteStore { get; set; }
        [StringLength(50)]
        public string FavoriteHobby { get; set; }
        [StringLength(30)]
        public string FavoriteAnimal { get; set; }
        public Season FavoriteSeason { get; set; }
        public virtual Student Student { get; set; }
    }
    public class FavoritesDTO
    {
        [Key, Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        [StringLength(30)]
        public string FavoriteStore { get; set; }
        [StringLength(50)]
        public string FavoriteHobby { get; set; }
        [StringLength(30)]
        public string FavoriteAnimal { get; set; }
        public Season FavoriteSeason { get; set; }
    }

    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
}

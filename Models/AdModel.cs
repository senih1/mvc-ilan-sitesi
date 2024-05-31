using System.ComponentModel.DataAnnotations;

namespace ilan_sitesi.Models
{
    public class Ad
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public string Image { get; set; }
    }
}

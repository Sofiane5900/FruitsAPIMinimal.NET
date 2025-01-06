using System.ComponentModel.DataAnnotations;

namespace FruitsAPI.NET
{
    public class Fruits
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string? Nom { get; set; }

        [Required]
        public string? Saison { get; set; }
    }
}

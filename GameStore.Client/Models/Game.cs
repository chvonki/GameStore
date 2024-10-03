using System.ComponentModel.DataAnnotations;

namespace GameStore.Client.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = null!;

        [Required]
        public string? Genre { get; set; }

        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
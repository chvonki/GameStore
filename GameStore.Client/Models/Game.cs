using System.ComponentModel.DataAnnotations;

namespace GameStore.Client.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название игры не указано")]
        [StringLength(100, ErrorMessage = "Слишком длинное название")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Жанр не выбран")]
        [StringLength(30, ErrorMessage = "Слишком длинное название жанра")]
        public string? Genre { get; set; }

        [Range(0.0, 150.0, ErrorMessage = "Цена должна быть от 0 до 150")]
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
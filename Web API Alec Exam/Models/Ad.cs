using System.ComponentModel.DataAnnotations;

namespace Web_API_Alec_Exam.Models
{
    public class Ad
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100_000)]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

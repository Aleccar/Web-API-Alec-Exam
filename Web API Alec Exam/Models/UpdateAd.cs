using System.ComponentModel.DataAnnotations;

namespace Web_API_Alec_Exam.Models
{
    public class UpdateAd
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100_000)]
        public decimal Price { get; set; } = decimal.Zero;
    }
}

using System.ComponentModel.DataAnnotations;

namespace InvestmentGameAPI.Models
{
    public class CompanyPriceHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Company Company { get; set; }
    }
}

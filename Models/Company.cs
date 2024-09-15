using System.ComponentModel.DataAnnotations;

namespace InvestmentGameAPI.Models
{
    public class Company
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;  // Varsayılan değer olarak boş string

        [Required(ErrorMessage = "Current stock price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Stock price must be greater than 0.")]
        public decimal CurrentStockPrice { get; set; }

        [Required(ErrorMessage = "Total shares are required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total shares must be at least 1.")]
        public int TotalShares { get; set; }

        [Required(ErrorMessage = "Market cap is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Market cap must be greater than 0.")]
        public decimal MarketCap { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[0]; // Varsayılan değer olarak boş byte dizisi

        public ICollection<UserCompanyOwnership> UserCompanyOwnerships { get; set; } = new List<UserCompanyOwnership>(); // Boş liste ile başlat
    }
}

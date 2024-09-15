using System.ComponentModel.DataAnnotations;

namespace InvestmentGameAPI.Models
{
    public class UserCompanyOwnership
    {
        [Required(ErrorMessage = "User Id is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Company Id is required.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Number of shares is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of shares must be at least 1.")]
        public int NumberOfShares { get; set; }

        // İlişkiler
        public User User { get; set; } = new User();  // Varsayılan olarak yeni bir User başlat
        public Company Company { get; set; } = new Company();  // Varsayılan olarak yeni bir Company başlat
    }
}

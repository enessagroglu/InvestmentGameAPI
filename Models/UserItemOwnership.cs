using System.ComponentModel.DataAnnotations;

namespace InvestmentGameAPI.Models
{
    public class UserItemOwnership
    {
        [Required(ErrorMessage = "User Id is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Item Id is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; }

        // İlişkiler
        public User User { get; set; } = new User();  // Varsayılan olarak yeni bir User başlat
        public InGameItem InGameItem { get; set; } = new InGameItem();  // Varsayılan olarak yeni bir InGameItem başlat
    }
}

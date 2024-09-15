using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InvestmentGameAPI.Models
{
    public class User
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; } = string.Empty;  // Varsayılan değer olarak boş string

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }

        public ICollection<UserCompanyOwnership> UserCompanyOwnerships { get; set; } = new List<UserCompanyOwnership>(); // Boş liste ile başlat
        public ICollection<UserItemOwnership> UserItemOwnerships { get; set; } = new List<UserItemOwnership>();  // Boş liste ile başlat
    }
}

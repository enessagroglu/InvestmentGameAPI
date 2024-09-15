using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InvestmentGameAPI.Models
{
    public class InGameItem
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name cannot be longer than 100 characters.")]
        public string ItemName { get; set; } = string.Empty;  // Varsayılan değer olarak boş string

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        // Bu öğeyi satın alan kullanıcılar
        public ICollection<UserItemOwnership> UserItemOwnerships { get; set; } = new List<UserItemOwnership>();  // Boş liste ile başlat
    }
}

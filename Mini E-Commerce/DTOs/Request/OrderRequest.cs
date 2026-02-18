using Mini_E_Commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce.DTOs.Request
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "this is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "this is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "this is required")]
        public int Phone { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new();

    }
}

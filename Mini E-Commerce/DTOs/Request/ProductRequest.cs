using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce.DTOs.Request
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }   
        [Range(0.01, int.MaxValue, ErrorMessage = "The Price must br greater than 0")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The Quantity must br greater than 0")]
         public int Quantity { get; set; }
    }
}

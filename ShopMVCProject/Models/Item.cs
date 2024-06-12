using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVCProject.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; } 

        public int ProductId { get; set; } 

        [ForeignKey("ProductId")]
        public Product Product { get; set; } 

        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; } 

        [ForeignKey("ShoppingCartId")]

        public ShoppingCart ShoppingCart { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVCProject.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public List<Item>? Items { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

    }
}

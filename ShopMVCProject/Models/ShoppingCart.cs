using System.ComponentModel.DataAnnotations;

namespace ShopMVCProject.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        //public int UserId {  get; set; }

    }
}

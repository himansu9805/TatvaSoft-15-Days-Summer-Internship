using BookStore.Models.ViewModels;

namespace BookStore.Models.Models
{
    public class CartModel
    {
        public CartModel() { }

        public CartModel(Cart c)
        {
            Id = c.Id;
            UserId = c.Userid;
            BookId = c.Bookid;
            Quantity = c.Quantity;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }


    }
}

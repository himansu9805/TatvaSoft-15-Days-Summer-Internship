using BookStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Models
{
    public class BookModel
    {

        public BookModel() { }

        public BookModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Price = book.Price;
            Description = book.Description;
            Base64image = book.Base64image;
            Categoryid = book.Categoryid;
            Publisherid = book.Publisherid;
            Quantity = book.Quantity;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Base64image { get; set; }
        [Required]
        public int Categoryid { get; set; }
        [Required]
        public int? Publisherid { get; set; }
        [Required]
        public int? Quantity { get; set; }
    }
}

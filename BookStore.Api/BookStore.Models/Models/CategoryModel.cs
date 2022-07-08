using BookStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public CategoryModel() { }
        public CategoryModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}

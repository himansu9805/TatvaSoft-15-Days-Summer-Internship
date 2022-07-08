using BookStore.Models.ViewModels;

namespace BookStore.Repository
{
    public class BaseRepository
    {
        protected BookStoreContext _context = new BookStoreContext();
    }
}

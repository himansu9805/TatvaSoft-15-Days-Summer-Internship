using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public ListResponse<Cart> GetCartItems(int id)
        {
            var query = _context.Carts.Include(c => c.Book).Include(c => c.User).Where(c => c.Userid == id).AsQueryable();
            int totalRecords = query.Count();
            List<Cart> cart = query.ToList();

            return new ListResponse<Cart>
            {
                records = cart,
                totalRecords = totalRecords,
            };
        }

        public Cart AddCart(CartModel cart)
        {
            Cart cartItem = new Cart()
            {
                Quantity = cart.Quantity,
                Bookid = cart.BookId,
                Userid = cart.UserId,

            };
            var result = _context.Carts.FirstOrDefault(c => c.Bookid == cart.BookId && c.Userid == cart.UserId);
            if (result == null)
            {
                var entry = _context.Carts.Add(cartItem);
                _context.SaveChanges();
                return entry.Entity;
            }
            else
            {
                return null;
            }
        }

        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return false;
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;

        }
    }
}

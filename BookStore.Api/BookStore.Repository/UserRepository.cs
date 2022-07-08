using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {

        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Users.Where(c => keyword == null || c.Firstname.ToLower().Contains(keyword) || c.Lastname.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<User> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<User>()
            {
                records = categories,
                totalRecords = totalRecords
            };
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(c => c.Id == id);
        }

        public User Login(LoginModel model)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(model.email) && c.Password.Equals(model.password));
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Password = model.Password,
                Roleid = model.Roleid
            };
            var result = _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email));
            if(result == null)
            {
                var entry = _context.Users.Add(user);
                _context.SaveChanges();
                return entry.Entity;
            }
            else
            {
                return null;
            }
        }

        public User UpdateUser(User model)
        {
            var entry = _context.Update(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public ListResponse<Role> GetRoles()
        {
            var query = _context.Roles.AsQueryable();
            int totalRecords = query.Count();
            List<Role> roles = query.ToList();
            return new ListResponse<Role>()
            {
                records = roles,
                totalRecords = totalRecords
            };
        }
    }
}

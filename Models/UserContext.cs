using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            //LoadDefaultUsers();
        }

        public List<User> GetUsers() {

           return Users.ToList();

        }

        //private void LoadDefaultUsers()
        //{
        //    Users.Add(new User { Name = "qwer", Email = "qwer@gmail.com" });
        //    Users.Add(new User { Name = "aasdf", Email = "aasdf@gmail.com" });
        //}

        public void AddUser(User user)
        {
            Users.Add(user);
            this.SaveChanges();
            return;
        }

        internal void DeleteUser(long id)
        {
            User user = Users.Find(id);

            Users.Remove(user);

            this.SaveChanges();
            return;
        }

        internal void UpdateUser(User user)
        {
            Users.Update(user);
            this.SaveChanges();
            return;
        }
    }
}

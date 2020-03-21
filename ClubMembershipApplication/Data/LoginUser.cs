using System;
using System.Collections.Generic;
using System.Text;
using ClubMembershipApplication.Models;
using System.Linq;

namespace ClubMembershipApplication.Data
{
    public class LoginUser : ILogin
    {
        public User Login(string emailAddress, string password)
        {
            User user = null;

            using (var dbContext = new ClubMembershipDbContext())
            {
                user = dbContext.Users.FirstOrDefault(u => u.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower() && u.Password.Equals(password));
            }
            return user;
        }
    }
}

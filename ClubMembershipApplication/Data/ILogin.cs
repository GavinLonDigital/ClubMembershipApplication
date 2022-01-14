using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Data
{
    public interface ILogin
    {
        User Login(string emailAddress, string password);

    }
}

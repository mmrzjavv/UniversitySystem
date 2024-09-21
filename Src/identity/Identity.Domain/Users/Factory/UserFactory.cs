using Identity.Domain.Users.ValueObject;

namespace Identity.Domain.Users.Factory
{
    public class UserFactory 
    {
        public Users CreateUser(string fullName, MobileNumber mobileNo, string username, string password, Guid roleId)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            return new Users(fullName, mobileNo, username, password, roleId); 
        }

    }
}

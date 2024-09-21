using Identity.Domain.Roles.Factory;
using Identity.Domain.Users.Factory;
using Identity.Domain.Users.ValueObject;
using Identity.infrastructure.Services;

namespace Identity.Application.Users
{
    public class UserApplicationService
    {
        private readonly IRepository<Domain.Users.Users> userRepository;
        private readonly IRepository<Domain.Roles.Roles> roleRepository;
        private readonly UserFactory userFactory;

        public UserApplicationService(IRepository<Domain.Users.Users> userRepository, IRepository<Domain.Roles.Roles> roleRepository, UserFactory userFactory )
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userFactory = userFactory;
        }

        public async Task CreateUserAsync(string fullName, string mobileNo, string username, string password, Guid roleId)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            var mobileNumber = new MobileNumber(mobileNo); 
            var user = userFactory.CreateUser(fullName, mobileNumber, username, password, roleId);
            await userRepository.AddAsync(user);
        }


        public async Task<IEnumerable<Domain.Users.Users>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<Domain.Users.Users?> GetUserByIdAsync(Guid id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task UpdateUserAsync(Domain.Users.Users user)
        {
            await userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await userRepository.DeleteAsync(user);
            }
        }
    }
}

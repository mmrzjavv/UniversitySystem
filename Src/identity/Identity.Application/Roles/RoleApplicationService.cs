using Identity.Domain.Roles.Factory;
using Identity.infrastructure.Services;

namespace Identity.Application.Roles
{
    public class RoleApplicationService
    {
        private readonly IRepository<Domain.Roles.Roles> roleRepository;
        private readonly RoleFactory roleFactory;

        public RoleApplicationService(IRepository<Domain.Roles.Roles> roleRepository, RoleFactory roleFactory)
        {
            this.roleRepository = roleRepository;
            this.roleFactory = roleFactory;
        }

        public async Task CreateRoleAsync(string displayName, string description)
        {
            var role = roleFactory.CreateRole(displayName, description);
            await roleRepository.AddAsync(role);
        }

        public async Task<IEnumerable<Domain.Roles.Roles>> GetAllRole()
        {
            var roles = await roleRepository.GetAllAsync();
            if (roles is null)
            {
                throw new ArgumentNullException(nameof(roles), "Role Repository is null");
            }
            return roles;
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await roleRepository.GetByIdAsync(id);
            if (role != null)
            {
                await roleRepository.DeleteAsync(role);
            }
        }
    }
}

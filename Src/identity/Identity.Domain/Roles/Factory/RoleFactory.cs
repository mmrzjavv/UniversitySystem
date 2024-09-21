namespace Identity.Domain.Roles.Factory;

public class RoleFactory
{
    public Roles CreateRole(string displayName, string description)
    {
        return new Roles(displayName, description);
    }
}
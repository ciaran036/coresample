using FluentValidation;

namespace Entities.Configurations
{
    public class UserRoleConfiguration : AbstractValidator<UserRole>
    {
        public UserRoleConfiguration()
        {
            RuleFor(ur => ur.UserId).NotEmpty();
            RuleFor(ur => ur.RoleId).NotEmpty();
        }
    }
}

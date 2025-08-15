using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Users
{
    public static class UserExtensions
    {
        public static UserResponse ToDto(this User user)
        {
            return new UserResponse(
                user.Id.Value,
                user.Name,
                user.Contact.Email,
                user.Contact.PhoneNumber,
                user.Role,
                user.TenantId.Value
            );
        }

        public static List<UserResponse> ToDto(this IEnumerable<User> users)
        {
            return users
                .Select(ToDto)
                .ToList();
        }
    }
}

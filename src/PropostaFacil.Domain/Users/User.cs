using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Rules;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users
{
    public class User : Aggregate<UserId>
    {
        public static User Create(string name, Contact contact, string passwordHash, UserRoleEnum role, TenantId tenantId, IPasswordHash hasher)
        {
            return new User {
                Id = UserId.Of(Guid.NewGuid()),
                Name = name,
                Contact = contact,
                PasswordHash = hasher.HashPassword(passwordHash),
                Role = role,
                TenantId = tenantId,
                Verified = false,
                VerifiedToken = Guid.NewGuid().ToString(),
                Active = null
            };
        }
        public string Name { get; private set; } = default!;
        public Contact Contact { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserRoleEnum Role { get; private set; } = default!;

        public bool? Active { get; private set; } = default!;

        public bool Verified { get; private set; } = default!;

        public string? VerifiedToken { get; private set; } = default!;

        public string? ForgottenToken { get; set; } = default!;

        public DateTimeOffset? ForgottenTokenExpiry { get; set; } = default!;

        public TenantId? TenantId { get; private set; } = default!;
        public Tenant Tenant { get; private set; } = default!;

        public ICollection<RefreshToken> RefreshTokens { get; private set; } = default!;

        public bool CanUserLogin(string password, IPasswordCheck checker)
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustBeActiveRule(this));
            return checker.Matches(password, PasswordHash);
        }

        public void ForgotPassword()
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustBeActiveRule(this));
            ForgottenToken = Guid.NewGuid().ToString();
            ForgottenTokenExpiry = DateTime.Now + TimeSpan.FromHours(24);
        }

        public void ResetPassword(string token, string password, IPasswordHash hasher)
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustBeActiveRule(this));
            CheckRule(new MustHaveForgotPasswordRule(this));
            CheckRule(new ForgotTokenMustMatchRule(this, token));
            CheckRule(new ForgotTokenMustNotBeExpiredRule(this));
            ForgottenToken = null;
            ForgottenTokenExpiry = null;
            PasswordHash = hasher.HashPassword(password);
        }

        public void SetRole(UserRoleEnum role)
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustBeActiveRule(this));
            Role = role;
        }

        public void VerifyAndActivate(string verification)
        {
            CheckRule(new MustNotBeVerifiedRule(this));
            CheckRule(new VerificationTokenMustMatchRule(this, verification));
            Verified = true;
            VerifiedToken = null;
            Active = true;
        }

        public void Activate()
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustNotBeActiveRule(this));
            Active = true;
        }

        public void Deactivate(IUserContext context)
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustNotBeSelfRule(this, context));
            CheckRule(new MustBeActiveRule(this));
            Active = false;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangePassword(string oldPassword, string newPassword, IPasswordCheck checker, IPasswordHash hasher)
        {
            CheckRule(new MustBeVerifiedRule(this));
            CheckRule(new MustBeActiveRule(this));
            CheckRule(new PasswordMustMatchRule(this, oldPassword, checker));
            PasswordHash = hasher.HashPassword(newPassword);
        }

        public void ChangeTenant(TenantId organisationId)
        {
            TenantId = organisationId;
        }
    }
}

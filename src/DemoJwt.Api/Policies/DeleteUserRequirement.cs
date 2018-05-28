using Microsoft.AspNetCore.Authorization;

namespace DemoJwt.Api.Policies
{
    public class DeleteUserRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }

        public DeleteUserRequirement(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }
}
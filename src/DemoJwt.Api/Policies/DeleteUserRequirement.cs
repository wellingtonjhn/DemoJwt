using Microsoft.AspNetCore.Authorization;

namespace DemoJwt.Api.Policies
{
    public class DeleteUserRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }

        public DeleteUserRequirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}
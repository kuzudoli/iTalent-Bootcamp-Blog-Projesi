using Microsoft.AspNetCore.Authorization;

namespace iTalentBootcamp_Blog.Identity.Requirements
{
    public class ViolenceRequirement : IAuthorizationRequirement
    {
        public int ThresholdAge { get; set; }

        public ViolenceRequirement(int thresholdAge)
        {
            ThresholdAge = thresholdAge;
        }
    }

    public class ViolenceRequirementHandler : AuthorizationHandler<ViolenceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViolenceRequirement requirement)
        {
            var hasBirthDayClaim = context.User.HasClaim(x => x.Type == "birthday");
            if (!hasBirthDayClaim)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var birthDayClaim = context.User.FindFirst("birthday")!;
            var birthDay = Convert.ToDateTime(birthDayClaim.Value);

            var today = DateTime.Now;
            var age = today.Year - birthDay.Year;

            if (birthDay > today.AddYears(-age))
                age--;

            if (age < requirement.ThresholdAge)
                context.Fail();
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace iTalentBootcamp_Blog.Identity.Requirements
{
    public class ExchangeExpireRequirement : IAuthorizationRequirement
    {

    }

    public class ExchangeExpireRequirementHandler : AuthorizationHandler<ExchangeExpireRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeExpireRequirement requirement)
        {
            var hasExchangeClaim = context.User.HasClaim(x => x.Type == "ExchangeExpireDate");
            if (!hasExchangeClaim)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var exchangeExpireDate = context.User.FindFirst("ExchangeExpireDate")!;
            if (Convert.ToDateTime(exchangeExpireDate.Value) < DateTime.Now)
                context.Fail();
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
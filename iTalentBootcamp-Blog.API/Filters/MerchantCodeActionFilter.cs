using iTalentBootcamp_Blog.Core.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iTalentBootcamp_Blog.API.Filters
{
    public class MerchantCodeActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var key = "merchCode";
            var merchCode = context.RouteData.Values[key];

            /*RequestModel içerisinde gönderilen bir yapıda bunu BaseModel aracılığıyla generic olarak yakalayabiliriz.*/
            var baseRequest = context.ActionArguments
                .FirstOrDefault(x => x.Value != null && typeof(MerchantBaseModel).IsAssignableFrom(x.Value.GetType()));

            if(baseRequest.Value != null)
            {
                var req = baseRequest.Value as MerchantBaseModel;
                req.MerchantCode = merchCode.ToString();//Manipüle ediliyor
            }
            else
            {
                if (!context.ActionArguments.ContainsKey(key))
                    context.ActionArguments.Add(key, merchCode);
                else
                    context.ActionArguments[key] = merchCode;
            }

            context.ActionArguments[key] = merchCode;

            await next();
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iTalentBootcamp_Blog.Identity.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddModelIdentityError(this ModelStateDictionary modelState, List<string> errors)
        {
            foreach (var err in errors)
                modelState.AddModelError(string.Empty, err);
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace iTalentBootcamp_Blog.Identity.TagHelpers
{
    public class UserPictureThumbnailTagHelper : TagHelper
    {
        public string pictureUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var path = string.IsNullOrEmpty(pictureUrl) ? "/user-images/default-user.jpg" : $"/user-images/{pictureUrl}";
            
            output.TagName = "img";
            output.Attributes.SetAttribute("src", path);
        }
    }
}

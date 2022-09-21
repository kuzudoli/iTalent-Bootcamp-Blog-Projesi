using iTalentBootcamp_Blog.Core.Dtos;

namespace iTalentBootcamp_Blog.Web.Helpers
{
    public interface IPhotoHelper
    {
        Task<string> PhotoSave(IFormFile photo);
        Task<string> PhotoUpdate(string oldUrl, IFormFile photo);

        Task<bool> PhotoDelete(string photoUrl);
    }
}

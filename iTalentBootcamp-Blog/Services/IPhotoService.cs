namespace iTalentBootcamp_Blog.Services
{
    public interface IPhotoService
    {
        Task<string> PhotoSave(IFormFile photo);
        Task<string> PhotoUpdate(int id, IFormFile photo);
    }
}
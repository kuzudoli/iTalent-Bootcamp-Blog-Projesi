using iTalentBootcamp_Blog.Core.Dtos;
using Microsoft.Extensions.FileProviders;

namespace iTalentBootcamp_Blog.Web.Helpers
{
    public class PhotoHelper : IPhotoHelper
    {
        private readonly IFileProvider _fileProvider;

        public PhotoHelper(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<bool> PhotoDelete(string photoUrl)
        {
            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var picturesDirectory = root.Single(pD => pD.Name == "pictures");

            //deletes image from path
            var photoPath = Path.Combine(picturesDirectory.PhysicalPath, photoUrl);
            FileInfo fileInfo = new FileInfo(photoPath);

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                return true;
            }

            return false;
        }

        public async Task<string> PhotoSave(IFormFile photo)
        {
            if (photo == null)
                return null;

            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var picturesDirectory = root.Single(pD => pD.Name == "pictures");
            var fileName = Convert.ToString(DateTime.Now.Ticks) + Path.GetExtension(photo.FileName);

            var path = Path.Combine(picturesDirectory.PhysicalPath, fileName);

            //if file exist it's gonna overwrite
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<string> PhotoUpdate(string oldUrl, IFormFile photo)
        {
            if (photo == null)
                return oldUrl;

            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var picturesDirectory = root.Single(pD => pD.Name == "pictures");

            var fileName = Convert.ToString(DateTime.Now.Ticks) + Path.GetExtension(photo.FileName);

            var path = Path.Combine(picturesDirectory.PhysicalPath, fileName);

            //if file exist it's gonna overwrite
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            if (oldUrl != null)
            {
                await PhotoDelete(oldUrl);
            }

            return fileName;
        }
    }
}

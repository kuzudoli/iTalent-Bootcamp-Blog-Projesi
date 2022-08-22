using iTalentBootcamp_Blog.Data;
using Microsoft.Extensions.FileProviders;

namespace iTalentBootcamp_Blog.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IPostRepository _postRepository;

        public PhotoService(IFileProvider fileProvider, IPostRepository postRepository)
        {
            _fileProvider = fileProvider;
            _postRepository = postRepository;
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

        public async Task<string> PhotoUpdate(int postId, IFormFile photo)
        {
            var oldUrl = _postRepository.GetById(postId).ImageUrl;

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
                //deletes old image from path
                var oldPath = Path.Combine(picturesDirectory.PhysicalPath, oldUrl);
                FileInfo fileInfo = new FileInfo(oldPath);

                if (fileInfo.Exists)
                    fileInfo.Delete();
            }

            return fileName;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Souq.core.Services;

namespace Souq.infrastructure.Repositories.Services
{
    public class ImageServiceManagement : IImageServiceManagement
    {
        private readonly IFileProvider _fileProvider;

        public ImageServiceManagement(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImagesAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            string imageDirectory = Path.Combine("wwwroot" , "Images" , src);

            if (!Directory.Exists(imageDirectory)) 
            { 
                Directory.CreateDirectory(imageDirectory);
            }
            foreach (var file in files) 
            {
                if (file.Length > 0) 
                {
                    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imageSrc = $"/Images/{src}/{imageName}";
                    string root = Path.Combine(imageDirectory, imageName);
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(imageSrc);
                }
            }
            return SaveImageSrc;
        }

        public async void DeleteImage(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            if (info.Exists && info.PhysicalPath != null)
            {
                await Task.Run(() => File.Delete(info.PhysicalPath));
            }          
        }
    }
}

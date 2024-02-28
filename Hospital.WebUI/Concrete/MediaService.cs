using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Hospital.WebUI.Abstract;
using Hospital.WebUI.Models;
using Microsoft.VisualBasic;

namespace Hospital.WebUI.Concrete
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration _configuration;

        private readonly CloudinarySettings? _cloudinarySettings;

        private readonly Cloudinary _cloudinary;


        public MediaService(IConfiguration configuration)
        {
            //_cloudinary = cloudinary;

            _configuration = configuration;
            _cloudinarySettings = _configuration.GetSection("CloudinarySettings")
                                                .Get<CloudinarySettings>();
            Account account = new(

                _cloudinarySettings.CloudName,

                _cloudinarySettings.ApiKey,

                _cloudinarySettings.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadMediaAsync(IFormFile file)
        {
            var fileType = file.ContentType;

            RawUploadParams uploadParams;

            if (fileType.StartsWith("image/"))
            {
                uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
            }
            else if (fileType.StartsWith("video/"))
            {
                uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
            }
            else
            {
                throw new NotSupportedException("File type not supported");
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        public bool IsVideoFile(IFormFile mediaFile)
        {
            if (mediaFile.ContentType.StartsWith("video/"))
            {
                return true;
            }
            return false;
        }
    }
}
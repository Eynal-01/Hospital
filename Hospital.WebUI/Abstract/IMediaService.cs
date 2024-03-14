namespace Hospital.WebUI.Abstract
{
    public interface IMediaService
    {
        Task<string> UploadMediaAsync(IFormFile file);
        bool IsVideoFile(IFormFile mediaFile);
    }
}

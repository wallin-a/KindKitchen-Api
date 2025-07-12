namespace recipe_app_api.Interfaces
{
    public interface IImageService
    {
        Task<string> GetImageAsync(int id);
    }
}

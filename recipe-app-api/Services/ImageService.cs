using recipe_app_api.Data.Repository;
using recipe_app_api.Exceptions;
using recipe_app_api.Interfaces;

namespace recipe_app_api.Services
{
    
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly BlobService _blobService;

        public ImageService(IImageRepository blobRepository, BlobService blobService)
        {
            _imageRepository = blobRepository;
            _blobService = blobService;
        }

        public async Task<string> GetImageAsync(int id)
        {
            var recipeImageEntity = await _imageRepository.GetAsync(id);

            if (recipeImageEntity == null)
                throw new NotFoundException($"Image with id {id} not found.");

            var sasUrl = _blobService.GetSasUrl(recipeImageEntity.BlobName, TimeSpan.FromMinutes(30));

            return sasUrl;
        }
    }
}

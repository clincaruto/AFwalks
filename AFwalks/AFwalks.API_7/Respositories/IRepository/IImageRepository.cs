using AFwalks.API_7.Models.Domain;

namespace AFwalks.API_7.Respositories.IRepository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

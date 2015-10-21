using System.Collections.Generic;
using System.Linq;
using PrivatePhotoStorageXamarin.Model;
using PrivatePhotoStorageXamarin.Services.Database;
using SQLite;
using Xamarin.Forms;

namespace PrivatePhotoStorageXamarin.Services.Images
{
    public interface IImageService
    {
        void Add(string item);
        void Delete(ImageItem item);
        IEnumerable<ImageItem> GetImages();
        int GetId();
    }
    public class ImageService : IImageService
    {
        private readonly SQLiteConnection _connection;

        public ImageService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<ImageItem>();
        }

        public IEnumerable<ImageItem> GetImages()
        {
            var images = _connection.Table<ImageItem>().AsEnumerable();
            return images;
        }

        public void Add(string item)
        {
            _connection.Insert(new ImageItem { Source = item });
        }

        public void Delete(ImageItem item)
        {
            _connection.Delete(item);
            _connection.UpdateAll(_connection.Table<ImageItem>());
        }

        public int GetId()
        {
            var id = _connection.Table<ImageItem>().Count();
            return id;
        }
    }
}

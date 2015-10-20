using System;
using System.IO;
using Foundation;
using PrivatePhotoStorageXamarin.Services;
using PrivatePhotoStorageXamarin.Services.Images;
using UIKit;
using Xamarin.Forms;

namespace PrivatePhotoStorageXamarin.iOS.Service.Images
{
    public class SaveImage: ISaveImage
    {
        private IImageService _imageService;

        public string CopyPhotoTo(string photoPath)
        {
            _imageService = DependencyService.Get<IImageService>();

            var imgData = new UIImage(photoPath).AsJPEG();
            var ID = _imageService.GetId();
            var ExtensionsFile = ".jpg";
            var jpgFilename = Path.Combine(GetPath(), ID + ExtensionsFile);
            NSError err = null;

            if (imgData.Save(jpgFilename, false, out err))
            {
                Console.WriteLine("saved as " + jpgFilename);

            }
            else
            {
                Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
            }
            return ID + ExtensionsFile;
        }

        public string GetPath()
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var documentsDirectory = Path.Combine(root, "SavedPhotos/.sercet");

            if (!Directory.Exists(documentsDirectory))
                Directory.CreateDirectory(documentsDirectory);

            return documentsDirectory;
        }
    }
}

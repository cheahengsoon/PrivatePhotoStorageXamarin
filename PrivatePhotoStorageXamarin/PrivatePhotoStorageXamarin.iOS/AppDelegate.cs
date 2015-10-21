using Foundation;
using PrivatePhotoStorageXamarin.iOS.Service.Database;
using PrivatePhotoStorageXamarin.iOS.Service.Images;
using PrivatePhotoStorageXamarin.Services.Images;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms;
using XLabs.Platform.Services.Media;

namespace PrivatePhotoStorageXamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : XFormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            DependencyService.Register<MediaPicker>();
            DependencyService.Register<SaveImage>();
            DependencyService.Register<ImageService>();
            DependencyService.Register<SQLiteTouch>();
            Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public async void SavePictureToDisk(string photoPath)
        {
            var someImage = new UIImage(photoPath);
            someImage.SaveToPhotosAlbum((image, error) => {
                var o = image;
             });
        }
    }
}

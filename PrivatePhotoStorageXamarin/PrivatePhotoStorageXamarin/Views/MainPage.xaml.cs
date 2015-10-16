using System;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PrivatePhotoStorageXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        IMediaPicker _mediaPicker = DependencyService.Get<IMediaPicker>();

        public MainPage()
        {
            InitializeComponent();
            takePhoto.Clicked += TakePhotoClicked;
            choosePhoto.Clicked += SelectPicture;
        }

        async void TakePhotoClicked(object sender, EventArgs e)
        {
            MediaFile file = null;
            if (_mediaPicker.IsCameraAvailable)
            {
                file = await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions());
            }
            else
            {
                file = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions());
            }
            previewImage.Source = ImageSource.FromStream(() => file.Source);
        }

        async void SelectPicture(object sender, EventArgs e)
        {
            MediaFile file = null;
            try
            {
                file = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                previewImage.Source = ImageSource.FromStream(() => file.Source);
            }
            catch (Exception ex)
            {

            }

        }
    }
}

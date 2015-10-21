using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PrivatePhotoStorageXamarin.Annotations;
using PrivatePhotoStorageXamarin.Services;
using PrivatePhotoStorageXamarin.Services.Images;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PrivatePhotoStorageXamarin.ViewModels
{
    public class GalleryViewModel : INotifyPropertyChanged
    {
        private ISaveImage _imageSaver;
        private ImageService _imageService;
        private IMediaPicker _mediaPicker;


        public GalleryViewModel(StackLayout stack)
        {
            AddPictureCommand = new Command(AddPicture);
            _imageSaver = DependencyService.Get<ISaveImage>();
            _imageService = DependencyService.Get<ImageService>();
            _mediaPicker = DependencyService.Get<IMediaPicker>();
            Images = new ObservableCollection<ImageViewModel>();
            ShowImages();
        }

        public ICommand AddPictureCommand { get; private set; }

        public ObservableCollection<ImageViewModel> Images { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ShowImages()
        {
            foreach (var image in _imageService.GetImages())
            {
                var path = image.Source;
                Images.Add(new ImageViewModel
                {
                    Source = ImageSource.FromFile(_imageSaver.GetPath() + "/" + path)
                });
            }
        }

        async void AddPicture()
        {
            MediaFile file = null;
            if (_mediaPicker.IsCameraAvailable)
            {
                file = await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions());
                AddImageToDB(file.Path);
            }
            else
            {
                file = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions());
                AddImageToDB(file.Path);
            }

            var image = new ImageViewModel
            {
                Source = ImageSource.FromStream(() => file.Source)
            };
            Images.Add(image);
        }

        private void AddImageToDB(string path)
        {
            var newPath = _imageSaver.CopyPhotoTo(path);
            _imageService.Add(newPath);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

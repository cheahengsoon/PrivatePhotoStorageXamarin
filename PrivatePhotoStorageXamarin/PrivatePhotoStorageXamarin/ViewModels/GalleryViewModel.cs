using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PrivatePhotoStorageXamarin.Annotations;
using PrivatePhotoStorageXamarin.Model;
using PrivatePhotoStorageXamarin.Services;
using PrivatePhotoStorageXamarin.Services.Images;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PrivatePhotoStorageXamarin.ViewModels
{
    public class GalleryViewModel:INotifyPropertyChanged
    {
        private ImageSource _images;
        private ISaveImage _imageSaver;
        private IImageService _imageService;
        private IMediaPicker _mediaPicker;


        public GalleryViewModel()
        {
             AddPictureCommand = new Command(AddPicture);
             Images = new ObservableCollection<ImageItem>(_imageService.GetImages());
            _imageSaver = DependencyService.Get<ISaveImage>();
            _imageService = DependencyService.Get<IImageService>();
            _mediaPicker = DependencyService.Get<IMediaPicker>();

            foreach (var image in Images)
            {
                AddImageView(image.Source);
            }

        }

        public ICommand AddPictureCommand { get; private set; }

        public ImageSource Image
        {
            get { return _images; }
            set { _images = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ImageItem> Images { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void AddImageView(string path)
        {
            var img = new Image
            {
                Source = ImageSource.FromFile(_imageSaver.GetPath() + "/" + path)
            };
        }

        async void AddPicture()
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
            //Images.Add(new Image
            //{
            //    Source = ImageSource.FromStream(() => file.Source)
            //});

            var newPath = _imageSaver.CopyPhotoTo(file.Path);
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {AddImageView(newPath);
            //});

            _imageService.Add(new ImageItem { Source = newPath });

        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

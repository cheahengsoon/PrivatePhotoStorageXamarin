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


        public GalleryViewModel()
        {
            AddPictureCommand = new Command(AddPicture);
            DeletePictureCommand = new Command(DeletePicture);
            _imageSaver = DependencyService.Get<ISaveImage>();
            _imageService = DependencyService.Get<ImageService>();
            _mediaPicker = DependencyService.Get<IMediaPicker>();
            Images = new ObservableCollection<ImageViewModel>();
            ShowImages();
        }

        public ICommand AddPictureCommand { get; private set; }
        public ICommand DeletePictureCommand { get; private set; }

        public ObservableCollection<ImageViewModel> Images { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void DeletePicture()
        {
          _imageService.Delete(); 
            Images.Clear();
        }

        private void ShowImages()
        {
            Images.Clear();
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
            if (_mediaPicker.IsCameraAvailable)
            {
                {
                    await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions()).ContinueWith(t =>
                    {
                        if (t.IsCanceled)
                        {
                            return;
                        }
                        var file = t.Result;
                        AddImageToDB(file.Path);
                    });
                }
            }
            else
            {
                {
                    await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions()).ContinueWith(t =>
                    {
                        if (t.IsCanceled)
                        {
                            return;
                        }
                        var file = t.Result;
                        AddImageToDB(file.Path);
                    });
                }
                ShowImages();
            }
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

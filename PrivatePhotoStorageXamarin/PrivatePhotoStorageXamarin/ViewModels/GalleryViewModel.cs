using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PrivatePhotoStorageXamarin.Annotations;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PrivatePhotoStorageXamarin.ViewModels
{
    public class GalleryViewModel:INotifyPropertyChanged
    {
        private ImageSource _images;
        private IMediaPicker _mediaPicker = DependencyService.Get<IMediaPicker>();


        public GalleryViewModel()
        {
             AddPictureCommand = new Command(AddPicture);  
             Images = new ObservableCollection<Image>();
        }


        public ICommand AddPictureCommand { get; private set; }

        public ImageSource Image
        {
            get { return _images; }
            set { _images = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Image> Images { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

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
            Images.Add(new Image
            {
                Source = ImageSource.FromStream(() => file.Source)
            });
            
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

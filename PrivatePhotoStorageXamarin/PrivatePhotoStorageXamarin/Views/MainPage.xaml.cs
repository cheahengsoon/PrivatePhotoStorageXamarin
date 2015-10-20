using PrivatePhotoStorageXamarin.ViewModels;
using Xamarin.Forms;

namespace PrivatePhotoStorageXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        private GalleryViewModel _vm;

        public MainPage()
        {
            InitializeComponent();
            _vm = new GalleryViewModel();
            BindingContext = _vm;
            
        }
    }
}

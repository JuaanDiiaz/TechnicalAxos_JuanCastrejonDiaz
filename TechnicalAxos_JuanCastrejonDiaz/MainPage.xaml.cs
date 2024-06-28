namespace TechnicalAxos_JuanCastrejonDiaz
{
    public partial class MainPage : ContentPage
    {
        private double _maxScrollY = 0;
        private MainViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = ((MainViewModel)BindingContext);
        }

        private async void OnChooseImageClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            {
                if (result == null)
                {
                    return;
                }
                ImageSource imageSource = ImageSource.FromStream(() => result.OpenReadAsync().Result);
                openGallery.Source = imageSource;
            }
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            double maxPixelsY = _maxScrollY == 0 ? DeviceDisplay.MainDisplayInfo.Height : _maxScrollY;
            double currentPixelsY = e.ScrollY;

            if (currentPixelsY >= (maxPixelsY/2))
            {
                _maxScrollY = currentPixelsY;
                _viewModel.LoadMoreCommand.Execute(null);
            }

        }
    }
}
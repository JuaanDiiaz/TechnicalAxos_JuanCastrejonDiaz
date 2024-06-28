namespace TechnicalAxos_JuanCastrejonDiaz
{
    public partial class MainPage : ContentPage
    {
        private double _maxScrollY = 0; // Almacena el máximo de desplazamiento en el eje Y
        private MainViewModel _viewModel; // ViewModel asociado a la página

        public MainPage()
        {
            InitializeComponent();
            _viewModel = ((MainViewModel)BindingContext); // Asigna el ViewModel obtenido del contexto de enlace
        }

        // Método para manejar el evento de selección de imagen
        private async void OnChooseImageClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(); // Abre el selector de imágenes y espera el resultado

            if (result == null) // Si no se seleccionó ninguna imagen, salir del método
            {
                return;
            }

            // Asigna la imagen seleccionada al control de imagen (openGallery)
            ImageSource imageSource = ImageSource.FromStream(() => result.OpenReadAsync().Result);
            openGallery.Source = imageSource;
        }

        // Método para manejar el evento de desplazamiento del ScrollView
        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            // Calcula el máximo de píxeles en el eje Y, usando el tamaño de la pantalla si _maxScrollY es 0
            double maxPixelsY = _maxScrollY == 0 ? DeviceDisplay.MainDisplayInfo.Height : _maxScrollY;

            double currentPixelsY = e.ScrollY; // Píxeles actuales de desplazamiento en el eje Y

            // Verifica si se ha alcanzado o superado la mitad del máximo de desplazamiento en Y
            if (currentPixelsY >= (maxPixelsY / 2))
            {
                _maxScrollY = currentPixelsY; // Actualiza el máximo de desplazamiento en Y

                // Ejecuta el comando de carga adicional del ViewModel
                _viewModel.LoadMoreCommand.Execute(null);
            }
        }
    }
}

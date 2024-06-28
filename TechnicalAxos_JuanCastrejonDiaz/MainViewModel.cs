using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalAxos_JuanCastrejonDiaz.Models;
using TechnicalAxos_JuanCastrejonDiaz.Services;

namespace TechnicalAxos_JuanCastrejonDiaz
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _packageId; // Campo para almacenar el ID del paquete
        private bool _isLoading; // Indica si se está cargando datos
        private int _currentPage = 0; // Página actual para la paginación

        private const int PageSize = 20; // Tamaño de la página para la paginación

        public ObservableCollection<Country> Countries { get; set; } 
        public ICommand LoadMoreCommand { get; set; }

        public string PackageId
        {
            get => _packageId; 
            set => SetProperty(ref _packageId, value); 
        }

        public bool IsLoading
        {
            get => _isLoading; 
            set => SetProperty(ref _isLoading, value); 
        }

        public MainViewModel()
        {
            // Obtener el ID del paquete si la plataforma es iOS o Android y asigna el nombre del paquete de la aplicación
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android)
            {
                PackageId = AppInfo.PackageName;
            }

            Countries = new ObservableCollection<Country>(); 
            LoadMoreCommand = new Command(async () => await LoadCountries()); // Comando para cargar más países
            LoadCountries(); // Carga inicial de países
        }

        // Método para cargar países
        public async Task LoadCountries()
        {
            if (_isLoading) return; // Si ya se está cargando, no hacer nada
            _isLoading = true;

            try
            {
                var countries = await CountryService.getInstance().GetCountriesAsync(); // Obtener países desde el servicio
                var pagedCountries = countries.Skip(_currentPage * PageSize).Take(PageSize).ToList(); // Paginar los países obtenidos

                // Agregar países paginados a la colección observable
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var country in pagedCountries)
                    {
                        Countries.Add(country); 
                    }

                    _currentPage++; 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading countries: {ex.Message}"); 
            }
            finally
            {
                _isLoading = false; // Finalizar la carga (ya sea exitosa o con error)
            }
        }

        // Implementación de INotifyPropertyChanged para notificar cambios en las propiedades
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Método auxiliar para establecer propiedades y notificar cambios
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", bool forceUpdate = false)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value) && !forceUpdate)
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

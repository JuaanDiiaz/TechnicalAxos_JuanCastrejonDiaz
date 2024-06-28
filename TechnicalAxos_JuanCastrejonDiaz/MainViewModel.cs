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
    public class MainViewModel
    {
        private string _packageId;
        private bool _isLoading;
        private int _currentPage = 0;

        private const int PageSize = 20;

        public ObservableCollection<Country> Countries { get; set; }
        public ICommand LoadMoreCommand { get; set; }

        public string PackageId
        {
            get => _packageId;
            set { 
                _packageId = value;
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set {
                _isLoading = value;
            }
        }

        public MainViewModel()
        {

            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android)
            {
                PackageId = AppInfo.PackageName;
            }

            Countries = new ObservableCollection<Country>();
            LoadMoreCommand = new Command(async () => await LoadCountries());
            LoadCountries();
        }

        public async Task LoadCountries()
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                var countries = await CountryService.getInstance().GetCountriesAsync();
                var pagedCountries = countries.Skip(_currentPage * PageSize).Take(PageSize).ToList();

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
                _isLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

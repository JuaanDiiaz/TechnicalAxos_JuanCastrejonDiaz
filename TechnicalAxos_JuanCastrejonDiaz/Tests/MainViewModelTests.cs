using Moq;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAxos_JuanCastrejonDiaz.Interfaces;
using TechnicalAxos_JuanCastrejonDiaz.Models;
using Xunit;
using IAppInfo = TechnicalAxos_JuanCastrejonDiaz.Interfaces.IAppInfo;

namespace TechnicalAxos_JuanCastrejonDiaz.Tests
{
    public class MainViewModelTests
    {
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly Mock<IAppInfo> _mockAppInfo;
        private readonly Mock<IDeviceInfo> _mockDeviceInfo;
        private readonly MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _mockCountryService = new Mock<ICountryService>();
            _mockAppInfo = new Mock<IAppInfo>();
            _mockDeviceInfo = new Mock<IDeviceInfo>();

            // Setup mocks
            _mockDeviceInfo.Setup(d => d.Platform).Returns(DevicePlatform.iOS);
            _mockAppInfo.Setup(a => a.GetPackageName()).Returns("com.companyname.technicalaxos_juancastrejondiaz");

            // Inject mocks into the ViewModel
            _viewModel = new MainViewModel();
        }

        [Fact]
        public void PackageId_Should_Be_Set_Correctly()
        {
            // Arrange
            string expectedPackageId = "com.companyname.technicalaxos_juancastrejondiaz";

            // Act
            string actualPackageId = _viewModel.PackageId;

            // Assert
            Assert.Equal(expectedPackageId, actualPackageId);
        }
    }
}


using AutoMapper;
using Moq;
using Shipping.Application.Contracts.Adapters;
using Shipping.Application.Services;
using Shipping.Domain;
using Shipping.Infrastructure.Adapters;
using Shipping.Infrastructure.Clients;
using Shipping.Infrastructure.DTO;
using Shipping.Infrastructure.Mapping;

namespace Shipping.UnitTests
{
    [TestFixture]
    public class ShippingTests
    {
        private IShippingPackageService _shippingPackageService;
        private List<IShippingProviderService> _listShippingProviderService;
        private Api1ProviderAdapter _providerAdapterApi1;
        private Api2ProviderAdapter _providerAdapterApi2;
        private Api3ProviderAdapter _providerAdapterApi3;
        private IMapper _mapper;
        private Mock<IClientRest> _clientRestMock;
        private Uri _urlApi1;
        private Uri _urlApi2;
        private Uri _urlApi3;
        private string _credentialsApi1;
        private string _credentialsApi2;
        private string _credentialsApi3;

        [SetUp]
        public void Init()
        {
            //Init variables
            _urlApi1 = new Uri("http://api1.com");
            _urlApi2 = new Uri("http://api2.com");
            _urlApi3 = new Uri("http://api3.com");
            _credentialsApi1 = "credentials1";
            _credentialsApi2 = "credentials2";
            _credentialsApi3 = "credentials3";

            //Init Clien APIs
            this._clientRestMock = new Mock<IClientRest>();       
            
            //Init ProviderServices
            this._listShippingProviderService = new List<IShippingProviderService>();
            
            //Init ShippingPackageService
            this._shippingPackageService = new ShippingPackageService(this._listShippingProviderService);

            //Init mapper
            var configurarion = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper= configurarion.CreateMapper();

        }

        [Test]
        public async Task IsApi1LowestRate()
        {
            int[] cartonDimensions = { 1, 2, 3, 4 };
            decimal lowestRateExpected = 100000;

            //Input
            Package package = new Package()
            {
                CartonDimensions = cartonDimensions,
                DestinationAdress = "Test",
                SourceAdress="Test"
            };

            //Output
            RateApi1DTO rateApi1DTO = new RateApi1DTO()
            {
                Total = lowestRateExpected
            };
            RateApi2DTO rateApi2DTO = new RateApi2DTO()
            {
                Amount= 110000
            };
            RateApi3DTO rateApi3DTO = new RateApi3DTO()
            {
                Quote = 210000
            };

            //Api1
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi1, _credentialsApi1, It.IsAny<PackageApi1DTO>())).ReturnsAsync(rateApi1DTO);
            this._providerAdapterApi1 = new Api1ProviderAdapter(_urlApi1, _credentialsApi1, this._clientRestMock.Object, _mapper);

            //Api2
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi2, _credentialsApi2, It.IsAny<PackageApi2DTO>())).ReturnsAsync(rateApi2DTO);
            this._providerAdapterApi2 = new Api2ProviderAdapter(_urlApi2, _credentialsApi2, this._clientRestMock.Object, _mapper);

            //Api3
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi3, _credentialsApi3, It.IsAny<PackageApi3DTO>())).ReturnsAsync(rateApi3DTO);
            this._providerAdapterApi3 = new Api3ProviderAdapter(_urlApi3, _credentialsApi3, this._clientRestMock.Object, _mapper);

            this._listShippingProviderService.RemoveAll(c => true);
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi1));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi2));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi3));

            var lowestRate= await this._shippingPackageService.GetLowestRate(package); 

            Assert.AreEqual(lowestRateExpected, lowestRate,"Api1 didn't return the lowest rate");
        }

        
        [Test]
        public async Task IsApi2LowestRate()
        {
            int[] cartonDimensions = { 1, 2, 3, 4 };
            decimal lowestRateExpected = 150000;
            Package package = new Package()
            {
                CartonDimensions = cartonDimensions,
                DestinationAdress = "Test",
                SourceAdress = "Test"
            };
            //Output
            RateApi1DTO rateApi1DTO = new RateApi1DTO()
            {
                Total = 500000
            };
            RateApi2DTO rateApi2DTO = new RateApi2DTO()
            {
                Amount = lowestRateExpected
            };
            RateApi3DTO rateApi3DTO = new RateApi3DTO()
            {
                Quote = 250000
            };

            //Api1
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi1, _credentialsApi1, It.IsAny<PackageApi1DTO>())).ReturnsAsync(rateApi1DTO);
            this._providerAdapterApi1 = new Api1ProviderAdapter(_urlApi1, _credentialsApi1, this._clientRestMock.Object, _mapper);

            //Api2
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi2, _credentialsApi2, It.IsAny<PackageApi2DTO>())).ReturnsAsync(rateApi2DTO);
            this._providerAdapterApi2 = new Api2ProviderAdapter(_urlApi2, _credentialsApi2, this._clientRestMock.Object, _mapper);

            //Api3
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi3, _credentialsApi3, It.IsAny<PackageApi3DTO>())).ReturnsAsync(rateApi3DTO);
            this._providerAdapterApi3 = new Api3ProviderAdapter(_urlApi3, _credentialsApi3, this._clientRestMock.Object, _mapper);

            this._listShippingProviderService.RemoveAll(c => true);
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi1));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi2));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi3));

            var lowestRate = await this._shippingPackageService.GetLowestRate(package);

            Assert.AreEqual(lowestRateExpected, lowestRate, "Api2 didn't return the lowest rate");
        }

        [Test]
        public async Task IsApi3LowestRate()
        {
            int[] cartonDimensions = { 1, 2, 3, 4 };
            decimal lowestRateExpected = 50000;
            Package package = new Package()
            {
                CartonDimensions = cartonDimensions,
                DestinationAdress = "Test",
                SourceAdress = "Test"
            };
            //Output
            RateApi1DTO rateApi1DTO = new RateApi1DTO()
            {
                Total = 80000
            };
            RateApi2DTO rateApi2DTO = new RateApi2DTO()
            {
                Amount = 100000
            };
            RateApi3DTO rateApi3DTO = new RateApi3DTO()
            {
                Quote = lowestRateExpected
            };

            //Api1
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi1, _credentialsApi1, It.IsAny<PackageApi1DTO>())).ReturnsAsync(rateApi1DTO);
            this._providerAdapterApi1 = new Api1ProviderAdapter(_urlApi1, _credentialsApi1, this._clientRestMock.Object, _mapper);

            //Api2
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi2, _credentialsApi2, It.IsAny<PackageApi2DTO>())).ReturnsAsync(rateApi2DTO);
            this._providerAdapterApi2 = new Api2ProviderAdapter(_urlApi2, _credentialsApi2, this._clientRestMock.Object, _mapper);

            //Api3
            this._clientRestMock.Setup(c => c.CallRestEndPointAsync(_urlApi3, _credentialsApi3, It.IsAny<PackageApi3DTO>())).ReturnsAsync(rateApi3DTO);
            this._providerAdapterApi3 = new Api3ProviderAdapter(_urlApi3, _credentialsApi3, this._clientRestMock.Object, _mapper);

            this._listShippingProviderService.RemoveAll(c => true);
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi1));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi2));
            this._listShippingProviderService.Add(new ShippingProviderService(this._providerAdapterApi3));

            var lowestRate = await this._shippingPackageService.GetLowestRate(package);

            Assert.AreEqual(lowestRateExpected, lowestRate, "Api3 didn't return the lowest rate");
        }        
    }
}

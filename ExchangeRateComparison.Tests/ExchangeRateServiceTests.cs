using ExchangeRateComparison.Application;
using ExchangeRateComparison.Domain;
using Moq;
using Xunit;

namespace ExchangeRateComparison.Tests;

public class ExchangeRateServiceTests
{
    [Fact]
    public async Task GetBestOfferAsync_ReturnsHighestRate()
    {
        // Arrange
        var request = new ExchangeRequest("USD", "DOP", 100);

        var mockProvider1 = new Mock<IExchangeRateProvider>();
        mockProvider1.Setup(x => x.GetRateAsync(It.IsAny<ExchangeRequest>()))
                     .ReturnsAsync(new ExchangeResponse("Provider1", 5800));

        var mockProvider2 = new Mock<IExchangeRateProvider>();
        mockProvider2.Setup(x => x.GetRateAsync(It.IsAny<ExchangeRequest>()))
                     .ReturnsAsync(new ExchangeResponse("Provider2", 6000));

        var mockProvider3 = new Mock<IExchangeRateProvider>();
        mockProvider3.Setup(x => x.GetRateAsync(It.IsAny<ExchangeRequest>()))
                     .ReturnsAsync(new ExchangeResponse("Provider3", 5900));

        var providers = new List<IExchangeRateProvider>
        {
            mockProvider1.Object,
            mockProvider2.Object,
            mockProvider3.Object
        };

        var service = new ExchangeRateService(providers);

        // Act
        var best = await service.GetBestOfferAsync(request);

        // Assert
        Assert.NotNull(best);
        Assert.Equal("Provider2", best!.Provider);
        Assert.Equal(6000, best.Result);
    }

    [Fact]
    public async Task GetBestOfferAsync_ReturnsNull_WhenAllFail()
    {
        // Arrange
        var request = new ExchangeRequest("USD", "DOP", 100);

        var mockProvider = new Mock<IExchangeRateProvider>();
        mockProvider.Setup(x => x.GetRateAsync(It.IsAny<ExchangeRequest>()))
                    .ReturnsAsync((ExchangeResponse?)null);

        var service = new ExchangeRateService(new[] { mockProvider.Object });

        // Act
        var best = await service.GetBestOfferAsync(request);

        // Assert
        Assert.Null(best);
    }
}

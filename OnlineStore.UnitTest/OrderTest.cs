using OnlineStore.IntegrationTest.Factories;
using System.Net;

namespace OnlineStore.IntegrationTest;

public class OrderTest : IClassFixture<WebAppDbContextFactory<Program>>
{
    private readonly HttpClient _client;
    private const string _baseUrl = "http://localhost:5157/api/Order";
    public OrderTest(WebAppDbContextFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(1, 1)]
    public async Task BuyProduct_ShouldReturnSuccess(int productId, int userId)
    {
        // Arrange

        // Act
        var result = await _client.PostAsync(string.Concat(_baseUrl, "/", $"?productId={productId}&userId={userId}"), null);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Theory]
    [InlineData(1, 10)]
    public async Task BuyProduct_ShouldReturn_BadRequest(int productId, int userId)
    {
        // Arrange

        // Act
        var result = await _client.PostAsync(string.Concat(_baseUrl, "/", $"?productId={productId}&userId={userId}"), null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(10, 1)]
    public async Task BuyProduct_ShouldReturn_NotFound(int productId, int userId)
    {
        // Arrange

        // Act
        var result = await _client.PostAsync(string.Concat(_baseUrl, "/", $"?productId={productId}&userId={userId}"), null);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

}
using Newtonsoft.Json;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Wrappers;
using OnlineStore.IntegrationTest.Factories;
using System.Net;
using System.Text;

namespace OnlineStore.IntegrationTest;

public class ProductTest : IClassFixture<WebAppDbContextFactory<Program>>
{
    private readonly HttpClient _client;
    private const string _baseUrl = "http://localhost:5157/api/Product";
    public ProductTest(WebAppDbContextFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async Task AddProduct_ShouldReturnBadRequest_WhenTitleIsTooLong()
    {
        // Arrange
        var product = new ProductDto
        {
            Title = new string('a', 41),
            Discount = 2,
            InventoryCount = 2,
            Price = 56000
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var expected = "\"errors\":{\"Title\":[\"Title should be less than 40 characters.\"]}";

        // Act 
        var result = await _client.PostAsync(_baseUrl, content);
        var actual = await result.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains(expected, actual);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task AddDuplicateProduct_ShouldReturn_BadRequest()
    {
        // Arrange
        var product = new ProductDto
        {
            Title = "ProductA",
            Discount = 2,
            InventoryCount = 2,
            Price = 56000
        };
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var expected = "{\"Data\":null,\"Success\":false,\"Errors\":[{\"ErrorCode\":4,\"FieldName\":null,\"Description\":\"A product with the same name is exist\"}]}";


        // Act
        var result = await _client.PostAsync(_baseUrl, content);
        var actual = await result.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains(expected, actual);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Theory]
    [InlineData(1, 3)]
    public async Task UpDateProduct_ShouldReturn_Success(int productId, int inventoryCount)
    {
        // Arrange

        // Act
        var result = await _client.PutAsync(string.Concat(_baseUrl, "/", $"?id={productId}&inventoryCount={inventoryCount}"), null);


        // Assert

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Theory]
    [InlineData(10, 3)]
    public async Task UpDateProduct_ShouldReturn_NotFound(int productId, int inventoryCount)
    {
        // Arrange

        // Act
        var result = await _client.PutAsync(string.Concat(_baseUrl, "/", $"?id={productId}&inventoryCount={inventoryCount}"), null);


        // Assert

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }


    [Theory]
    [InlineData(1)]
    public async Task GetProduct_ShouldReturnProductWithDiscountedPrice(int id)
    {
        // Arrange
        var expectedData = new ProductDto
        {
            Discount = 2,
            InventoryCount = 10,
            Price = 980,
            Title = "ProductA"
        };

        // Act 
        var result = await _client.GetAsync(string.Concat(_baseUrl, "/", id));
        var actualString = await result.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<BaseResult<ProductDto>>(actualString);


        // Assert
        Assert.Equal(expectedData.Title, actual.Data.Title);
        Assert.Equal(expectedData.Discount, actual.Data.Discount);
        Assert.Equal(expectedData.Price, actual.Data.Price);
        Assert.Equal(expectedData.InventoryCount, actual.Data.InventoryCount);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }


}
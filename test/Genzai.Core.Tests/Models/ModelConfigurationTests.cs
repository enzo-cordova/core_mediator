namespace Genzai.Core.Tests.Models;

/// <summary>
/// Model configuration test.
/// </summary>
public class ModelConfigurationTests
{
    /// <summary>
    /// Test api manager configuration.
    /// </summary>
    [Fact]
    public void Test_Api_Manager_Configuration()
    {
        var endpoint = new Faker<Endpoint>()
            .RuleFor(x => x.Method, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Name, f => f.Random.AlphaNumeric(100));
        var apiEndpoint = new Faker<ApiManagerEndpoint>()
            .RuleFor(x => x.ApiName, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ApiUrl, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.OcpApimSubscriptionKey, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Endpoints, () => endpoint.Generate(1));
        var arrange = new Faker<ApiManagerConfiguration>()
            .RuleFor(x => x.ApiList, () => apiEndpoint.Generate(2));

        var action = arrange.Generate(1).FirstOrDefault();
        var actionApiEndpoint = action?.ApiList.FirstOrDefault();
        var actionEndpoint = actionApiEndpoint?.Endpoints.FirstOrDefault();

        action.Should().NotBeNull();
        action?.ApiList.Should().NotBeEmpty();
        actionApiEndpoint?.OcpApimSubscriptionKey.Should().NotBeEmpty();
        actionApiEndpoint?.ApiName.Should().NotBeEmpty();
        actionApiEndpoint?.ApiUrl.Should().NotBeEmpty();
        actionApiEndpoint?.Endpoints.Should().NotBeEmpty();
        actionEndpoint?.Method.Should().NotBeEmpty();
        actionEndpoint?.Name.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test authorization configuration.
    /// </summary>
    [Fact]
    public void Test_Authorization_Configuration()
    {
        var arrange = new Faker<AuthorizationConfiguration>()
            .RuleFor(x => x.ResourceId, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.TenantId, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Oauth2Url, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Instance, f => f.Random.AlphaNumeric(100));

        var action = arrange.Generate(1).FirstOrDefault();

        action.Should().NotBeNull();
        action?.ResourceId.Should().NotBeEmpty();
        action?.TenantId.Should().NotBeEmpty();
        action?.Oauth2Url.Should().NotBeEmpty();
        action?.Instance.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test cosmos configuration.
    /// </summary>
    [Fact]
    public void Test_Cosmos_Configuration()
    {
        var arrange = new Faker<CosmosConfiguration>()
            .RuleFor(x => x.Endpoint, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ApiKey, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.DatabaseName, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ApplicationName, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Collection, f => f.Random.AlphaNumeric(100));

        var action = arrange.Generate(1).FirstOrDefault();

        action.Should().NotBeNull();
        action?.Endpoint.Should().NotBeEmpty();
        action?.ApiKey.Should().NotBeEmpty();
        action?.DatabaseName.Should().NotBeEmpty();
        action?.ApplicationName.Should().NotBeEmpty();
        action?.Collection.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test send grid configuration.
    /// </summary>
    [Fact]
    public void Test_Send_Grid_Configuration()
    {
        var arrange = new Faker<SendGridConfiguration>()
            .RuleFor(x => x.ApiKey, f => f.Random.AlphaNumeric(100));

        var action = arrange.Generate(1).FirstOrDefault();

        action.Should().NotBeNull();
        action?.ApiKey.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test swagger configuration.
    /// </summary>
    [Fact]
    public void Test_Swagger_Configuration()
    {
        var apiSwaggerInfo = new Faker<ApiSwaggerInfo>()
            .RuleFor(x => x.Title, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Version, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Description, f => f.Random.AlphaNumeric(100));
        var swaggerOauth = new Faker<SwaggerOauth>()
            .RuleFor(x => x.AppNamePrompt, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ClientId, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ClientSecret, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.ReturnUrl, f => f.Random.AlphaNumeric(100));
        var arrange = new Faker<SwaggerConfiguration>()
            .RuleFor(x => x.Endpoint, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Environment, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Name, f => f.Random.AlphaNumeric(100))
            .RuleFor(x => x.Oauth, () => swaggerOauth.Generate(1).FirstOrDefault())
            .RuleFor(x => x.ApiInfo, () => apiSwaggerInfo.Generate(1).FirstOrDefault());

        var action = arrange.Generate(1).FirstOrDefault();
        var actionSwaggerOauth = action?.Oauth;
        var actionSwaggerInfo = action?.ApiInfo;

        action.Should().NotBeNull();
        action?.Endpoint.Should().NotBeEmpty();
        action?.Environment.Should().NotBeEmpty();
        action?.Name.Should().NotBeEmpty();
        action?.Oauth.Should().NotBeNull();
        action?.ApiInfo.Should().NotBeNull();
        actionSwaggerOauth?.ReturnUrl.Should().NotBeNullOrEmpty();
        actionSwaggerOauth?.ClientSecret.Should().NotBeNullOrEmpty();
        actionSwaggerOauth?.ClientId.Should().NotBeNullOrEmpty();
        actionSwaggerOauth?.AppNamePrompt.Should().NotBeNullOrEmpty();
        actionSwaggerInfo?.Title.Should().NotBeNullOrWhiteSpace();
        actionSwaggerInfo?.Version.Should().NotBeNullOrWhiteSpace();
        actionSwaggerInfo?.Description.Should().NotBeNullOrWhiteSpace();
    }
}
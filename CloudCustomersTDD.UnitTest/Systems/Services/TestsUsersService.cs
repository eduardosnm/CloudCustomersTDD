using CloudCustomersTDD.API.Config;
using CloudCustomersTDD.API.Models;
using CloudCustomersTDD.API.Services;
using CloudCustomersTDD.UnitTest.Fixtures;
using CloudCustomersTDD.UnitTest.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomersTDD.UnitTest.Systems.Services;

public class TestsUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);

        await sut.GetAllUsers();
        
        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);

        var result = await sut.GetAllUsers();

        result.Should().BeOfType<List<User>>();
    }
    
    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);

        var result = await sut.GetAllUsers();

        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnListOfUsersExpectedSize()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);

        var result = await sut.GetAllUsers();

        result.Count.Should().Be(expectedResponse.Count);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);

        var result = await sut.GetAllUsers();

        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
                                                     && req.RequestUri.ToString() == endpoint),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
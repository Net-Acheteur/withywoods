﻿using System.Net.Http;
using FluentAssertions;
using OpenQA.Selenium;
using Withywoods.Selenium;
using Withywoods.WebTesting.TestHost;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class SwaggerResourceTest : SeleniumTestBase, IClassFixture<LocalServerFactory<Startup>>
    {
        private const string _ResourceEndpoint = "swagger";

        private readonly LocalServerFactory<Startup> _server;

        public SwaggerResourceTest(LocalServerFactory<Startup> server)
            : base()
        {
            _server = server;
            _ = _server.CreateClient(); // this call is needed to change state of server
        }

        [Fact]
        public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
        {
            _server.RootUri.Should().Be("https://localhost:5001");

            try
            {
                // Arrange & Act
                WebDriver.Navigate().GoToUrl($"{_server.RootUri}/{_ResourceEndpoint}");

                // Assert
                WebDriver.FindElement(By.ClassName("title"), 60);
                WebDriver.Title.Should().Be("Swagger UI");
                WebDriver.FindElementByClassName("title").Text.Should().Contain("My API");

                TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
            }
            catch
            {
                TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
                throw;
            }
        }
    }
}
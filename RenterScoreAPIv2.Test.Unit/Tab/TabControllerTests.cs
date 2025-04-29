namespace RenterScoreAPIv2.Test.Unit.Tab;

using Microsoft.AspNetCore.Mvc;
using Moq;
using RenterScoreAPIv2.Tab;
using RenterScoreAPIv2.Tab.Tabs;

[TestFixture]
public class TabControllerTests
{
    private Mock<ITabFactory> _mockTabFactory;
    private TabController _controller;

    [SetUp]
    public void Setup()
    {
        _mockTabFactory = new Mock<ITabFactory>();
        _controller = new TabController(_mockTabFactory.Object);
    }

    [Test]
    public async Task GetTabData_ValidTabId_ReturnsOkResult()
    {
        // Arrange
        var mockTab = new Mock<BaseTab>();
        _mockTabFactory.Setup(factory => factory.CreateTab("home")).Returns(mockTab.Object);

        // Act
        var result = await _controller.GetTabData("home");

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(mockTab.Object));
    }

    [Test]
    public async Task GetTabData_FactoryThrowsArgumentException_ReturnsNotFound()
    {
        // Arrange
        _mockTabFactory.Setup(factory => factory.CreateTab("invalid"))
            .Throws(new ArgumentException("Unknown tab ID: invalid"));

        // Act
        var result = await _controller.GetTabData("invalid");

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task GetTabData_TabLoadThrowsException_Returns500()
    {
        // Arrange
        var mockTab = new Mock<BaseTab>();
        mockTab.Setup(tab => tab.LoadDataAsync()).ThrowsAsync(new Exception("Database error"));
        _mockTabFactory.Setup(factory => factory.CreateTab("home")).Returns(mockTab.Object);

        // Act
        var result = await _controller.GetTabData("home");

        // Assert
        Assert.That(result.Result, Is.TypeOf<ObjectResult>());
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
    }
} 
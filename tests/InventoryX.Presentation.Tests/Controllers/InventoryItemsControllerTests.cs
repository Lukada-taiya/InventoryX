using AutoFixture;
using FluentAssertions;
using InventoryX.Application;
using InventoryX.Application.Queries.Requests.InventoryItems;
using InventoryX.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Presentation.Tests.Controllers
{
    public class InventoryItemsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly InventoryItemsController _sut;

        public InventoryItemsControllerTests()
        {
            _fixture = new Fixture();
            _mediatorMock = _fixture.Freeze<Mock<IMediator>>();
            _sut = new InventoryItemsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            var mockApiResponse = _fixture.Create<ApiResponse>();
            var mockId = _fixture.Create<int>(); 
            _mediatorMock
        .Setup(x => x.Send(It.Is<GetInventoryItemRequest>(r => r.Id == mockId), It.IsAny<CancellationToken>()))
        .ReturnsAsync(mockApiResponse);

            //Act
            var result = await _sut.Get(mockId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().BeEquivalentTo(mockApiResponse);
            _mediatorMock.Verify(x => x.Send(It.Is<GetInventoryItemRequest>(r => r.Id == mockId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

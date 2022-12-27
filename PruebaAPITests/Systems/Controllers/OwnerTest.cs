using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaAPI.Controllers;
using PruebaAPI.DTO;
using PruebaAPI.Exceptions;
using PruebaAPI.Services.OwnerService;
using PruebaAPITests.MockData;

namespace PruebaAPITests;

public class OwnerTest
{
    public Mock<IOwnerService> ownerServiceMock;

    public OwnerTest(){
        ownerServiceMock = new Mock<IOwnerService>();
    }

    [Fact]
    public async Task GetAllOwners_Ok()
    {
        //Arrange
        ownerServiceMock.Setup(_ => _.GetOwners()).ReturnsAsync(new OwnerBuilderDTO().BuildList());
        var controller = new OwnerController(ownerServiceMock.Object);

        //Act
        var result = await controller.GetAllOwners();

        //Assert
        Assert.IsType<OkObjectResult>(result);
       
    }

    [Fact]
    public async Task GetAllOwners_AssertDataOK()
    {
        //Arrange
        ownerServiceMock.Setup(_ => _.GetOwners()).ReturnsAsync(new OwnerBuilderDTO().BuildList());
        var controller = new OwnerController(ownerServiceMock.Object);

        //Act
        var result = await controller.GetAllOwners();
        var objectResult = (OkObjectResult)result;

        //Assert
        if(objectResult.Value is not null){
            var listResult = (ICollection<OwnerDTO>)objectResult.Value;
        Assert.Collection<OwnerDTO>(listResult
        , o1 => Assert.Contains("Henry", o1.FirstName)
        , o2 => Assert.Contains("Carlos", o2.FirstName));
        }      
    }

    [Fact]
    public async Task GetAllOwners_EmptyOk()
    {
        ownerServiceMock.Setup(_ => _.GetOwners()).ReturnsAsync(new OwnerBuilderDTO().BuildEmptyList());
        var controller = new OwnerController(ownerServiceMock.Object);

        var result = await controller.GetAllOwners();
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetOwnerById_OK(){
        ownerServiceMock.Setup(_ => _.GetOwnerById(It.IsAny<int>())).ReturnsAsync(new OwnerBuilderDTO().Build());
        var controller = new OwnerController(ownerServiceMock.Object);

        var result = await controller.GetOwnerById(It.IsAny<int>());

        Assert.IsType<OkObjectResult>(result);

    }
    
    [Fact]
    public async Task GetOwnerById_AssertDataOk(){
        int id = 2;
        ownerServiceMock.Setup(_ => _.GetOwnerById(It.IsAny<int>())).ReturnsAsync(new OwnerBuilderDTO().Build());
        var controller = new OwnerController(ownerServiceMock.Object);

        var result = await controller.GetOwnerById(id);
        var objectResult = (OkObjectResult)result;

        Assert.Equivalent(objectResult.Value, new OwnerBuilderDTO().Build());
    }
    
}
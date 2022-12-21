using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaAPI.Controllers;
using PruebaAPI.DTO;
using PruebaAPI.Services.OwnerService;
using PruebaAPITests.MockData;

namespace PruebaAPITests;

public class OwnerTest
{

    [Fact]
    public async void GetAllOwners_Ok()
    {
        //Arrange
        var ownerService = new Mock<IOwnerService>();
        ownerService.Setup(_ => _.GetOwners()).ReturnsAsync(OwnerMockData.GetOwnersData());
        var cont = new OwnerController(ownerService.Object);

        //Act
        var result = await cont.GetAllOwners();

        //Assert
        Assert.IsType<OkObjectResult>(result);
       
    }

    [Fact]
    public async void GetAllOwners_AssertDataOK()
    {
        //Arrange
        var ownerService = new Mock<IOwnerService>();
        ownerService.Setup(_ => _.GetOwners()).ReturnsAsync(OwnerMockData.GetOwnersData());
        var cont = new OwnerController(ownerService.Object);

        //Act
        var result = await cont.GetAllOwners();
        var objectResult = result as OkObjectResult;

        //Assert
        Assert.Collection<OwnerDTO>((IEnumerable<OwnerDTO>)objectResult.Value
        , o1 => Assert.Contains("Henry", o1.FirstName)
        , o2 => Assert.Contains("Carlos", o2.FirstName));
    
    }
}
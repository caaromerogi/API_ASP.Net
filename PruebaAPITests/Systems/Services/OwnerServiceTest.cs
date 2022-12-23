using AutoMapper;
using DB;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using PruebaAPI.DTO;
using PruebaAPI.Helpers;
using PruebaAPI.Services.OwnerService;
using PruebaAPITests.MockData;

namespace PruebaAPITests;
//https://medium.com/@jonblankenship/global-exception-handling-in-asp-net-core-web-api-36668452844
public class OwnerTestService
{
    private Mock<DbSet<Owner>> _context;
    private IMapper _mapper;
    private Mock<IValidator<CreateOwnerDTO>> _validator;
    public OwnerTestService(Mock<DbSet<Owner>> context, Mock<IValidator<CreateOwnerDTO>> validator)
    {
        //Inject mock dbcontext
        _context = context;

        //Inject Automapper profiles
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);

        //Inject Validator
        _validator = validator;
    }
    /*
    [Fact]
    public async Task GetAllOwners_Success(){
        //Arrange
        var mockDbOwnerSet = new OwnerBuilder().BuildList().AsQueryable();
        _context.As<IQueryable>


        //Act
        var result = await controller.GetAllOwners();

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }*/
}
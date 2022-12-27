using System.Data.Entity.Infrastructure;
using AutoMapper;
using DB;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Helpers;
using PruebaAPI.Services.OwnerService;
using PruebaAPITests.MockData;
using PruebaAPITests.SetUp;

namespace PruebaAPITests;
//https://medium.com/@jonblankenship/global-exception-handling-in-asp-net-core-web-api-36668452844
public class OwnerTestService 
{   
    private Mock<PetClinicContext> _context;
    private Mock<DbSet<Owner>> _set;
    private IMapper _mapper;
    private Mock<IValidator<CreateOwnerDTO>> _validator;

    public OwnerTestService()
    {
        //Inject mock dbcontext
        _context = new Mock<PetClinicContext>(new DbContextOptions<PetClinicContext>());

        //Inject mock set db
        _set = new Mock<DbSet<Owner>>();

        //Inject Automapper profiles
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);


        //Inject Validator
        _validator = new Mock<IValidator<CreateOwnerDTO>>();
    }

    [Fact]
    public async Task GetAllOwners_Success(){

        var ownersList = new OwnerBuilder().BuildList().AsQueryable();
        var expectedList = new OwnerBuilderDTO().BuildList();

        _context.Setup(o => o.Owners).ReturnsDbSet(ownersList);

        var service = new OwnerService(_context.Object, _mapper, _validator.Object);

        var result = await service.GetOwners();

        Assert.Equivalent(expectedList, result);
    }

    [Fact]
    public async Task GetOwnerById_Ok()
    {
        var owner = new OwnerBuilder().BuildList();

        var expectOwnerDTO = new OwnerBuilderDTO().Build();

        var mock = owner.AsQueryable().BuildMockDbSet();

        mock.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(_mapper.Map<OwnerDTO, Owner>(expectOwnerDTO));

        _context.Setup(_ => _.Owners).Returns(mock.Object);

        var service = new OwnerService(_context.Object, _mapper, _validator.Object);

        var result = await service.GetOwnerById(12);

        Assert.Equal(expectOwnerDTO.FirstName, result.FirstName);
        Assert.Equal(expectOwnerDTO.LastName, result.LastName);
        Assert.Collection(result.Pets,
        item =>Assert.Equal(expectOwnerDTO.Pets.First().Name, item.Name));
        Assert.Equal(expectOwnerDTO.OwnerId, result.OwnerId);
    }
    
    [Fact]
    public async Task PostOwner_Ok(){
        var owner = new OwnerBuilder().BuildList();

        var ownerToAdd = new OwnerBuilder().Build();

        var ownerDTOToAdd = new CreateOwnerDTOBuilder().Build();

        var mock = owner.AsQueryable().BuildMockDbSet();

        mock.Setup(_ => _.AddAsync(It.IsAny<Owner>(), It.IsAny<CancellationToken>()))
        .Callback<Owner, CancellationToken>((o, token)=> 
        {
            owner.Add(ownerToAdd);
        }).Returns((Owner o, CancellationToken token) => ValueTask.FromResult(new EntityEntry<Owner>
        (new InternalEntityEntry(
            new Mock<IStateManager>().Object,
            new RuntimeEntityType("Owner",typeof(Owner), false, null, null, null,
             ChangeTrackingStrategy.Snapshot,null,false, null),
             o
        ))));
        
        _context.Setup(_ => _.Owners).Returns(mock.Object);

        _context.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

        _validator.Setup(_ => _.ValidateAsync(It.IsAny<CreateOwnerDTO>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new FluentValidation.Results.ValidationResult());


        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        
        var result = await service.AddOwner(ownerDTOToAdd);

       Assert.Equivalent(ownerDTOToAdd, result);
    }
    

}
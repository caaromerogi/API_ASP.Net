
using System.Collections;
using AutoMapper;
using DB;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Exceptions;
using PruebaAPI.Helpers;
using PruebaAPI.Services.OwnerService;
using PruebaAPITests.MockData;


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
        //Arrange
        var ownersList = new OwnerBuilder().BuildList().AsQueryable();
        var expectedList = new OwnerBuilderDTO().BuildList();
        _context.Setup(_ => _.Owners).ReturnsDbSet(ownersList);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        var result = await service.GetOwners();

        //Assert
        Assert.Equivalent(expectedList, result);
    }

    [Fact]
    public async Task GetOwnerById_Ok()
    {
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var expectOwnerDTO = new OwnerBuilderDTO().Build();
        var mock = owner.AsQueryable().BuildMockDbSet();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>()))
        .ReturnsAsync(_mapper.Map<OwnerDTO, Owner>(expectOwnerDTO));
        _context.Setup(_ => _.Owners).Returns(mock.Object);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        var result = await service.GetOwnerById(12);

        //Assert
        Assert.Equal(expectOwnerDTO.FirstName, result.FirstName);
        Assert.Equal(expectOwnerDTO.LastName, result.LastName);
        Assert.Collection(result.Pets,
        item =>Assert.Equal(expectOwnerDTO.Pets.First().Name, item.Name));
        Assert.Equal(expectOwnerDTO.OwnerId, result.OwnerId);
    }

    [Fact]
    public async Task GetOwnerById_NotFoundException()
    {
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var expectOwnerDTO = new OwnerBuilderDTO().Build();
        var mock = owner.AsQueryable().BuildMockDbSet();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>()))
        .Returns(null);
        _context.Setup(_ => _.Owners).Returns(mock.Object);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        var ex = await Assert
        .ThrowsAsync<ElementNotFoundException>(async ()=>await service.GetOwnerById(4));

        //Another Way
        /*
        try
        {
            var result =await service.GetOwnerById(4);
        }
        catch (ElementNotFoundException excp)
        {
            Assert.Equal("Cannot find the owner with ID:4", excp.Message);
        }
        */     
    }
    
    [Fact]
    public async Task PostOwner_Ok(){
        //Arrange
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

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        var result = await service.AddOwner(ownerDTOToAdd);

        //Assert
        Assert.Equivalent(ownerDTOToAdd, result);
    }

    [Fact]
    public async Task PostOwner_OkEasy(){
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var ownerToAdd = new OwnerBuilder().Build();
        var ownerDTOToAdd = new CreateOwnerDTOBuilder().Build();
        var mock = owner.AsQueryable().BuildMockDbSet();
        _context.Setup(_ => _.Owners).Returns(mock.Object);
        _validator.Setup(_ => _.ValidateAsync(It.IsAny<CreateOwnerDTO>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        //Act
        var service = new OwnerService(_context.Object, _mapper,_validator.Object);
        var result = await service.AddOwner(ownerDTOToAdd);

        //Assert
        Assert.Equivalent(ownerDTOToAdd, result);
        
    }

    [Fact]
    public async Task PostOwner_ValidationFail()
    {
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var ownerToAdd = new OwnerBuilder().Build();
        var ownerDTOToAdd = new CreateOwnerDTOBuilder().Build();
        var mock = owner.AsQueryable().BuildMockDbSet();
        _context.Setup(_ => _.Owners).Returns(mock.Object);
        _validator.Setup(_ => _.ValidateAsync(It.IsAny<CreateOwnerDTO>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ValidationResult(
            new List<ValidationFailure>(){
                new ValidationFailure("FirstName", "Invalid arguments")
            }
            ));
        
        //Act
        var service = new OwnerService(_context.Object, _mapper,_validator.Object);

        //Assert
        var ex = await Assert
        .ThrowsAsync<InvalidElementException<List<ValidationFailure>>>(
            async () => await service.AddOwner(ownerDTOToAdd)
            );
    }

    [Fact]
    public async Task UpdateOwner_Ok(){
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var beforeOwner = new OwnerBuilder().Build();
        var ownerToUpdate = new OwnerBuilder().BuildUpdate();
        var ownerDTOToUpdate = new OwnerBuilderDTO().BuildUpdate();
        var mock = owner.AsQueryable().BuildMockDbSet();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>())).ReturnsAsync(beforeOwner);
        _context.Setup(_ => _.Owners).Returns(mock.Object);
        _context.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .Callback<CancellationToken>(token => {
            var o1 = owner.FirstOrDefault(o => o.OwnerId==12);
            o1.FirstName=ownerToUpdate.FirstName;
            o1.LastName=ownerToUpdate.LastName;
            o1.Pets=ownerToUpdate.Pets;
        })
        .Returns(Task.FromResult(1));

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        await service.UpdateOwner(12, ownerDTOToUpdate);

        //Assert
        Assert.Collection(owner, o => {
            Assert.Equal("Jose",o.FirstName);
            Assert.Equal("Gil",o.LastName);
        },
        o => {
            Assert.Equal("Carlos",o.FirstName);
            Assert.Equal("Romero",o.LastName);
        });
    }

    [Fact]
    public async Task UpdateOwner_Fail(){
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var mock = owner.AsQueryable().BuildMockDbSet();
        var ownerDTOToUpdate = new OwnerBuilderDTO().BuildUpdate();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>())).Returns(null);
        _context.Setup(_ => _.Owners).Returns(mock.Object);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);

        //Assert
        var ex = await Assert
        .ThrowsAsync<ElementNotFoundException>(async ()=>await service.UpdateOwner(12,ownerDTOToUpdate));
    }
    
    [Fact]
    public async Task DeleteOwner_OK(){
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var mock = owner.AsQueryable().BuildMockDbSet();
        var ownerToDelete = new OwnerBuilder().Build();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>())).ReturnsAsync(ownerToDelete);
        /*
        mock.Setup(_ =>_.Remove(ownerToDelete))
        .Returns((Owner o) =>new EntityEntry<Owner>
        (new InternalEntityEntry(
            new Mock<IStateManager>().Object,
            new RuntimeEntityType("Owner",typeof(Owner), false, null, null, null,
            ChangeTrackingStrategy.Snapshot,null,false, null),
            o
        )));
        */
        _context.Setup(_ => _.Owners).Returns(mock.Object);
        _context.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .Callback<CancellationToken>(t => {
            var ownerToRemove = owner.FirstOrDefault(o => o.OwnerId==12);
            owner.Remove(ownerToRemove);
        })
        .ReturnsAsync(1);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);
        await service.DeleteOwner(12);

        //Assert
        Assert.Single(owner);
        Assert.Collection(owner, o=>{
            Assert.Equal("Carlos", o.FirstName);
        });
    }
    
    [Fact]
    public async Task DeleteOwner_Fail(){
        //Arrange
        var owner = new OwnerBuilder().BuildList();
        var mock = owner.AsQueryable().BuildMockDbSet();
        mock.Setup(_ => _.FindAsync(It.IsAny<int>())).Returns(null);
        _context.Setup(_ => _.Owners).Returns(mock.Object);

        //Act
        var service = new OwnerService(_context.Object, _mapper, _validator.Object);

        //Assert
        var ex = await Assert
        .ThrowsAsync<ElementNotFoundException>(async ()=>await service.DeleteOwner(12));

    }
    

}
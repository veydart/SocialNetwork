using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Database;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Models.Request;
using SocialNetwork.Domain.Models.View;
using SocialNetwork.Domain.Services;
using SocialNetwork.Tests.Extensions;

namespace SocialNetwork.Tests.Domain.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> _userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
        }

        [Test]
        public void GetAllUsers_ValidGet()
        {
            //Arrange
            var fixture = new Fixture();

            var users = fixture.CreateMany<User>();
            var userViewModels = users.Select(x => new UserViewModel(x));

            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable());

            //Act
            var result = CreateUserService().GetAll();

            //Assert
            _userRepositoryMock.Verify(x => x.Get(), Times.Once);
            AssertExtension.IsEqual(userViewModels, result);
        }

        [Test]
        public async Task CreateUser_ValidCreate()
        {
            //Arrange
            var model = new UserRequestModel() { Name = "Test" };
            var user = new User(model);

            var createUsers = new List<User>();
            _userRepositoryMock.Setup(x => x.Create(Capture.In(createUsers)));

            //Act
            await CreateUserService().Create(model);

            //Assert
            _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);

            user.Id = createUsers.First().Id;
            AssertExtension.IsEqual(user, createUsers.First());
        }

        private IUserService CreateUserService()
        {
            return new UserService(_userRepositoryMock.Object);
        }
    }
}
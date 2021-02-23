using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using MockQueryable.Moq;
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
    public class UserSubscribeServiceTest
    {
        private Mock<IRepository<User>> _userRepositoryMock;

        private Mock<IRepository<UserSubscriber>> _userSubscriberRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _userSubscriberRepositoryMock = new Mock<IRepository<UserSubscriber>>();
        }

        [Test]
        public void GetPopular_ValidGet()
        {
            //Arrange
            var fixture = new Fixture();

            var users = fixture.CreateMany<User>();
            var userSubscribers = fixture.CreateMany<UserSubscriber>().ToList();

            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable());
            _userSubscriberRepositoryMock.Setup(x => x.Get()).Returns(userSubscribers.AsQueryable);

            //Act
            var result = CreateSubscriberService().GetPopular();

            //Assert
            _userRepositoryMock.Verify(x => x.Get(), Times.Once);
            _userSubscriberRepositoryMock.Verify(x => x.Get(), Times.Once);
            Assert.IsAssignableFrom<EnumerableQuery<PopularUserViewModel>>(result);
        }

        [Test]
        public async Task CreateUserSubscriber_ValidCreate()
        {
            //Arrange
            var fixture = new Fixture();

            var user = new User();
            var subscriber = new User();

            var model = new UserSubscribeRequestModel() { UserId = user.Id, SubscriberId = subscriber.Id };
            var userSubscriber = new UserSubscriber(model);

            var users = new List<User>() { user, subscriber };
            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable().BuildMock().Object);

            var userSubscribers = fixture.CreateMany<UserSubscriber>().ToList();
            _userSubscriberRepositoryMock.Setup(x => x.Get()).Returns(userSubscribers.AsQueryable().BuildMock().Object);

            var createUserSubscribe = new List<UserSubscriber>();
            _userSubscriberRepositoryMock.Setup(x => x.Create(Capture.In(createUserSubscribe)));

            //Act
            await CreateSubscriberService().SubscribeOn(model);

            //Assert
            _userSubscriberRepositoryMock.Verify(x => x.Create(createUserSubscribe.First()), Times.Once);
            AssertExtension.IsEqual(userSubscriber, createUserSubscribe.First());
            _userRepositoryMock.Verify(x => x.Get(), Times.Exactly(2));
            _userSubscriberRepositoryMock.Verify(x => x.Get(), Times.Once);
        }

        [Test]
        public void CreateUserSubscriber_InvalidUserValidation()
        {
            //Arrange
            var user = new User();
            var subscriber = new User();

            var model = new UserSubscribeRequestModel() { UserId = Guid.NewGuid(), SubscriberId = subscriber.Id };

            var users = new List<User>() { user, subscriber };
            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable().BuildMock().Object);

            //Assert
            Assert.ThrowsAsync<ValidationException>(async () => await CreateSubscriberService().SubscribeOn(model));
        }

        [Test]
        public void CreateUserSubscriber_InvalidSubscriberValidation()
        {
            //Arrange
            var user = new User();
            var subscriber = new User();

            var model = new UserSubscribeRequestModel() { UserId = user.Id, SubscriberId = Guid.NewGuid() };

            var users = new List<User>() { user, subscriber };
            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable().BuildMock().Object);

            //Assert
            Assert.ThrowsAsync<ValidationException>(async () => await CreateSubscriberService().SubscribeOn(model));
        }

        [Test]
        public void CreateUserSubscriber_InvalidUserSubscriberValidation()
        {
            //Arrange
            var fixture = new Fixture();

            var user = new User();
            var subscriber = new User();

            var model = new UserSubscribeRequestModel() { UserId = user.Id, SubscriberId = subscriber.Id };

            var users = new List<User>() { user, subscriber };
            _userRepositoryMock.Setup(x => x.Get()).Returns(users.AsQueryable().BuildMock().Object);

            var userSubscribers = fixture.CreateMany<UserSubscriber>().ToList();
            userSubscribers.Add(new UserSubscriber()
            {
                UserId = user.Id,
                SubscriberId = subscriber.Id
            });
            _userSubscriberRepositoryMock.Setup(x => x.Get()).Returns(userSubscribers.AsQueryable().BuildMock().Object);

            //Assert
            Assert.ThrowsAsync<ValidationException>(async () => await CreateSubscriberService().SubscribeOn(model));
        }

        [Test]
        public void CreateUserSubscriber_InvalidSameIdsValidation()
        {
            //Arrange
            var user = new User();

            var model = new UserSubscribeRequestModel() { UserId = user.Id, SubscriberId = user.Id };

            //Assert
            Assert.ThrowsAsync<ValidationException>(async () => await CreateSubscriberService().SubscribeOn(model));
        }

        private IUserSubscribeService CreateSubscriberService()
        {
            return new UserSubscribeService(_userRepositoryMock.Object, _userSubscriberRepositoryMock.Object);
        }
    }
}
using System;
using System.Security;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class SimpleSecurityServiceFixture
    {
        SimpleSecurityService _service;
        Mock<IUserRepository> _userRepositoty;

        [SetUp]
        public void TestSetup()
        {
            _userRepositoty = new Mock<IUserRepository>();
            _service = new SimpleSecurityService(_userRepositoty.Object);
        }

        [Test]
        public void Authenticate_Success()
        {
            User user = new User();
            user.Id = Guid.NewGuid();

            _userRepositoty.Expect(r => r.FindByUserNameAndPassword("userName", "password")).Returns(user);

            string token = _service.Authenticate("userName", "password");

            Assert.AreEqual(user.Id.ToString(), token);

            _userRepositoty.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void Authenticate_InvalidCredentials()
        {
            _userRepositoty.Expect(r => r.FindByUserNameAndPassword("invalid", "credentials")).Returns((User)null);

            _service.Authenticate("invalid", "credentials");
        }

        [Test]
        public void GetAuthenticatedUserFromToken_Success()
        {
            User user = new User();
            user.Id = Guid.NewGuid();

            _userRepositoty.Expect(r => r.FindById(user.Id)).Returns(user);

            User authenticatedUser = _service.GetAuthenticatedUserFromToken(user.Id.ToString());

            Assert.AreEqual(user, authenticatedUser);
        }

        [Test]
        public void GetAuthenticatedUserFromToken_InvalidToken()
        {
            User authenticatedUser = _service.GetAuthenticatedUserFromToken("invalid token");

            Assert.IsNull(authenticatedUser);
        }
    }
}

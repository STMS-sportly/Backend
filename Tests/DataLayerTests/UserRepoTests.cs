using Core.Controllers;
using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Tests.DataLayerTests
{
    public class UserRepoTests
    {
        public Mock<IUserRepository> mock = new Mock<IUserRepository>();
        private IUserRepository? _userRepo;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly User _userEntity;
        private readonly StmsContext context;

        public UserRepoTests()
        {
            _userEntity = new User()
            {
                Email = "tmp@gmail.com",
                Firstname = "test",
                Surname = "test",
                UserId = 1
            };

            _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(repo => repo.GetUserByEmail("tmp@gmail.com")).Returns(_userEntity);
            var options = new DbContextOptionsBuilder<StmsContext>().UseInMemoryDatabase(databaseName: "stmsDb").Options;
            context = new StmsContext(options);
        }

        [Fact]
        public void Test_GetUserByEmail_OK()
        {
            _userRepo = new UserRepository(context);
            var user = _userRepo.GetUserByEmail("tmp@gmail.com");
            Assert.Equal(_userEntity.Email, user.Email);
        }

        [Fact]
        public void Test_GetUserByEmail_Default()
        {
            _userRepo = new UserRepository(context);
            var user = _userRepo.GetUserByEmail("tmp2@gmail.com");
            Assert.Equal(new User().Email, user.Email);
        }

        [Fact]
        public void Test_UserExists_Yes()
        {
            _userRepo = new UserRepository(context);
            var exist = _userRepo.UserExists("tmp@gmail.com");
            Assert.True(exist);
        }

        [Fact]
        public void Test_UserExists_No()
        {
            _userRepo = new UserRepository(context);
            var exist = _userRepo.UserExists("tmp2@gmail.com");
            Assert.False(exist);
        }

        [Fact]
        public void Test_InsertUser_Ok()
        {
            _userRepo = new UserRepository(context);
            var newUser = new User()
            {
                Email = "t@gmail.com",
                Surname = "test",
                Firstname = "test",
                UserId = 2
            };
            _userRepoMock.Setup(repo => repo.InsertUser(newUser));
            _userRepo.InsertUser(newUser);
            _userRepo.Save();
            var userInserted = context.Users.Where(e => e.Email == newUser.Email).FirstOrDefault();
            Assert.True(userInserted.Email == newUser.Email);
        }

        [Fact]
        public void Test_InsertUser_ArgumentExecption_ExistingID()
        {
            _userRepo = new UserRepository(context);
            var newUser = new User()
            {
                Email = "t@gmail.com",
                Surname = "test",
                Firstname = "test",
                UserId = 1
            };
            _userRepo.InsertUser(newUser);
            Assert.Throws<ArgumentException>(() => _userRepo.Save());
        }

        [Fact]
        public void Test_InsertUser_ArgumentExecption_NoEmail()
        {
            _userRepo = new UserRepository(context);
            var newUser = new User()
            {
                Surname = "test",
                Firstname = "test",
                UserId = 2
            };
            _userRepo.InsertUser(newUser);
            Assert.Throws<DbUpdateException>(() => _userRepo.Save());
        }
        [Fact]
        public void Test_DeleteUser_Ok()
        {
            _userRepo = new UserRepository(context);
            _userRepo.DeleteUser("tmp@gmail.com");
            _userRepo.Save();
            Assert.False(_userRepo.UserExists("tmp@gmail.com"));
        }

    }
}
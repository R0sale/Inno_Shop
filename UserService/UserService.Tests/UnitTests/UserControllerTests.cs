using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Application.Contracts;
using Application.Dtos;
using Application.RequestFeatures;
using UserService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Security.Claims;
using Entities.Models;
using MediatR;
using Application.Queries.UserQueries;
using Application.Commands.UserCommands;

namespace UserService.Tests.UnitTests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAllUsers_Test()
        {
            var mockSender = new Mock<ISender>();
            var req = new RequestParams();
            mockSender.Setup(s => s.Send(new GetAllUsersQuery(req), new CancellationToken()))
                .ReturnsAsync(GetUsers());

            var controller = new UserController(mockSender.Object);

            var result = await controller.GetAllUsers(req);

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var userAsserts = Assert.IsAssignableFrom<IEnumerable<UserDTO>>(viewResult.Value);
            Assert.Equal(StatusCodes.Status200OK, viewResult.StatusCode);
            Assert.Equal(2, userAsserts.Count());
        }

        [Fact]
        public async Task GetUser_Test()
        {
            var testId = Guid.NewGuid();
            var testUser = new UserDTO
            {
                Id = testId,
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@mail.ru",
                Roles = new List<string> { "User" },
                UserName = "username"
            };


            var mockSender = new Mock<ISender>();
            mockSender.Setup(s => s.Send(new GetUserByIdQuery(testId.ToString()), new CancellationToken()))
                .ReturnsAsync(testUser);

            var controller = new UserController(mockSender.Object);

            var result = await controller.GetUser(testId);

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var userAsserts = Assert.IsAssignableFrom<UserDTO>(viewResult.Value);
            Assert.Equal(testUser.Id, userAsserts.Id);
            Assert.Equal(testUser.FirstName, userAsserts.FirstName);
            Assert.Equal(testUser.LastName, userAsserts.LastName);
            Assert.Equal(testUser.Email, userAsserts.Email);
            Assert.Equal(testUser.Roles, userAsserts.Roles);
            Assert.Equal(testUser.UserName, userAsserts.UserName);
            Assert.Equal(StatusCodes.Status200OK, viewResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_Test()
        {
            var testId = Guid.NewGuid();

            var mockSender = new Mock<ISender>();
            mockSender.Setup(s => s.Send(new DeleteUserCommand(testId.ToString()), new CancellationToken()))
                .ReturnsAsync(Unit.Value);

            var controller = new UserController(mockSender.Object);

            var result = await controller.DeleteUser(testId);

            var viewResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, viewResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_Test()
        {
            var userForUpd = new UserForUpdateDTO
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@mail.ru",
                Roles = new List<string> { "User" },
                UserName = "username"
            };

            var mockSender = new Mock<ISender>();
            mockSender.Setup(s => s.Send(new UpdateUserCommand(userForUpd), new CancellationToken()))
                .ReturnsAsync(Unit.Value);

            var controller = new UserController(mockSender.Object);

            var result = await controller.UpdateUser(userForUpd);

            var viewResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, viewResult.StatusCode);
        }

        [Fact]
        public async Task PartiallyUpdateProductTest()
        {
            var testId = Guid.NewGuid();

            var patchDoc = new JsonPatchDocument<UserForUpdateDTO>();
            patchDoc.Replace(u => u.FirstName, "Updated FirstName");

            var userForUpd = new UserForUpdateDTO
            {
                FirstName = "OldFirstName",
                LastName = "lastName",
                Email = "email@mail.ru",
                Roles = new List<string> { "User" },
                UserName = "username"
            };

            var userEntity = new User
            {
                Id = testId.ToString(),
                FirstName = "OldFirstName",
                LastName = "lastName",
                Email = "email@mail.ru",
                UserName = "username"
            };

            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(new GetUserForPartialUpdateQuery(testId.ToString()), new CancellationToken()))
                .ReturnsAsync(new ValueTuple<UserForUpdateDTO, User>(userForUpd, userEntity));

            mockSender.Setup(x => x.Send(new PartiallyUpdateUserCommand(userEntity, userForUpd), new CancellationToken()))
                .ReturnsAsync(Unit.Value);

            var controller = new UserController(mockSender.Object);

            controller.ObjectValidator = new Mock<IObjectModelValidator>().Object;

            var result = await controller.PartiallyUpdateProduct(testId, patchDoc);

            Assert.IsType<NoContentResult>(result);
        }

        private IEnumerable<UserDTO> GetUsers()
        {
            var list = new List<UserDTO>()
            {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lexa",
                    LastName = "Dada",
                    Email = "goshanchik@mail.ru",
                    Roles = new List<string> {"User"},
                    UserName = "username"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lexa",
                    LastName = "Dada",
                    Email = "goshanchik@mail.ru",
                    Roles = new List<string> {"User"},
                    UserName = "username"
                }
            };

            return list;
        }
    }
}

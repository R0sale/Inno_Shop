using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using Xunit;
using Entities.Exceptions;
using MediatR;
using Application.Commands.AuthenticationCommands;
using Application.Queries.UserQueries;
using Application.Dtos;

namespace UserService.Tests.UnitTests
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task RegisterUser_ReturnsStatus201_WhenRegistrationIsSuccessful()
        {
            var userForRegistration = new UserForRegistrationDTO
            {
                Email = "test@mail.com",
                Password = "Secure123!",
            };

            var user = new UserDTO
            {
                Id = Guid.NewGuid(),
                Email = "test@mail.com",
                FirstName = "Dada"
            };

            var identityResult = IdentityResult.Success;

            var mockPublisher = new Mock<IPublisher>();

            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(new RegisterUserCommand(userForRegistration), new CancellationToken()))
                .ReturnsAsync(identityResult);

            mockSender.Setup(x => x.Send(new GetUserByEmailQuery(userForRegistration.Email), new CancellationToken()))
                .ReturnsAsync(user);

            var controller = new AuthenticationController(mockSender.Object, mockPublisher.Object);

            var result = await controller.RegisterUser(userForRegistration);

            var statusResult = Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task RegisterUser_ReturnsBadRequest_WhenRegistrationFails()
        {
            var userForRegistration = new UserForRegistrationDTO
            {
                Email = "test@mail.com",
                Password = "Secure123!",
            };

            var identityErrors = new List<IdentityError>
            {
                new IdentityError { Code = "DuplicateEmail", Description = "Email already taken." }
            };
            var failedResult = IdentityResult.Failed(identityErrors.ToArray());

            var mockPublisher = new Mock<IPublisher>();

            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(new RegisterUserCommand(userForRegistration), new CancellationToken()))
                .ReturnsAsync(failedResult);

            var controller = new AuthenticationController(mockSender.Object, mockPublisher.Object);

            var result = await controller.RegisterUser(userForRegistration);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Authenticate_ReturnsOkResult_WithToken_WhenCredentialsAreValid()
        {
            var userForAuth = new UserForAuthenticationDTO
            {
                UserName = "userName",
                Password = "Secure123!"
            };

            var mockPublisher = new Mock<IPublisher>();

            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(new ValidateUserCommand(userForAuth), new CancellationToken()))
                .ReturnsAsync(true);

            mockSender.Setup(x => x.Send(new CreateTokenCommand(userForAuth.UserName), new CancellationToken()))
                .ReturnsAsync("mocked-jwt-token");

            var controller = new AuthenticationController(mockSender.Object, mockPublisher.Object);

            var result = await controller.Authenticate(userForAuth);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenObj = Assert.IsAssignableFrom<TokenResponse>(okResult.Value);
            Assert.Equal("mocked-jwt-token", tokenObj.Token);
        }

        [Fact]
        public async Task Authenticate_ThrowsUnauthorizedException_WhenCredentialsAreInvalid()
        {
            var userForAuth = new UserForAuthenticationDTO
            {
                UserName = "userName",
                Password = "WrongPassword"
            };

            var mockPublisher = new Mock<IPublisher>();

            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(new ValidateUserCommand(userForAuth), new CancellationToken()))
                .Throws(new InvalidUserNameOrPasswordException("userName"));

            var controller = new AuthenticationController(mockSender.Object, mockPublisher.Object);

            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(() => controller.Authenticate(userForAuth));
        }

    }
}

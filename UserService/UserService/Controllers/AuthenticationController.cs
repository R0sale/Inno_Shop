using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Application.Dtos;
using MediatR;
using Application.Commands;
using Application.Notifications;
using Application.Commands.AuthenticationCommands;
using Application.Queries.UserQueries;
using Application.Queries;
using Entities.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public AuthenticationController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistration)
        {
            var result = await _sender.Send(new RegisterUserCommand(userForRegistration));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest();
            }

            var user = await _sender.Send(new GetUserByEmailQuery(userForRegistration.Email));

            await _publisher.Publish(new UserConfirmingEmailNotification(user.Id.ToString()));

            return CreatedAtRoute(routeName: "UserById",
                                  routeValues: new { id = user.Id },
                                  value: new { message = "To end registration check your email and follow the link." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO userForAuth)
        {
            var isEmailConfirmed = await _sender.Send(new ValidateUserCommand(userForAuth));

            if (!isEmailConfirmed)
            {
                var user = await _sender.Send(new GetUserByUserNameQuery(userForAuth.UserName));

                await _publisher.Publish(new UserConfirmingEmailNotification(user.Id.ToString()));

                return Unauthorized(new
                {
                    StatusCode = 401,
                    Message = "Your email is not confirmed"
                });
            }

            return Ok(new TokenResponse
            {
                Token = await _sender.Send(new CreateTokenCommand(userForAuth.UserName))
            });
        }

        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string encodedToken)
        {
            var result = await _sender.Send(new ConfirmEmailCommand(userId, encodedToken));

            if (result.Succeeded)
                return Ok("Email confirmed.");
            else
                return Content("Couldn't confirm email.");
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserDTO restoreUser)
        {
            await _publisher.Publish(new SendChangePasswordEmailNotification(restoreUser));

            return Content("To change your password check the email and follow the link.");
        }

        [HttpGet("confirmPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPassword(string userId, string encodedToken, string newPassword)
        {
            var result = await _sender.Send(new ConfirmPasswordCommand(userId, encodedToken, newPassword));

            if (result.Succeeded)
                return Ok("Password changed.");
            else
                return Content("Couldn't change your password.");
        }
    }
}

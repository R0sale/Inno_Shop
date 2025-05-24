using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using UserService.ActionFilters;
using Application.Contracts;
using Application.Dtos;
using Application.RequestFeatures;
using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using MediatR;
using Application.Queries;
using Application.Commands;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] RequestParams param)
        {
            var users = await _sender.Send(new GetAllUsersQuery(param));

            return Ok(users);
        }


        [HttpGet("{id:guid}", Name = "UserById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _sender.Send(new GetUserByIdQuery(id.ToString()));

            return Ok(user);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _sender.Send(new DeleteUserCommand(id.ToString()));

            return NoContent();
        }


        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> UpdateUser([FromBody] UserForUpdateDTO userForUpdate)
        {
            await _sender.Send(new UpdateUserCommand(userForUpdate));

            return NoContent();
        }


        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateProduct(Guid id, [FromBody] JsonPatchDocument<UserForUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("The patchDoc is null.");

            var result = await _sender.Send(new GetUserForPartialUpdateQuery(id.ToString()));
            patchDoc.ApplyTo(result.userForUpd);

            TryValidateModel(result.userForUpd);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _sender.Send(new PartiallyUpdateUserCommand(result.user, result.userForUpd));

            return NoContent();
        }
    }
}

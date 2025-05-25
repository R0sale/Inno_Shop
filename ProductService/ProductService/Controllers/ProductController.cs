using Azure;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Application.Dtos;
using Application.RequestFeatures;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MediatR;
using Application.Queries;
using Application.Commands;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "Products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParams productParams)
        {
            var products = await _sender.Send(new GetAllProductsQuery(productParams, false));

            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _sender.Send(new GetProductByIdQuery(id, false));

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDTO productForCreation)
        {
            var productDTO = await _sender.Send(new CreateProductCommand(productForCreation));

            return CreatedAtRoute("ProductById", new { id = productDTO.Id }, productDTO);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _sender.Send(new DeleteProductCommand(id, User, false));

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDTO productForUpd)
        {
            await _sender.Send(new UpdateProductCommand(id, User, productForUpd, true));

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> PartiallyUpdateProduct(Guid id, [FromBody] JsonPatchDocument<ProductForUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("The patchDoc is null.");

            var result = await _sender.Send(new GetProductForPartialUpdateQuery(id, User, true));
            patchDoc.ApplyTo(result.productForUpd);

            TryValidateModel(result.productForUpd);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _sender.Send(new SaveChangesForPartialUpdateCommand(result.productForUpd, result.product));
            return NoContent();
        }
    }
}

using Application.Commands;
using Application.Contracts;
using Application.Dtos;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IProductValidationService _validation;
        private readonly IHttpClient _client;

        public DeleteProductHandler(IProductRepository repository, IMapper mapper, IProductValidationService validation, IHttpClient client)
        {
            _repository = repository;
            _validation = validation;
            _client = client;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellation)
        {
            var product = await _validation.CheckAndFindIfProductExists(request.Id, request.TrackChanges);

            if (request.User.Identity.Name is null)
                throw new ClaimsPrincipalNameNullException();

            var ownerId = request.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null)
                throw new ClaimsPrincipalIdNullException();

            if (!product.OwnerId.ToString().Equals(ownerId))
                throw new UserNotCorrespondException(new Guid(ownerId));

            _repository.DeleteProductRep(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}

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

            var user = await _client.GetUser(product.OwnerId);

            if (!user.UserName.Equals(request.User.Identity.Name))
                throw new UserNotCorrespondException(user.Id);

            _repository.DeleteProductRep(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}

using Application.Commands;
using Application.Contracts;
using Application.Dtos;
using AutoMapper;
using Entities.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductValidationService _validation;
        private readonly IHttpClient _client;

        public UpdateProductHandler(IProductRepository repository, IMapper mapper, IProductValidationService validation, IHttpClient client)
        {
            _repository = repository;
            _mapper = mapper;
            _validation = validation;
            _client = client;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellation)
        {
            var product = await _validation.CheckAndFindIfProductExists(request.Id, request.TrackChanges);

            var user = await _client.GetUser(product.OwnerId);

            if (user.UserName is null)
                throw new UserNotFoundException(user.Id);

            if (!user.UserName.Equals(request.User.Identity.Name))
                throw new UserNotCorrespondException(user.Id);

            var newOwner = await _client.GetUser(request.ProductForUpd.OwnerId);

            if (newOwner.UserName is null)
                throw new UserNotFoundException(user.Id);

            _mapper.Map(request.ProductForUpd, product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}

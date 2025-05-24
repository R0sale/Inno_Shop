using Application.Contracts;
using Application.Dtos;
using Application.Queries;
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
    class GetProductForPartialUpdateHandler : IRequestHandler<GetProductForPartialUpdateQuery, (ProductForUpdateDTO productForUpd, Product product)>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductValidationService _validation;
        private readonly IHttpClient _client;

        public GetProductForPartialUpdateHandler(IProductRepository repository, IMapper mapper, IProductValidationService validation, IHttpClient client)
        {
            _repository = repository;
            _mapper = mapper;
            _validation = validation;
            _client = client;
        }

        public async Task<(ProductForUpdateDTO productForUpd, Product product)> Handle(GetProductForPartialUpdateQuery request, CancellationToken cancellation)
        {
            var product = await _validation.CheckAndFindIfProductExists(request.Id, request.TrackChanges);

            var user = await _client.GetUser(product.OwnerId);

            if (!user.UserName.Equals(request.User.Identity.Name))
                throw new UserNotCorrespondException(user.Id);

            var productForUpd = _mapper.Map<ProductForUpdateDTO>(product);

            return (productForUpd, product);
        }
    }
}

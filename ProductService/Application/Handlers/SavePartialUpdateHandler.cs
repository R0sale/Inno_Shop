using Application.Commands;
using Application.Contracts;
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
    public class SavePartialUpdateHandler : IRequestHandler<SaveChangesForPartialUpdateCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public SavePartialUpdateHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveChangesForPartialUpdateCommand request, CancellationToken cancellation)
        {
            _mapper.Map(request.productForUpd, request.product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}

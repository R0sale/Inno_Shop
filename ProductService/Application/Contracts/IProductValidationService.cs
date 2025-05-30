﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IProductValidationService
    {
        Task<Product> CheckAndFindIfProductExists(Guid id, bool trackChanges);
    }
}

﻿using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface ITaxRepository : IBaseIdentityRepository<Tax, IdentityContext>
    {
    }
}

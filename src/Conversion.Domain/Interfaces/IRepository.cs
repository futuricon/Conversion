﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversion.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Upsert(T entity);
    }
}

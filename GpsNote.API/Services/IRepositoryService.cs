using GpsNote.API.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GpsNote.API.Services
{
    public interface IRepositoryService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> pred = null) where T : class;

        Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> pred = null) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task DeleteAsync<T>(T entity) where T : class;

        Task UpdateAsync<T>(T entity) where T : class;
    }
}

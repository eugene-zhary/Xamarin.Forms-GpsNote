using GpsNote.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace GpsNote.Services.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetRowsAsync<T>(Expression<Func<T, bool>> func) where T : IEntityModel, new();
        Task<T> FindAsync<T>(Expression<Func<T, bool>> func) where T : IEntityModel, new();

        Task AddAsync<T>(T item) where T : IEntityModel, new();
        Task RemoveAsync<T>(T item) where T : IEntityModel, new();
        Task UpdateAsync<T>(T item) where T : IEntityModel, new();
        Task AddOrUpdataAsync<T>(T item) where T : IEntityModel, new();
    }
}

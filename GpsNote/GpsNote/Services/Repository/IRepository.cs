using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using GpsNote.Interfaces;

namespace GpsNote.Services.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetRowsAsync<T>(Expression<Func<T, bool>> func) 
            where T : IEntityModel, new();

        Task<T> FindAsync<T>(Expression<Func<T, bool>> func) 
            where T : IEntityModel, new();

        Task AddAsync<T>(T item)
            where T : IEntityModel, new();

        Task RemoveAsync<T>(T item)
            where T : IEntityModel, new();

        Task UpdateAsync<T>(T item)
            where T : IEntityModel, new();
    }
}

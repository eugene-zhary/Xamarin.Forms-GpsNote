using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpsNote.Services.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T : IEntityModel, new();
        Task<IEnumerable<T>> GetAllWithCommand<T>(string sql) where T : IEntityModel, new();
        Task<T> FindWithCommand<T>(string sql) where T : IEntityModel, new();
        Task Add<T>(T item) where T : IEntityModel, new();
        Task Remove<T>(T item) where T : IEntityModel, new();
        Task Update<T>(T item) where T : IEntityModel, new();
        Task AddOrUpdata<T>(T item) where T : IEntityModel, new();
    }
}

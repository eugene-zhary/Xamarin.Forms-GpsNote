using GpsNote.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace GpsNote.Services.Repository
{
    public class Repository : IRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public Repository()
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "GpsNote.db");

            _connection = new SQLiteAsyncConnection(databasePath);
        }

        #region -- IRepostitory implementation --

        public async Task<IEnumerable<T>> GetRowsAsync<T>(Expression<Func<T, bool>> func) where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            return await _connection.Table<T>().Where(func).ToListAsync();
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> func) where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            return await _connection.Table<T>().Where(func).FirstOrDefaultAsync();
        }

        public async Task AddAsync<T>(T item) where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            await _connection.InsertAsync(item);
        }

        public async Task RemoveAsync<T>(T item) where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            await _connection.DeleteAsync(item);
        }

        public async Task UpdateAsync<T>(T item) where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            await _connection.UpdateAsync(item);
        }

        public async Task AddOrUpdataAsync<T>(T item) where T : IEntityModel, new()
        {
            if(item.Id == 0)
            {
                await AddAsync(item);
            }
            else
            {
                await UpdateAsync(item);
            }
        }

        public async Task DeleteAllAsync<T>() where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();
            await _connection.DeleteAllAsync<T>();
        }

        #endregion
    }
}

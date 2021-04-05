using GpsNote.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GpsNote.Services.Repository
{
    public class Repository : IRepository
    {
        private SQLiteAsyncConnection _connection;

        #region -- IRepostitory implementation --

        public async Task<IEnumerable<T>> GetAll<T>() where T : IEntityModel,new()
        {
            await CreateConnection<T>();
            return await _connection.Table<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithCommand<T>(string sql) where T : IEntityModel, new()
        {
            await CreateConnection<T>();
            return await _connection.QueryAsync<T>(sql);
        }

        public async Task<T> FindWithCommand<T>(string sql) where T : IEntityModel, new()
        {
            await CreateConnection<T>();
            return await _connection.FindWithQueryAsync<T>(sql);
        }

        public async Task Add<T>(T item) where T : IEntityModel, new()
        {
            await CreateConnection<T>();
            await _connection.InsertAsync(item);
        }

        public async Task Remove<T>(T item) where T : IEntityModel, new()
        {
            await CreateConnection<T>();
            await _connection.DeleteAsync(item);
        }

        public async Task Update<T>(T item) where T : IEntityModel, new()
        {
            await CreateConnection<T>();
            await _connection.UpdateAsync(item);
        }

        public async Task AddOrUpdata<T>(T item) where T : IEntityModel, new()
        {
            if (item.Id == 0)
            {
                await Add(item);
            }
            else
            {
                await Update(item);
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task CreateConnection<T>() where T : IEntityModel, new()
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "GpsNote.db");

            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<T>();
        }

        #endregion
    }
}

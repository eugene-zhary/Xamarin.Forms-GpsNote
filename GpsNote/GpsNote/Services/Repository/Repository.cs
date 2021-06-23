using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GpsNote.Interfaces;

namespace GpsNote.Services.Repository
{
    public class Repository : IRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public Repository()
        {
            string localDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databasePath = Path.Combine(localDocumentsPath, Constants.Database.DOCUMENT_PATH);

            _connection = new SQLiteAsyncConnection(databasePath);
        }

        #region -- IRepostitory implementation --

        public async Task<IEnumerable<T>> GetRowsAsync<T>(Expression<Func<T, bool>> func)
            where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();

            return await _connection.Table<T>().Where(func).ToListAsync();
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> func)
            where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();

            return await _connection.Table<T>().Where(func).FirstOrDefaultAsync();
        }

        public async Task AddAsync<T>(T item)
            where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();

            await _connection.InsertAsync(item);
        }

        public async Task RemoveAsync<T>(T item)
            where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();

            await _connection.DeleteAsync(item);
        }

        public async Task UpdateAsync<T>(T item)
            where T : IEntityModel, new()
        {
            await _connection.CreateTableAsync<T>();

            await _connection.UpdateAsync(item);
        }

        #endregion
    }
}

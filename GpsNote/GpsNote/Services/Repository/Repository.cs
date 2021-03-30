using GpsNote.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GpsNote.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : IEntityModel, new()
    {
        private SQLiteAsyncConnection connection;

        public async Task<IEnumerable<T>> GetAll()
        {
            await CreateConnection();
            return await connection.Table<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithCommand(string sql)
        {
            await CreateConnection();
            return await connection.QueryAsync<T>(sql);
        }

        public async Task<T> FindWithCommand(string sql)
        {
            await CreateConnection();
            return await connection.FindWithQueryAsync<T>(sql);
        }

        public async Task Add(T item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
        }

        public async Task Remove(T item)
        {
            await CreateConnection();
            await connection.DeleteAsync(item);
        }

        public async Task Update(T item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
        }

        public async Task AddOrUpdata(T item)
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

        // create connection with database
        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "GpsNote.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<T>();
        }
    }
}

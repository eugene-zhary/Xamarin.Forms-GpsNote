using GpsNote.API.Data;
using GpsNote.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GpsNote.API.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DataContext _context;

        public RepositoryService(DataContext context)
        {
            _context = context;
        }

        #region -- IRepositoryService implementation --

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> pred = null) where T : class
        {
            var dbSet = _context.Set<T>();

            return pred == null
                ? await dbSet.AsNoTracking().ToListAsync()
                : await dbSet.AsNoTracking().Where(pred).ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> pred = null) where T : class
        {
            var dbSet = _context.Set<T>();

            return pred == null
                ? await dbSet.AsNoTracking().FirstOrDefaultAsync()
                : await dbSet.AsNoTracking().FirstOrDefaultAsync(pred);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            var dbSet = _context.Set<T>();

            dbSet.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            var dbSet = _context.Set<T>();

            dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            var dbSet = _context.Set<T>();

            dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}

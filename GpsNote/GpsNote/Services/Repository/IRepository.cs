using GpsNote.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services.Repository
{
    public interface IRepository<T> where T : IEntityModel, new()
    {
        /// <summary>
        /// get all elements from database
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// get all elements with sql command
        /// </summary>
        /// <param name="sql">sql command</param>
        /// <returns>IEnumerable<T></returns>
        Task<IEnumerable<T>> GetAllWithCommand(string sql);

        /// <summary>
        /// find element with sql command
        /// </summary>
        /// <param name="sql">sql command</param>
        Task<T> FindWithCommand(string sql);

        /// <summary>
        /// Add new element to the database
        /// </summary>
        /// <param name="element">Element Model</param>
        Task Add(T element);

        /// <summary>
        /// Remove the element from the database
        /// </summary>
        /// <param name="element">Element Model</param>
        Task Remove(T element);

        /// <summary>
        /// Update the element in the database
        /// </summary>
        /// <param name="element">Element Model</param>
        Task Update(T element);

        /// <summary>
        /// Add element if element doesn't exist in the database or update the exist element
        /// </summary>
        /// <param name="element">Element Model</param>
        Task AddOrUpdata(T element);
    }
}

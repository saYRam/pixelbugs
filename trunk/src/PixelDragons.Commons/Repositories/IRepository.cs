using System;
using Castle.MonoRail.Framework.Helpers;

namespace PixelDragons.Commons.Repositories
{
    /// <summary>
    /// A persistence repository for a particular entity type
    /// </summary>
    /// <typeparam name="T">The entity class that this repository operates on</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Load the entity from the persistence store
        /// Will throw an exception if there isn't an entity that matches
        /// the id.
        /// </summary>
        /// <param name="id">The entity's id</param>
        /// <returns>The entity that matches the id</returns>
        T FindById(Guid id);

        /// <summary>
        /// Register the entity for deletion when the unit of work
        /// is completed. 
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Register the entity for save in the database when the unit of work
        /// is completed. (INSERT)
        /// </summary>
        /// <param name="entity">the entity to save</param>
        void Save(T entity);

        /// <summary>
        /// Loads all the entities that match the criteria
        /// by order
        /// </summary>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>All the entities that match the criteria</returns>
        T[] Find(IQueryBuilder query);

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The entities that match the criteria in the slice</returns>
        T[] SlicedFind(int firstResult, int maxResults, IQueryBuilder query);

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The entities that match the criteria in the slice and the total number of matching records</returns>
        SliceAndCount<T> SlicedFindWithTotalCount(int firstResult, int maxResults, IQueryBuilder query);

        /// <summary>
        /// Loads a page of entities based on the criteria, page size and page number.
        /// </summary>
        /// <param name="page">The 1 based page number to return (zero will default to page 1)</param>
        /// <param name="pageSize">The size of the page to return</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The page of entities that match the criteria</returns>
        IPaginatedPage PagedFind(int page, int pageSize, IQueryBuilder query);
    }
}
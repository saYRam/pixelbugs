using System;
using Castle.ActiveRecord;
using Castle.MonoRail.Framework.Helpers;

namespace PixelDragons.Commons.Repositories
{
    /// <summary>
    /// An <c>ActiveRecord</c> persistence repository for a particular entity type
    /// </summary>
    /// <typeparam name="T">The entity class that this repository operates on</typeparam>
    public class ARRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Register the entity for deletion when the unit of work
        /// is completed. 
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(T entity)
        {
            ActiveRecordMediator<T>.Delete(entity);
        }

        /// <summary>
        /// Register the entity for save in the database when the unit of work
        /// is completed. (INSERT)
        /// </summary>
        /// <param name="entity">the entity to save</param>
        /// <returns>The saved entity</returns>
        public T Save(T entity)
        {
            ActiveRecordMediator<T>.Save(entity);

            return entity;
        }

        /// <summary>
        /// Load the entity from the persistence store
        /// Will throw an exception if there isn't an entity that matches
        /// the id.
        /// </summary>
        /// <param name="id">The entity's id</param>
        /// <returns>The entity that matches the id</returns>
        public T FindById(Guid id)
        {
            return ActiveRecordMediator<T>.FindByPrimaryKey(id);
        }

        /// <summary>
        /// Loads all the entities that match the criteria
        /// by order
        /// </summary>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>All the entities that match the criteria</returns>
        public T[] Find(IQueryBuilder query)
        {
            return ActiveRecordMediator<T>.FindAll(query.BuildQuery());
        }

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The entities that match the criteria in the slice</returns>
        public T[] SlicedFind(int firstResult, int maxResults, IQueryBuilder query)
        {
            return ActiveRecordMediator<T>.SlicedFindAll(firstResult, maxResults, query.BuildQuery());
        }

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The entities that match the criteria in the slice and the total number of matching records</returns>
        public SliceAndCount<T> SlicedFindWithTotalCount(int firstResult, int maxResults, IQueryBuilder query)
        {
            SliceAndCount<T> sliceAndCount = new SliceAndCount<T>
            {
                TotalCount = ActiveRecordMediator<T>.Count(query.BuildQuery()),
                Slice = SlicedFind(firstResult, maxResults, query)
            };

            return sliceAndCount;
        }

        /// <summary>
        /// Loads a page of entities based on the criteria, page size and page number.
        /// </summary>
        /// <param name="page">The 1 based page number to return (zero will default to page 1)</param>
        /// <param name="pageSize">The size of the page to return</param>
        /// <param name="query">the query builder to use when querying</param>
        /// <returns>The page of entities that match the criteria</returns>
        public IPaginatedPage PagedFind(int page, int pageSize, IQueryBuilder query)
        {
            page = (page == 0) ? 1 : page;

            int firstResult = ((page - 1) * pageSize);

            SliceAndCount<T> entities = SlicedFindWithTotalCount(firstResult, pageSize, query);

            return PaginationHelper.CreateCustomPage<T>(entities.Slice, pageSize, page, entities.TotalCount);
        }
    }
}
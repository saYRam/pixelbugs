using System;
using System.Collections.Generic;
using Castle.MonoRail.Framework.Helpers;
using NHibernate.Criterion;

namespace PixelDragons.Commons.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Load the entity from the persistance store
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
        /// <returns>The saved entity</returns>
        T Save(T entity);

        /// <summary>
        /// Loads all the entities
        /// </summary>
        /// <returns>All the entities</returns>
        T[] FindAll();

        /// <summary>
        /// Loads all the entities that match the criteria
        /// by order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>All the entities that match the criteria</returns>
        T[] FindAll(Order order, params ICriterion[] criteria);

        /// <summary>
        /// Loads all the entities that match the criteria
        /// by order
        /// </summary>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>All the entities that match the criteria</returns>
        T[] FindAll(DetachedCriteria criteria);

        /// <summary>
        /// Loads all the entities that match the criteria
        /// by order
        /// </summary>
        /// <param name="orders">the order to load the entities</param>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>All the entities that match the criteria</returns>
        T[] FindAll(Order[] orders, params ICriterion[] criteria);

        /// <summary>
        /// Loads all the entities that match the criteria
        /// </summary>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>All the entities that match the criteria</returns>
        T[] FindAll(params ICriterion[] criteria);

        /// <summary>
        /// Gets a list of entities that match a particular property
        /// </summary>
        /// <param name="property">The property to use in the match</param>
        /// <param name="value">The value to match</param>
        /// <returns>An array of entities</returns>
        T[] FindAllByProperty(string property, object value);

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="orders">the order to load the entities</param>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>The entities that match the criteria in the slice</returns>
        T[] SlicedFind(int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria);

        /// <summary>
        /// Loads the entities that match the criteria and are part of the specified slice
        /// </summary>
        /// <param name="firstResult">The index of the first row to return</param>
        /// <param name="maxResults">The number of records to return in this slice</param>
        /// <param name="orders">the order to load the entities</param>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>The entities that match the criteria in the slice and the total number of matching records</returns>
        SliceAndCount<T> SlicedFindWithTotalCount(int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">The 1 based page number to return (zero will default to page 1)</param>
        /// <param name="pageSize">The size of the page to return</param>
        /// <param name="orders">the order to load the entities</param>
        /// <param name="criteria">the criteria to look for</param>
        /// <returns>The page of entities that match the criteria</returns>
        IPaginatedPage PagedFind(int page, int pageSize, Order[] orders, params ICriterion[] criteria);

        /// <summary>
        /// Find a single entity based on a criteria.
        /// Thorws is there is more than one result.
        /// </summary>
        /// <param name="criteria">The criteria to look for</param>
        /// <returns>The entity or null</returns>
        T FindOne(params ICriterion[] criteria);

        /// <summary>
        /// Find a single entity based on a criteria.
        /// Thorws is there is more than one result.
        /// </summary>
        /// <param name="criteria">The criteria to look for</param>
        /// <returns>The entity or null</returns>
        T FindOne(DetachedCriteria criteria);

        /// <summary>
        /// Find the entity based on a criteria.
        /// </summary>
        /// <param name="criteria">The criteria to look for</param>
        /// <param name="orders">Optional orderring</param>
        /// <returns>The entity or null</returns>
        T FindFirst(DetachedCriteria criteria, params Order[] orders);

        /// <summary>
        /// Find the first entity of type
        /// </summary>
        /// <param name="orders">Optional orderring</param>
        /// <returns>The entity or null</returns>
        T FindFirst(params Order[] orders);

        /// <summary>
        /// Check if any instance matches the criteria.
        /// </summary>
        /// <returns><c>true</c> if an instance is found; otherwise <c>false</c>.</returns>
        bool Exists(DetachedCriteria criteria);

        /// <summary>
        /// Check if any instance of the type exists
        /// </summary>
        /// <returns><c>true</c> if an instance is found; otherwise <c>false</c>.</returns>
        bool Exists();

        /// <summary>
        /// Counts the number of instances matching the criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(params ICriterion[] criteria);

        /// <summary>
        /// Counts the number of instances matching the criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int Count(DetachedCriteria criteria);

        /// <summary>
        /// Counts the overall number of instances.
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}

using System;
using Castle.ActiveRecord;
using NHibernate;
using System.Collections.Generic;
using Castle.Services.Transaction;
using Castle.MonoRail.Framework.Helpers;
using NHibernate.Criterion;

namespace PixelDragons.Commons.Repositories
{
    [Transactional]
    public class ARRepository<T> : IRepository<T> where T : class
    {
        [Transaction(Castle.Services.Transaction.TransactionMode.Requires)]
        public void Delete(T entity)
        {
            ActiveRecordMediator<T>.Delete(entity);
        }

        [Transaction(Castle.Services.Transaction.TransactionMode.Requires)]
        public T Save(T entity)
        {
            ActiveRecordMediator<T>.Save(entity);

            return entity;
        }

        public T FindById(Guid id)
        {
            return ActiveRecordMediator<T>.FindByPrimaryKey(id);
        }

        public T[] FindAll()
        {
            return ActiveRecordMediator<T>.FindAll();
        }

        public T[] FindAll(Order order, params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.FindAll(new Order[] { order }, criteria);
        }

        public T[] FindAll(DetachedCriteria criteria)
        {
            return ActiveRecordMediator<T>.FindAll(criteria);
        }        

        public T[] FindAll(Order[] orders, params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.FindAll(orders, criteria);
        }

        public T[] FindAll(params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.FindAll(criteria);
        }

        public T[] FindAllByProperty(string property, object value)
        {
            return (T[])ActiveRecordMediator<T>.FindAllByProperty(typeof(T), property, value);
        }

        public T[] SlicedFind(int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.SlicedFindAll(firstResult, maxResults, orders, criteria);
        }

        public SliceAndCount<T> SlicedFindWithTotalCount(int firstResult, int maxResults, Order[] orders, params ICriterion[] criteria)
        { 
            SliceAndCount<T> sliceAndCount = new SliceAndCount<T>();

            sliceAndCount.TotalCount = ActiveRecordMediator<T>.Count(criteria);     //May need to clone the criteria, see link: http://groups.google.com/group/castle-project-users/browse_thread/thread/39f9b4e9fa5e6e3f#
            sliceAndCount.Slice = SlicedFind(firstResult, maxResults, orders, criteria);

            return sliceAndCount;
        }

        public IPaginatedPage PagedFind(int page, int pageSize, Order[] orders, params ICriterion[] criteria)
        {
            page = (page == 0) ? 1 : page;

            int firstResult = ((page - 1) * pageSize);

            SliceAndCount<T> entities = SlicedFindWithTotalCount(firstResult, pageSize, orders, criteria);

            return PaginationHelper.CreateCustomPage<T>(entities.Slice, pageSize, page, entities.TotalCount);
        }

        public T FindOne(params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.FindOne(criteria);
        }

        public T FindOne(DetachedCriteria criteria)
        {
            return ActiveRecordMediator<T>.FindOne(criteria);
        }

        public T FindFirst(DetachedCriteria criteria, params Order[] orders)
        {
            return ActiveRecordMediator<T>.FindFirst(criteria, orders);
        }

        public T FindFirst(params Order[] orders)
        {
            return ActiveRecordMediator<T>.FindFirst(orders);
        }

        public bool Exists(DetachedCriteria criteria)
        {
            return ActiveRecordMediator<T>.Exists(criteria);
        }

        public bool Exists()
        {
            return ActiveRecordMediator<T>.Exists();
        }

        public int Count(params ICriterion[] criteria)
        {
            return ActiveRecordMediator<T>.Count(criteria);
        }

        public int Count(DetachedCriteria criteria)
        {
            return ActiveRecordMediator<T>.Count(criteria);
        }

        public int Count()
        {
            return ActiveRecordMediator<T>.Count();
        }
    }
}

using System;
using System.Collections.Generic;
namespace DapperStudy.DaoBase
{
    /// <summary>
    /// dao基类接口
    ///  2017/04/13 fhr
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IEntityBaseDao<T>
     where T : class
    {
        void Delete(T obj);
        void DeleteById(long id);
        void Dispose();
        System.Collections.Generic.IEnumerable<T> FindAll();
        T FindById(long id);
        IEnumerable<T> FindBySQL(string sql, object objectParam);
        dynamic Save(T obj);
        dynamic Update(T obj);
    }
}

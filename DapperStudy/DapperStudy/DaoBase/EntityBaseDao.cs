using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperStudy.Utils;
namespace DapperStudy.DaoBase
{
    /// <summary>
    /// 依靠Dapper的dao基类
    /// 2017/04/13 fhr
    /// </summary>
    public class EntityBaseDao<T> :IEntityBaseDao<T>,IDisposable where T:class
    {
        public void Delete(T obj)
        {
            DapperUtil.DeleteEntity<T>(obj);
        }

        public dynamic Save(T obj)
        {
            try
            {
                return DapperUtil.SaveByExtension(obj);
            }
            catch
            {
                return null;
            }
        }

        public dynamic Update(T obj)
        {
            try
            {
                return DapperUtil.UpdateByExtension<T>(obj);
            }
            catch
            {
                return null;
            }
        }
      
        public T FindById(long id)
        {
            return DapperUtil.QueryByIdExtension<T>(id);
        }
       
        public IEnumerable<T> FindAll()
        {
            return DapperUtil.QueryAllByExtension<T>();
        }
       
        public void DeleteById(long id)
        {
            DapperUtil.DeleteEntity<T>(id);
        }
      
        public IEnumerable<T> FindBySQL(string sql,object objectParam)
        {
            return DapperUtil.Query<T>(sql, objectParam);
        }
        public void Dispose()
        {
          //noting
        }
    }
}

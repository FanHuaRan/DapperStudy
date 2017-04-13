using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace DapperStudy.Utils
{
    /// <summary>
    /// Dapper数据访问封装
    /// 2017/04/13 fhr
    /// </summary>
    public class DapperUtil
    {
        /// <summary>
        /// 获取已开启的数据库链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["musicdb"].ConnectionString.ToString());
            connection.Open();
            return connection;
        }
        /// <summary>
        /// 泛型查询多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string sql, object objectParam = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query<T>(sql, objectParam);
            }
        }
        /// <summary>
        /// 通过匿名类型查询多个对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objectParam"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> Query(string sql, object objectParam = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query(sql, objectParam);
            }
        }
        /// <summary>
        /// 查询单个对象 泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="objectParam"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(string sql, object objectParam = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.QueryFirstOrDefault<T>(sql, objectParam);
            }
        }
        /// <summary>
        /// 查询单个对象 匿名类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objectParam"></param>
        /// <returns></returns>
        public static dynamic QuerySingle(string sql, object objectParam = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.QueryFirstOrDefault(sql, objectParam);
            }
        }
        /// <summary>
        /// 组合查询，多返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqls"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<dynamic>> MultiQuery(string[] sqls, object objectParam=null)
        {
            var sql = BuildSQL(sqls);
            using (var connection = OpenConnection())
            using (var multipleReader = connection.QueryMultiple(sql, objectParam))
            {
                var results = new List<IEnumerable<dynamic>>();
                for (int i = 0; i < sqls.Length; i++)
                {
                    results.Add(multipleReader.Read());
                }
                return results;
            }
        }
       /// <summary>
       /// 执行sql语句
       /// 返回受影响的行数
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="paramsObject"></param>
       /// <returns></returns>
        public static int ExecuteSQL(string sql,object paramsObject=null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Execute(sql,paramsObject);
            }
        }
        /// <summary>
        /// 多段sql执行存储过程
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="paramsObject"></param>
        /// <returns></returns>
        public static bool ExecuteTransaction(List<string> sqls, object paramsObject=null)
        {
            using (var connection = OpenConnection())
            using (var tran = connection.BeginTransaction())
            {
                var flag = true;
                try
                {
                    foreach (var sql in sqls)
                    {
                        connection.Execute(sql,paramsObject);
                    }
                }
                catch (Exception e)
                {
                    flag = false;
                    tran.Rollback();
                }
                return flag;
            }
        }
        /// <summary>
        /// 执行一个对象的插入或者修改操作
        /// 返回主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="enity"></param>
        public static void ExecuteSingleUpdateOrInsert<T>(string sql, T enity)
        {
            using (var connection = OpenConnection())
            {
                connection.ExecuteScalar(sql, enity);
            }
        }
        /// <summary>
        /// 执行一个对象的插入或者修改操作
        /// 返回主键
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="enity"></param>
        public static void ExecuteSingleUpdateOrInsert(string sql, object enity)
        {
            using (var connection = OpenConnection())
            {
                connection.ExecuteScalar(sql, enity);
            }
        }
        /// <summary>
        /// 执行多个对象的插入或者修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="enity"></param>
        public static void ExecuteUpdateOrInsert<T>(string sql, IEnumerable<T> enity)
        {
            using (var connection = OpenConnection())
            {
                connection.Execute(sql, enity);
            }
        }
        /// <summary>
        /// 执行多个对象的插入或者修改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="enity"></param>
        public static void ExecuteUpdateOrInsert(string sql, IEnumerable<object> enity)
        {
            using (var connection = OpenConnection())
            {
                connection.Execute(sql, enity);
            }
        }
        /// <summary>
        /// 多段sql语句拼接为一句sql
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        private static string BuildSQL(string[] sqls)
        {
            if (sqls == null || sqls.Length == 0)
            {
                throw new ArgumentException();
            }
            var sql = "";
            for (var i = 0; i < sqls.Length; i++)
            {
                sql += sqls[i];
                if (i != sqls.Length - 1)
                {
                    sql += ";";
                }
            }
            return sql;
        }
    }
}

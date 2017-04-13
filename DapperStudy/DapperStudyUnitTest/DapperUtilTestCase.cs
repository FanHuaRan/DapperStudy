using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using DapperStudy.Utils;
using DapperStudy;

namespace DapperStudyUnitTest
{
    [TestClass]
    public class DapperUtilTestCase
    {
        [TestMethod]
        public void TestOpenConnection()
        {
            IDbConnection connection = DapperUtil.OpenConnection();
            Assert.AreEqual(ConnectionState.Open, connection.State);
        }
        [TestMethod]
        public void TestQueryT()
        {
            //var albums = DapperUtil.Query<Album>("select * from album where genreId=@genreId", new { genreId = 1 });
            var albums = DapperUtil.Query<Album>("select * from album", null);
            Assert.IsNotNull(albums);
        }
        [TestMethod]
        public void TestQuery()
        {
            var albums = DapperUtil.Query("select * from album where genreId=@genreId", new { genreId = 1 });
            Assert.IsNotNull(albums);
        }
        [TestMethod]
        public void TestQuerySingleT()
        {
            var album = DapperUtil.QuerySingle<Album>("select * from album where albumId=@albumId", new { albumId = 1 });
            Assert.IsNotNull(album);
        }
        [TestMethod]
        public void TestQuerySingle()
        {
            var album = DapperUtil.QuerySingle("select * from album where albumId=@albumId", new { albumId = 1 });
            // Assert.IsNotNull(album);
        }
        [TestMethod]
        public void TestMultiQuery()
        {
            var result = DapperUtil.MultiQuery(new string[] { "select * from album", "select * from genre" });
        }
    }
}

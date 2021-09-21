using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LinenAndBird.DataAccess
{
    public class OrdersRepository 
    {
        const string _connectionString = "Server = localhost; Database = LinenAndBird; Trusted_Connection = True";
        public Guid Id { get; set; }

        internal void Add(Order order)
        {
            using var db = new SqlConnection(_connectionString);
            // can use c# to add Guid
            // order.Id = Guid.NewGuid();
            var sql = @"INSERT INTO[dbo].[Orders]
                       ([BirdId]
                        ,[HatId]
                       ,[Price])
            Output inserted.Id
             VALUES
                       (@Birdid
                       ,@Hatid
		               ,@Price)";

            // anonymous type to hold parameters
            var parameters = new
            {
                BirdID = order.Bird.Id,
                HatId = order.Hat.Id,
                Price = order.Price
            };
            var id = db.ExecuteScalar<Guid>(sql, parameters);
            order.Id = id;
        }

        internal IEnumerable<Order> GetAll()
        {
            // create a connection
            var db = new SqlConnection(_connectionString);
            var sql = @"select * 
                        from Orders O
                        join birds B
                            on B.Id = O.BirdId
                        join Hats H
                            on H.Id = O.HatId";
            // Query has an order, bird and hat object, but we want to return an order class object
            var results = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            }, splitOn: "Id");

            return results;
        }
        internal Order Get(Guid id)
        {
            // create a connection
            using var db = new SqlConnection(_connectionString);
            var sql = @"select * 
                        from Orders O
                        join birds B
                            on B.Id = O.BirdId
                        join Hats H
                            on H.Id = O.HatId
                        where O.id = @id";
            // multimapping doesn't work any other kind of dapper call,
            // so we take the collection and turn it into one item ourselves;
            /*
            var order = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
            {
                order.Bird = bird;
                order.Hat = hat;
                return order;
            }, 
            new { id },
            splitOn: "Id");
            */
            var order = db.Query<Order, Bird, Hat, Order>(sql, Map, new { id }, splitOn: "Id");
            return order.FirstOrDefault();
        }
        Order Map (Order order, Bird bird, Hat hat)
        {
                order.Bird = bird;
                order.Hat = hat;
                return order;
        }
    }

}

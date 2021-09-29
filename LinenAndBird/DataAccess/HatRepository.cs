using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace LinenAndBird.DataAccess
{
    // moving to implementation of interface to allow controller testing.
    public class HatRepository : IHatRepository
    {
        // not customary to hard code a connection string
        readonly string _connectionString;
        public HatRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LinenAndBird");
        }
        static List<Hat> _hats = new List<Hat> {
            new Hat
            {
                Id = Guid.NewGuid(),
                Color = "Blue",
                Designer = "Charlie",
                Style = HatStyle.OpenBack
            },
            new Hat
            {
                Id = Guid.NewGuid(),
                Color = "Black",
                Designer = "Nathan",
                Style = HatStyle.WideBrim
            },
            new Hat
            {
                Id = Guid.NewGuid(),
                Color = "Magenta",
                Designer = "Charlie",
                Style = HatStyle.OpenBack
            }
        };

        //internal Hat GetById(Guid hatId)
        public Hat GetById(Guid hatId)
        {
            // create connection
            using var db = new SqlConnection(_connectionString);
            var hat = db.QueryFirstOrDefault<Hat>("Select * from Hats where Id = @id", new { id = hatId });
            // shortcut from above line
            //var hat = db.QueryFirstOrDefault<Hat>("Select * from Hats where Id = @id", new { id });
            /* for paramaters this is what dapper is doing
             * for each property on the parameter list
             *   add a parameter with value to the sql command
             * end for each
             * 
             *  execute the command.
             */
            return hat;
        }

        public IEnumerable<Hat> GetAll() // iternal, anybody can use from within the project
        {
            var db = new SqlConnection(_connectionString);
            var sql = "Select * from Hats";
            var results = db.Query<Hat>(sql);
            return results;

        }

        public IEnumerable<Hat> GetByStyle(HatStyle style)
        {
            return _hats.Where(hat => hat.Style == style);
        }

        public void Add(Hat newHat)
        {
            var db = new SqlConnection(_connectionString);
            var sql = @"Insert into Hats(Designer, Color, Style)
                        output inserted.*
                        Values(@Designer, @Color, @Style)";
            var id = db.ExecuteScalar<Guid>(sql, newHat);
            newHat.Id = id;
        }
    }
}

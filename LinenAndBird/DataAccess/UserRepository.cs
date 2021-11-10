using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;


namespace LinenAndBird.DataAccess
{
    public class UserRepository
    {
        readonly string _connectionString;
        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LinenAndBird");
        }

        internal IEnumerable<User> GetAll()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * from User";
            var users = db.Query<User>(sql);
            return users;
        }

        internal Guid AddUser(User newUser)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO User (DisplayName, ImageUrl, FirebaseUid, Email)
                        OUTPUT Inserted.*
                        VALUES (@DisplayName, @ImageURL, @FirebaseUID, Email)";
            var id = db.ExecuteScalar<Guid>(sql, newUser);
            newUser.Id = id;
            return id;
        }
    }
}

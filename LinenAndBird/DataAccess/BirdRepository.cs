using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LinenAndBird.DataAccess
{
    public class BirdRepository
    {
        const string _connectionString = "Server = localhost; Database = LinenAndBird; Trusted_Connection = True";
        internal IEnumerable<Bird> GetAll()
        {
            // using keyword means to close the object when leaving set of braces, and close the connection
            // i-disposable - makes ready for garbage collector
            using var db = new SqlConnection(_connectionString);
            // Query<T> is for getting results from database and putting them into C# type
            var birds = db.Query<Bird>(@"Select * From Birds"); // replaces all of previous following commands
            return birds;
        }

        internal Bird UpDate(Guid id, Bird bird)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"update Birds
                        Set Color = @color,
	                        Name = @name,
	                        Type = @type,
	                        Size = @size
                        output inserted.*
                        Where id = @id";

            bird.Id = id;
            var updatedBird = db.QuerySingleOrDefault<Bird>(sql, bird);
            return updatedBird;


            /*
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"update Birds
                                    Set Color = @color,
	                                Name = @name,
	                                Type = @type,
	                                Size = @size
                                output inserted.*
                                Where id = @id";

            cmd.Parameters.AddWithValue("Type", bird.Type);
            cmd.Parameters.AddWithValue("Color", bird.Color);
            cmd.Parameters.AddWithValue("Size", bird.Size);
            cmd.Parameters.AddWithValue("Name", bird.Name);
            cmd.Parameters.AddWithValue("Id", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var birdObj = MapFromReader(reader);
                return birdObj;
            }
            return null;
*/
        }

        internal void Remove(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql  = @"Delete from Birds
                                Where Id = @id";

            db.Execute(sql, new { id });
        }

        internal void Add(Bird newBird)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"insert into birds(Type, Color, Size, Name)
                                output inserted.Id
                                values(@Type, @Color, @Size, @Name)";

            var id = db.ExecuteScalar<Guid>(sql, newBird);
            newBird.Id = id;
        }

        // prevent SQL injection with parameterization
        internal Bird GetById(Guid birdId)
        {
            using var db = new SqlConnection(_connectionString);
            db.Open();
            var command = db.CreateCommand();

            var sql =  @"Select * 
                         From Birds
                         Where id = @id";

            // object property name must match parameter / sql variable name
            // equivalent to: command.Parameters.AddWithValue("id", birdId);

            // db.QuerySingle = should be exactly one match
            var bird = db.QuerySingleOrDefault<Bird>(sql, new { id = birdId });

            return bird;
        }
    }
}

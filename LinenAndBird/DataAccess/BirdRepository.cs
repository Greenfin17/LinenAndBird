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
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"update Birds
                                    Set Color = @color,
	                                Name = @name,
	                                Type = @type,
	                                Size = @size
                                output inserted.*
                                Where id = @id";

            cmd.Parameters.AddWithValue("Color", bird.Color);
            cmd.Parameters.AddWithValue("Name", bird.Name);
            cmd.Parameters.AddWithValue("Type", bird.Type);
            cmd.Parameters.AddWithValue("Size", bird.Size);
            cmd.Parameters.AddWithValue("Id", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var birdObj = MapFromReader(reader);
                return birdObj;
            }
            return null;

        }

        internal void Remove(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Delete from Birds
                                Where Id = @id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        internal void Add(Bird newBird)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = $@"insert into birds(Type, Color, Size, Name)
                                output inserted.Id
                                values(@Type, @Color, @Size, @Name)";
            cmd.Parameters.AddWithValue("Type", newBird.Type);
            cmd.Parameters.AddWithValue("Color", newBird.Color);
            cmd.Parameters.AddWithValue("Size", newBird.Size);
            cmd.Parameters.AddWithValue("Name", newBird.Name);

            // execute query, but don't care about results, just number of rows 
            // var numberOfRowsAffected = cmd.ExecuteNonQuery();
           
            // exute query, but only get the id of the new row.
            var newId = (Guid) cmd.ExecuteScalar();
            newBird.Id = newId;
            
            // newBird.Id = Guid.NewGuid();
            //_birds.Add(newBird);
        }

        // prevent SQL injection with parameterization
        internal Bird GetById(Guid birdId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            // subject to SQL injection -- don't use
            //command.CommandText = $@"Select * 
            //                        From Birds
            //                        Where id = {birdId}";
            command.CommandText = @"Select * 
                                    From Birds
                                    Where id = @id";

            // paramaterization to prevent SQL injection
            command.Parameters.AddWithValue("id", birdId);

            var reader = command.ExecuteReader();
            // reader can hold only one row of data at a time.
            if (reader.Read())
            {
                // Mapping data from the relational model to the object model
                var birdObj = new Bird();
                birdObj.Id = reader.GetGuid(0);
                birdObj.Size = reader["Size"].ToString();
                // explicit casting
                // birdObj.Type = (BirdType) reader["Type"];
                // check for error
                if(Enum.TryParse<BirdType>(reader["Type"].ToString(), out var birdType))
                {
                    birdObj.Type = birdType;
                }

                birdObj.Name = reader["Name"].ToString();
                return birdObj;
            }

            return null;
            // birdsObj.FirstOrDefault(bird => bird.Id == birdId);
        }

        Bird MapFromReader(SqlDataReader reader)
        {
            var bird = new Bird();
            bird.Id = reader.GetGuid(0);
            bird.Size = reader["Size"].ToString(); 
            if(Enum.TryParse<BirdType>(reader["Type"].ToString(), out var birdType))
            {
                bird.Type = birdType;
            }
            bird.Name = reader["Name"].ToString();
            return bird;
        }
    }
}

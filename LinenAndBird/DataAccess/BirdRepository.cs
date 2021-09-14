using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using Microsoft.Data.SqlClient;

namespace LinenAndBird.DataAccess
{
    public class BirdRepository
    {
        static List<Bird> _birds = new List<Bird>
        {
            new Bird
            {
                Id = Guid.NewGuid(),
                Name = "Jimmy",
                Color = "Red",
                Type = BirdType.Dead,
                Size = "medium",
                Accessories = new List<string> { "Beanie", "gold wing tips" }
            }
        };
        internal IEnumerable<Bird> GetAll()
        {
            // using keyword means to close the object when leaving set of braces, and close the connection
            // i-disposable - makes ready for garbage collector
            using var connection = new SqlConnection("Server = localhost; Database = LinenAndBird; Trusted_Connection = True");
            // connection must be opened, not open by default
            connection.Open();

            // ADO.NET SQL COMMANDS
            // Tells SQL what we want to do.
            var command = connection.CreateCommand();
            command.CommandText = @"Select * 
                                    From Birds";

            // execute reader is for when we care about gettin gall the results of our query
            var reader = command.ExecuteReader();
            // reader can hold only one row of data at a time.

            var birds = new List<Bird>();

            // advances data reader to next record
            // no record initially, must execute Reader
            while (reader.Read())
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

                // each bird goes in the list of birds
                birds.Add(birdObj);
            }

            return birds;
        }

        internal void Add(Bird newBird)
        {
            newBird.Id = Guid.NewGuid();
            _birds.Add(newBird);
        }

        // prevent SQL injection with parameterization
        internal Bird GetById(Guid birdId)
        {
            using var connection = new SqlConnection("Server = localhost; Database = LinenAndBird; Trusted_Connection = True");
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
    }
}

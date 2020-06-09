using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Roommates.Models;

namespace Roommates.Repositories
{
   public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                        {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId FROM Roommate";
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                        string firstNameValue = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastNameValue = reader.GetString(reader.GetOrdinal("LastName"));
                        int rentPortionValue = reader.GetInt32(reader.GetOrdinal("RentPortion"));
                        DateTime moveInDateValue = reader.GetDateTime(reader.GetOrdinal("MoveInDate"));

                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortionValue,
                            MovedInDate = moveInDateValue,
                            Room = null
                        };

                        roommates.Add(roommate);
                    }

                    reader.Close();

                    return roommates;
                }
            }
        }
        
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, RentPortion, MoveInDate FROM Roommate WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName")),
                            Lastname = reader.GetString(reader.GetOrdinal("LastName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate"))
                        };
                    }
                    reader.Close();
                    return roommate;

                }
            }
        }

        public List<Roommate> GetAllWithRoom()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT rm.Id, rm.FirstName, rm.LastName, rm.RentPortion, rm.MoveInDate, r.Id as RoomId, r.Name as RoomName, r.MaxOccupancy as RoomOccupancy
                                        FROM Roommate rm
                                        JOIN Room r on rm.RoomId = r.Id;";

                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommatesWithRoom = new List<Roommate>();

                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("RoomId")),
                            Name = reader.GetString(reader.GetOrdinal("RoomName")),
                            MaxOccupancy = reader.GetInt32(reader.GetOrdinal("RoomOccupancy"))
                        };

                        Roommate roommate = new Roommate
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName")),
                            Lastname = reader.GetString(reader.GetOrdinal("LastName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = room
                        };

                        roommatesWithRoom.Add(roommate);
                    }
                    reader.Close();

                    return roommatesWithRoom;
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }

            /*Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

            Room bathroom = new Room
            {
                Name = "Bathroom",
                MaxOccupancy = 1
            };

            roomRepo.Insert(bathroom);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Room with id {bathroom.Id}");

            bathroom.MaxOccupancy = 3;
            roomRepo.Update(bathroom);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Updated room to have a new occupancy of {bathroom.MaxOccupancy}");

            roomRepo.Delete(9);
            roomRepo.Delete(8);

            allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }*/

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Getting All Roommates:");
            Console.WriteLine();

            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.RentPortion} {roommate.MovedInDate}");
            }

            Roommate roommateById = roommateRepo.GetById(1);
            Console.WriteLine();
            Console.WriteLine("Getting a roommate by Id");
            Console.WriteLine($"{roommateById.Id} {roommateById.Firstname} {roommateById.Lastname} {roommateById.RentPortion} {roommateById.MovedInDate}");

            List<Roommate> allRoommatesWithRooms = roommateRepo.GetAllWithRoom();

            Console.WriteLine();
            Console.WriteLine("Getting all roommates with rooms");

            foreach (Roommate roommateWithRoom in allRoommatesWithRooms)
            {
                
                Console.WriteLine($@"{roommateWithRoom.Id} {roommateWithRoom.Firstname} {roommateWithRoom.Lastname}
                                    {roommateWithRoom.RentPortion} {roommateWithRoom.MovedInDate} // 
                                    {roommateWithRoom.Room.Id} {roommateWithRoom.Room.Name} {roommateWithRoom.Room.MaxOccupancy}");
            }
        }
    }
}

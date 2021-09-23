using System.Collections.Generic;
using Models;
using System.IO;
using System.Text.Json;
using System;
using System.Linq;

namespace DL
{
    public class UserRepo : IUserRepo
    {
        //this is a relative path from where the program is running
        //aka UI folder
        private const string filePath = "../DL/Users.json";
        
        //this will hold my serialized objects
        private string jsonString;
        public User AddUser(User user)
        {
            //first, grab all store from the file as List<store>
            List<User> allUsers = GetAllUsers();
            //right now, this is in type of List<store>
            allUsers.Add(user);

            //serialize 
            jsonString = JsonSerializer.Serialize(allUsers);
            //write to a file
            File.WriteAllText(filePath, jsonString);

            return user;
        }

        public List<User> GetAllUsers()
        {
            //Read the file from the file path
            jsonString = File.ReadAllText(filePath);

            //translate the serialized string into List<store> object!
            return JsonSerializer.Deserialize<List<User>>(jsonString);
        }

        public User UpdateUser(User userToUpdate)
        {
            //first, find the restaurant to update
            //by first, getting all restaurants, using GetAllUsers method
            //and then use FindIndex method with the lambda expression
            //to get me the location of the restaurant in the list
            //if there is a match
            List<User> allUsers = GetAllUsers();
            int UserIndex = allUsers.FindIndex(r => r.Equals(userToUpdate));

            //update the restaurant in the list itself
            allUsers[UserIndex] = userToUpdate;

            //serialize 
            jsonString = JsonSerializer.Serialize(allUsers);

            //write to a file
            File.WriteAllText(filePath, jsonString);
            
            return userToUpdate;
        }
    }
}
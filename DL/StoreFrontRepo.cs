using System.Collections.Generic;
using Models;
using System.IO;
using System.Text.Json;
using System;
using System.Linq;

namespace DL
{
    public class StoreFrontRepo : IRepo
    {
        //this is a relative path from where the program is running
        //aka UI folder
        private const string filePath = "../DL/StoreFronts.json";
        
        //this will hold my serialized objects
        private string jsonString;
        public StoreFront AddStoreFront(StoreFront store)
        {
            //first, grab all store from the file as List<store>
            List<StoreFront> allStoreFronts = GetAllStoreFronts();
            //right now, this is in type of List<store>
            allStoreFronts.Add(store);

            //serialize 
            jsonString = JsonSerializer.Serialize(allStoreFronts);
            //write to a file
            File.WriteAllText(filePath, jsonString);

            return store;
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            //Read the file from the file path
            jsonString = File.ReadAllText(filePath);

            //translate the serialized string into List<store> object!
            return JsonSerializer.Deserialize<List<StoreFront>>(jsonString);
        }

        public StoreFront UpdateStoreFront(StoreFront storeToUpdate)
        {
            //first, find the restaurant to update
            //by first, getting all restaurants, using getallStoreFronts method
            //and then use FindIndex method with the lambda expression
            //to get me the location of the restaurant in the list
            //if there is a match
            List<StoreFront> allStoreFronts = GetAllStoreFronts();
            int storeFrontIndex = allStoreFronts.FindIndex(r => r.Equals(storeToUpdate));

            //update the restaurant in the list itself
            allStoreFronts[storeFrontIndex] = storeToUpdate;

            //serialize 
            jsonString = JsonSerializer.Serialize(allStoreFronts);

            //write to a file
            File.WriteAllText(filePath, jsonString);
            
            return storeToUpdate;
        }
    }
}
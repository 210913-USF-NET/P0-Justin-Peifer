using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DL
{
    public class DBRepo : IRepo
    {
        //dbcontext
        private Entity.PeiferP0Context _context;

        public DBRepo(Entity.PeiferP0Context context){
            _context = context;
        }

        // public Model.User AddUser(Model.User user){

        // }

        // public Model.User findUserById(int Id){
        //     List<Model.User> allUsers = GetAllUsers();
        //     foreach (Model.User user in allUsers)
        //     {
        //         if (user.Id == Id){
        //             return user.Id;
        //             break;
        //         }
        //         else{
        //             throw InputInvalidException("User not found.");
        //             goto loginstart;
                    
        //         }
                
        //     }

        // }

        // public int UserEmailSearch(string loginEmail){
        //     List<Model.User> allUsers = GetAllUsers();
        //     foreach (User user in allUsers)
        //     {
        //         if (user.Email == loginEmail){
        //             return user.Id;
        //             break;
        //         }
        //         else{
        //             System.Console.WriteLine("User not found.");
        //                 return -1;
        //         }
                
        //     }
        // }
        
        public List<Model.User> GetAllUsers(){

            return _context.Users.Select(
                User => new Model.User() {
                    Id = User.Id,
                    Email = User.Email,
                    Password = User.Password,
                    Name = User.Name,
                    Age = User.Age,
                    Access = User.ManagerAccess
                    }
            ).ToList();

        }

        // public User UpdateUser(User userToUpdate){

        // }

        public List<Model.StoreFront> GetAllStoreFronts(){

            //same as select * from StoreFront in sql query
            return _context.StoreFronts.Select(
                Storefront => new Model.StoreFront() {
                    Id = Storefront.Id,
                    State = Storefront.State,
                    Zipcode = Storefront.ZipCode
                    }
            ).ToList();
        }

        public Model.StoreFront StoreById(int id)
        {
            Entity.StoreFront storeById = 
                _context.StoreFronts
                .Include(i => i.Inventories)
                .FirstOrDefault(i => i.Id == id);

            return new Model.StoreFront() {
                    Id = storeById.Id,
                    Zipcode = storeById.ZipCode,
                    State = storeById.State,
                Inventory = storeById.Inventories.Select(i => new Model.Inventory(){
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    
                }).ToList()
            };
        }

        //products
        public List<Model.Product> GetAllProducts(){

                    return _context.Products.Select(
                        Product => new Model.Product() {
                            Id = Product.Id,
                            Name = Product.Name,
                            Description = Product.Description,
                            Price = Product.Price,
                            Alcohol = Product.Alcohol
                            }
                    ).ToList();
                }
                
        public Model.Product ProductByID(int id)
        {
            Entity.Product productByID = 
                _context.Products
                .FirstOrDefault(i => i.Id == id);

            return new Model.Product() {
                    Id = productByID.Id,
                    Name = productByID.Name,
                    Description = productByID.Description,
                    Price = productByID.Price,
                    Alcohol = productByID.Alcohol,
                Inventory = productByID.Inventories.Select(i => new Model.Inventory(){
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    
                }).ToList()
            };
        }

        //inventory

        public List<Model.Inventory> GetAllInventory(){
            //same as select * from Inventory in sql query
            return _context.Inventories.Select(
                Inventory => new Model.Inventory() {
                    Id = Inventory.Id,
                    StoreId = Inventory.StoreId,
                    ProductId = Inventory.ProductId,
                    Quantity = Inventory.Quantity
                    }
            ).ToList();
        }
        
    }
}
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
        private Entity.PeiferP0Context _context;

        public DBRepo(Entity.PeiferP0Context context){
            _context = context;
        }

        
        
        public Model.Order NewOrder(int UserId)
        {
            Entity.Order orderAdded = new Entity.Order(){
                UserId = UserId,
                DateOrdered = DateTime.Now,
            };
            
            orderAdded = _context.Add(orderAdded).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Model.Order()
            {
                Id = orderAdded.Id,
                DateOrdered = orderAdded.DateOrdered,
                UserId = orderAdded.UserId
            };
        }


        public Model.User AddUser(Model.User user)
        {
            Entity.User userToAdd = new Entity.User(){
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ManagerAccess = user.Access,
                Age = user.Age
            };
            
            userToAdd = _context.Add(userToAdd).Entity;

            //the "changes" don't get executed until you call the SaveChanges method
            _context.SaveChanges();

            //this clears the changetracker so it returns to a clean slate
            _context.ChangeTracker.Clear();

            return new Model.User()
            {
                Id = userToAdd.Id,
                Name = userToAdd.Name,
                Password = userToAdd.Password,
                Age = userToAdd.Age,
                Access = userToAdd.ManagerAccess,
                Email = userToAdd.Email
            };
        }



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

        

        public Model.Order OrderInfoById(int id){
            Entity.Order orderById = 
                _context.Orders
                .Include(l => l.LineItems)
                .FirstOrDefault(l => l.Id == id);

            return new Model.Order() {
                    Id = orderById.Id,
                    DateOrdered = orderById.DateOrdered,
                    UserId = orderById.UserId,
                LineItems = orderById.LineItems.Select(l => new Model.LineItem(){
                    OrderId = l.OrderId,
                    StoreId = l.StoreId,
                    Quantity = l.Quantity
                    
                }).ToList()
            };
        }
        public List<Model.Order> OrderByUserId(int UserId){
            List<Model.Order> allOrders =  GetAllOrders();
            List<Model.Order> userOrders = GetAllOrders();
            foreach (Model.Order order in allOrders){
                if (UserId !=order.UserId){
                    userOrders.Remove(order);
                }
            }
            return userOrders;
        }

        public List<Model.Order> GetAllOrders(){

            //same as select * from StoreFront in sql query
            return _context.Orders.Select(
                Order => new Model.Order() {
                    Id = Order.Id,
                    DateOrdered = Order.DateOrdered,
                    UserId = Order.UserId
                    }
            ).ToList();
        }
        

        public Model.Order PlaceOrder(Model.StoreFront storeOrderedFrom, Model.Order order){

            foreach (Model.LineItem item in order.LineItems){
                Entity.LineItem addedItem = new Entity.LineItem() {
                    OrderId = order.Id,
                    StoreId = storeOrderedFrom.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                addedItem = _context.Add(addedItem).Entity;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }
            return order;
        }

        public int UpdateStock(Model.StoreFront storeToUpdate, Model.LineItem orderedProduct){
            bool found = false;
            int x = 0;
            while (found){
                if (storeToUpdate.Inventory[x].ProductId == orderedProduct.ProductId && storeToUpdate.Inventory[x].StoreId ==storeToUpdate.Id){
                    found = true;
                }
                else {
                    x= x+1;
                }

            }
            Entity.Inventory inventoryToEdit = new Entity.Inventory() {
                Id = storeToUpdate.Inventory[x].Id,
                StoreId = storeToUpdate.Id,
                ProductId = orderedProduct.ProductId,
                Quantity = storeToUpdate.Inventory[x].Quantity- orderedProduct.Quantity
                                
            };

            inventoryToEdit = _context.Inventories.Update(inventoryToEdit).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return Convert.ToInt32(inventoryToEdit.Quantity);
        }

            public int UpdateStock(Model.Inventory inventoryToUpdate, int amountToAdd){
            Entity.Inventory inventoryToEdit = new Entity.Inventory() {
                Id = inventoryToUpdate.Id,
                StoreId = inventoryToUpdate.StoreId,
                ProductId = inventoryToUpdate.ProductId,
                Quantity = inventoryToUpdate.Quantity+amountToAdd
                                
            };

            

            inventoryToEdit = _context.Inventories.Update(inventoryToEdit).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return Convert.ToInt32(inventoryToEdit.Quantity);

        }

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
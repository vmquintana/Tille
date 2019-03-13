using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Controls;

namespace TilleWPF.Model
{
    public class WarehouseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Existence { get; set; }
        public double AmountBoughtCurrentMonth { get; set; }
        public double Cost { get; set; }
    }
    public class BookItem
    {
        public int Id { get; set; }
        public int ClientsAmount { get; set; }
        public string Country { get; set; }
        public string DateIn { get; set; }
        public string DateOut { get; set; }
        public double EstimatedPrice { get; set; }
        public double Price { get; set; }
    }
    public class ServiceItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Button button { get; set; }
    }
    public class MovementItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public double Cost { get; set; }
    }
    public class StatisticsItem
    {
        public string Action { get; set; }
        public double Total { get; set; }
    }

    public class MainController
    {
        public PuntillazoContext db;
        public MainController()
        {
            db = new PuntillazoContext();
           // InitialData();
           // UpdateProduct();
        }

        public void UpdateProduct()
        {
            //TODO
            //Make the comparison with current month

            //Finding the cost  and amount of every product in a given month
            var collectionProdIdPriceAmount = from prodMov in db.ProductMovs
                                       where prodMov.Date.Month == DateTime.Now.Month
                                       group new { prodMov.Movement.Price, prodMov.Amount } by prodMov.ProductId;

            Dictionary<int, double> pairsProdIdCost = new Dictionary<int, double>();
            Dictionary<int, int> pairsProdIdAmount = new Dictionary<int, int>();

            double price = 0;
            int amount = 0;
            foreach (var item in collectionProdIdPriceAmount)
            {
                price = item.Sum(lol => lol.Price);
                amount = item.Sum(lol => lol.Amount);

                pairsProdIdAmount.Add(item.Key,amount);
                pairsProdIdCost.Add(item.Key, price);

                //restart variables
                price = 0;
                amount = 0;
            }

            ICollection<Product> collectionProducts = db.Products.ToList();

            foreach (var item in collectionProducts)
            {
                if (pairsProdIdCost.Keys.Contains(item.Id))
                {
                    item.AmountBoughtCurrentMonth = pairsProdIdAmount[item.Id];
                    item.CurrentMonthCost = pairsProdIdCost[item.Id];
                }
                else
                {
                    item.AmountBoughtCurrentMonth = 0;
                    item.CurrentMonthCost = 0;
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            
        }

        //Fill data 
        public List<WarehouseItem> FillDataWarehouse()
        {
            List<WarehouseItem> warehouseItems= new List<WarehouseItem>();

            //Adding item to the warehouseItem list
            foreach (var item in db.Products)
            {
                warehouseItems.Add(new WarehouseItem()
                {
                    Id = item.Id,
                    Existence = item.Amount,
                    Name = item.Name,
                    Cost = item.CurrentMonthCost,
                    AmountBoughtCurrentMonth = item.AmountBoughtCurrentMonth
                });
            }
            return warehouseItems;
        }
        public List<MovementItem> FillDataExpense()
        {
            List<MovementItem> expensesItems = new List<MovementItem>();

            var collectionExpenses = db.Movements.Where( m => m.Price <= 0 
                                                        && m.Date.Month == DateTime.Now.Month 
                                                        && m.Date.Year == DateTime.Now.Year);

            foreach (var item in collectionExpenses)
            {
                expensesItems.Add(new MovementItem() {
                    Id = item.Id,
                    Date = item.Date.ToShortDateString(),
                    Cost = item.Price,
                    Type = ""+(MovementType)item.Type,
                    Description = item.Description
                });
            }

            return expensesItems;
        }
        public List<StatisticsItem> FillStats()
        {
            List<StatisticsItem> statisticsItems = new List<StatisticsItem>();

            IQueryable<Movement> movements = db.Movements.Where(m => m.Date.Month == DateTime.Now.Month
                                                                && m.Date.Year == DateTime.Now.Year);

            double profit = 0, expenses = 0;

            foreach (var item in movements)
            {
                if(item.Price <= 0)
                {
                    expenses += item.Price;
                }
                else
                {
                    profit += item.Price;
                }
            }

            statisticsItems.Add(new StatisticsItem()
            {
                Action = "Gastos",
                Total = expenses
            });
            statisticsItems.Add(new StatisticsItem()
            {
                Action = "Ganancias",
                Total = profit
            });

            return statisticsItems;
        }
        public List<MovementItem> FillDataProfit()
        {
            List<MovementItem> expensesItems = new List<MovementItem>();

            var collectionExpenses = db.Movements.Where(m => m.Price > 0 
                                                        && m.Date.Month == DateTime.Now.Month
                                                        && m.Date.Year == DateTime.Now.Year);

            foreach (var item in collectionExpenses)
            {
                expensesItems.Add(new MovementItem()
                {
                    Id = item.Id,
                    Date = item.Date.ToShortDateString(),
                    Cost = item.Price,
                    Type = "" + (MovementType)item.Type,
                    Description = item.Description
                });
            }

            return expensesItems;
        }
        public List<BookItem> FillBook()
        {
            List<BookItem> bookItems = new List<BookItem>();

            IQueryable<Book> collectionBooks = db.Books.Where(b=> b.Movement.Date.Month == DateTime.Now.Month
                                                              && b.Movement.Date.Year == DateTime.Now.Year);

            foreach (var item in collectionBooks)
            {
                bookItems.Add(new BookItem() {
                    Id = item.Id,
                    ClientsAmount = item.NumberOfClients,
                    Country = item.Country,
                    DateIn = item.DateIn.ToShortDateString(),
                    DateOut = item.DateOut.ToShortDateString(),
                    EstimatedPrice = item.EstimatedPrice,
                    Price = item.Movement.Price
                });
            }

            return bookItems;
        }
        public List<ServiceItem> FillServices()
        {
            List<ServiceItem> serviceItems = new List<ServiceItem>();

            IQueryable<Service> collectionServices = db.Services.Where(s => s.Movement.Date.Month == DateTime.Now.Month
                                                                         && s.Movement.Date.Year == DateTime.Now.Year);

            foreach (var item in collectionServices)
            {
                serviceItems.Add( new ServiceItem() {
                    Id = item.Id,
                    Date = item.Movement.Date.ToShortDateString(),
                    Description = item.Description,
                    Price = item.Movement.Price,
                    Type = ""+ (ServiceType)item.Type
                });
            }

            return serviceItems;
        }

        //Search
        public List<MovementItem> SearchDataExpense(int month, int year)
        {
            IQueryable<Movement> movementCollection;
            if (month != -1 && year == -1 )
            {
                movementCollection = db.Movements.Where(m => m.Date.Month == month);
            }
            else if (year != -1 && month == -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year);
            }
            else /*if(year != -1 && month != -1)*/
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year && m.Date.Month == month);
            }

            List<MovementItem> expensesItems = new List<MovementItem>();

            var collectionExpenses = movementCollection.Where(m => m.Price <= 0);

            foreach (var item in collectionExpenses)
            {
                expensesItems.Add(new MovementItem()
                {
                    Id = item.Id,
                    Date = item.Date.ToShortDateString(),
                    Cost = item.Price,
                    Type = "" + (MovementType)item.Type,
                    Description = item.Description
                });
            }

            return expensesItems;
        }
        public List<StatisticsItem> SearchStats(int month, int year)
        {
            IQueryable<Movement> movementCollection;
            if (month != -1 && year == -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Month == month);
            }
            else if (year != -1 && month == -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year);
            }
            else //(year != -1 && month != -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year && m.Date.Month == month);
            }


            List<StatisticsItem> statisticsItems = new List<StatisticsItem>();


            double profit = 0, expenses = 0;

            foreach (var item in movementCollection)
            {
                if (item.Price <= 0)
                {
                    expenses += item.Price;
                }
                else
                {
                    profit += item.Price;
                }
            }

            statisticsItems.Add(new StatisticsItem()
            {
                Action = "Gastos",
                Total = expenses
            });
            statisticsItems.Add(new StatisticsItem()
            {
                Action = "Ganancias",
                Total = profit
            });

            return statisticsItems;
        }
        public List<MovementItem> SearchDataProfit(int month, int year)
        {
            IQueryable<Movement> movementCollection;
            if (month != -1 && year == -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Month == month);
            }
            else if (year != -1 && month == -1)
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year);
            }
            else /*if (year != -1 && month != -1)*/
            {
                movementCollection = db.Movements.Where(m => m.Date.Year == year && m.Date.Month == month);
            }

            List<MovementItem> expensesItems = new List<MovementItem>();

            var collectionExpenses = movementCollection.Where(m => m.Price > 0);

            foreach (var item in collectionExpenses)
            {
                expensesItems.Add(new MovementItem()
                {
                    Id = item.Id,
                    Date = item.Date.ToShortDateString(),
                    Cost = item.Price,
                    Type = "" + (MovementType)item.Type,
                    Description = item.Description
                });
            }

            return expensesItems;
        }
        public List<BookItem> SearchBook(int month, int year)
        {
            IQueryable<Book> bookCollection;
            if (month != -1 && year == -1)
            {
                bookCollection = db.Books.Where(m => m.Movement.Date.Month == month);
            }
            else if (year != -1 && month == -1)
            {
                bookCollection = db.Books.Where(m => m.Movement.Date.Year == year);
            }
            else /*if (year != -1 && month != -1)*/
            {
                bookCollection = db.Books.Where(m => m.Movement.Date.Year == year && m.Movement.Date.Month == month);
            }

            List<BookItem> bookItems = new List<BookItem>();

            foreach (var item in bookCollection)
            {
                bookItems.Add(new BookItem()
                {
                    Id = item.Id,
                    ClientsAmount = item.NumberOfClients,
                    Country = item.Country,
                    DateIn = item.DateIn.ToShortDateString(),
                    DateOut = item.DateOut.ToShortDateString(),
                    EstimatedPrice = item.EstimatedPrice,
                    Price = item.Movement.Price
                });
            }

            return bookItems;
        }
        public List<ServiceItem> SearchServices(int month, int year)
        {
            IQueryable<Service> bookCollection;
            if (month != -1 && year == -1)
            {
                bookCollection = db.Services.Where(s => s.Movement.Date.Month == month);
            }
            else if (year != -1 && month == -1)
            {
                bookCollection = db.Services.Where(s => s.Movement.Date.Year == year);
            }
            else /*if (year != -1 && month != -1)*/
            {
                bookCollection = db.Services.Where(s => s.Movement.Date.Year == year && s.Movement.Date.Month == month);
            }   

            List<ServiceItem> serviceItems = new List<ServiceItem>();
           
            foreach (var item in bookCollection)
            {
                serviceItems.Add(new ServiceItem()
                {
                    Id = item.Id,
                    Date = item.Movement.Date.ToShortDateString(),
                    Description = item.Description,
                    Price = item.Movement.Price,
                    Type = "" + (ServiceType)item.Type
                });
            }

            return serviceItems;
        }
        //Availables products
        public List<WarehouseItem> AvailablesProduts()
        {
            List<WarehouseItem> warehouseItems = new List<WarehouseItem>();

            var collection = db.Products.Where(p => p.Amount > 0);
            //Adding item to the warehouseItem list
            foreach (var item in collection
)
            {
                warehouseItems.Add(new WarehouseItem()
                {
                    Id = item.Id,
                    Existence = item.Amount,
                    Name = item.Name,
                    Cost = item.CurrentMonthCost,
                    AmountBoughtCurrentMonth = item.AmountBoughtCurrentMonth
                });
            }
            return warehouseItems;
        }
        //all products
        public List<Product> Produts()
        {
            return db.Products.ToList();
        }

        //Amount available of a given product
        public int AmountAvailble(int Id)
        {
            return db.Products.Find(Id).Amount;
        }

        //Years in db
        public IQueryable<IGrouping<int, int>> YearsAvailable()
        {
            return from mov in db.Movements
                   group mov.Date.Year by mov.Date.Year;
        }
        //Month by years
        public IQueryable<IGrouping<int, int>> MonthByYears(int givenYear)
        {
            return from mov in db.Movements
                   where mov.Date.Year == givenYear
                   group mov.Date.Year by mov.Date.Month;
        }
        //Month availables
        public IQueryable<IGrouping<int, int>> MonthAvailables()
        {
            return from mov in db.Movements
                   group mov.Date.Year by mov.Date.Month;
        }

        //Fill products
        public List<Product> FillProductList()
        {
            return db.Products.ToList();
        }

        //Introduce data
        public void AddMovement(DateTime date, string description, double price, MovementType type)
        {
            try
            {
                db.Movements.Add(new Movement()
                {
                    Date = date,
                    Description = description,
                    Price = price,
                    Type = (int)type
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
           
        }
        public void AddProduct(string name, int amount,DateTime date, string description, double price, MovementType type)
        {
            try
            {
                Product prod = new Product()
                {
                    Name= name,
                    Amount=amount,
                    CurrentMonthCost=price,
                    AmountBoughtCurrentMonth=amount
                };

                Movement mov = new Movement()
                {
                    Date = date,
                    Description = description,
                    Price = price,
                    Type = (int)MovementType.Warehouse
                };

                db.Movements.Add(mov);
                db.Products.Add(prod);
                db.SaveChanges();

                ProductMov productMov = new ProductMov()
                {
                    Amount= amount,
                    Date = date,
                    ProductId = prod.Id,
                    MovementId = mov.Id
                };

                db.ProductMovs.Add(productMov);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        
        }
        public void UpdateProduct(int idProduct, int amount, DateTime date, string description, double price, MovementType type)
        {
            try
            {
                Movement mov = new Movement()
                {
                    Date = date,
                    Description = description,
                    Price = price,
                    Type = (int)MovementType.Warehouse
                };
                db.Movements.Add(mov);
                db.SaveChanges();

                ProductMov productMov = new ProductMov()
                {
                    Amount = amount,
                    Date = date,
                    ProductId = idProduct,
                    MovementId = mov.Id
                };

                Product product = db.Products.Find(idProduct);
                product.Amount += amount;
                if (amount >= 0)
                {
                    product.AmountBoughtCurrentMonth += amount;
                    product.CurrentMonthCost += price;
                }
                

                db.ProductMovs.Add(productMov);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
}
        public void AddService(DateTime date, string description, double price, ServiceType type)
        {
            try
            {
                Movement movement = new Movement()
                {
                    Date = date,
                    Description = $"{type}(${price})",
                    Price = price,
                    Type = (int)MovementType.Service
                };
                db.Movements.Add(movement);
                db.SaveChanges();

                db.Services.Add( new Service()
                {
                    Description = description,
                    Type = (int)type,
                    MovementId = movement.Id
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        public void AddBookIn(int numberOfClient, DateTime dateIn, DateTime dateOut, string country, double estimatedPrice, double realPrice)
        {
            try
            {
                Movement movement = new Movement()
                {
                    Date = dateIn,
                    Description = $"Book:{numberOfClient} clientes de {country}",
                    Price = realPrice,
                    Type = (int)MovementType.Book
                };
                db.Movements.Add(movement);
                db.SaveChanges();

                db.Books.Add(new Book() {
                    Country = country,
                    DateIn = dateIn,
                    DateOut = dateOut,
                    EstimatedPrice = estimatedPrice,
                    NumberOfClients = numberOfClient,
                    MovementId = movement.Id
                });
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void InitialData()
        {
            Movement beer = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 12
            };
            Movement beer2 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 12
            };
            Movement beer3 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 34
            };
            Movement beer4 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 27
            };

            Movement water = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 12
            };
            Movement water2 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 12
            };
            Movement water3 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 34
            };
            Movement water4 = new Movement()
            {
                Type = (int)MovementType.Warehouse,
                Date = DateTime.Now,
                Price = 73
            };

            Product beerProd = new Product()
            {
                Name = "Beer",
                Amount = 132,
                AmountBoughtCurrentMonth = 0,
                CurrentMonthCost = 0
            };
            Product waterProd = new Product()
            {
                Name = "Water",
                Amount = 84,
                AmountBoughtCurrentMonth = 0,
                CurrentMonthCost = 0
            };

            db.Movements.Add(beer);
            db.Movements.Add(beer2);
            db.Movements.Add(beer3);
            db.Movements.Add(beer4);

            db.Movements.Add(water);
            db.Movements.Add(water2);
            db.Movements.Add(water3);
            db.Movements.Add(water4);

            db.Products.Add(beerProd);
            db.Products.Add(waterProd);

            db.SaveChanges();

            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 12,
                Date = DateTime.Now,
                MovementId = beer.Id,
                ProductId = beerProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 24,
                Date = DateTime.Now,
                MovementId = beer2.Id,
                ProductId = beerProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 36,
                Date = DateTime.Now,
                MovementId = beer3.Id,
                ProductId = beerProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 83,
                Date = DateTime.Now,
                MovementId = beer4.Id,
                ProductId = beerProd.Id,
            });

            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 12,
                Date = DateTime.Now,
                MovementId = water.Id,
                ProductId = waterProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 24,
                Date = DateTime.Now,
                MovementId = water2.Id,
                ProductId = waterProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 32,
                Date = DateTime.Now,
                MovementId = water3.Id,
                ProductId = waterProd.Id,
            });
            db.ProductMovs.Add(new ProductMov()
            {
                Amount = 87,
                Date = DateTime.Now,
                MovementId = water4.Id,
                ProductId = waterProd.Id,
            });

            db.SaveChanges();
        }

    }
}

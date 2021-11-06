using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Domain
{
    public class CarShopContext:DbContext
    {
        public DbSet<Manufacturer>Manufactures { get; set; }
        public DbSet<Car>Cars { get; set; }
        public CarShopContext(DbContextOptions<CarShopContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Manufacturer> manufactures = new List<Manufacturer>()
            {
                new Manufacturer(){ Id=1, Name="Chevrolet", Location="KOREA REPUBLIC OF" },
                new Manufacturer(){ Id=2, Name="Hyundai", Location="KOREA REPUBLIC OF" }
            };
            modelBuilder.Entity<Manufacturer>().HasData(manufactures);
            List<Car> cars = new List<Car>()
            {
                new Car(){Id=1, ManufacturerId=1, Model="Matiz Creative Jazz", Year=2010, Price=1600d, Description="Fuel Type - Gasoline, Transmission - Automatic, Seats - 5"},
                new Car(){Id=2, ManufacturerId=1, Model="Lacetti Premier 4Door 1.6", Year=2009, Price=1900d, Description="Fuel Type - Gasoline, Transmission - Automatic, Seats - 5"},
                new Car(){Id=3, ManufacturerId=1, Model="Spark gasoline", Year=2012, Price=2450d, Description="Fuel Type - Gasoline, Transmission - Manual, Seats - 5"},
                new Car(){Id=4, ManufacturerId=2, Model="Santa Fe (CM) 2.0 e-VGT 2WD", Year=2011, Price=4590d, Description="Fuel Type - Diesel, Transmission - Automatic, Seats - 7"},
                new Car(){Id=5, ManufacturerId=2, Model="Tucson ix Diesel R20 4WD", Year=2012, Price=5400d, Description="Fuel Type - Diesel, Transmission - Automatic, Seats - 5"},
                new Car(){Id=6, ManufacturerId=2, Model="Tucson 2.0 VGT 2WD", Year=2007, Price=1840d, Description="Fuel Type - Diesel, Transmission - Automatic, Seats - 5"}
            };
            modelBuilder.Entity<Car>().HasData(cars);
        }
    }
}

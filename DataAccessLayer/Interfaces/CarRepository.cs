using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Car> Cars => _context.Cars;
        public IQueryable<Seller> Sellers => _context.Sellers;

        public async Task<IEnumerable<Car>> GetCarAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task GetDetailsAsync(Car car)
        {
            var user = await _context.Cars.FindAsync(car.Id);
        }

        public async Task UpdateCarAsync(Car car)
        {
            var existingCar = await _context.Cars.FindAsync(car.Id);
            if (existingCar != null)
            {
                existingCar.Id = car.Id;
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.Trim = car.Trim;
                existingCar.Mileage = car.Mileage;
                existingCar.Price = car.Price;
                existingCar.Seller = car.Seller;
                await _context.SaveChangesAsync();
            }
            else
            {
                await Console.Out.WriteLineAsync("Not found");
            }
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.Include(c=> c.Seller).FirstOrDefaultAsync(c => c.Id == id);
            if(car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Seller>> GetSellersAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task AddSellerAsync(Seller seller)
        {
            await _context.Sellers.AddAsync(seller);
            await _context.SaveChangesAsync();
        }
    }
}

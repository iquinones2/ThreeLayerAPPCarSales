using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetCarAsync()
        {
            return await _carRepository.GetCarAsync();
        }

        public async Task AddCarAsync(Car car)
        {
            var existingCar = _carRepository.Cars
                .FirstOrDefault(c => c.Make == car.Make
                                  && c.Model == car.Model
                                  && c.Year == car.Year
                                  && c.Trim == car.Trim);

            if (existingCar != null)
            {
                throw new InvalidOperationException
                    ("A car with the same Make, Model, Year and Trim already exists.");
            }

            await _carRepository.AddCarAsync(car);
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _carRepository.GetCarByIdAsync(id);
        }

        public async Task GetDetailsAsync(Car car)
        {
            await _carRepository.GetDetailsAsync(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            // Business Rule: Price cannot be negative
            if (car.Price <= 0)
            {
                throw new InvalidOperationException
                    ("Car price must be greater than zero.");
            }

            // Business Rule: Mileage cannot be negative
            if (car.Mileage < 0)
            {
                throw new InvalidOperationException
                    ("Car mileage cannot be negative.");
            }

            // Business Rule: Year must be realistic
            if (car.Year <= 1886)
            {
                throw new InvalidOperationException
                    ("Car year is not valid.");
            }
            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);

            // Business Rule: Cannot delete a car with 0 mileage (brand new car)
            if (car.Mileage <= 5000)
            {
                throw new InvalidOperationException
                    ("Cannot delete a brand new car with 0 mileage.");
            }

            // Business Rule: Cannot delete a car priced over $100,000
            if (car.Price > 100000)
            {
                throw new InvalidOperationException
                    ("Cannot delete a car priced over $100,000. Contact admin.");
            }

            await _carRepository.DeleteCarAsync(id);
        }

        //LINQ join query
        public List<CarSellerViewModel> GetCarsWithSellers()
        {
            var result = (from car in _carRepository.Cars
                          join seller in _carRepository.Sellers
                          on car.SellerId equals seller.SellerId
                          select new CarSellerViewModel
                          {
                              FirstName = seller.FirstName,
                              LastName = seller.LastName,
                              Make = car.Make,
                              Model = car.Model,
                          }).ToList();

            return result;
        }

        public async Task<IEnumerable<Seller>> GetSellersAsync()
        {
            return await _carRepository.GetSellersAsync();
        }

        public async Task AddSellerAsync(Seller seller)
        {
            await _carRepository.AddSellerAsync(seller);
        }
    }
}

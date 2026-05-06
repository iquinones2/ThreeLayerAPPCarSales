using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCarAsync();
        Task AddCarAsync(Car car);
        Task<Car> GetCarByIdAsync(int id);
        Task GetDetailsAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);

        IQueryable<Car> Cars { get; }
        IQueryable<Seller> Sellers { get; }
        Task<IEnumerable<Seller>> GetSellersAsync();
        Task AddSellerAsync(Seller seller);
    }
}

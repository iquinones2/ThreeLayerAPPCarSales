using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCarAsync();
        Task AddCarAsync(Car car);
        Task<Car> GetCarByIdAsync(int id);
        Task GetDetailsAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}

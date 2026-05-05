using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ThreeLayerAPPCarSales.Models;

namespace ThreeLayerAPPCarSales.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var car = await carService.GetCarAsync();
            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car carmodel)
        {
            var car = new Car()
            {
                Id = carmodel.Id,
                Make = carmodel.Make,
                Model = carmodel.Model,
                Year = carmodel.Year,
                Trim = carmodel.Trim,
                Mileage = carmodel.Mileage,
                Price = carmodel.Price,
            };
            await carService.AddCarAsync(car);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Details(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return View(car);
        }

        public async Task<IActionResult>Edit(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car editedCar)
        {
            if (ModelState.IsValid)
            {
                var car = new Car()
                {
                    Id = editedCar.Id,
                    Make = editedCar.Make,
                    Model = editedCar.Model,
                    Year = editedCar.Year,
                    Trim = editedCar.Trim,
                    Mileage = editedCar.Mileage,
                    Price = editedCar.Price,
                };
                await carService.AddCarAsync(car);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editedCar);
            }
        }

        public async Task<IActionResult>Delete(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Car car)
        {
            await carService.DeleteCarAsync(car.Id);
            return RedirectToAction("Index");   
        }
    }
}

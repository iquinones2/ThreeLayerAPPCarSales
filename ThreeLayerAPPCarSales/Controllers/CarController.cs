using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var sellers = await carService.GetSellersAsync();
            ViewBag.SellerId = new SelectList(sellers, "SellerId", "LastName");
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
                SellerId = carmodel.SellerId,
            };
            await carService.AddCarAsync(car);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            var sellers = await carService.GetSellersAsync();
            ViewBag.SellerId = new SelectList(sellers, "SellerId", "LastName", car.SellerId);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car editedCar)
        {
            ModelState.Remove("Seller");

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
                    SellerId = editedCar.SellerId,
                };
                await carService.UpdateCarAsync(car);
                return RedirectToAction("Index");
            }
            else
            {
                var sellers = await carService.GetSellersAsync();
                ViewBag.SellerId = new SelectList(sellers, "SellerId",
                                    "LastName", editedCar.SellerId);
                return View(editedCar);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return View(car);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Car car)
        {
            await carService.DeleteCarAsync(car.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CarSellerList()
        {
            List<CarSellerViewModel> result = carService.GetCarsWithSellers();
            return View(result);
        }
    }
}

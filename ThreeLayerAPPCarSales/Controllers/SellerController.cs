using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ThreeLayerAPPCarSales.Models;

namespace ThreeLayerAPPCarSales.Controllers
{
    public class SellerController:Controller
    {
        private readonly ICarService _carService;

        public SellerController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Seller sellerModel)
        {
            var seller = new Seller()
            {
                FirstName = sellerModel.FirstName,
                LastName = sellerModel.LastName,
                Email = sellerModel.Email,
                PhoneNumber = sellerModel.PhoneNumber
            };
            await _carService.AddSellerAsync(seller);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sellers = await _carService.GetSellersAsync();
            return View(sellers);
        }
    }
}

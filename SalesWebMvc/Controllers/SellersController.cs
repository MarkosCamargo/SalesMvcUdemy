using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        /**
        * <summary>Serviço Seller(ModelBO)</summary>
        */
        private readonly SellerService _sellersService;
        /**
        * <summary>Serviço Departmet(ModelBO)</summary>
        */
        private readonly DepartmentService _departmentService;

        /// <summary>
        /// Construtor com injeção de dependencia, definida no Startup.cs
        /// </summary>
        public SellersController(SellerService sellersService, DepartmentService departmentService)
        {
            _sellersService = sellersService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.insert(seller);
            return RedirectToAction(nameof(Index));
        }

    }
}

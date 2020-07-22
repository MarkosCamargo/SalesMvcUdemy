using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        /**
        * <summary>Serviço SalesRecord(ModelBO)</summary>
        */
        private readonly SalesRecordService _salesRecordService;
 
        /// <summary>
        /// Construtor com injeção de dependencia, definida no Startup.cs
        /// </summary>
        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            checkDates(minDate, maxDate);
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        private string getDateFormat(DateTime date, string format = "yyyy-MM-dd") 
        {
           return date.ToString(format);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            checkDates(minDate, maxDate);
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }

        private void checkDates(DateTime? minDate, DateTime? maxDate) 
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = getDateFormat(minDate.Value);
            ViewData["maxDate"] = getDateFormat(maxDate.Value);
        }
    }
}

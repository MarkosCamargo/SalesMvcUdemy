using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        /// <summary>
        /// Construtor com injeção de dependencia, definida no Startup.cs
        /// </summary>
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) 
        {
            var result = GetSalesRecordsByDate(minDate, maxDate);
            return await GetSalesRecordsOrdedByDate(result).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = GetSalesRecordsByDate(minDate, maxDate);
            return await GetSalesRecordsOrdedByDate(result).GroupBy(sales => sales.Seller.Department).ToListAsync();
        }

        private IOrderedQueryable<SalesRecord> GetSalesRecordsOrdedByDate(IQueryable<SalesRecord> result) 
        {
            return result.
                Include(sales => sales.Seller).
                Include(sales => sales.Seller.Department).
                OrderByDescending(sales => sales.Date);
        }

        private IQueryable<SalesRecord> GetSalesRecordsByDate(DateTime? minDate, DateTime? maxDate) 
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(sales => sales.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sales => sales.Date <= maxDate.Value);
            }
            return result;
        }

    }
}

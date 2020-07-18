using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        /// <summary>
        /// Construtor com injeção de dependencia, definida no Startup.cs
        /// </summary>
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os Departments do banco de dados
        /// </summary>
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(department => department.Name).ToListAsync();
        }
    }
}

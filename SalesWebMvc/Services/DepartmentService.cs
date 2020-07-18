using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(department => department.Name).ToList();
        }
    }
}

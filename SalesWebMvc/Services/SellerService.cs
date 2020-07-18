using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        /// <summary>
        /// Construtor com injeção de dependencia, definida no Startup.cs
        /// </summary>
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os Sellers do banco de dados
        /// </summary>
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public void insert(Seller seller)
        {
             _context.Add(seller);
             _context.SaveChanges();
        }
    }
}

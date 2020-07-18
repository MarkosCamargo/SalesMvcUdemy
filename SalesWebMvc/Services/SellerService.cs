using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

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
        /// Retorna todos os Sellers do banco de dados
        /// </summary>
        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(seller => seller.Id == id);
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public void Insert(Seller seller)
        {
             _context.Add(seller);
             _context.SaveChanges();
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public void Delete(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}

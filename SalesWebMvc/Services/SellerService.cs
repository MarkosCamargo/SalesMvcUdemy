using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
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
        public Seller FindById(int id, bool usingJoin)
        {
            return usingJoin ? _context.Seller.Include(Seller => Seller.Department).FirstOrDefault(seller => seller.Id == id) : _context.Seller.FirstOrDefault(seller => seller.Id == id);
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

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}

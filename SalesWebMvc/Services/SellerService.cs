using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        /// <summary>
        /// Retorna todos os Sellers do banco de dados
        /// </summary>
        public async Task<Seller> FindByIdAsync(int id, bool usingJoin)
        {
            return  usingJoin ? await _context.Seller.Include(Seller => Seller.Department).FirstOrDefaultAsync(seller => seller.Id == id) : await _context.Seller.FirstOrDefaultAsync(seller => seller.Id == id);
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            try
            {
                var seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) 
            {
                throw new  IntegrityException("The seller cannot be deleted because he/she has sales");
            }
        }

        /// <summary>
        /// Salva um objeto Seller do banco de dados
        /// </summary>
        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(seller);
                await  _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}

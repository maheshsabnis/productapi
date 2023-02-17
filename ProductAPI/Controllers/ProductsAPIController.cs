using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly eShoppingCodiContext _context;

        public ProductsAPIController(eShoppingCodiContext context)
        {
            _context = context;
        }

        // GET: api/ProductsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInfo>>> GetProductInfos()
        {
          if (_context.ProductInfos == null)
          {
              return NotFound();
          }
            return await _context.ProductInfos.ToListAsync();
        }

        // GET: api/ProductsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInfo>> GetProductInfo(int id)
        {
          if (_context.ProductInfos == null)
          {
              return NotFound();
          }
            var productInfo = await _context.ProductInfos.FindAsync(id);

            if (productInfo == null)
            {
                return NotFound();
            }

            return productInfo;
        }

        // PUT: api/ProductsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInfo(int id, ProductInfo productInfo)
        {
            if (id != productInfo.ProductRowId)
            {
                return BadRequest();
            }

            _context.Entry(productInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInfo>> PostProductInfo(ProductInfo productInfo)
        {
          if (_context.ProductInfos == null)
          {
              return Problem("Entity set 'eShoppingCodiContext.ProductInfos'  is null.");
          }
            _context.ProductInfos.Add(productInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInfo", new { id = productInfo.ProductRowId }, productInfo);
        }

        // DELETE: api/ProductsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInfo(int id)
        {
            if (_context.ProductInfos == null)
            {
                return NotFound();
            }
            var productInfo = await _context.ProductInfos.FindAsync(id);
            if (productInfo == null)
            {
                return NotFound();
            }

            _context.ProductInfos.Remove(productInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInfoExists(int id)
        {
            return (_context.ProductInfos?.Any(e => e.ProductRowId == id)).GetValueOrDefault();
        }
    }
}

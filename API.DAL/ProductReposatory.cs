using API.BLL.Entities;
using API.BLL.Interfaces;
using API.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class ProductReposatory : IProductReposatory
    {
        private readonly StoreContext context;

        public ProductReposatory(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync()
           => await context.Products.Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
          => await context.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int? id)
          => await context.Products.Include(p=>p.ProductType).
             Include(p=>p.ProductBrand)
            .FirstOrDefaultAsync(x=>x.Id==id);

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
            => await context.ProductTypes.ToListAsync();
    }
}

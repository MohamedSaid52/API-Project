using API.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifications<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationPrams productprams)
            : base(x=>
            (string.IsNullOrEmpty(productprams.Search)||x.Name.ToLower().Contains(productprams.Search))&&
            (!productprams.BrandId.HasValue||x.ProductBrandId==productprams.BrandId)&&
            (!productprams.TypeId.HasValue || x.ProductTypeId == productprams.TypeId)
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productprams.PageSize * (productprams.PageIndex - 1), productprams.PageSize);
            if (!string.IsNullOrEmpty(productprams.Sort))
            {
                switch (productprams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescinding(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
           : base(x =>x.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
